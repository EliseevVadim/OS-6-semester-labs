using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab7_OS.Core.Models;

namespace Lab7_OS.Core
{
    static class DefinedVariablesList
    {
        private static List<UserVariable> _variables;

        static DefinedVariablesList()
        {
            _variables = new List<UserVariable>();
        }

        public static void AddVariable(UserVariable addition)
        {
            _variables.Add(addition);
        }

        public static void ClearList()
        {
            _variables.Clear();
        }

        public static bool VariableAreDefined(string name)
        {
            return _variables.Any(variable => variable.Name == name);
        }

        public static UserVariable GetVariableByName(string name)
        {
            return _variables.FirstOrDefault(variable => variable.Name == name);
        }
    }
}
