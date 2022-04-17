using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7_OS.Core
{
    internal class LexemToCodeConverter
    {
        private string _lexem;
        private string[] _oneByteRegisters = { "AL", "AH", "BL", "BH", "CL", "CH", "DL", "DH" };
        private string[] _twoByteRegisters = { "AX", "BX", "CX", "DX" };
        private string[] _segmentRegisters = { "DS", "SS", "ES" };
        private char[] _restrictedVariableSymbols = { '.', '!', '*', '?', ':'};
        private char[] _validHeximalNumberSymbols = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

        public LexemToCodeConverter(string lexem)
        {
            _lexem = lexem;
        }
        /// <summary>
        /// Конвертирует лексемы в числовые коды
        /// </summary>
        /// <returns>Код лексемы</returns>
        /// <exception cref="ArgumentException">
        /// В случае нераспознанной лексемы бросается исключение
        /// </exception>
        public int GetCode()
        {
            if (isDigit())
                return 11;
            if (_oneByteRegisters.Contains(_lexem))
                return 7;
            if (_twoByteRegisters.Contains(_lexem))
                return 8;
            if (_segmentRegisters.Contains(_lexem))
                return 9;
            if (_lexem == "CS")
                return 10;
            if (_lexem == ",")
                return 1;
            if (_lexem == "DB")
                return 2;
            if (_lexem == "DW")
                return 3;
            if (_lexem == "ADD")
                return 4;
            if (_lexem == "IDIV")
                return 5;
            if (_lexem == "POP")
                return 6;
            else
            {
                if (!StartsWithDigit() && !_lexem.Any(symbol => _restrictedVariableSymbols.Contains(symbol)))
                    return 12;
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// Проверяет, начинается ли лексема с цифры
        /// </summary>
        /// <returns>bool значение</returns>
        private bool StartsWithDigit()
        {
            if (_lexem == string.Empty)
                return false;
            int value = 0;
            return int.TryParse(_lexem[0].ToString(), out value); 
        }
        /// <summary>
        /// Проверяет, является ли лексема двоичным, десятеричным или шестнадацатеричным числом
        /// </summary>
        /// <returns>bool значение</returns>
        private bool isDigit()
        {
            ushort outValue = 0;
            bool hexNumberCondition = _lexem.EndsWith("H") && StartsWithDigit() && _lexem.Substring(0, _lexem.Length - 1).All(letter => _validHeximalNumberSymbols.Contains(letter));
            bool binaryNumberCondition = _lexem.EndsWith("B") && HasOnlyBinaryDigits();
            bool decimalNumberCondition = ushort.TryParse(_lexem, out outValue);
            return hexNumberCondition || binaryNumberCondition || decimalNumberCondition;
        }
        /// <summary>
        /// Проверяет, состоит ли лексема только из 0 и 1
        /// </summary>
        /// <returns>bool значение</returns>
        private bool HasOnlyBinaryDigits()
        {
            foreach (char letter in _lexem)
            {
                if (letter != '0' || letter != '1')
                    return false;
            }
            return true;
        }

        public bool LexemIsOneByteRegister(string lexem)
        {
            return _oneByteRegisters.Contains(lexem);
        }

        public bool LexemIsTwoBytesRegister(string lexem)
        {
            return _lexem == "CS" || _twoByteRegisters.Contains(lexem);
        }
    }
}
