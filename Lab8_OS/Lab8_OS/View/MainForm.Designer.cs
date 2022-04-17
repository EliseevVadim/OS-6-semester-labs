namespace Lab8_OS.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.analyzeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fileContentArea = new System.Windows.Forms.RichTextBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.numberOfLineOfCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.symbolicCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineOfProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // analyzeButton
            // 
            this.analyzeButton.BackColor = System.Drawing.Color.Lime;
            this.analyzeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.analyzeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analyzeButton.Location = new System.Drawing.Point(474, 544);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(197, 41);
            this.analyzeButton.TabIndex = 9;
            this.analyzeButton.Text = "Анализировать файл";
            this.analyzeButton.UseVisualStyleBackColor = false;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(183, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Содержимое файла";
            // 
            // fileContentArea
            // 
            this.fileContentArea.BackColor = System.Drawing.Color.White;
            this.fileContentArea.Location = new System.Drawing.Point(12, 65);
            this.fileContentArea.Name = "fileContentArea";
            this.fileContentArea.ReadOnly = true;
            this.fileContentArea.Size = new System.Drawing.Size(550, 430);
            this.fileContentArea.TabIndex = 7;
            this.fileContentArea.Text = "";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(12, 12);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(119, 23);
            this.openFileButton.TabIndex = 6;
            this.openFileButton.Text = "Выбрать файл";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(770, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "Объектный код";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numberOfLineOfCode,
            this.address,
            this.symbolicCode,
            this.lineOfProgram});
            this.dataGridView1.Location = new System.Drawing.Point(568, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(549, 430);
            this.dataGridView1.TabIndex = 11;
            // 
            // numberOfLineOfCode
            // 
            this.numberOfLineOfCode.HeaderText = "№ строки программы";
            this.numberOfLineOfCode.Name = "numberOfLineOfCode";
            // 
            // address
            // 
            this.address.HeaderText = "Адрес";
            this.address.Name = "address";
            // 
            // symbolicCode
            // 
            this.symbolicCode.HeaderText = "Символический код директивы или команды";
            this.symbolicCode.Name = "symbolicCode";
            // 
            // lineOfProgram
            // 
            this.lineOfProgram.HeaderText = "Строка программы";
            this.lineOfProgram.Name = "lineOfProgram";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 611);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileContentArea);
            this.Controls.Add(this.openFileButton);
            this.MaximumSize = new System.Drawing.Size(1145, 650);
            this.MinimumSize = new System.Drawing.Size(1145, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лабораторная работа №8";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox fileContentArea;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberOfLineOfCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn symbolicCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineOfProgram;
    }
}

