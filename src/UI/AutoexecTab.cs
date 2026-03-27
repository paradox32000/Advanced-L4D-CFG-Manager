using System;
using System.Windows.Forms;
using System.Drawing;
using L4D2_CFG_Manager.Core;

namespace L4D2_CFG_Manager.UI
{
    public class AutoexecTab : TabPage
    {
        private RichTextBox editor;
        private Button openButton;
        private Button saveButton;
        private Button openFolderButton;
		private Button addCommandsButton;
		private TextBox rateBox;
		private TextBox cmdRateBox;
		private TextBox updateRateBox;
		private RichTextBox previewBox;
		private TextBox interpRateBox;
		private TextBox ratioRateBox;

        private Panel topBar;

        private ConfigService configService;

        public AutoexecTab()
        {
            Text = "Autoexec";

            configService = new ConfigService();

            topBar = new Panel();
            topBar.Height = 40;
            topBar.Dock = DockStyle.Top;

            openButton = new Button();
            openButton.Text = "Open CFG";
            openButton.Location = new Point(10, 8);
            openButton.Click += OpenCFG;

            saveButton = new Button();
            saveButton.Text = "Save CFG";
            saveButton.Location = new Point(110, 8);
            saveButton.Click += SaveCFG;

            openFolderButton = new Button();
            openFolderButton.Text = "CFG Folder";
            openFolderButton.Location = new Point(210, 8);
            openFolderButton.Click += OpenFolder;

            topBar.Controls.Add(openButton);
            topBar.Controls.Add(saveButton);
            topBar.Controls.Add(openFolderButton);

            editor = new RichTextBox();
            editor.Dock = DockStyle.Top;
			editor.Height = 250;
            editor.Font = new Font("Consolas", 11);

            Controls.Add(editor);
            Controls.Add(topBar);
			addCommandsButton = new Button();
			addCommandsButton.Text = "Add Recommended";
			addCommandsButton.Location = new Point(330, 8);
			addCommandsButton.Click += AddCommands;
			Label rateLabel = new Label();
			rateLabel.Text = "rate";
			rateLabel.Location = new Point(10, 60);

			rateBox = new TextBox();
			rateBox.Location = new Point(10, 90);
			rateBox.TextChanged += UpdatePreview;

			Label cmdLabel = new Label();
			cmdLabel.Text = "cl_cmdrate";
			cmdLabel.Location = new Point(10, 90);

			cmdRateBox = new TextBox();
			cmdRateBox.Location = new Point(120, 90);
			cmdRateBox.TextChanged += UpdatePreview;

			Label updateLabel = new Label();
			updateLabel.Text = "cl_updaterate";
			updateLabel.Location = new Point(10, 120);

			updateRateBox = new TextBox();
			updateRateBox.Location = new Point(120, 120);
			updateRateBox.TextChanged += UpdatePreview;
			previewBox = new RichTextBox();
			previewBox.Dock = DockStyle.Bottom;
			previewBox.Height = 200;
			previewBox.Font = new System.Drawing.Font("Consolas", 11);
			Controls.Add(rateLabel);
			Controls.Add(rateBox);

			Controls.Add(cmdLabel);
			Controls.Add(cmdRateBox);

			Controls.Add(updateLabel);
			Controls.Add(updateRateBox);

			Controls.Add(previewBox);
			SplitContainer split = new SplitContainer();
			split.Dock = DockStyle.Fill;
			split.Orientation = Orientation.Horizontal;
			split.SplitterDistance = 300;

			Controls.Add(split);
			
			split.Panel1.Controls.Add(editor);
			split.Panel2.Controls.Add(previewBox);
			
			editor.Font = new Font("Consolas", 11);
			previewBox.Font = new Font("Consolas", 11);

			topBar.Controls.Add(addCommandsButton);
        }

        private void OpenCFG(object sender, EventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "CFG files (*.cfg)|*.cfg";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				string content = configService.LoadConfig(dialog.FileName);

				CreateNewTab(dialog.SafeFileName, content);
			}
		}
		
		private void CreateNewTab(string title, string content)
		{
			TabPage tab = new TabPage(title);

			RichTextBox newEditor = new RichTextBox();
			newEditor.Dock = DockStyle.Fill;
			newEditor.Font = new Font("Consolas", 11);
			newEditor.Text = content;

			tab.Controls.Add(newEditor);

			((TabControl)this.Parent).TabPages.Add(tab);
		}
		
		private void ParseCFG(string content)
		{
			var lines = content.Split('\n');

			foreach (var line in lines)
			{
				string clean = line.Trim();

				if (string.IsNullOrWhiteSpace(clean) || clean.StartsWith("//"))
					continue;

				clean = clean.Replace("\"", "");

				var parts = clean.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);

				if (parts.Length < 2)
					continue;

				string key = parts[0];
				string value = parts[1];

				if (key == "rate")
					rateBox.Text = value;

				if (key == "cl_cmdrate")
					cmdRateBox.Text = value;

				if (key == "cl_updaterate")
					updateRateBox.Text = value;
				
				if (key == "cl_interp")
					interpRateBox.Text = value;
				
				if (key == "cl_interp_ratio")
					ratioRateBox.Text = value;
			}
		}
		
		private void AddCommands(object? sender, EventArgs e)
		{
			EnsureCommand("cl_interp_ratio 1");
			EnsureCommand("cl_cmdrate 66");
			EnsureCommand("cl_updaterate 66");
			EnsureCommand("rate 100000");
			EnsureCommand("cl_interp 0");
		}
		
		private void EnsureCommand(string command)
		{
			var tabControl = this.Parent as TabControl;

			if (tabControl == null) return;

			var currentTab = tabControl.SelectedTab;

			if (currentTab == null) return;

			var editor = currentTab.Controls[0] as RichTextBox;

			if (editor == null) return;

			if (!editor.Text.Contains(command))
			{
        editor.AppendText(Environment.NewLine + command);
			}
		}

        private void SaveCFG(object sender, EventArgs e)
        {
            configService.SaveConfig(editor.Text);
        }

        private void OpenFolder(object sender, EventArgs e)
        {
            string path = @"C:\Program Files (x86)\Steam\steamapps\common\Left 4 Dead 2\left4dead2\cfg";

            if (System.IO.Directory.Exists(path))
            {
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            else
            {
                MessageBox.Show("CFG folder not found.");
            }
        }
		private void UpdatePreview(object? sender, EventArgs e)
		{
			string cfg = "";

			if (!string.IsNullOrWhiteSpace(rateBox.Text))
				cfg += $"rate {rateBox.Text}\n";

			if (!string.IsNullOrWhiteSpace(cmdRateBox.Text))
				cfg += $"cl_cmdrate {cmdRateBox.Text}\n";

			if (!string.IsNullOrWhiteSpace(updateRateBox.Text))
				cfg += $"cl_updaterate {updateRateBox.Text}\n";
			
			if (!string.IsNullOrWhiteSpace(interpRateBox.Text))
				cfg += $"cl_interp {interpRateBox.Text}\n";

			if (!string.IsNullOrWhiteSpace(ratioRateBox.Text))
				cfg += $"cl_interp_ratio {ratioRateBox.Text}\n";

			previewBox.Text = cfg;
		}
    }
}