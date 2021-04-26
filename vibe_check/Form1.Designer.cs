namespace vibe_check
{
    partial class VibeCheck
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.bOpenFile = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.bCheck = new System.Windows.Forms.Button();
            this.lb = new System.Windows.Forms.Label();
            this.ms = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьПапкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьРазделыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.уровеньПроверкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поверхностныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.проверкаСтруктурыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.глубокийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbParts = new System.Windows.Forms.Label();
            this.bOpenFolder = new System.Windows.Forms.Button();
            this.lbChosenFileNum = new System.Windows.Forms.Label();
            this.lbFileNum = new System.Windows.Forms.Label();
            this.bChooseParts = new System.Windows.Forms.Button();
            this.lbFiles = new System.Windows.Forms.Label();
            this.cmbCheckLevel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbCheckedFiles = new System.Windows.Forms.Label();
            this.lbCheckedFilesCount = new System.Windows.Forms.Label();
            this.ms.SuspendLayout();
            this.SuspendLayout();
            // 
            // bOpenFile
            // 
            this.bOpenFile.Location = new System.Drawing.Point(52, 85);
            this.bOpenFile.Name = "bOpenFile";
            this.bOpenFile.Size = new System.Drawing.Size(133, 23);
            this.bOpenFile.TabIndex = 0;
            this.bOpenFile.Text = "Выбрать файл";
            this.bOpenFile.UseVisualStyleBackColor = true;
            this.bOpenFile.Click += new System.EventHandler(this.bOpenFile_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.BackColor = System.Drawing.Color.White;
            this.tbFileName.Location = new System.Drawing.Point(52, 143);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(272, 20);
            this.tbFileName.TabIndex = 1;
            // 
            // bCheck
            // 
            this.bCheck.Enabled = false;
            this.bCheck.Location = new System.Drawing.Point(134, 169);
            this.bCheck.Name = "bCheck";
            this.bCheck.Size = new System.Drawing.Size(108, 23);
            this.bCheck.TabIndex = 2;
            this.bCheck.Text = "Начать проверку";
            this.bCheck.UseVisualStyleBackColor = true;
            this.bCheck.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.BackColor = System.Drawing.SystemColors.Control;
            this.lb.ForeColor = System.Drawing.Color.Black;
            this.lb.Location = new System.Drawing.Point(12, 195);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(86, 13);
            this.lb.TabIndex = 4;
            this.lb.Text = "Идет проверка:";
            this.lb.Visible = false;
            // 
            // ms
            // 
            this.ms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.настройкиToolStripMenuItem,
            this.справкаToolStripMenuItem});
            this.ms.Location = new System.Drawing.Point(0, 0);
            this.ms.Name = "ms";
            this.ms.Size = new System.Drawing.Size(376, 24);
            this.ms.TabIndex = 5;
            this.ms.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьФайлToolStripMenuItem,
            this.выбратьПапкуToolStripMenuItem,
            this.toolStripSeparator1,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // выбратьФайлToolStripMenuItem
            // 
            this.выбратьФайлToolStripMenuItem.Name = "выбратьФайлToolStripMenuItem";
            this.выбратьФайлToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.выбратьФайлToolStripMenuItem.Text = "Выбрать файл...";
            this.выбратьФайлToolStripMenuItem.Click += new System.EventHandler(this.выбратьФайлToolStripMenuItem_Click);
            // 
            // выбратьПапкуToolStripMenuItem
            // 
            this.выбратьПапкуToolStripMenuItem.Name = "выбратьПапкуToolStripMenuItem";
            this.выбратьПапкуToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.выбратьПапкуToolStripMenuItem.Text = "Выбрать папку...";
            this.выбратьПапкуToolStripMenuItem.Click += new System.EventHandler(this.выбратьПапкуToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выбратьРазделыToolStripMenuItem,
            this.уровеньПроверкиToolStripMenuItem});
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // выбратьРазделыToolStripMenuItem
            // 
            this.выбратьРазделыToolStripMenuItem.Name = "выбратьРазделыToolStripMenuItem";
            this.выбратьРазделыToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.выбратьРазделыToolStripMenuItem.Text = "Выбрать разделы";
            this.выбратьРазделыToolStripMenuItem.Click += new System.EventHandler(this.выбратьРазделыToolStripMenuItem_Click);
            // 
            // уровеньПроверкиToolStripMenuItem
            // 
            this.уровеньПроверкиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поверхностныйToolStripMenuItem,
            this.проверкаСтруктурыToolStripMenuItem,
            this.глубокийToolStripMenuItem});
            this.уровеньПроверкиToolStripMenuItem.Name = "уровеньПроверкиToolStripMenuItem";
            this.уровеньПроверкиToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.уровеньПроверкиToolStripMenuItem.Text = "Уровень проверки";
            // 
            // поверхностныйToolStripMenuItem
            // 
            this.поверхностныйToolStripMenuItem.Name = "поверхностныйToolStripMenuItem";
            this.поверхностныйToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.поверхностныйToolStripMenuItem.Text = "Поверхностный";
            this.поверхностныйToolStripMenuItem.Click += new System.EventHandler(this.поверхностныйToolStripMenuItem_Click);
            // 
            // проверкаСтруктурыToolStripMenuItem
            // 
            this.проверкаСтруктурыToolStripMenuItem.Name = "проверкаСтруктурыToolStripMenuItem";
            this.проверкаСтруктурыToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.проверкаСтруктурыToolStripMenuItem.Text = "Проверка структуры";
            this.проверкаСтруктурыToolStripMenuItem.Click += new System.EventHandler(this.проверкаСтруктурыToolStripMenuItem_Click);
            // 
            // глубокийToolStripMenuItem
            // 
            this.глубокийToolStripMenuItem.Name = "глубокийToolStripMenuItem";
            this.глубокийToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.глубокийToolStripMenuItem.Text = "Глубокий";
            this.глубокийToolStripMenuItem.Click += new System.EventHandler(this.глубокийToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            this.справкаToolStripMenuItem.Click += new System.EventHandler(this.справкаToolStripMenuItem_Click);
            // 
            // lbParts
            // 
            this.lbParts.AutoSize = true;
            this.lbParts.BackColor = System.Drawing.SystemColors.Control;
            this.lbParts.Location = new System.Drawing.Point(330, 119);
            this.lbParts.Name = "lbParts";
            this.lbParts.Size = new System.Drawing.Size(15, 13);
            this.lbParts.TabIndex = 7;
            this.lbParts.Text = "✓";
            this.lbParts.Visible = false;
            // 
            // bOpenFolder
            // 
            this.bOpenFolder.Location = new System.Drawing.Point(191, 85);
            this.bOpenFolder.Name = "bOpenFolder";
            this.bOpenFolder.Size = new System.Drawing.Size(133, 23);
            this.bOpenFolder.TabIndex = 8;
            this.bOpenFolder.Text = "Выбрать папку";
            this.bOpenFolder.UseVisualStyleBackColor = true;
            this.bOpenFolder.Click += new System.EventHandler(this.bOpenFolder_Click);
            // 
            // lbChosenFileNum
            // 
            this.lbChosenFileNum.AutoSize = true;
            this.lbChosenFileNum.BackColor = System.Drawing.SystemColors.Control;
            this.lbChosenFileNum.ForeColor = System.Drawing.Color.Black;
            this.lbChosenFileNum.Location = new System.Drawing.Point(249, 208);
            this.lbChosenFileNum.Name = "lbChosenFileNum";
            this.lbChosenFileNum.Size = new System.Drawing.Size(96, 13);
            this.lbChosenFileNum.TabIndex = 9;
            this.lbChosenFileNum.Text = "Выбрано файлов:";
            this.lbChosenFileNum.Visible = false;
            // 
            // lbFileNum
            // 
            this.lbFileNum.AutoSize = true;
            this.lbFileNum.BackColor = System.Drawing.SystemColors.Control;
            this.lbFileNum.ForeColor = System.Drawing.Color.Black;
            this.lbFileNum.Location = new System.Drawing.Point(351, 208);
            this.lbFileNum.Name = "lbFileNum";
            this.lbFileNum.Size = new System.Drawing.Size(13, 13);
            this.lbFileNum.TabIndex = 10;
            this.lbFileNum.Text = "0";
            this.lbFileNum.Visible = false;
            // 
            // bChooseParts
            // 
            this.bChooseParts.Location = new System.Drawing.Point(52, 114);
            this.bChooseParts.Name = "bChooseParts";
            this.bChooseParts.Size = new System.Drawing.Size(272, 23);
            this.bChooseParts.TabIndex = 11;
            this.bChooseParts.Text = "Выбрать разделы";
            this.bChooseParts.UseVisualStyleBackColor = true;
            this.bChooseParts.Click += new System.EventHandler(this.bChooseParts_Click);
            // 
            // lbFiles
            // 
            this.lbFiles.AutoSize = true;
            this.lbFiles.BackColor = System.Drawing.SystemColors.Control;
            this.lbFiles.Location = new System.Drawing.Point(330, 90);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(15, 13);
            this.lbFiles.TabIndex = 12;
            this.lbFiles.Text = "✓";
            this.lbFiles.Visible = false;
            // 
            // cmbCheckLevel
            // 
            this.cmbCheckLevel.DisplayMember = "0";
            this.cmbCheckLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCheckLevel.FormattingEnabled = true;
            this.cmbCheckLevel.Items.AddRange(new object[] {
            "Поверхностный",
            "Проверка структуры",
            "Глубокий"});
            this.cmbCheckLevel.Location = new System.Drawing.Point(191, 58);
            this.cmbCheckLevel.Name = "cmbCheckLevel";
            this.cmbCheckLevel.Size = new System.Drawing.Size(133, 21);
            this.cmbCheckLevel.TabIndex = 13;
            this.cmbCheckLevel.SelectedIndexChanged += new System.EventHandler(this.cmbCheckLevel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(75, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Уровень проверки:";
            // 
            // lbCheckedFiles
            // 
            this.lbCheckedFiles.AutoSize = true;
            this.lbCheckedFiles.BackColor = System.Drawing.SystemColors.Control;
            this.lbCheckedFiles.ForeColor = System.Drawing.Color.Black;
            this.lbCheckedFiles.Location = new System.Drawing.Point(12, 35);
            this.lbCheckedFiles.Name = "lbCheckedFiles";
            this.lbCheckedFiles.Size = new System.Drawing.Size(107, 13);
            this.lbCheckedFiles.TabIndex = 15;
            this.lbCheckedFiles.Text = "Проверено файлов:";
            this.lbCheckedFiles.Visible = false;
            // 
            // lbCheckedFilesCount
            // 
            this.lbCheckedFilesCount.AutoSize = true;
            this.lbCheckedFilesCount.BackColor = System.Drawing.SystemColors.Control;
            this.lbCheckedFilesCount.ForeColor = System.Drawing.Color.Black;
            this.lbCheckedFilesCount.Location = new System.Drawing.Point(125, 35);
            this.lbCheckedFilesCount.Name = "lbCheckedFilesCount";
            this.lbCheckedFilesCount.Size = new System.Drawing.Size(24, 13);
            this.lbCheckedFilesCount.TabIndex = 16;
            this.lbCheckedFilesCount.Text = "0/0";
            this.lbCheckedFilesCount.Visible = false;
            // 
            // VibeCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(376, 229);
            this.Controls.Add(this.lbCheckedFilesCount);
            this.Controls.Add(this.lbCheckedFiles);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCheckLevel);
            this.Controls.Add(this.lbFiles);
            this.Controls.Add(this.bChooseParts);
            this.Controls.Add(this.lbFileNum);
            this.Controls.Add(this.lbChosenFileNum);
            this.Controls.Add(this.bOpenFolder);
            this.Controls.Add(this.lbParts);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.bCheck);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.bOpenFile);
            this.Controls.Add(this.ms);
            this.MainMenuStrip = this.ms;
            this.Name = "VibeCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Приложение";
            this.ms.ResumeLayout(false);
            this.ms.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bOpenFile;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button bCheck;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.MenuStrip ms;
        private System.Windows.Forms.Label lbParts;
        private System.Windows.Forms.Button bOpenFolder;
        private System.Windows.Forms.Label lbChosenFileNum;
        private System.Windows.Forms.Label lbFileNum;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.Button bChooseParts;
        private System.Windows.Forms.Label lbFiles;
        private System.Windows.Forms.ComboBox cmbCheckLevel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьПапкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьРазделыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem уровеньПроверкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поверхностныйToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem проверкаСтруктурыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem глубокийToolStripMenuItem;
        private System.Windows.Forms.Label lbCheckedFiles;
        private System.Windows.Forms.Label lbCheckedFilesCount;
    }
}

