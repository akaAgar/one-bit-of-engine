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
            this.ArchiveTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ArchiveFilesGroupBox = new System.Windows.Forms.GroupBox();
            this.ArchiveFilesListBox = new System.Windows.Forms.ListBox();
            this.ArchiveButtonAddFiles = new System.Windows.Forms.Button();
            this.ArchiveButtonCreateArchive = new System.Windows.Forms.Button();
            this.ArchivePasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.ArchivePasswordTextBox = new System.Windows.Forms.TextBox();
            this.ArchiveInfoTextBox = new System.Windows.Forms.TextBox();
            this.TabPageTileSetAnims = new System.Windows.Forms.TabPage();
            this.AnimationsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.MainTabControl.SuspendLayout();
            this.TabPageArchive.SuspendLayout();
            this.ArchiveTableLayoutPanel.SuspendLayout();
            this.ArchiveFilesGroupBox.SuspendLayout();
            this.ArchivePasswordGroupBox.SuspendLayout();
            this.TabPageTileSetAnims.SuspendLayout();
            this.AnimationsTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.TabPageArchive);
            this.MainTabControl.Controls.Add(this.TabPageTileSetAnims);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(588, 456);
            this.MainTabControl.TabIndex = 0;
            // 
            // TabPageArchive
            // 
            this.TabPageArchive.Controls.Add(this.ArchiveTableLayoutPanel);
            this.TabPageArchive.Location = new System.Drawing.Point(4, 22);
            this.TabPageArchive.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TabPageArchive.Name = "TabPageArchive";
            this.TabPageArchive.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TabPageArchive.Size = new System.Drawing.Size(580, 430);
            this.TabPageArchive.TabIndex = 0;
            this.TabPageArchive.Text = "Archive creator";
            this.TabPageArchive.UseVisualStyleBackColor = true;
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
            this.ArchiveTableLayoutPanel.Location = new System.Drawing.Point(2, 2);
            this.ArchiveTableLayoutPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ArchiveTableLayoutPanel.Name = "ArchiveTableLayoutPanel";
            this.ArchiveTableLayoutPanel.RowCount = 4;
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.ArchiveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.ArchiveTableLayoutPanel.Size = new System.Drawing.Size(576, 426);
            this.ArchiveTableLayoutPanel.TabIndex = 0;
            // 
            // ArchiveFilesGroupBox
            // 
            this.ArchiveFilesGroupBox.Controls.Add(this.ArchiveFilesListBox);
            this.ArchiveFilesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveFilesGroupBox.Location = new System.Drawing.Point(2, 2);
            this.ArchiveFilesGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ArchiveFilesGroupBox.Name = "ArchiveFilesGroupBox";
            this.ArchiveFilesGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ArchiveTableLayoutPanel.SetRowSpan(this.ArchiveFilesGroupBox, 4);
            this.ArchiveFilesGroupBox.Size = new System.Drawing.Size(380, 422);
            this.ArchiveFilesGroupBox.TabIndex = 0;
            this.ArchiveFilesGroupBox.TabStop = false;
            this.ArchiveFilesGroupBox.Text = "Files to include in the archive";
            // 
            // ArchiveFilesListBox
            // 
            this.ArchiveFilesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveFilesListBox.FormattingEnabled = true;
            this.ArchiveFilesListBox.Location = new System.Drawing.Point(2, 15);
            this.ArchiveFilesListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ArchiveFilesListBox.Name = "ArchiveFilesListBox";
            this.ArchiveFilesListBox.ScrollAlwaysVisible = true;
            this.ArchiveFilesListBox.Size = new System.Drawing.Size(376, 405);
            this.ArchiveFilesListBox.Sorted = true;
            this.ArchiveFilesListBox.TabIndex = 0;
            this.ArchiveFilesListBox.DoubleClick += new System.EventHandler(this.ArchiveFilesListBox_DoubleClick);
            // 
            // ArchiveButtonAddFiles
            // 
            this.ArchiveButtonAddFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveButtonAddFiles.Location = new System.Drawing.Point(386, 315);
            this.ArchiveButtonAddFiles.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.ArchiveButtonCreateArchive.Location = new System.Drawing.Point(386, 396);
            this.ArchiveButtonCreateArchive.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.ArchivePasswordGroupBox.Location = new System.Drawing.Point(386, 347);
            this.ArchivePasswordGroupBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ArchivePasswordGroupBox.Name = "ArchivePasswordGroupBox";
            this.ArchivePasswordGroupBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ArchivePasswordGroupBox.Size = new System.Drawing.Size(188, 45);
            this.ArchivePasswordGroupBox.TabIndex = 4;
            this.ArchivePasswordGroupBox.TabStop = false;
            this.ArchivePasswordGroupBox.Text = "Password";
            // 
            // ArchivePasswordTextBox
            // 
            this.ArchivePasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchivePasswordTextBox.Location = new System.Drawing.Point(2, 15);
            this.ArchivePasswordTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
            this.ArchiveInfoTextBox.Location = new System.Drawing.Point(386, 2);
            this.ArchiveInfoTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ArchiveInfoTextBox.Multiline = true;
            this.ArchiveInfoTextBox.Name = "ArchiveInfoTextBox";
            this.ArchiveInfoTextBox.ReadOnly = true;
            this.ArchiveInfoTextBox.Size = new System.Drawing.Size(188, 309);
            this.ArchiveInfoTextBox.TabIndex = 5;
            this.ArchiveInfoTextBox.Text = resources.GetString("ArchiveInfoTextBox.Text");
            // 
            // TabPageTileSetAnims
            // 
            this.TabPageTileSetAnims.Controls.Add(this.AnimationsTableLayoutPanel);
            this.TabPageTileSetAnims.Location = new System.Drawing.Point(4, 22);
            this.TabPageTileSetAnims.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TabPageTileSetAnims.Name = "TabPageTileSetAnims";
            this.TabPageTileSetAnims.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TabPageTileSetAnims.Size = new System.Drawing.Size(580, 430);
            this.TabPageTileSetAnims.TabIndex = 1;
            this.TabPageTileSetAnims.Text = "Tileset animations maker";
            this.TabPageTileSetAnims.UseVisualStyleBackColor = true;
            // 
            // AnimationsTableLayoutPanel
            // 
            this.AnimationsTableLayoutPanel.ColumnCount = 4;
            this.AnimationsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.AnimationsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.AnimationsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.AnimationsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.AnimationsTableLayoutPanel.Controls.Add(this.pictureBox1, 2, 0);
            this.AnimationsTableLayoutPanel.Controls.Add(this.pictureBox2, 0, 0);
            this.AnimationsTableLayoutPanel.Controls.Add(this.label1, 1, 1);
            this.AnimationsTableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.AnimationsTableLayoutPanel.Controls.Add(this.numericUpDown1, 1, 2);
            this.AnimationsTableLayoutPanel.Controls.Add(this.numericUpDown2, 0, 2);
            this.AnimationsTableLayoutPanel.Controls.Add(this.label3, 2, 1);
            this.AnimationsTableLayoutPanel.Controls.Add(this.numericUpDown3, 2, 2);
            this.AnimationsTableLayoutPanel.Controls.Add(this.button1, 0, 3);
            this.AnimationsTableLayoutPanel.Controls.Add(this.button2, 2, 3);
            this.AnimationsTableLayoutPanel.Controls.Add(this.button3, 3, 1);
            this.AnimationsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationsTableLayoutPanel.Location = new System.Drawing.Point(2, 2);
            this.AnimationsTableLayoutPanel.Name = "AnimationsTableLayoutPanel";
            this.AnimationsTableLayoutPanel.RowCount = 4;
            this.AnimationsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.AnimationsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.AnimationsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.AnimationsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.AnimationsTableLayoutPanel.Size = new System.Drawing.Size(576, 426);
            this.AnimationsTableLayoutPanel.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnimationsTableLayoutPanel.SetColumnSpan(this.pictureBox1, 2);
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(291, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(282, 316);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnimationsTableLayoutPanel.SetColumnSpan(this.pictureBox2, 2);
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(282, 316);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(147, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 322);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown1.Location = new System.Drawing.Point(147, 357);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(138, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown2.Location = new System.Drawing.Point(3, 357);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(138, 20);
            this.numericUpDown2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(291, 322);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "label3";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numericUpDown3.Location = new System.Drawing.Point(291, 357);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(138, 20);
            this.numericUpDown3.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Location = new System.Drawing.Point(3, 389);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 34);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Location = new System.Drawing.Point(291, 389);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(138, 34);
            this.button2.TabIndex = 9;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Location = new System.Drawing.Point(435, 325);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(138, 26);
            this.button3.TabIndex = 10;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 456);
            this.Controls.Add(this.MainTabControl);
            this.Name = "MainForm";
            this.Text = "Asterion Engine Tools";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainTabControl.ResumeLayout(false);
            this.TabPageArchive.ResumeLayout(false);
            this.ArchiveTableLayoutPanel.ResumeLayout(false);
            this.ArchiveTableLayoutPanel.PerformLayout();
            this.ArchiveFilesGroupBox.ResumeLayout(false);
            this.ArchivePasswordGroupBox.ResumeLayout(false);
            this.ArchivePasswordGroupBox.PerformLayout();
            this.TabPageTileSetAnims.ResumeLayout(false);
            this.AnimationsTableLayoutPanel.ResumeLayout(false);
            this.AnimationsTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel AnimationsTableLayoutPanel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}