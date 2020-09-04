namespace OneBitOfEngine.Tools.Forms
{
    partial class ArchiveCreatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveCreatorForm));
            this.ArchiveTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ArchiveButtonAddFiles = new System.Windows.Forms.Button();
            this.ArchiveButtonCreateArchive = new System.Windows.Forms.Button();
            this.ArchivePasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.ArchivePasswordTextBox = new System.Windows.Forms.TextBox();
            this.ArchiveInfoTextBox = new System.Windows.Forms.TextBox();
            this.ArchiveFilesListBox = new System.Windows.Forms.ListBox();
            this.ArchiveFilesGroupBox = new System.Windows.Forms.GroupBox();
            this.ArchiveTableLayoutPanel.SuspendLayout();
            this.ArchivePasswordGroupBox.SuspendLayout();
            this.ArchiveFilesGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ArchiveTableLayoutPanel
            // 
            this.ArchiveTableLayoutPanel.ColumnCount = 2;
            this.ArchiveTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ArchiveTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 192F));
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchiveFilesGroupBox, 0, 0);
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchiveButtonAddFiles, 1, 1);
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchiveButtonCreateArchive, 1, 3);
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchivePasswordGroupBox, 1, 2);
            this.ArchiveTableLayoutPanel.Controls.Add(this.ArchiveInfoTextBox, 1, 0);
            this.ArchiveTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.ArchiveTableLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
            this.ArchiveTableLayoutPanel.Name = "ArchiveTableLayoutPanel";
            this.ArchiveTableLayoutPanel.RowCount = 4;
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.ArchiveTableLayoutPanel.Size = new System.Drawing.Size(624, 441);
            this.ArchiveTableLayoutPanel.TabIndex = 1;
            // 
            // ArchiveButtonAddFiles
            // 
            this.ArchiveButtonAddFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveButtonAddFiles.Location = new System.Drawing.Point(434, 330);
            this.ArchiveButtonAddFiles.Margin = new System.Windows.Forms.Padding(2);
            this.ArchiveButtonAddFiles.Name = "ArchiveButtonAddFiles";
            this.ArchiveButtonAddFiles.Size = new System.Drawing.Size(188, 28);
            this.ArchiveButtonAddFiles.TabIndex = 2;
            this.ArchiveButtonAddFiles.Text = "Add files";
            this.ArchiveButtonAddFiles.UseVisualStyleBackColor = true;
            this.ArchiveButtonAddFiles.Click += new System.EventHandler(this.ArchiveButtonAddFiles_Click);
            // 
            // ArchiveButtonCreateArchive
            // 
            this.ArchiveButtonCreateArchive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveButtonCreateArchive.Location = new System.Drawing.Point(434, 411);
            this.ArchiveButtonCreateArchive.Margin = new System.Windows.Forms.Padding(2);
            this.ArchiveButtonCreateArchive.Name = "ArchiveButtonCreateArchive";
            this.ArchiveButtonCreateArchive.Size = new System.Drawing.Size(188, 28);
            this.ArchiveButtonCreateArchive.TabIndex = 3;
            this.ArchiveButtonCreateArchive.Text = "Create archive";
            this.ArchiveButtonCreateArchive.UseVisualStyleBackColor = true;
            this.ArchiveButtonCreateArchive.Click += new System.EventHandler(this.ArchiveButtonCreateArchive_Click);
            // 
            // ArchivePasswordGroupBox
            // 
            this.ArchivePasswordGroupBox.Controls.Add(this.ArchivePasswordTextBox);
            this.ArchivePasswordGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchivePasswordGroupBox.Location = new System.Drawing.Point(434, 362);
            this.ArchivePasswordGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.ArchivePasswordGroupBox.Name = "ArchivePasswordGroupBox";
            this.ArchivePasswordGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.ArchivePasswordGroupBox.Size = new System.Drawing.Size(188, 45);
            this.ArchivePasswordGroupBox.TabIndex = 4;
            this.ArchivePasswordGroupBox.TabStop = false;
            this.ArchivePasswordGroupBox.Text = "Password";
            // 
            // ArchivePasswordTextBox
            // 
            this.ArchivePasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchivePasswordTextBox.Location = new System.Drawing.Point(2, 15);
            this.ArchivePasswordTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.ArchivePasswordTextBox.Name = "ArchivePasswordTextBox";
            this.ArchivePasswordTextBox.Size = new System.Drawing.Size(184, 20);
            this.ArchivePasswordTextBox.TabIndex = 0;
            // 
            // ArchiveInfoTextBox
            // 
            this.ArchiveInfoTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.ArchiveInfoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ArchiveInfoTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ArchiveInfoTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveInfoTextBox.Location = new System.Drawing.Point(440, 8);
            this.ArchiveInfoTextBox.Margin = new System.Windows.Forms.Padding(8);
            this.ArchiveInfoTextBox.Multiline = true;
            this.ArchiveInfoTextBox.Name = "ArchiveInfoTextBox";
            this.ArchiveInfoTextBox.ReadOnly = true;
            this.ArchiveInfoTextBox.Size = new System.Drawing.Size(176, 312);
            this.ArchiveInfoTextBox.TabIndex = 5;
            this.ArchiveInfoTextBox.Text = resources.GetString("ArchiveInfoTextBox.Text");
            // 
            // ArchiveFilesListBox
            // 
            this.ArchiveFilesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveFilesListBox.FormattingEnabled = true;
            this.ArchiveFilesListBox.Location = new System.Drawing.Point(3, 16);
            this.ArchiveFilesListBox.Margin = new System.Windows.Forms.Padding(2);
            this.ArchiveFilesListBox.Name = "ArchiveFilesListBox";
            this.ArchiveFilesListBox.ScrollAlwaysVisible = true;
            this.ArchiveFilesListBox.Size = new System.Drawing.Size(420, 416);
            this.ArchiveFilesListBox.Sorted = true;
            this.ArchiveFilesListBox.TabIndex = 0;
            this.ArchiveFilesListBox.Click += new System.EventHandler(this.ArchiveFilesListBox_DoubleClick);
            // 
            // ArchiveFilesGroupBox
            // 
            this.ArchiveFilesGroupBox.Controls.Add(this.ArchiveFilesListBox);
            this.ArchiveFilesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveFilesGroupBox.Location = new System.Drawing.Point(3, 3);
            this.ArchiveFilesGroupBox.Name = "ArchiveFilesGroupBox";
            this.ArchiveTableLayoutPanel.SetRowSpan(this.ArchiveFilesGroupBox, 4);
            this.ArchiveFilesGroupBox.Size = new System.Drawing.Size(426, 435);
            this.ArchiveFilesGroupBox.TabIndex = 0;
            this.ArchiveFilesGroupBox.TabStop = false;
            this.ArchiveFilesGroupBox.Text = "Files to include in the archive";
            // 
            // ArchiveCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.ArchiveTableLayoutPanel);
            this.Name = "ArchiveCreatorForm";
            this.Text = "One Bit of Engine Tools";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ArchiveTableLayoutPanel.ResumeLayout(false);
            this.ArchiveTableLayoutPanel.PerformLayout();
            this.ArchivePasswordGroupBox.ResumeLayout(false);
            this.ArchivePasswordGroupBox.PerformLayout();
            this.ArchiveFilesGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ArchiveTableLayoutPanel;
        private System.Windows.Forms.Button ArchiveButtonAddFiles;
        private System.Windows.Forms.Button ArchiveButtonCreateArchive;
        private System.Windows.Forms.GroupBox ArchivePasswordGroupBox;
        private System.Windows.Forms.TextBox ArchivePasswordTextBox;
        private System.Windows.Forms.TextBox ArchiveInfoTextBox;
        private System.Windows.Forms.GroupBox ArchiveFilesGroupBox;
        private System.Windows.Forms.ListBox ArchiveFilesListBox;
    }
}