using System;
using System.Windows.Forms;
using L4D2_CFG_Manager.UI;

namespace L4D2_CFG_Manager
{
    public class MainForm : Form
    {
        private TabControl tabControl;

        public MainForm()
        {
            Text = "L4D2 CFG Manager";
            Width = 1000;
            Height = 700;

            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            tabControl.TabPages.Add(new AutoexecTab());
            tabControl.TabPages.Add(new BindsTab());

            Controls.Add(tabControl);
        }
    }
}