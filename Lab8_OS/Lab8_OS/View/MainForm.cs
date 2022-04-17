using Lab8_OS.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lab8_OS.Core.Models;
using System.IO;
using Lab8_OS.Exceptions;

namespace Lab8_OS.View
{
    public partial class MainForm : Form
    {
        private string _selectedPath = string.Empty;
        private string[] _content;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Открываем файл и делаем первичную обработку строк. Чистим список переменных.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileButton_Click(object sender, EventArgs e)
        {
            DefinedVariablesList.ClearList();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            _selectedPath = dialog.FileName;
            InitialFileProcessor fileProcessor = new InitialFileProcessor(_selectedPath);
            _content = fileProcessor.ProcessFile();
            fileContentArea.Lines = _content;
        }

        /// <summary>
        /// Анализируем обработанные строки из файла, формируем и записываем
        /// выводимые данные в datagridview и файл output.txt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void analyzeButton_Click(object sender, EventArgs e)
        {
            try
            {
                LinesAnalyzer analyzer = new LinesAnalyzer(_content);
                OutputDataTemplate[] result = analyzer.GenerateOutputContent();
                foreach (OutputDataTemplate record in result)
                {
                    dataGridView1.Rows.Add(record.LineOfCode, record.Address, record.ObjectCode, record.ActualLineOfCode);
                }
                using (StreamWriter writer = new StreamWriter("output.txt"))
                {
                    writer.Write($"Запуск от {DateTime.Now.ToString("G")}\n");
                    writer.Write("№ строки программы | Адрес | Символический код директивы или команды | Строка программы |\n");
                    for (int i = 0; i < result.Length; i++)
                    {
                        writer.WriteLine(result[i].ToString());
                    }
                }
                MessageBox.Show("Обработка завершена.", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidCodeException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка обработки данных", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
