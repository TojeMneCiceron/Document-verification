namespace vibe_check
{
    partial class HelpWindow
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
            this.tb = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbHelpMain = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbHelpParts = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbHelpErrors = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tbHelpRecommendations = new System.Windows.Forms.TextBox();
            this.tb.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb
            // 
            this.tb.Controls.Add(this.tabPage1);
            this.tb.Controls.Add(this.tabPage2);
            this.tb.Controls.Add(this.tabPage4);
            this.tb.Controls.Add(this.tabPage3);
            this.tb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb.Location = new System.Drawing.Point(0, 0);
            this.tb.Name = "tb";
            this.tb.SelectedIndex = 0;
            this.tb.Size = new System.Drawing.Size(870, 247);
            this.tb.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tbHelpMain);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(862, 221);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Работа с программой";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbHelpMain
            // 
            this.tbHelpMain.BackColor = System.Drawing.Color.White;
            this.tbHelpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHelpMain.Location = new System.Drawing.Point(3, 3);
            this.tbHelpMain.Multiline = true;
            this.tbHelpMain.Name = "tbHelpMain";
            this.tbHelpMain.ReadOnly = true;
            this.tbHelpMain.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbHelpMain.Size = new System.Drawing.Size(856, 215);
            this.tbHelpMain.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbHelpParts);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(862, 221);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Выбор разделов";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbHelpParts
            // 
            this.tbHelpParts.BackColor = System.Drawing.Color.White;
            this.tbHelpParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHelpParts.Location = new System.Drawing.Point(3, 3);
            this.tbHelpParts.Multiline = true;
            this.tbHelpParts.Name = "tbHelpParts";
            this.tbHelpParts.ReadOnly = true;
            this.tbHelpParts.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbHelpParts.Size = new System.Drawing.Size(856, 215);
            this.tbHelpParts.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbHelpErrors);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(862, 221);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Если ошибок избежать не удалось...";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbHelpErrors
            // 
            this.tbHelpErrors.BackColor = System.Drawing.Color.White;
            this.tbHelpErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHelpErrors.Location = new System.Drawing.Point(0, 0);
            this.tbHelpErrors.Multiline = true;
            this.tbHelpErrors.Name = "tbHelpErrors";
            this.tbHelpErrors.ReadOnly = true;
            this.tbHelpErrors.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbHelpErrors.Size = new System.Drawing.Size(862, 221);
            this.tbHelpErrors.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tbHelpRecommendations);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(862, 221);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Рекомендации по оформлению документов";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tbHelpRecommendations
            // 
            this.tbHelpRecommendations.BackColor = System.Drawing.Color.White;
            this.tbHelpRecommendations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbHelpRecommendations.Location = new System.Drawing.Point(0, 0);
            this.tbHelpRecommendations.Multiline = true;
            this.tbHelpRecommendations.Name = "tbHelpRecommendations";
            this.tbHelpRecommendations.ReadOnly = true;
            this.tbHelpRecommendations.Size = new System.Drawing.Size(862, 221);
            this.tbHelpRecommendations.TabIndex = 0;
            // 
            // HelpWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(870, 247);
            this.Controls.Add(this.tb);
            this.Name = "HelpWindow";
            this.Text = "Справка";
            this.tb.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tb;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox tbHelpMain;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbHelpParts;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbHelpErrors;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox tbHelpRecommendations;
    }
}