using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace vibe_check
{
    public partial class GOSTRequired : Form
    {
        Dictionary<string, int> structure = new Dictionary<string, int>();
        HashSet<string> requiredParts = new HashSet<string>();
        HashSet<string> mainParts = new HashSet<string>();
        HashSet<string> subParts = new HashSet<string>();
        Dictionary<string, string> attachments = new Dictionary<string, string>();

        bool cancel = false;

        XmlSerializer xs = new XmlSerializer(typeof(Save1));

        public GOSTRequired()
        {
            InitializeComponent();
            InitializeDGV();
            tvParts.ExpandAll();

            buttonsCheck();
        }

        public Dictionary<string, int> Structure { get => structure; set => structure = value; }
        public HashSet<string> RequiredParts { get => requiredParts; set => requiredParts = value; }
        public HashSet<string> MainParts { get => mainParts; set => mainParts = value; }
        public Dictionary<string, string> Attachments { get => attachments; set => attachments = value; }
        public HashSet<string> SubParts { get => subParts; set => subParts = value; }

        void buttonsCheck()
        {
            разделыToolStripMenuItem.Enabled = tbc.SelectedIndex == 1;

            редактироватьToolStripMenuItem.Enabled = удалитьToolStripMenuItem.Enabled = !(tvParts.SelectedNode is null);

            добавитьПодразделToolStripMenuItem.Enabled = !(tvParts.SelectedNode is null) && tvParts.SelectedNode.Level == 0;
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            cancel = true;

            Close();
        }

        void InitializeDGV()
        {
            dgvRequired.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                HeaderText = @"Название элемента",
                ReadOnly = true
            });

            dgvRequired.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "Add",
                HeaderText = @""
            });

            dgvRequired.AllowUserToAddRows = dgvRequired.AllowUserToDeleteRows = false;

            string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string path = Path.GetDirectoryName(appPath) + "\\Settings.xml";

            Deserialize(path);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            structure.Clear();
            requiredParts.Clear();
            mainParts.Clear();

            int i = 0;

            foreach (DataGridViewRow row in dgvRequired.Rows)
            {
                if ((bool)row.Cells[1].Value)
                {
                    structure.Add(((string)row.Cells[0].Value).ToLower(), ++i);
                    requiredParts.Add(((string)row.Cells[0].Value).ToLower());
                }
                if ((string)row.Cells[0].Value == "Введение")
                    foreach (TreeNode node in tvParts.Nodes)
                    {                        
                        structure.Add(node.Text.ToLower(), ++i);
                        foreach (TreeNode subNode in node.Nodes)
                        {
                            structure.Add(subNode.Text.ToLower(), ++i);
                            subParts.Add(subNode.Text.ToLower());
                        }
                        mainParts.Add(node.Text.ToLower());
                    }
            }

            char attachmentNum = 'А';

            foreach (DataGridViewRow row in dgvAttachments.Rows)
            {
                if (row.Cells[0].Value is null)
                    break;
                //structure.Add(((string)row.Cells[0].Value).ToLower(), ++i);
                structure.Add(("Приложение " + attachmentNum).ToLower(), i + 1);
                structure.Add(("Приложение " + attachmentNum + "\v" + Regularochki.DeleteExtraSpaces((string)row.Cells[0].Value)).ToLower(), ++i);
                requiredParts.Add(("Приложение " + attachmentNum).ToLower());
                requiredParts.Add(("Приложение " + attachmentNum + "\v" + Regularochki.DeleteExtraSpaces((string)row.Cells[0].Value)).ToLower());
                attachments.Add(("Приложение " + attachmentNum).ToLower(), Regularochki.DeleteExtraSpaces((string)row.Cells[0].Value).ToLower());
                attachmentNum++;
            }

            if (mainParts.Count == 0)
            {
                MessageBox.Show("Введите хотя бы один раздел");
                return;
            }

            SerializeLastSetting();

            DialogResult = DialogResult.OK;

            Close();
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpWindow hw = new HelpWindow();
            hw.ShowDialog();
        }

        private void GOSTRequired_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cancel)
                SerializeLastSetting();
        }

        private void основнаяЧастьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tvParts.Nodes.Clear();
        }

        private void приложенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgvAttachments.Rows.Clear();
        }

        Save1 CreateSave()
        {
            List<RequiredPart> requiredParts = new List<RequiredPart>();
            List<Part> Parts = new List<Part>();
            List<Attachment> attachments = new List<Attachment>();

            foreach (DataGridViewRow row in dgvRequired.Rows)
                requiredParts.Add(new RequiredPart((string)row.Cells[0].Value, (bool)row.Cells[1].Value, row.Cells[1].ReadOnly));

            foreach (TreeNode node in tvParts.Nodes)
            {
                List<string> subParts_ = new List<string>();
                foreach (TreeNode subNode in node.Nodes)
                    subParts_.Add(subNode.Text);

                Parts.Add(new Part(node.Text, subParts_));
            }

            foreach (DataGridViewRow row in dgvAttachments.Rows)
            {
                if (row.Cells[0].Value is null)
                    break;
                attachments.Add(new Attachment((string)row.Cells[0].Value));
            }

            return new Save1(requiredParts, Parts, attachments);
        }

        void SerializeLastSetting()
        {
            string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string path = Path.GetDirectoryName(appPath) + "\\Settings.xml";

            TextWriter textWriter = new StreamWriter(path);

            xs.Serialize(textWriter, CreateSave());

            textWriter.Close();
        }

        void Deserialize(string path)
        {
            dgvRequired.Rows.Clear();
            tvParts.Nodes.Clear();
            dgvAttachments.Rows.Clear();

            Save1 save;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                save = (Save1)xs.Deserialize(fs);
            }

            foreach (RequiredPart rp in save.requiredParts)
            {
                int i = dgvRequired.Rows.Add(rp.name, rp.chosen);
                dgvRequired.Rows[i].Cells[1].ReadOnly = rp.alwaysChosen;
            }

            foreach (Part part in save.Parts)
            {
                var node = tvParts.Nodes.Add(part.name);
                foreach (string subPart in part.subParts)
                    node.Nodes.Add(subPart);
            }

            foreach (Attachment ip in save.attachments)
                dgvAttachments.Rows.Add(ip.name);
        }

        private void открытьФайлНастроекToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFile = new OpenFileDialog();
            openFile.Filter = "Файл XML (*.xml)|*.xml";
            openFile.Multiselect = false;

            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            Deserialize(openFile.FileName);
        }

        private void сохранитьНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saveFile = new SaveFileDialog();
            saveFile.Filter = "Файл XML (*.xml)|*.xml";

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            TextWriter textWriter = new StreamWriter(saveFile.FileName);

            xs.Serialize(textWriter, CreateSave());

            textWriter.Close();

            MessageBox.Show("Настройки успешно сохранены!", "Сохранение");
        }

        private void добавитьРазделToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nw = new NameForm("Введите название раздела");

            if (nw.ShowDialog() != DialogResult.OK)
                return;

            tvParts.Nodes.Add(nw.PartName);
        }

        private void добавитьПодразделToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nw = new NameForm("Введите название подраздела");

            if (nw.ShowDialog() != DialogResult.OK)
                return;

            tvParts.SelectedNode.Nodes.Add(nw.PartName);
            tvParts.SelectedNode.Expand();
        }

        private void tbc_TabIndexChanged(object sender, EventArgs e)
        {
            buttonsCheck();
        }

        private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var nw = new NameForm("Введите название");

            if (nw.ShowDialog() != DialogResult.OK)
                return;

            tvParts.SelectedNode.Text = nw.PartName;
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить этот элемент?", "Удаление", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            tvParts.SelectedNode.Remove();
            buttonsCheck();
        }

        private void tvParts_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvParts.SelectedNode = e.Node;
            buttonsCheck();
        }

        private void tbc_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonsCheck();
        }
    }
}
