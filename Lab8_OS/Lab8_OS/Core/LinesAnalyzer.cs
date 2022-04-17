using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab8_OS.Core.Models;
using Lab8_OS.Enums;
using Lab8_OS.Exceptions;

namespace Lab8_OS.Core
{
    internal class LinesAnalyzer
    {
        private string[] _lines;
        private Dictionary<string, string> _registersWithCodes = new Dictionary<string, string>()
        {
            { "AX", "000" },
            { "BX", "011" },
            { "CX", "001" },
            { "DX", "010" },
            { "SP", "100" },
            { "BP", "101" },
            { "SI", "110" },
            { "DI", "111" },
            { "ES", "00" },
            { "CS", "01" },
            { "SS", "10" },
            { "DS", "11" },
            { "AL", "000" },
            { "CL", "001" },
            { "DL", "010" },
            { "BL", "011" },
            { "AH", "100" },
            { "BH", "111" },
            { "CH", "101" },
            { "DH", "110" },
        };
        private char[] _validHeximalNumberSymbols = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        private string[] _oneByteRegisters = { "AL", "AH", "BL", "BH", "CL", "CH", "DL", "DH" };
        private string[] _twoByteRegisters = { "AX", "BX", "CX", "DX" };
        private string[] _segmentRegisters = { "DS", "SS", "ES", "CS" };
        private string[] _nonSegmentRegisters = { "AL", "AH", "BL", "BH", "CL", "CH", "DL", "DH", "AX", "BX", "CX", "DX" };
        private int _address;

        public LinesAnalyzer(string[] lines)
        {
            _lines = lines;
            _address = 0;
        }

        /// <summary>
        /// На основе массива строк генерирует массив объектных кодов
        /// </summary>
        /// <returns>Массив объектов класса, описывающего объектный код строки программы</returns>
        /// <exception cref="InvalidCodeException">
        /// В случае несгенериованного объектного кода строки бросается исключение
        /// </exception>
        public OutputDataTemplate[] GenerateOutputContent()
        {
            OutputDataTemplate[] generationResult = new OutputDataTemplate[_lines.Length];
            for (int i = 0; i < _lines.Length; i++)
            {
                ObjectCodeGenerationResult result = GetObjectCodeOfLine(_lines[i]);
                generationResult[i] = new OutputDataTemplate(i + 1, _address.ToString("X").PadLeft(4, '0'), result.ObjectCode, _lines[i]);
                _address += result.Offset;
            }
            return generationResult;
        }

        /// <summary>
        /// На основе входной строки генерирует ее объектный код
        /// </summary>
        /// <param name="line"></param>
        /// <returns>Объектный код строки программы</returns>
        public ObjectCodeGenerationResult GetObjectCodeOfLine(string line)
        {
            // разбиваем строку на массив лексем
            string[] splittedLine = line.Split(' ');
            // если участвуют директивы DB, DW, ADD
            if (splittedLine.Length > 2)
            {
                // инициализация переменной
                if (splittedLine[1] == "DB" || splittedLine[1] == "DW")
                {
                    // добавляем переменную в список инициализированных
                    int offset = splittedLine[1] == "DB" ? 1 : 2;
                    DefinedVariablesList.AddVariable(new UserVariable()
                    {
                        Name = splittedLine[0],
                        Address = _address.ToString("X"),
                        SizeType = offset == 1 ? VariableSizeType.OneByteVariable : VariableSizeType.TwoBytesVariable
                    });
                    // если переменная - шестнадцатеричное число
                    if (splittedLine[2].EndsWith("H"))
                    {
                        return new ObjectCodeGenerationResult()
                        {
                            ObjectCode = splittedLine[2].Substring(0, splittedLine[2].Length - 1),
                            Offset = offset
                        };
                    }
                    // если переменная - двоичное число
                    if (splittedLine[2].EndsWith("B"))
                    {
                        return new ObjectCodeGenerationResult()
                        {
                            ObjectCode = Convert.ToInt32(splittedLine[2], 2).ToString(),
                            Offset = offset
                        };
                    }
                    // если переменная - десятеричное число
                    else
                    {
                        return new ObjectCodeGenerationResult()
                        {
                            ObjectCode = int.Parse(splittedLine[2]).ToString("X"),
                            Offset = offset
                        };
                    }
                }
                // операция сложения
                if (splittedLine[0] == "ADD")
                {
                    // складываем двухбайтный регистр с чем-либо
                    if (_twoByteRegisters.Contains(splittedLine[1]) || _segmentRegisters.Contains(splittedLine[1]))
                    {
                        // складываем двухбайтные регистры
                        if (_twoByteRegisters.Contains(splittedLine[3]) || _segmentRegisters.Contains(splittedLine[3]))
                        {
                            string addressingSequence = "11" + _registersWithCodes[splittedLine[1]] + _registersWithCodes[splittedLine[3]];
                            string[] objectCodeArray = new string[]
                            {
                                Convert.ToInt32("00000011", 2).ToString("X").PadLeft(2, '0'), // складываем двухбайтные регистры: reg & w - первый операнд, mod & w & r/m - второй
                                Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                            };
                            return new ObjectCodeGenerationResult()
                            {
                                ObjectCode = String.Join(" ", objectCodeArray),
                                Offset = 2
                            };
                        }
                        // складываем двухбайтный регистр с переменной
                        if (DefinedVariablesList.VariableAreDefined(splittedLine[3]))
                        {
                            UserVariable variable = DefinedVariablesList.GetVariableByName(splittedLine[3]);
                            if (variable.SizeType == VariableSizeType.TwoBytesVariable)
                            {
                                string addressingSequence = "00" + _registersWithCodes[splittedLine[1]] + "110";
                                string[] objectCodeArray = new string[]
                                {
                                    Convert.ToInt32("00000011", 2).ToString("X").PadLeft(2, '0'), // складываем двухбайтный регистр и переменную: reg & w - первый операнд, mod & w & r/m - второй
                                    Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                    variable.Address.PadLeft(4, '0')
                                };
                                return new ObjectCodeGenerationResult()
                                {
                                    ObjectCode = String.Join(" ", objectCodeArray),
                                    Offset = 4
                                };
                            }
                        }
                        // складываем двухбайтный регистр с числом
                        if (isDigit(splittedLine[3]))
                        {
                            if (splittedLine[1] == "AX")
                            {
                                string[] objectCodeArray = new string[]
                                {
                                    Convert.ToInt32("00000101", 2).ToString("X").PadLeft(2, '0'), // складываем AX и число
                                    GetHexCodeOfNumber(splittedLine[3]).PadLeft(16, '0')
                                };
                                return new ObjectCodeGenerationResult()
                                {
                                    ObjectCode = String.Join(" ", objectCodeArray),
                                    Offset = 2
                                };
                            }
                            string addressingSequence = "11000" + _registersWithCodes[splittedLine[1]];
                            string[] codeArray = new string[]
                            {
                                Convert.ToInt32("10000001", 2).ToString("X").PadLeft(2, '0'), // складываем двухбайтный регистр и число
                                Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                GetHexCodeOfNumber(splittedLine[3]).PadLeft(16, '0')
                            };
                            return new ObjectCodeGenerationResult()
                            {
                                ObjectCode = String.Join(" ", codeArray),
                                Offset = 10
                            };
                        }
                    }
                    // складываем однобайтный регистр с чем-либо
                    if (_oneByteRegisters.Contains(splittedLine[1]))
                    {
                        // складываем однобайтные регистры
                        if (_oneByteRegisters.Contains(splittedLine[3]))
                        {
                            string addressingSequence = "11" + _registersWithCodes[splittedLine[1]] + _registersWithCodes[splittedLine[3]];
                            string[] objectCodeArray = new string[]
                            {
                                Convert.ToInt32("00000010", 2).ToString("X").PadLeft(2, '0'), // складываем однобайтные регистры: reg & w - первый операнд, mod & w & r/m - второй
                                Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                            };
                            return new ObjectCodeGenerationResult()
                            {
                                ObjectCode = String.Join(" ", objectCodeArray),
                                Offset = 2
                            };
                        }
                        // складываем однобайтный регистр с переменной
                        if (DefinedVariablesList.VariableAreDefined(splittedLine[3]))
                        {
                            UserVariable variable = DefinedVariablesList.GetVariableByName(splittedLine[3]);
                            if (variable.SizeType == VariableSizeType.OneByteVariable)
                            {
                                string addressingSequence = "00" + _registersWithCodes[splittedLine[1]] + "110";
                                string[] objectCodeArray = new string[]
                                {
                                    Convert.ToInt32("00000010", 2).ToString("X").PadLeft(2, '0'), // складываем однобайтный регистр и переменную: reg & w - первый операнд, mod & w & r/m - второй
                                    Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                    variable.Address.PadLeft(4, '0')
                                };
                                return new ObjectCodeGenerationResult()
                                {
                                    ObjectCode = String.Join(" ", objectCodeArray),
                                    Offset = 4
                                };
                            }
                        }
                        // складываем однобайтный регистр с числом
                        if (isDigit(splittedLine[3]))
                        {
                            if (splittedLine[1] == "AL")
                            {
                                string[] objectCodeArray = new string[]
                                {
                                    Convert.ToInt32("00000100", 2).ToString("X").PadLeft(2, '0'), // складываем AL и число
                                    GetHexCodeOfNumber(splittedLine[3])
                                };
                                return new ObjectCodeGenerationResult()
                                {
                                    ObjectCode = String.Join(" ", objectCodeArray),
                                    Offset = 2
                                };
                            }
                            string addressingSequence = "11000" + _registersWithCodes[splittedLine[1]];
                            string[] codeArray = new string[]
                            {
                                Convert.ToInt32("10000000", 2).ToString("X").PadLeft(2, '0'), // складываем однобайтный регистр и число
                                Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                GetHexCodeOfNumber(splittedLine[3]).PadLeft(8, '0')
                            };
                            return new ObjectCodeGenerationResult()
                            {
                                ObjectCode = String.Join(" ", codeArray),
                                Offset = 6
                            };
                        }
                    }
                    // складываем переменные с чем-либо
                    if (DefinedVariablesList.VariableAreDefined(splittedLine[1]))
                    {
                        UserVariable variable = DefinedVariablesList.GetVariableByName(splittedLine[1]);
                        switch (variable.SizeType)
                        {
                            // складываем однобайтную переменную с чем-либо
                            case VariableSizeType.OneByteVariable:
                                // однобайтная переменная + регистр
                                if (_oneByteRegisters.Contains(splittedLine[3]))
                                {
                                    string addressingSequence = "11110" + _registersWithCodes[splittedLine[3]];
                                    string[] objectCodeArray = new string[]
                                    {
                                        Convert.ToInt32("00000010", 2).ToString("X").PadLeft(2, '0'), // складываем переменную и однобайтный регистр: reg & w - первый операнд, mod & w & r/m - второй
                                        Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                        variable.Address.PadLeft(4, '0')
                                    };
                                    return new ObjectCodeGenerationResult()
                                    {
                                        ObjectCode = String.Join(" ", objectCodeArray),
                                        Offset = 4
                                    };
                                }
                                // однобайтная переменная + число
                                if (isDigit(splittedLine[3]))
                                {
                                    string addressingSequence = "00000110";
                                    string[] codeArray = new string[]
                                    {
                                        Convert.ToInt32("10000000", 2).ToString("X").PadLeft(2, '0'), // складываем двухбайтный регистр и число
                                        Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                        GetHexCodeOfNumber(splittedLine[3]).PadLeft(8, '0')
                                    };
                                    return new ObjectCodeGenerationResult()
                                    {
                                        ObjectCode = String.Join(" ", codeArray),
                                        Offset = 6
                                    };
                                }
                                break;
                            // складываем двухбайтную переменную с чем-либо
                            case VariableSizeType.TwoBytesVariable:
                                // двухбайтная переменная + регистр
                                if (_twoByteRegisters.Contains(splittedLine[3]) || _segmentRegisters.Contains(splittedLine[3]))
                                {
                                    string addressingSequence = "11110" + _registersWithCodes[splittedLine[3]];
                                    string[] objectCodeArray = new string[]
                                    {
                                        Convert.ToInt32("00000011", 2).ToString("X").PadLeft(2, '0'), // складываем переменную и двухбайтный регистр: reg & w - первый операнд, mod & w & r/m - второй
                                        Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                        variable.Address.PadLeft(4, '0')
                                    };
                                    return new ObjectCodeGenerationResult()
                                    {
                                        ObjectCode = String.Join(" ", objectCodeArray),
                                        Offset = 4
                                    };
                                }
                                // двухбайтная переменная + число
                                if (isDigit(splittedLine[3]))
                                {
                                    string addressingSequence = "00000110";
                                    string[] codeArray = new string[]
                                    {
                                        Convert.ToInt32("10000001", 2).ToString("X").PadLeft(2, '0'), // складываем двухбайтный регистр и число
                                        Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                        GetHexCodeOfNumber(splittedLine[3]).PadLeft(16, '0')
                                    };
                                    return new ObjectCodeGenerationResult()
                                    {
                                        ObjectCode = String.Join(" ", codeArray),
                                        Offset = 10
                                    };
                                }
                                break;
                        }                       
                    }
                }
            }
            // если строка состоит из 2х символов (операции IDIV или POP)
            if (splittedLine.Length == 2)
            {
                // обработка директивы POP
                if (splittedLine[0] == "POP")
                {
                    // изымаем сегментный регистр
                    if (_segmentRegisters.Contains(splittedLine[1]))
                    {
                        return new ObjectCodeGenerationResult()
                        {
                            ObjectCode = Convert.ToInt32("000" + _registersWithCodes[splittedLine[1]] + "111", 2).ToString("X"),
                            Offset = 1
                        };
                    }
                    // изызмаем любой несегментый регистр
                    if (_nonSegmentRegisters.Contains(splittedLine[1]))
                    {
                        return new ObjectCodeGenerationResult()
                        {
                            ObjectCode = Convert.ToInt32("01011" + _registersWithCodes[splittedLine[1]], 2).ToString("X"),
                            Offset = 1
                        };
                    }
                    // изымаем переменную
                    if (DefinedVariablesList.VariableAreDefined(splittedLine[1]))
                    {
                        string variableAddress = DefinedVariablesList.GetVariableByName(splittedLine[1]).Address;
                        string addressingSequence = "00000110"; // абсолютный адрес без смещения + непосредственная адресация
                        string[] objectCodeArray = new string[]
                        {
                            Convert.ToInt32("10001111", 2).ToString("X"), // код команды POP для переменной
                            Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                            variableAddress.PadLeft(4, '0')
                        };
                        return new ObjectCodeGenerationResult()
                        {
                            ObjectCode = String.Join(" ", objectCodeArray),
                            Offset = 4
                        };
                    }
                }
                // обработка директивы IDIV
                if (splittedLine[0] == "IDIV")
                {
                    // делим на однобайтный регистр
                    if (_oneByteRegisters.Contains(splittedLine[1]))
                    {
                        string addressingSequence = "11111" + _registersWithCodes[splittedLine[1]]; // используется однобайтный регистр
                        string[] objectCodeArray = new string[]
                        {
                            Convert.ToInt32("11110110", 2).ToString("X"), // однобайтный регистр
                            Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                        };
                        return new ObjectCodeGenerationResult()
                        {
                            ObjectCode = String.Join(" ", objectCodeArray),
                            Offset = 2
                        };
                    }
                    // делим на двухбайтный регистр
                    if (_twoByteRegisters.Contains(splittedLine[1]))
                    {
                        string addressingSequence = "11111" + _registersWithCodes[splittedLine[1]]; // используется двухбайтный регистр
                        string[] objectCodeArray = new string[]
                        {
                            Convert.ToInt32("11110111", 2).ToString("X"), // двухбайтный регистр
                            Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                        };
                        return new ObjectCodeGenerationResult()
                        {
                            ObjectCode = String.Join(" ", objectCodeArray),
                            Offset = 2
                        };
                    }
                    // делим на переменную
                    if (DefinedVariablesList.VariableAreDefined(splittedLine[1]))
                    {
                        UserVariable variable = DefinedVariablesList.GetVariableByName(splittedLine[1]);
                        string address = variable.Address;
                        switch (variable.SizeType)
                        {
                            // делим на однобайтную переменную
                            case VariableSizeType.OneByteVariable:
                                string addressingSequence = "00111110"; // абсолютный адрес без смещения + непосредственная адресация
                                string[] objectCodeArray = new string[]
                                {
                                    Convert.ToInt32("11110110", 2).ToString("X"), // однобайтная переменная
                                    Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                    address.PadLeft(4, '0')
                                };
                                return new ObjectCodeGenerationResult()
                                {
                                    ObjectCode = String.Join(" ", objectCodeArray),
                                    Offset = 4
                                };
                            // делим на двухбайтную переменную
                            case VariableSizeType.TwoBytesVariable:
                                addressingSequence = "00111110"; // абсолютный адрес без смещения + непосредственная адресация
                                objectCodeArray = new string[]
                                {
                                    Convert.ToInt32("11110111", 2).ToString("X"), // двухбайтная переменная
                                    Convert.ToInt32(addressingSequence, 2).ToString("X").PadLeft(2, '0'),
                                    address.PadLeft(4, '0')
                                };
                                return new ObjectCodeGenerationResult()
                                {
                                    ObjectCode = String.Join(" ", objectCodeArray),
                                    Offset = 4
                                };
                        }
                    }
                }
            }
            throw new InvalidCodeException("Код содержит ошибку компиляции, либо не соответствует заданию");
        }

        /// <summary>
        /// Проверяет, является ли лексема двоичным, десятеричным или шестнадацатеричным числом
        /// </summary>
        /// <param name="lexem"></param>
        /// <returns>bool значение</returns>
        private bool isDigit(string lexem)
        {
            ushort outValue = 0;
            bool hexNumberCondition = lexem.EndsWith("H") && StartsWithDigit(lexem) && lexem.Substring(0, lexem.Length - 1).All(letter => _validHeximalNumberSymbols.Contains(letter));
            bool binaryNumberCondition = lexem.EndsWith("B") && HasOnlyBinaryDigits(lexem.Substring(0, lexem.Length - 1));
            bool decimalNumberCondition = ushort.TryParse(lexem, out outValue);
            return hexNumberCondition || binaryNumberCondition || decimalNumberCondition;
        }

        /// <summary>
        /// Проверяет, состоит ли лекема только из 0 и 1
        /// </summary>
        /// <param name="lexem"></param>
        /// <returns>bool значение</returns>
        private bool HasOnlyBinaryDigits(string lexem)
        {
            foreach (char letter in lexem)
            {
                if (letter != '0' || letter != '1')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Проверяет, начинается ли лексема с цифры
        /// </summary>
        /// <param name="lexem"></param>
        /// <returns>bool значение</returns>
        private bool StartsWithDigit(string lexem)
        {
            if (lexem == string.Empty)
                return false;
            int value = 0;
            return int.TryParse(lexem[0].ToString(), out value);
        }

        /// <summary>
        /// Переводит число, представленное в строковом виде в строку-шестнадцатиричное число
        /// </summary>
        /// <param name="lexem"></param>
        /// <returns>Строка-шестнадцатиричное число</returns>
        private string GetHexCodeOfNumber(string lexem)
        {
            if (HasOnlyBinaryDigits(lexem) && lexem.EndsWith("b"))
                return Convert.ToInt32(lexem.Substring(0, lexem.Length - 1), 2).ToString("X");
            if (lexem.EndsWith("H") && StartsWithDigit(lexem) && lexem.Substring(0, lexem.Length - 1).All(letter => _validHeximalNumberSymbols.Contains(letter)))
                return lexem.Substring(0, lexem.Length - 1);
            return int.Parse(lexem).ToString("X");
        }
    }
}
