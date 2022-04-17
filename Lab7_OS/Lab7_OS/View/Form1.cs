using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Lab7_OS.Core;
using Lab7_OS.Exceptions;

namespace Lab7_OS.View
{
    public partial class Form1 : Form
    {
        private string _error;
        private string _selectedPath = string.Empty;
        private string[] _content;

        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Открываем файл и делаем первичную обработку строк. Чистим список переменных и поле ошибок.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            _selectedPath = dialog.FileName;
            InitialFileProcessor fileProcessor = new InitialFileProcessor(_selectedPath);
            _content = fileProcessor.ProcessFile();
            fileContentArea.Lines = _content;
            DefinedVariablesList.ClearList();
            errorsList.Text = string.Empty;
        }
        /// <summary>
        /// Получает коды из лексем, проверяет их на валидность. Формирует отображение кодов в поле вывода.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void analyzeButton_Click(object sender, EventArgs e)
        {
            try
            {
                LinesToCodeConverter converter = new LinesToCodeConverter(_content);
                try
                {
                    int[][] codes = converter.CalculateCodes();
                    string[] test = new string[codes.Length];
                    for (int i = 0; i < test.Length; i++)
                    {
                        string line = string.Empty;
                        if (codes[i] == null)
                            continue;
                        foreach (int code in codes[i])
                        {
                            line += code.ToString() + " ";
                        }
                        test[i] = line;
                    }
                    CodeValidityChecker checker = new CodeValidityChecker(codes);
                    checker.CheckSyntaxCorrection();
                    fileContentArea.Lines = test;
                    MessageBox.Show("Файл не содержит ошибок", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SyntaxException ex)
                {
                    _error = ex.Message;
                    errorsList.Text = _error;
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Файл не был выбран!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

