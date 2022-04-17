using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab7_OS.Exceptions;
using Lab7_OS.Core.Models;
using Lab7_OS.Enums;

namespace Lab7_OS.Core
{
    internal class LinesToCodeConverter
    {
        private string[] _tagrgetLines;

        private const int AddCode = 4;
        private const int PopCode = 6;
        private const int IdivCode = 5;
        private const int ComaCode = 1;

        public LinesToCodeConverter(string[] tagrgetLines)
        {
            _tagrgetLines = tagrgetLines;
        }
        /// <summary>
        /// Метод вычисляет коды на основе лексем из входного массива (_targetLines)
        /// </summary>
        /// <returns>
        /// Возвращает ступенчатый массив, содержащий числовые коды, соответствующие каждой из лексем
        /// обрабатываемого массива
        /// </returns>
        /// <exception cref="SyntaxException">
        /// В случае логической ошибки (повторная инициализация переменной, операции с переменными 
        /// разного размера, попытке доступиться к неинициализированной переменной) выбрасывается
        /// исключение с сообщением об ошибке
        /// </exception>
        public int[][] CalculateCodes()
        {
            int i = 0;
            int j = 0;
            try
            {
                int[][] codes = new int[_tagrgetLines.Length][];
                for (i = 0; i < _tagrgetLines.Length; i++)
                {
                    // игнорируем пустые строки
                    if (_tagrgetLines[i]==string.Empty)
                        continue;
                    string[] words = _tagrgetLines[i].Split(new char[] { ' ' });
                    codes[i] = new int[words.Length];
                    for (j = 0; j < words.Length; j++)
                    {
                        LexemToCodeConverter lexemConverter = new LexemToCodeConverter(words[j]);
                        codes[i][j] = lexemConverter.GetCode();
                        // если встретили идентификатор
                        if (codes[i][j] == 12)
                        {
                            UserVariable variable = new UserVariable();
                            variable.Name = words[j];   
                            // идентификатор стоит на 1м месте (инициализация переменной)
                            if (j == 0)
                            {
                                // проверка размера
                                switch (words[j + 1])
                                {
                                    case "DB":
                                        variable.SizeType = VariableSizeType.OneByteVariable;
                                        break;
                                    case "DW":
                                        variable.SizeType = VariableSizeType.TwoBytesVariable;
                                        break;
                                    default:
                                        throw new SyntaxException($"Неправильно использована переменная в строке № {i + 1}");
                                }
                            }
                            // с идентификатором производятся любые неинициализирующие действия
                            else
                            {
                                UserVariable current = DefinedVariablesList.GetVariableByName(words[j]);
                                switch (codes[i][j - 1])
                                {
                                    // проверка корректности выполняемой операции ADD (добавляем что-либо к переменной)
                                    case AddCode:
                                        try
                                        {
                                            CheckVariableDefinition(i, j, words);
                                            // проверка на совпадение размеров переменных
                                            switch (current.SizeType)
                                            {
                                                case VariableSizeType.OneByteVariable:
                                                    if (lexemConverter.LexemIsTwoBytesRegister(words[j + 2]))
                                                        throw new SyntaxException($"Ошибка в строке № {i + 1}: размерности переменных не совпадают.");
                                                    continue;
                                                case VariableSizeType.TwoBytesVariable:
                                                    if (lexemConverter.LexemIsOneByteRegister(words[j + 2]))
                                                        throw new SyntaxException($"Ошибка в строке № {i + 1}: размерности переменных не совпадают.");
                                                    continue;
                                            }
                                            break;
                                        }
                                        catch
                                        {
                                            throw new SyntaxException($"Обнаружена синтаксическая ошибка в строке № {i + 1}");
                                        }
                                    // проверка корректности выполняемых операций Idiv и Pop
                                    case PopCode:
                                    case IdivCode:
                                        CheckVariableDefinition(i, j, words);
                                        continue;
                                    // проверка корректности использования конструкции ADD
                                    case ComaCode:
                                        CheckVariableDefinition(i, j, words);
                                        switch (codes[i][j - 2])
                                        {
                                            // если добавляем переменную к любому однобайтному регистру
                                            case 7:
                                                if (current.SizeType != VariableSizeType.OneByteVariable)
                                                    throw new SyntaxException($"Ошибка в строке № {i + 1}: размерности переменных не совпадают.");
                                                continue;
                                            // если добавляем переменную к любому двухбайтному регистру
                                            case 8:
                                            case 9:
                                            case 10:
                                                if (current.SizeType != VariableSizeType.TwoBytesVariable)
                                                    throw new SyntaxException($"Ошибка в строке № {i + 1}: размерности переменных не совпадают.");
                                                continue;
                                        }
                                        // проверка корректности сложения двух переменных
                                        UserVariable latest = DefinedVariablesList.GetVariableByName(words[j - 2]);
                                        if (latest.SizeType == current.SizeType)
                                            continue;
                                        throw new SyntaxException($"Ошибка в строке № {i + 1}: размерности переменных не совпадают.");
                                    default:
                                        throw new SyntaxException($"Неправильно использована переменная в строке № {i + 1}");
                                }
                            }
                            // проверка повторного объявления переменной и ее инициализация, если переменная не была объявлена
                            if (DefinedVariablesList.VariableAreDefined(words[j]))
                                throw new SyntaxException($"Ошибка в строке № {i + 1}. Переменная {words[j]} была определена ранее");
                            DefinedVariablesList.AddVariable(variable);
                        }
                    }
                }
                return codes;
            }
            catch (ArgumentException)
            {           
                throw new SyntaxException($"Синтаксическая ошибка обнаружена в строке № {i + 1}");
            }
        }

        /// <summary>
        /// Проверяет, была ли определена переменная ранее
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="words"></param>
        /// <exception cref="SyntaxException">
        /// Если переменная не была определена - бросаем исключение
        /// </exception>
        private static void CheckVariableDefinition(int i, int j, string[] words)
        {
            if (!DefinedVariablesList.VariableAreDefined(words[j]))
                throw new SyntaxException($"Переменная {words[j]} не была определена (строка {i + 1})");
        }
    }
}
