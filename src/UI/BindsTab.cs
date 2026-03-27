using System;
using System.Windows.Forms;

namespace L4D2_CFG_Manager.UI
{
    public class BindsTab : TabPage
    {
        private DataGridView bindsGrid;
        private Button addBindButton;
        private Button generateCFGButton;
		private Button loadCFGButton;

        public BindsTab()
        {
            Text = "Binds";

            bindsGrid = new DataGridView();
            bindsGrid.Dock = DockStyle.Fill;
            bindsGrid.AllowUserToAddRows = false;

            bindsGrid.Columns.Add("key", "Key");
            bindsGrid.Columns.Add("command", "Command");

            addBindButton = new Button();
            addBindButton.Text = "Add Bind";
            addBindButton.Dock = DockStyle.Top;
            addBindButton.Click += AddBind;
			
			loadCFGButton = new Button();
			loadCFGButton.Text = "Load CF	G";
			loadCFGButton.Dock = DockStyle.Top;
			loadCFGButton.Click += LoadCFG;

			Controls.Add(loadCFGButton);

            generateCFGButton = new Button();
            generateCFGButton.Text = "Generate CFG";
            generateCFGButton.Dock = DockStyle.Top;
            generateCFGButton.Click += GenerateCFG;

            Controls.Add(bindsGrid);
            Controls.Add(generateCFGButton);
            Controls.Add(addBindButton);

            AddDefaultBinds();
			bindsGrid.Columns[0].Width = 150;
			bindsGrid.Columns[1].Width = 500;
        }

        private void AddBind(object? sender, EventArgs e)
        {
            bindsGrid.Rows.Add("", "");
        }

        private void GenerateCFG(object? sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			dialog.Filter = "CFG files (*.cfg)|*.cfg";
			dialog.FileName = "binds.cfg";

			if (dialog.ShowDialog() == DialogResult.OK)
			{
				string cfg = "";

				foreach (DataGridViewRow row in bindsGrid.Rows)
				{
					if (row.Cells[0].Value != null && row.Cells[1].Value != null)
					{
						string key = row.Cells[0].Value.ToString();
						string command = row.Cells[1].Value.ToString();

						cfg += $"bind \"{key}\" \"{command}\"" + Environment.NewLine;
					}
				}

				System.IO.File.WriteAllText(dialog.FileName, cfg);

				MessageBox.Show("CFG exported successfully!");
			}
		}

        private void AddDefaultBinds()
        {
            bindsGrid.Rows.Add("mouse1", "+attack");
            bindsGrid.Rows.Add("mouse2", "+attack2");
            bindsGrid.Rows.Add("r", "+reload");
            bindsGrid.Rows.Add("space", "+jump");
        }
		private void LoadCFG(object? sender, EventArgs e)
{
    OpenFileDialog dialog = new OpenFileDialog();
    dialog.Filter = "CFG files (*.cfg)|*.cfg";

    if (dialog.ShowDialog() == DialogResult.OK)
    {
        bindsGrid.Rows.Clear();

        var lines = System.IO.File.ReadAllLines(dialog.FileName);

        foreach (var line in lines)
        {
            if (line.StartsWith("bind"))
            {
                var parts = line.Split('"');

                if (parts.Length >= 4)
                {
                    string key = parts[1];
                    string command = parts[3];

                    bindsGrid.Rows.Add(key, command);
						}
					}
				}
			}
		}
	}
}