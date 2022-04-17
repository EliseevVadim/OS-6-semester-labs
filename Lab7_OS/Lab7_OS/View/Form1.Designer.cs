namespace Lab7_OS.View
{
    partial class Form1
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
            this.openFileButton = new System.Windows.Forms.Button();
            this.fileContentArea = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorsList = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(12, 12);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(119, 23);
            this.openFileButton.TabIndex = 0;
            this.openFileButton.Text = "Выбрать файл";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // fileContentArea
            // 
            this.fileContentArea.BackColor = System.Drawing.Color.White;
            this.fileContentArea.Location = new System.Drawing.Point(12, 65);
            this.fileContentArea.Name = "fileContentArea";
            this.fileContentArea.ReadOnly = true;
            this.fileContentArea.Size = new System.Drawing.Size(550, 430);
            this.fileContentArea.TabIndex = 1;
            this.fileContentArea.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(183, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Содержимое файла";
            // 
            // errorsList
            // 
            this.errorsList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.errorsList.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.errorsList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.errorsList.Location = new System.Drawing.Point(568, 65);
            this.errorsList.Name = "errorsList";
            this.errorsList.ReadOnly = true;
            this.errorsList.Size = new System.Drawing.Size(550, 430);
            this.errorsList.TabIndex = 3;
            this.errorsList.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(782, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Список ошибок";
            // 
            // analyzeButton
            // 
            this.analyzeButton.BackColor = System.Drawing.Color.Lime;
            this.analyzeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.analyzeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.analyzeButton.Location = new System.Drawing.Point(474, 544);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(197, 41);
            this.analyzeButton.TabIndex = 5;
            this.analyzeButton.Text = "Анализировать файл";
            this.analyzeButton.UseVisualStyleBackColor = false;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 611);
            this.Controls.Add(this.analyzeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.errorsList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fileContentArea);
            this.Controls.Add(this.openFileButton);
            this.MaximumSize = new System.Drawing.Size(1145, 650);
            this.MinimumSize = new System.Drawing.Size(1145, 650);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Лабораторная работа №7";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.RichTextBox fileContentArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox errorsList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button analyzeButton;
    }
}

