namespace Stego.UI {
    partial class WindowPanel {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.TopBarPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.ImagePanel = new System.Windows.Forms.Panel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.CornerResizePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.WindowWidth = new System.Windows.Forms.NumericUpDown();
            this.WindowHeight = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.TopBarPanel.SuspendLayout();
            this.ImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // TopBarPanel
            // 
            this.TopBarPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TopBarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TopBarPanel.Controls.Add(this.button2);
            this.TopBarPanel.Controls.Add(this.WindowHeight);
            this.TopBarPanel.Controls.Add(this.WindowWidth);
            this.TopBarPanel.Controls.Add(this.label1);
            this.TopBarPanel.Controls.Add(this.button1);
            this.TopBarPanel.Location = new System.Drawing.Point(0, 0);
            this.TopBarPanel.Name = "TopBarPanel";
            this.TopBarPanel.Size = new System.Drawing.Size(1170, 27);
            this.TopBarPanel.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1136, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(32, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ImagePanel
            // 
            this.ImagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePanel.Controls.Add(this.CornerResizePanel);
            this.ImagePanel.Controls.Add(this.PictureBox);
            this.ImagePanel.Location = new System.Drawing.Point(2, 30);
            this.ImagePanel.Name = "ImagePanel";
            this.ImagePanel.Size = new System.Drawing.Size(1167, 762);
            this.ImagePanel.TabIndex = 1;
            // 
            // PictureBox
            // 
            this.PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox.BackColor = System.Drawing.SystemColors.MenuText;
            this.PictureBox.Location = new System.Drawing.Point(3, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(1161, 757);
            this.PictureBox.TabIndex = 0;
            this.PictureBox.TabStop = false;
            // 
            // CornerResizePanel
            // 
            this.CornerResizePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CornerResizePanel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CornerResizePanel.Location = new System.Drawing.Point(1155, 749);
            this.CornerResizePanel.Name = "CornerResizePanel";
            this.CornerResizePanel.Size = new System.Drawing.Size(14, 14);
            this.CornerResizePanel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(180, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // WindowWidth
            // 
            this.WindowWidth.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.WindowWidth.Location = new System.Drawing.Point(5, 4);
            this.WindowWidth.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.WindowWidth.Name = "WindowWidth";
            this.WindowWidth.Size = new System.Drawing.Size(81, 20);
            this.WindowWidth.TabIndex = 4;
            this.WindowWidth.ValueChanged += new System.EventHandler(this.WindowWidth_ValueChanged);
            // 
            // WindowHeight
            // 
            this.WindowHeight.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.WindowHeight.Location = new System.Drawing.Point(92, 5);
            this.WindowHeight.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.WindowHeight.Name = "WindowHeight";
            this.WindowHeight.Size = new System.Drawing.Size(82, 20);
            this.WindowHeight.TabIndex = 5;
            this.WindowHeight.ValueChanged += new System.EventHandler(this.WindowHeight_ValueChanged);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1098, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(32, 21);
            this.button2.TabIndex = 6;
            this.button2.Text = "-----";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // WindowPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.ImagePanel);
            this.Controls.Add(this.TopBarPanel);
            this.Name = "WindowPanel";
            this.Size = new System.Drawing.Size(1171, 793);
            this.TopBarPanel.ResumeLayout(false);
            this.TopBarPanel.PerformLayout();
            this.ImagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WindowHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TopBarPanel;
        private System.Windows.Forms.Panel ImagePanel;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel CornerResizePanel;
        private System.Windows.Forms.NumericUpDown WindowHeight;
        private System.Windows.Forms.NumericUpDown WindowWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
    }
}
