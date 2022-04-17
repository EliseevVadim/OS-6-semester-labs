using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab8_OS.Core.Models
{
    /// <summary>
    /// Класс, являющийся результатом генерации объектного кода для одной строки символьного
    /// </summary>
    internal class OutputDataTemplate
    {
        private int _lineOfCode;
        private string _address;
        private string _objectCode;
        private string _actualLineOfCode;

        public OutputDataTemplate(int lineOfCode, string address, string objectCode, string actualLineOfCode)
        {
            _lineOfCode = lineOfCode;
            _address = address;
            _objectCode = objectCode;
            _actualLineOfCode = actualLineOfCode;
        }

        public int LineOfCode { get => _lineOfCode; set => _lineOfCode = value; }
        public string Address { get => _address; set => _address = value; }
        public string ObjectCode { get => _objectCode; set => _objectCode = value; }
        public string ActualLineOfCode { get => _actualLineOfCode; set => _actualLineOfCode = value; }

        /// <summary>
        /// Строковое представление объекта класса для более удобной записи в файл
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string dashes = new string('-', 80);
            return dashes + "\n" + $"{_lineOfCode} | {_address} | {_objectCode} | {_actualLineOfCode}|\n" + dashes;
        }
    }
}
