namespace AsterionEngine.Tools.Forms
{
    partial class TilemapAnimationMakerForm
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
            this.AnimationsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TileHeightLabel = new System.Windows.Forms.Label();
            this.TileWidthLabel = new System.Windows.Forms.Label();
            this.TileHeightNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.TileWidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.AnimationFramesLabel = new System.Windows.Forms.Label();
            this.AnimationFramesLabelNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.SaveImageButton = new System.Windows.Forms.Button();
            this.CreateAnimatedTilemapButton = new System.Windows.Forms.Button();
            this.SourceTilemapPanel = new System.Windows.Forms.Panel();
            this.SourceTilemapPictureBox = new System.Windows.Forms.PictureBox();
            this.OutputImagePanel = new System.Windows.Forms.Panel();
            this.OutputImagePictureBox = new System.Windows.Forms.PictureBox();
            this.AnimationsTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileHeightNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TileWidthNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationFramesLabelNumericUpDown)).BeginInit();
            this.SourceTilemapPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceTilemapPictureBox)).BeginInit();
            this.OutputImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutputImagePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // AnimationsTableLayoutPanel
            // 
            this.AnimationsTableLayoutPanel.ColumnCount = 4;
            this.AnimationsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.AnimationsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.AnimationsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.AnimationsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.AnimationsTableLayoutPanel.Controls.Add(this.TileHeightLabel, 1, 1);
            this.AnimationsTableLayoutPanel.Controls.Add(this.TileWidthLabel, 0, 1);
            this.AnimationsTableLayoutPanel.Controls.Add(this.TileHeightNumericUpDown, 1, 2);
            this.AnimationsTableLayoutPanel.Controls.Add(this.TileWidthNumericUpDown, 0, 2);
            this.AnimationsTableLayoutPanel.Controls.Add(this.AnimationFramesLabel, 2, 1);
            this.AnimationsTableLayoutPanel.Controls.Add(this.AnimationFramesLabelNumericUpDown, 2, 2);
            this.AnimationsTableLayoutPanel.Controls.Add(this.LoadImageButton, 0, 3);
            this.AnimationsTableLayoutPanel.Controls.Add(this.SaveImageButton, 2, 3);
            this.AnimationsTableLayoutPanel.Controls.Add(this.CreateAnimatedTilemapButton, 3, 1);
            this.AnimationsTableLayoutPanel.Controls.Add(this.SourceTilemapPanel, 0, 0);
            this.AnimationsTableLayoutPanel.Controls.Add(this.OutputImagePanel, 2, 0);
            this.AnimationsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationsTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.AnimationsTableLayoutPanel.Name = "AnimationsTableLayoutPanel";
            this.AnimationsTableLayoutPanel.RowCount = 4;
            this.AnimationsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.AnimationsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.AnimationsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.AnimationsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.AnimationsTableLayoutPanel.Size = new System.Drawing.Size(784, 561);
            this.AnimationsTableLayoutPanel.TabIndex = 1;
            // 
            // TileHeightLabel
            // 
            this.TileHeightLabel.AutoSize = true;
            this.TileHeightLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileHeightLabel.Location = new System.Drawing.Point(199, 460);
            this.TileHeightLabel.Margin = new System.Windows.Forms.Padding(3);
            this.TileHeightLabel.Name = "TileHeightLabel";
            this.TileHeightLabel.Size = new System.Drawing.Size(190, 26);
            this.TileHeightLabel.TabIndex = 2;
            this.TileHeightLabel.Text = "Tile height";
            this.TileHeightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TileWidthLabel
            // 
            this.TileWidthLabel.AutoSize = true;
            this.TileWidthLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileWidthLabel.Location = new System.Drawing.Point(3, 460);
            this.TileWidthLabel.Margin = new System.Windows.Forms.Padding(3);
            this.TileWidthLabel.Name = "TileWidthLabel";
            this.TileWidthLabel.Size = new System.Drawing.Size(190, 26);
            this.TileWidthLabel.TabIndex = 3;
            this.TileWidthLabel.Text = "Tile width";
            this.TileWidthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TileHeightNumericUpDown
            // 
            this.TileHeightNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileHeightNumericUpDown.Location = new System.Drawing.Point(199, 492);
            this.TileHeightNumericUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.TileHeightNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.TileHeightNumericUpDown.Name = "TileHeightNumericUpDown";
            this.TileHeightNumericUpDown.Size = new System.Drawing.Size(190, 20);
            this.TileHeightNumericUpDown.TabIndex = 4;
            this.TileHeightNumericUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // TileWidthNumericUpDown
            // 
            this.TileWidthNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TileWidthNumericUpDown.Location = new System.Drawing.Point(3, 492);
            this.TileWidthNumericUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.TileWidthNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.TileWidthNumericUpDown.Name = "TileWidthNumericUpDown";
            this.TileWidthNumericUpDown.Size = new System.Drawing.Size(190, 20);
            this.TileWidthNumericUpDown.TabIndex = 5;
            this.TileWidthNumericUpDown.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // AnimationFramesLabel
            // 
            this.AnimationFramesLabel.AutoSize = true;
            this.AnimationFramesLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationFramesLabel.Location = new System.Drawing.Point(395, 460);
            this.AnimationFramesLabel.Margin = new System.Windows.Forms.Padding(3);
            this.AnimationFramesLabel.Name = "AnimationFramesLabel";
            this.AnimationFramesLabel.Size = new System.Drawing.Size(190, 26);
            this.AnimationFramesLabel.TabIndex = 6;
            this.AnimationFramesLabel.Text = "Animation frames";
            this.AnimationFramesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AnimationFramesLabelNumericUpDown
            // 
            this.AnimationFramesLabelNumericUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnimationFramesLabelNumericUpDown.Location = new System.Drawing.Point(395, 492);
            this.AnimationFramesLabelNumericUpDown.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.AnimationFramesLabelNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AnimationFramesLabelNumericUpDown.Name = "AnimationFramesLabelNumericUpDown";
            this.AnimationFramesLabelNumericUpDown.Size = new System.Drawing.Size(190, 20);
            this.AnimationFramesLabelNumericUpDown.TabIndex = 7;
            this.AnimationFramesLabelNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // LoadImageButton
            // 
            this.AnimationsTableLayoutPanel.SetColumnSpan(this.LoadImageButton, 2);
            this.LoadImageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LoadImageButton.Location = new System.Drawing.Point(3, 524);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(386, 34);
            this.LoadImageButton.TabIndex = 8;
            this.LoadImageButton.Text = "Load source tilemap";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // SaveImageButton
            // 
            this.AnimationsTableLayoutPanel.SetColumnSpan(this.SaveImageButton, 2);
            this.SaveImageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SaveImageButton.Location = new System.Drawing.Point(395, 524);
            this.SaveImageButton.Name = "SaveImageButton";
            this.SaveImageButton.Size = new System.Drawing.Size(386, 34);
            this.SaveImageButton.TabIndex = 9;
            this.SaveImageButton.Text = "Save animated tilemap";
            this.SaveImageButton.UseVisualStyleBackColor = true;
            this.SaveImageButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // CreateAnimatedTilemapButton
            // 
            this.CreateAnimatedTilemapButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CreateAnimatedTilemapButton.Location = new System.Drawing.Point(591, 460);
            this.CreateAnimatedTilemapButton.Name = "CreateAnimatedTilemapButton";
            this.AnimationsTableLayoutPanel.SetRowSpan(this.CreateAnimatedTilemapButton, 2);
            this.CreateAnimatedTilemapButton.Size = new System.Drawing.Size(190, 58);
            this.CreateAnimatedTilemapButton.TabIndex = 10;
            this.CreateAnimatedTilemapButton.Text = "Create animated tilemap";
            this.CreateAnimatedTilemapButton.UseVisualStyleBackColor = true;
            this.CreateAnimatedTilemapButton.Click += new System.EventHandler(this.ButtonClick);
            // 
            // SourceTilemapPanel
            // 
            this.SourceTilemapPanel.AutoScroll = true;
            this.SourceTilemapPanel.BackColor = System.Drawing.Color.Magenta;
            this.SourceTilemapPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnimationsTableLayoutPanel.SetColumnSpan(this.SourceTilemapPanel, 2);
            this.SourceTilemapPanel.Controls.Add(this.SourceTilemapPictureBox);
            this.SourceTilemapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceTilemapPanel.Location = new System.Drawing.Point(3, 3);
            this.SourceTilemapPanel.Name = "SourceTilemapPanel";
            this.SourceTilemapPanel.Size = new System.Drawing.Size(386, 451);
            this.SourceTilemapPanel.TabIndex = 11;
            // 
            // SourceTilemapPictureBox
            // 
            this.SourceTilemapPictureBox.BackColor = System.Drawing.Color.Black;
            this.SourceTilemapPictureBox.Location = new System.Drawing.Point(0, 0);
            this.SourceTilemapPictureBox.Name = "SourceTilemapPictureBox";
            this.SourceTilemapPictureBox.Size = new System.Drawing.Size(1, 1);
            this.SourceTilemapPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.SourceTilemapPictureBox.TabIndex = 0;
            this.SourceTilemapPictureBox.TabStop = false;
            // 
            // OutputImagePanel
            // 
            this.OutputImagePanel.AutoScroll = true;
            this.OutputImagePanel.BackColor = System.Drawing.Color.Magenta;
            this.OutputImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AnimationsTableLayoutPanel.SetColumnSpan(this.OutputImagePanel, 2);
            this.OutputImagePanel.Controls.Add(this.OutputImagePictureBox);
            this.OutputImagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutputImagePanel.Location = new System.Drawing.Point(395, 3);
            this.OutputImagePanel.Name = "OutputImagePanel";
            this.OutputImagePanel.Size = new System.Drawing.Size(386, 451);
            this.OutputImagePanel.TabIndex = 12;
            // 
            // OutputImagePictureBox
            // 
            this.OutputImagePictureBox.BackColor = System.Drawing.Color.Black;
            this.OutputImagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.OutputImagePictureBox.Name = "OutputImagePictureBox";
            this.OutputImagePictureBox.Size = new System.Drawing.Size(1, 1);
            this.OutputImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.OutputImagePictureBox.TabIndex = 0;
            this.OutputImagePictureBox.TabStop = false;
            // 
            // TilemapAnimationMakerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.AnimationsTableLayoutPanel);
            this.Name = "TilemapAnimationMakerForm";
            this.Text = "Tilemap animation maker";
            this.Load += new System.EventHandler(this.TilemapAnimationMakerForm_Load);
            this.AnimationsTableLayoutPanel.ResumeLayout(false);
            this.AnimationsTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TileHeightNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TileWidthNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AnimationFramesLabelNumericUpDown)).EndInit();
            this.SourceTilemapPanel.ResumeLayout(false);
            this.SourceTilemapPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourceTilemapPictureBox)).EndInit();
            this.OutputImagePanel.ResumeLayout(false);
            this.OutputImagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OutputImagePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel AnimationsTableLayoutPanel;
        private System.Windows.Forms.Label TileHeightLabel;
        private System.Windows.Forms.Label TileWidthLabel;
        private System.Windows.Forms.NumericUpDown TileHeightNumericUpDown;
        private System.Windows.Forms.NumericUpDown TileWidthNumericUpDown;
        private System.Windows.Forms.Label AnimationFramesLabel;
        private System.Windows.Forms.NumericUpDown AnimationFramesLabelNumericUpDown;
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.Button SaveImageButton;
        private System.Windows.Forms.Button CreateAnimatedTilemapButton;
        private System.Windows.Forms.Panel SourceTilemapPanel;
        private System.Windows.Forms.Panel OutputImagePanel;
        private System.Windows.Forms.PictureBox SourceTilemapPictureBox;
        private System.Windows.Forms.PictureBox OutputImagePictureBox;
    }
}