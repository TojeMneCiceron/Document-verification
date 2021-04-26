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


namespace vibe_check
{
    public partial class HelpWindow : Form
    {
        public HelpWindow()
        {
            InitializeComponent();

            tbHelpMain.Text = ReadHelp("Main");
            if (!string.IsNullOrEmpty(tbHelpMain.Text)) tbHelpMain.Select(0, 0);

            tbHelpParts.Text = ReadHelp("Parts");
            //if (!string.IsNullOrEmpty(tbHelpMain.Text)) tbHelpMain.Select(0, 0);

            tbHelpErrors.Text = ReadHelp("Errors");
            //if (!string.IsNullOrEmpty(tbHelpMain.Text)) tbHelpMain.Select(0, 0);

            tbHelpRecommendations.Text = ReadHelp("Recommendations");            
        }

        string ReadHelp(string name)
        {
            string appPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string helpPath = Path.GetDirectoryName(appPath) + $"\\help{name}.txt";
            StreamReader sr = new StreamReader(helpPath);
            string help = sr.ReadToEnd();
            sr.Close();
            return help;
        }
    }
}
