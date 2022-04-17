using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab7_OS.Exceptions;

namespace Lab7_OS.Core
{
    internal class CodeValidityChecker
    {
        private int[][] _codes;
        // массив валидных последовательностей кодов лексем
        private int[][] _correctSequences =
        {
            new int[] { 12, 2, 11},
            new int[] { 12, 3, 11},
            new int[] { 4, 7, 1, 7},
            new int[] { 4, 7, 1, 11},
            new int[] { 4, 7, 1, 12},
            new int[] { 4, 8, 1, 8},
            new int[] { 4, 8, 1, 9},
            new int[] { 4, 8, 1, 10},
            new int[] { 4, 8, 1, 11},
            new int[] { 4, 8, 1, 12},
            new int[] { 4, 12, 1, 12},
            new int[] { 4, 12, 1, 7},
            new int[] { 4, 12, 1, 8},
            new int[] { 4, 12, 1, 11},
            new int[] { 4, 12, 1, 9},
            new int[] { 4, 12, 1, 10},
            new int[] { 4, 9, 1, 8},
            new int[] { 4, 9, 1, 9},
            new int[] { 4, 9, 1, 11},
            new int[] { 4, 9, 1, 12},
            new int[] { 4, 9, 1, 10},
            new int[] { 4, 10, 1, 8},
            new int[] { 4, 10, 1, 9},
            new int[] { 4, 10, 1, 11},
            new int[] { 4, 10, 1, 12},
            new int[] { 4, 10, 1, 10},
            new int[] { 6, 7},
            new int[] { 6, 8},
            new int[] { 6, 9},
            new int[] { 6, 10},
            new int[] { 6, 12},
            new int[] { 5, 7},
            new int[] { 5, 8},
            new int[] { 5, 12}
        };

        public CodeValidityChecker(int[][] codes)
        {
            _codes = codes;
        }
        /// <summary>
        /// Проверяет синтаксическую валидность полученного ранее массива кодов лексем
        /// </summary>
        /// <exception cref="SyntaxException">
        /// Если любой из подмассивов массива последовательностей кодов лексем не содержится в массиве
        /// валидных последовательностей (_correctSequences) - бросаем исключение
        /// </exception>
        public void CheckSyntaxCorrection()
        {
            for (int i = 0; i < _codes.Length; i++)
            {
                if (!ArrayInCorrectSequences(_codes[i]))
                    throw new SyntaxException($"Обнаружена синтаксическая ошибка в строке № {i + 1}");
            }
        }
        /// <summary>
        /// Проверка наличия массива в ступенчатом массиве валидных последовательностей
        /// </summary>
        /// <param name="needle">проверяемы массив кодов лексем</param>
        /// <returns>bool значение</returns>
        private bool ArrayInCorrectSequences(int[] needle)
        {
            // пропускаем массив, полученный на основе пустой строки
            if (needle == null)
                return true;
            for (int i = 0; i < _correctSequences.Length; i++)
            {
                if (_correctSequences[i].SequenceEqual(needle))
                    return true;
            }
            return false;
        }
    }
}
