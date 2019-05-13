namespace NetMonitorClient
{
    partial class Password
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Password));
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonPassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(12, 25);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(160, 20);
            this.textBoxPassword.TabIndex = 0;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(9, 8);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(91, 13);
            this.labelPassword.TabIndex = 1;
            this.labelPassword.Text = "Введите пароль:";
            // 
            // buttonPassword
            // 
            this.buttonPassword.Location = new System.Drawing.Point(97, 51);
            this.buttonPassword.Name = "buttonPassword";
            this.buttonPassword.Size = new System.Drawing.Size(75, 23);
            this.buttonPassword.TabIndex = 2;
            this.buttonPassword.Text = "Вход";
            this.buttonPassword.UseVisualStyleBackColor = true;
            // 
            // Password
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 87);
            this.Controls.Add(this.buttonPassword);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.textBoxPassword);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Password";
            this.Text = "Вход";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBoxPassword;
        public System.Windows.Forms.Label labelPassword;
        public System.Windows.Forms.Button buttonPassword;
    }
}