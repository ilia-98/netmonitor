namespace NetMonitorClient
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.labelIP = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.buttonChangePassword = new System.Windows.Forms.Button();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.checkBoxEmailSend = new System.Windows.Forms.CheckBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(12, 9);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(98, 13);
            this.labelIP.TabIndex = 0;
            this.labelIP.Text = "IP адрес сервера:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(15, 25);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(238, 20);
            this.textBoxIP.TabIndex = 1;
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(182, 163);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 2;
            this.buttonSubmit.Text = "Сохранить";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            // 
            // buttonChangePassword
            // 
            this.buttonChangePassword.Location = new System.Drawing.Point(68, 163);
            this.buttonChangePassword.Name = "buttonChangePassword";
            this.buttonChangePassword.Size = new System.Drawing.Size(108, 23);
            this.buttonChangePassword.TabIndex = 3;
            this.buttonChangePassword.Text = "Сменить пароль";
            this.buttonChangePassword.UseVisualStyleBackColor = true;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Enabled = false;
            this.textBoxEmail.Location = new System.Drawing.Point(15, 115);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(238, 20);
            this.textBoxEmail.TabIndex = 4;
            // 
            // checkBoxEmailSend
            // 
            this.checkBoxEmailSend.AutoSize = true;
            this.checkBoxEmailSend.Location = new System.Drawing.Point(15, 68);
            this.checkBoxEmailSend.Name = "checkBoxEmailSend";
            this.checkBoxEmailSend.Size = new System.Drawing.Size(190, 17);
            this.checkBoxEmailSend.TabIndex = 5;
            this.checkBoxEmailSend.Text = "Отправка уведомлений на почту";
            this.checkBoxEmailSend.UseVisualStyleBackColor = true;
            this.checkBoxEmailSend.CheckedChanged += new System.EventHandler(this.checkBoxEmailSend_CheckedChanged);
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(12, 98);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(68, 13);
            this.labelEmail.TabIndex = 6;
            this.labelEmail.Text = "Email адрес:";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(269, 198);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.checkBoxEmailSend);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.buttonChangePassword);
            this.Controls.Add(this.buttonSubmit);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.labelIP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Настройки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelIP;
        public System.Windows.Forms.TextBox textBoxIP;
        public System.Windows.Forms.Button buttonSubmit;
        public System.Windows.Forms.Button buttonChangePassword;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.CheckBox checkBoxEmailSend;
        private System.Windows.Forms.Label labelEmail;
    }
}