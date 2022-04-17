using Lab8_OS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab8_OS.Core.Models
{
    internal class UserVariable
    {
        public string Name { get; set; }
        public VariableSizeType SizeType { get; set; }
        public string Address { get; set; }
    }
}
