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
            this.button1 = new System.Windows.Forms.Button();
            this.gpioImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gpioImage)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(25, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(69, 56);
            this.button1.TabIndex = 0;
            this.button1.Text = "Magic";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gpioImage
            // 
            this.gpioImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gpioImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gpioImage.Cursor = System.Windows.Forms.Cursors.Default;
            this.gpioImage.Image = ((System.Drawing.Image)(resources.GetObject("gpioImage.Image")));
            this.gpioImage.InitialImage = ((System.Drawing.Image)(resources.GetObject("gpioImage.InitialImage")));
            this.gpioImage.Location = new System.Drawing.Point(493, 12);
            this.gpioImage.Name = "gpioImage";
            this.gpioImage.Size = new System.Drawing.Size(125, 493);
            this.gpioImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gpioImage.TabIndex = 1;
            this.gpioImage.TabStop = false;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 561);
            this.Controls.Add(this.gpioImage);
            this.Controls.Add(this.button1);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "mainForm";
            this.Text = "Form1";
            this.SizeChanged += new System.EventHandler(this.mainForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.gpioImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox gpioImage;
    }
}

