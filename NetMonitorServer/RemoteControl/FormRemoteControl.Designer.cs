namespace NetMonitorServer
{
    partial class FormRemoteControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRemoteControl));
            this.pictureBoxScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxScreen
            // 
            this.pictureBoxScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxScreen.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxScreen.Name = "pictureBoxScreen";
            this.pictureBoxScreen.Size = new System.Drawing.Size(800, 450);
            this.pictureBoxScreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreen.TabIndex = 0;
            this.pictureBoxScreen.TabStop = false;
            this.pictureBoxScreen.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxScreen_MouseClick);
            this.pictureBoxScreen.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxScreen_MouseMove);
            // 
            // FormRemoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBoxScreen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRemoteControl";
            this.Text = "FormRemoteControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormRemoteControl_FormClosing);
            this.Load += new System.EventHandler(this.FormRemoteControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBoxScreen;
    }
}