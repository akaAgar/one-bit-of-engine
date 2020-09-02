namespace AsterionEngine.Tools.Forms
{
    partial class MainMenuForm
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
            this.MenuTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ArchiveCreatorButton = new System.Windows.Forms.Button();
            this.TileMapEnumCreatorButton = new System.Windows.Forms.Button();
            this.TilemapAnimationMakerButton = new System.Windows.Forms.Button();
            this.MenuTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuTableLayoutPanel
            // 
            this.MenuTableLayoutPanel.ColumnCount = 1;
            this.MenuTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MenuTableLayoutPanel.Controls.Add(this.ArchiveCreatorButton, 0, 0);
            this.MenuTableLayoutPanel.Controls.Add(this.TileMapEnumCreatorButton, 0, 1);
            this.MenuTableLayoutPanel.Controls.Add(this.TilemapAnimationMakerButton, 0, 2);
            this.MenuTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MenuTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.MenuTableLayoutPanel.Name = "MenuTableLayoutPanel";
            this.MenuTableLayoutPanel.RowCount = 4;
            this.MenuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MenuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MenuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.MenuTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.MenuTableLayoutPanel.Size = new System.Drawing.Size(368, 201);
            this.MenuTableLayoutPanel.TabIndex = 0;
            // 
            // ArchiveCreatorButton
            // 
            this.ArchiveCreatorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveCreatorButton.Location = new System.Drawing.Point(3, 3);
            this.ArchiveCreatorButton.Name = "ArchiveCreatorButton";
            this.ArchiveCreatorButton.Size = new System.Drawing.Size(362, 34);
            this.ArchiveCreatorButton.TabIndex = 0;
            this.ArchiveCreatorButton.Text = "Archive creator";
            this.ArchiveCreatorButton.UseVisualStyleBackColor = true;
            this.ArchiveCreatorButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // TileMapEnumCreatorButton
            // 
            this.TileMapEnumCreatorButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileMapEnumCreatorButton.Location = new System.Drawing.Point(3, 43);
            this.TileMapEnumCreatorButton.Name = "TileMapEnumCreatorButton";
            this.TileMapEnumCreatorButton.Size = new System.Drawing.Size(362, 34);
            this.TileMapEnumCreatorButton.TabIndex = 1;
            this.TileMapEnumCreatorButton.Text = "Tilemap enum creator";
            this.TileMapEnumCreatorButton.UseVisualStyleBackColor = true;
            this.TileMapEnumCreatorButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // TilemapAnimationMakerButton
            // 
            this.TilemapAnimationMakerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TilemapAnimationMakerButton.Location = new System.Drawing.Point(3, 83);
            this.TilemapAnimationMakerButton.Name = "TilemapAnimationMakerButton";
            this.TilemapAnimationMakerButton.Size = new System.Drawing.Size(362, 34);
            this.TilemapAnimationMakerButton.TabIndex = 2;
            this.TilemapAnimationMakerButton.Text = "Tilemap animation maker creator";
            this.TilemapAnimationMakerButton.UseVisualStyleBackColor = true;
            this.TilemapAnimationMakerButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 201);
            this.Controls.Add(this.MenuTableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asterion Engine Development Tools";
            this.Load += new System.EventHandler(this.MainMenuForm_Load);
            this.MenuTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel MenuTableLayoutPanel;
        private System.Windows.Forms.Button ArchiveCreatorButton;
        private System.Windows.Forms.Button TileMapEnumCreatorButton;
        private System.Windows.Forms.Button TilemapAnimationMakerButton;
    }
}