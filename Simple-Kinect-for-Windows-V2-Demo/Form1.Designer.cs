namespace Simple_Kinect_for_Windows_V2_Demo
{
    partial class Form1
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
            this.previewPictureBox = new System.Windows.Forms.PictureBox();
            this.colorImageButton = new System.Windows.Forms.Button();
            this.depthImageButton = new System.Windows.Forms.Button();
            this.irImageButton = new System.Windows.Forms.Button();
            this.snapshotButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // previewPictureBox
            // 
            this.previewPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewPictureBox.Location = new System.Drawing.Point(12, 12);
            this.previewPictureBox.Name = "previewPictureBox";
            this.previewPictureBox.Size = new System.Drawing.Size(800, 600);
            this.previewPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.previewPictureBox.TabIndex = 0;
            this.previewPictureBox.TabStop = false;
            // 
            // colorImageButton
            // 
            this.colorImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.colorImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colorImageButton.Location = new System.Drawing.Point(818, 12);
            this.colorImageButton.Name = "colorImageButton";
            this.colorImageButton.Size = new System.Drawing.Size(104, 78);
            this.colorImageButton.TabIndex = 1;
            this.colorImageButton.Text = "Color";
            this.colorImageButton.UseVisualStyleBackColor = true;
            this.colorImageButton.Click += new System.EventHandler(this.colorImageButton_Click);
            // 
            // depthImageButton
            // 
            this.depthImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.depthImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.depthImageButton.Location = new System.Drawing.Point(818, 96);
            this.depthImageButton.Name = "depthImageButton";
            this.depthImageButton.Size = new System.Drawing.Size(104, 78);
            this.depthImageButton.TabIndex = 2;
            this.depthImageButton.Text = "Depth";
            this.depthImageButton.UseVisualStyleBackColor = true;
            this.depthImageButton.Click += new System.EventHandler(this.depthImageButton_Click);
            // 
            // irImageButton
            // 
            this.irImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.irImageButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.irImageButton.Location = new System.Drawing.Point(818, 180);
            this.irImageButton.Name = "irImageButton";
            this.irImageButton.Size = new System.Drawing.Size(104, 78);
            this.irImageButton.TabIndex = 3;
            this.irImageButton.Text = "IR";
            this.irImageButton.UseVisualStyleBackColor = true;
            this.irImageButton.Click += new System.EventHandler(this.irImageButton_Click);
            // 
            // snapshotButton
            // 
            this.snapshotButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.snapshotButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.snapshotButton.Location = new System.Drawing.Point(818, 534);
            this.snapshotButton.Name = "snapshotButton";
            this.snapshotButton.Size = new System.Drawing.Size(104, 78);
            this.snapshotButton.TabIndex = 4;
            this.snapshotButton.Text = "Take Picture";
            this.snapshotButton.UseVisualStyleBackColor = true;
            this.snapshotButton.Click += new System.EventHandler(this.snapshotButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 636);
            this.Controls.Add(this.snapshotButton);
            this.Controls.Add(this.irImageButton);
            this.Controls.Add(this.depthImageButton);
            this.Controls.Add(this.colorImageButton);
            this.Controls.Add(this.previewPictureBox);
            this.Name = "Form1";
            this.Text = "Simple Kinect for Windows V2 Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.previewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox previewPictureBox;
        private System.Windows.Forms.Button colorImageButton;
        private System.Windows.Forms.Button depthImageButton;
        private System.Windows.Forms.Button irImageButton;
        private System.Windows.Forms.Button snapshotButton;
    }
}

