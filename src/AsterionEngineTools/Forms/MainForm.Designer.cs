namespace AsterionEngine.Tools.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.TabPageArchive = new System.Windows.Forms.TabPage();
            this.TabPageTileSetAnims = new System.Windows.Forms.TabPage();
            this.ArchiveTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ArchiveFilesGroupBox = new System.Windows.Forms.GroupBox();
            this.ArchiveButtonAddFiles = new System.Windows.Forms.Button();
            this.ArchiveButtonCreateArchive = new System.Windows.Forms.Button();
            this.ArchiveFilesListBox = new System.Windows.Forms.ListBox();
            this.ArchivePasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.ArchivePasswordTextBox = new System.Windows.Forms.TextBox();
            this.ArchiveInfoTextBox = new System.Windows.Forms.TextBox();
            this.MainTabControl.SuspendLayout();
            this.TabPageArchive.SuspendLayout();
            this.ArchiveTableLayoutPanel.SuspendLayout();
            this.ArchiveFilesGroupBox.SuspendLayout();
            this.ArchivePasswordGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.TabPageArchive);
            this.MainTabControl.Controls.Add(this.TabPageTileSetAnims);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(784, 561);
            this.MainTabControl.TabIndex = 0;
            // 
            // TabPageArchive
            // 
            this.TabPageArchive.Controls.Add(this.ArchiveTableLayoutPanel);
            this.TabPageArchive.Location = new System.Drawing.Point(4, 25);
            this.TabPageArchive.Name = "TabPageArchive";
            this.TabPageArchive.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageArchive.Size = new System.Drawing.Size(776, 532);
            this.TabPageArchive.TabIndex = 0;
            this.TabPageArchive.Text = "Archive creator";
            this.TabPageArchive.UseVisualStyleBackColor = true;
            // 
            // TabPageTileSetAnims
            // 
            this.TabPageTileSetAnims.Location = new System.Drawing.Point(4, 25);
            this.TabPageTileSetAnims.Name = "TabPageTileSetAnims";
            this.TabPageTileSetAnims.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageTileSetAnims.Size = new System.Drawing.Size(776, 532);
            this.TabPageTileSetAnims.TabIndex = 1;
            this.TabPageTileSetAnims.Text = "Tileset animations maker";
            this.TabPageTileSetAnims.UseVisualStyleBackColor = true;
            // 
            // ArchiveTableLayoutPanel
            // 
            this.ArchiveTableLayoutPanel.ColumnCount = 2;
            this.ArchiveTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ArchiveTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchiveFilesGroupBox, 0, 0);
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchiveButtonAddFiles, 1, 1);
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchiveButtonCreateArchive, 1, 3);
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchivePasswordGroupBox, 1, 2);
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchiveInfoTextBox, 1, 0);
            this.ArchiveTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveTableLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.ArchiveTableLayoutPanel.Name = "ArchiveTableLayoutPanel";
            this.ArchiveTableLayoutPanel.RowCount = 4;
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.ArchiveTableLayoutPanel.Size = new System.Drawing.Size(770, 526);
            this.ArchiveTableLayoutPanel.TabIndex = 0;
            // 
            // ArchiveFilesGroupBox
            // 
            this.ArchiveFilesGroupBox.Controls.Add(this.ArchiveFilesListBox);
            this.ArchiveFilesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveFilesGroupBox.Location = new System.Drawing.Point(3, 3);
            this.ArchiveFilesGroupBox.Name = "ArchiveFilesGroupBox";
            this.ArchiveTableLayoutPanel.SetRowSpan(this.ArchiveFilesGroupBox, 4);
            this.ArchiveFilesGroupBox.Size = new System.Drawing.Size(508, 520);
            this.ArchiveFilesGroupBox.TabIndex = 0;
            this.ArchiveFilesGroupBox.TabStop = false;
            this.ArchiveFilesGroupBox.Text = "Files to include in the archive";
            // 
            // ArchiveButtonAddFiles
            // 
            this.ArchiveButtonAddFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveButtonAddFiles.Location = new System.Drawing.Point(517, 389);
            this.ArchiveButtonAddFiles.Name = "ArchiveButtonAddFiles";
            this.ArchiveButtonAddFiles.Size = new System.Drawing.Size(250, 34);
            this.ArchiveButtonAddFiles.TabIndex = 2;
            this.ArchiveButtonAddFiles.Text = "Add files";
            this.ArchiveButtonAddFiles.UseVisualStyleBackColor = true;
            this.ArchiveButtonAddFiles.Click += new System.EventHandler(this.ArchiveButtonAddFiles_Click);
            // 
            // ArchiveButtonCreateArchive
            // 
            this.ArchiveButtonCreateArchive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveButtonCreateArchive.Location = new System.Drawing.Point(517, 489);
            this.ArchiveButtonCreateArchive.Name = "ArchiveButtonCreateArchive";
            this.ArchiveButtonCreateArchive.Size = new System.Drawing.Size(250, 34);
            this.ArchiveButtonCreateArchive.TabIndex = 3;
            this.ArchiveButtonCreateArchive.Text = "Create archive";
            this.ArchiveButtonCreateArchive.UseVisualStyleBackColor = true;
            this.ArchiveButtonCreateArchive.Click += new System.EventHandler(this.ArchiveButtonCreateArchive_Click);
            // 
            // ArchiveFilesListBox
            // 
            this.ArchiveFilesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveFilesListBox.FormattingEnabled = true;
            this.ArchiveFilesListBox.ItemHeight = 16;
            this.ArchiveFilesListBox.Location = new System.Drawing.Point(3, 18);
            this.ArchiveFilesListBox.Name = "ArchiveFilesListBox";
            this.ArchiveFilesListBox.ScrollAlwaysVisible = true;
            this.ArchiveFilesListBox.Size = new System.Drawing.Size(502, 499);
            this.ArchiveFilesListBox.Sorted = true;
            this.ArchiveFilesListBox.TabIndex = 0;
            this.ArchiveFilesListBox.DoubleClick += new System.EventHandler(this.ArchiveFilesListBox_DoubleClick);
            // 
            // ArchivePasswordGroupBox
            // 
            this.ArchivePasswordGroupBox.Controls.Add(this.ArchivePasswordTextBox);
            this.ArchivePasswordGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchivePasswordGroupBox.Location = new System.Drawing.Point(517, 429);
            this.ArchivePasswordGroupBox.Name = "ArchivePasswordGroupBox";
            this.ArchivePasswordGroupBox.Size = new System.Drawing.Size(250, 54);
            this.ArchivePasswordGroupBox.TabIndex = 4;
            this.ArchivePasswordGroupBox.TabStop = false;
            this.ArchivePasswordGroupBox.Text = "Password";
            // 
            // ArchivePasswordTextBox
            // 
            this.ArchivePasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchivePasswordTextBox.Location = new System.Drawing.Point(3, 18);
            this.ArchivePasswordTextBox.Name = "ArchivePasswordTextBox";
            this.ArchivePasswordTextBox.Size = new System.Drawing.Size(244, 22);
            this.ArchivePasswordTextBox.TabIndex = 0;
            // 
            // ArchiveInfoTextBox
            // 
            this.ArchiveInfoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.ArchiveInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ArchiveInfoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ArchiveInfoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveInfoTextBox.Location = new System.Drawing.Point(517, 3);
            this.ArchiveInfoTextBox.Multiline = true;
            this.ArchiveInfoTextBox.Name = "ArchiveInfoTextBox";
            this.ArchiveInfoTextBox.ReadOnly = true;
            this.ArchiveInfoTextBox.Size = new System.Drawing.Size(250, 380);
            this.ArchiveInfoTextBox.TabIndex = 5;
            this.ArchiveInfoTextBox.Text = resources.GetString("ArchiveInfoTextBox.Text");
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.MainTabControl);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Asterion Engine Tools";
            this.MainTabControl.ResumeLayout(false);
            this.TabPageArchive.ResumeLayout(false);
            this.ArchiveTableLayoutPanel.ResumeLayout(false);
            this.ArchiveTableLayoutPanel.PerformLayout();
            this.ArchiveFilesGroupBox.ResumeLayout(false);
            this.ArchivePasswordGroupBox.ResumeLayout(false);
            this.ArchivePasswordGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage TabPageArchive;
        private System.Windows.Forms.TabPage TabPageTileSetAnims;
        private System.Windows.Forms.TableLayoutPanel ArchiveTableLayoutPanel;
        private System.Windows.Forms.GroupBox ArchiveFilesGroupBox;
        private System.Windows.Forms.ListBox ArchiveFilesListBox;
        private System.Windows.Forms.Button ArchiveButtonAddFiles;
        private System.Windows.Forms.Button ArchiveButtonCreateArchive;
        private System.Windows.Forms.GroupBox ArchivePasswordGroupBox;
        private System.Windows.Forms.TextBox ArchivePasswordTextBox;
        private System.Windows.Forms.TextBox ArchiveInfoTextBox;
    }
}