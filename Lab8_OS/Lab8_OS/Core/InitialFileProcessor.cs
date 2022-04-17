using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab8_OS.Core
{
    internal class InitialFileProcessor
    {
        private string _path;

        public InitialFileProcessor(string path)
        {
            _path = path;
        }
        /// <summary>
        /// Метод удаляет ненужные пробелы и табуляцию, приводит все символы к верхнему регистру,
        /// оставляя по одному пробелу до и после запятых
        /// </summary>
        /// <returns>Массив обработанных строк, пригодный для дальнейшего анализа </returns>
        public string[] ProcessFile()
        {
            string[] lines = File.ReadAllLines(_path)
                .Where(line => !String.IsNullOrWhiteSpace(line))
                .ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == ',')
                    {
                        lines[i] = lines[i].Insert(j, " ");
                        lines[i] = lines[i].Insert(j + 2, " ");
                        j++;
                    }
                }
                string[] words = lines[i].Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                for (int k = 0; k < words.Length; k++)
                {
                    words[k] = words[k].ToUpper();
                }
                lines[i] = string.Join(" ", words);
            }
            return lines;
        }
    }
}
