using Asterion.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AsterionEngine.Tools.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ArchiveButtonAddFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Filter = "Any files (*.*)|*";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                List<string> files = ArchiveFilesListBox.Items.OfType<string>().ToList();

                foreach (string f in ofd.FileNames)
                    files.Add(f);

                ArchiveFilesListBox.Items.Clear();
                ArchiveFilesListBox.Items.AddRange(files.Distinct().ToArray());
            }
        }

        private void ArchiveFilesListBox_DoubleClick(object sender, EventArgs e)
        {
            if (ArchiveFilesListBox.SelectedIndex < 0) return;
            ArchiveFilesListBox.Items.RemoveAt(ArchiveFilesListBox.SelectedIndex);
        }

        private void ArchiveButtonCreateArchive_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Asterion archives (*.ast)|*.ast";
                if (sfd.ShowDialog() != DialogResult.OK) return;

                FileSourceArchive.CreateArchive(sfd.FileName, ArchivePasswordTextBox.Text, ArchiveFilesListBox.Items.OfType<string>().ToArray());
            }
        }
    }
}
