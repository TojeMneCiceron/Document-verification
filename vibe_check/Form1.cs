using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace vibe_check
{
    public partial class VibeCheck : Form
    {
        string fileName = "";
        List<string> files = new List<string>();

        bool rpChosen = false;
        bool folder = false;

        Dictionary<string, int> Structure;
        HashSet<string> RequiredParts, MainParts, SubParts;
        Dictionary<string, string> Attachments;

        string fileNameFormat = "\nДЗ1 Иванов П.С., ПМИ-3-18.doc\nДЗ2 Сидорова О.И, КМБ-1-18.docx\nЛР1 Ч1 Скоробогатова М.М., ПМИ-1-18.docx\n";

        public VibeCheck()
        {
            InitializeComponent();
            cmbCheckLevel.SelectedItem = cmbCheckLevel.Items[0];
        }

        void EnableCheckButton()
        {
            bCheck.Enabled = lbFiles.Visible & (lbParts.Visible || cmbCheckLevel.SelectedItem == cmbCheckLevel.Items[0]);
        }

        private void bOpenFile_Click(object sender, EventArgs e)
        {
            lbChosenFileNum.Visible = lbFileNum.Visible = false;

            //pictureBox.Image = new Bitmap(@"C:\Users\

            var openFile = new OpenFileDialog();
            openFile.Filter = "Документы Word (*.doc *.docx)|*.doc;*.docx";
            openFile.Multiselect = false;

            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            if (!VerifRegex.CheckFileName(openFile.SafeFileName))
            {
                MessageBox.Show($"Имя файла \"{openFile.SafeFileName}\" не соотвествует требованиям по именованию файлов.\nПереименуйте файл в следующем формате:{fileNameFormat}или выберите другой файл", "Неверное имя файла");
                return;
            }
              
            tbFileName.Text = openFile.SafeFileName;
            fileName = openFile.FileName;

            var size = new FileInfo(fileName).Length;

            if (size == 0)
            {
                bCheck.Enabled = false;
                MessageBox.Show("Выбран пустой файл", "Пустой файл");
                bCheck.Enabled = false;
                lbFiles.Visible = false;
            }

            lbFiles.Visible = true;
            folder = false;

            EnableCheckButton();
            //MessageBox.Show(EditAndHash.CheckFileName(tbFileName.Text).ToString());
        }

        private void bCheck_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Убедитесь, что все файлы, предоставленные на проверку не являются открытыми.\nПродолжить?", "Начать проверку", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            string fp = "";

            DateTime dateTime = DateTime.Now;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            CheckMode checkMode = (CheckMode)cmbCheckLevel.SelectedIndex;

            if (folder)
            {
                lb.Visible = true;
                int i = 0;

                lbCheckedFiles.Visible = lbCheckedFilesCount.Visible = true;

                foreach (string file in files)
                {
                    lbCheckedFilesCount.Text = $"{i}/{files.Count}";

                    lb.Text = $"Идет проверка:\n{file.Remove(0, tbFileName.Text.Length + 1)}";
                    VibeChecker vc = new VibeChecker(checkMode, file, file.Remove(0, tbFileName.Text.Length + 1), Structure, RequiredParts, MainParts, SubParts, Attachments, dateTime);
                    vc.Check();

                    i++;

                    fp = vc.FolderPath;
                }

                lbCheckedFiles.Visible = lbCheckedFilesCount.Visible = false;
                lb.Visible = false;
            }
            else
            {
                lb.Visible = true;
                lb.Text = $"Идет проверка:\n{tbFileName.Text}";

                VibeChecker vc = new VibeChecker(checkMode, fileName, tbFileName.Text, Structure, RequiredParts, MainParts, SubParts, Attachments, dateTime);
                bool res = vc.Check();

                fp = vc.FolderPath;

                lb.Visible = false;

                //if (res)
                //{
                //    MessageBox.Show("Congratulations! You passed your vibe check!");
                //}
                //else
                //{
                //    pictureBox.Image = new Bitmap(@"


                //}

            }

            sw.Stop();

            MessageBox.Show($"Проверка завершена! Результаты находятся в следующей папке:\n{fp}\nВремя работы программы: {(sw.ElapsedMilliseconds / 1000).ToString()} c");
        }

        private void bOpenFolder_Click(object sender, EventArgs e)
        {
            lbChosenFileNum.Visible = lbFileNum.Visible = false;

            var folderBrowser = new FolderBrowserDialog
            {
                ShowNewFolderButton = false
            };

            if (folderBrowser.ShowDialog() != DialogResult.OK)
                return;

            files.Clear();

            string[] files_temp = Directory.GetFiles(folderBrowser.SelectedPath, "*.doc");

            foreach (string f in files_temp)
            {
                string temp = f.Remove(0, folderBrowser.SelectedPath.Length + 1);

                var size = new FileInfo(f).Length;

                if (size == 0)
                {
                    MessageBox.Show($"Файл {temp} пуст", "Пустой файл");
                    continue;
                }

                if (!VerifRegex.CheckFileName(temp))
                {
                    MessageBox.Show($"Имя файла \"{temp}\" не соотвествует требованиям по именованию файлов.\nФайл проверке не подлежит", "Неверное имя файла");
                    continue;
                }
                files.Add(f);
            }

            tbFileName.Text = folderBrowser.SelectedPath;

            lbFileNum.Text = files.Count().ToString();
            lbChosenFileNum.Visible = lbFileNum.Visible = true;

            if (lbFileNum.Text != "0")
                lbFiles.Visible = true;
            else
            {
                lbFiles.Visible = false;
                MessageBox.Show($"Ни один файл в выбранной папке не подлежит проверке.\nПереименуйте файлы в следующем формате:{fileNameFormat}или выберите другую папку", "Не выбран ни один файл");
            }

            folder = true;
            EnableCheckButton();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpWindow hw = new HelpWindow();
            hw.ShowDialog();
        }

        private void поверхностныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbCheckLevel.SelectedItem = cmbCheckLevel.Items[0];
        }

        private void проверкаСтруктурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbCheckLevel.SelectedItem = cmbCheckLevel.Items[1];
        }

        private void глубокийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmbCheckLevel.SelectedItem = cmbCheckLevel.Items[2];
        }

        private void выбратьРазделыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bChooseParts_Click(sender, e);
        }

        private void выбратьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bOpenFile_Click(sender, e);
        }

        private void выбратьПапкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bOpenFolder_Click(sender, e);
        }

        private void cmbCheckLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            выбратьРазделыToolStripMenuItem.Enabled = bChooseParts.Enabled = cmbCheckLevel.SelectedItem != cmbCheckLevel.Items[0];
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bChooseParts_Click(object sender, EventArgs e)
        {
            var rp = new StructureWindow();
            rp.ShowDialog();

            Structure = rp.Structure;
            RequiredParts = rp.RequiredParts;
            MainParts = rp.MainParts;
            SubParts = rp.SubParts;
            Attachments = rp.Attachments;

            rpChosen = lbParts.Visible = MainParts.Count > 0;
            EnableCheckButton();
        }
    }
}
