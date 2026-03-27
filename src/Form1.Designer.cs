using System;
using System.Windows.Forms;
using System.Drawing;

namespace L4D2_CFG_Manager
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		private TextBox cfgEditor;
		private Button btnOpen;
		private Button btnSave;
		private Button btnNew;

		private void InitializeComponent()
		{
    		this.cfgEditor = new TextBox();
    		this.btnOpen = new Button();
    		this.btnSave = new Button();
    		this.btnNew = new Button();

   		this.SuspendLayout();

   		this.cfgEditor.Multiline = true;
   		this.cfgEditor.ScrollBars = ScrollBars.Vertical;
   		this.cfgEditor.Location = new Point(10, 50);
   		this.cfgEditor.Size = new Size(780, 390);

   		this.btnOpen.Text = "Open CFG";
   		this.btnOpen.Location = new Point(10, 10);
  		this.btnOpen.Click += new EventHandler(this.btnOpen_Click);

   		this.btnSave.Text = "Save CFG";
   		this.btnSave.Location = new Point(110, 10);
   		this.btnSave.Click += new EventHandler(this.btnSave_Click);

   		this.btnNew.Text = "New CFG";
   		this.btnNew.Location = new Point(210, 10);
   		this.btnNew.Click += new EventHandler(this.btnNew_Click);

		this.ClientSize = new Size(800, 450);
   		this.Controls.Add(this.cfgEditor);
		this.Controls.Add(this.btnOpen);
   		this.Controls.Add(this.btnSave);
   		this.Controls.Add(this.btnNew);
   		this.Text = "L4D2 CFG Manager";

   		this.ResumeLayout(false);
		}
    }
}