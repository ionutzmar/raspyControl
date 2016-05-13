namespace RaspberryControl
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.gpioImage = new System.Windows.Forms.PictureBox();
            this.ipAddressTextBox = new System.Windows.Forms.TextBox();
            this.ipAddressLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.connectButon = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gpioImage)).BeginInit();
            this.SuspendLayout();
            // 
            // gpioImage
            // 
            this.gpioImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gpioImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gpioImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.gpioImage.Image = ((System.Drawing.Image)(resources.GetObject("gpioImage.Image")));
            this.gpioImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("gpioImage.InitialImage")));
            this.gpioImage.Location = new System.Drawing.Point(493, 37);
            this.gpioImage.Name = "gpioImage";
            this.gpioImage.Size = new System.Drawing.Size(125, 493);
            this.gpioImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gpioImage.TabIndex = 1;
            this.gpioImage.TabStop = false;
            // 
            // ipAddressTextBox
            // 
            this.ipAddressTextBox.Location = new System.Drawing.Point(895, 55);
            this.ipAddressTextBox.Name = "ipAddressTextBox";
            this.ipAddressTextBox.Size = new System.Drawing.Size(100, 20);
            this.ipAddressTextBox.TabIndex = 2;
            // 
            // ipAddressLabel
            // 
            this.ipAddressLabel.AutoSize = true;
            this.ipAddressLabel.Location = new System.Drawing.Point(826, 58);
            this.ipAddressLabel.Name = "ipAddressLabel";
            this.ipAddressLabel.Size = new System.Drawing.Size(63, 13);
            this.ipAddressLabel.TabIndex = 3;
            this.ipAddressLabel.Text = "IP address: ";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(849, 98);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 13);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = "Status:";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(895, 98);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(73, 13);
            this.status.TabIndex = 5;
            this.status.Text = "Disconnected";
            // 
            // connectButon
            // 
            this.connectButon.Location = new System.Drawing.Point(868, 136);
            this.connectButon.Name = "connectButon";
            this.connectButon.Size = new System.Drawing.Size(97, 50);
            this.connectButon.TabIndex = 6;
            this.connectButon.Text = "Connect";
            this.connectButon.UseVisualStyleBackColor = true;
            this.connectButon.Click += new System.EventHandler(this.connectButon_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 611);
            this.Controls.Add(this.connectButon);
            this.Controls.Add(this.status);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.ipAddressLabel);
            this.Controls.Add(this.ipAddressTextBox);
            this.Controls.Add(this.gpioImage);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "mainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.SizeChanged += new System.EventHandler(this.mainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.gpioImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox gpioImage;
        private System.Windows.Forms.TextBox ipAddressTextBox;
        private System.Windows.Forms.Label ipAddressLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button connectButon;
    }
}

