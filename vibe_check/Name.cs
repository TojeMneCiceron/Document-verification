using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vibe_check
{
    public partial class NameForm : Form
    {
        string partName = "";

        public NameForm(string name)
        {
            InitializeComponent();
            this.Name = name;
        }

        public string PartName { get => partName; set => partName = value; }

        private void bSave_Click(object sender, EventArgs e)
        {
            partName = Regularochki.DeleteExtraSpaces(textBox1.Text);
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
