using System;
using System.IO;
using System.Windows.Forms;

namespace L4D2_CFG_Manager
{
    public partial class Form1 : Form
    {
        string currentFile = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CFG Files (*.cfg)|*.cfg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentFile = ofd.FileName;
                cfgEditor.Text = File.ReadAllText(currentFile);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (currentFile == "")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "CFG Files (*.cfg)|*.cfg";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    currentFile = sfd.FileName;
                }
            }

            if (currentFile != "")
            {
                File.WriteAllText(currentFile, cfgEditor.Text);
                MessageBox.Show("CFG Saved!");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            cfgEditor.Clear();
            currentFile = "";
        }
    }
}