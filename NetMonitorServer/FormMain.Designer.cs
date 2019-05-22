namespace NetMonitorServer
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listViewClients = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTextStatus = new System.Windows.Forms.Label();
            this.labelServerStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxScreenMain = new System.Windows.Forms.PictureBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelHardwareInfo = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panelFiles = new System.Windows.Forms.Panel();
            this.labelTextArgs = new System.Windows.Forms.Label();
            this.textBoxArgs = new System.Windows.Forms.TextBox();
            this.labelTextPath = new System.Windows.Forms.Label();
            this.buttonFilesDelete = new System.Windows.Forms.Button();
            this.buttonFilesGetElements = new System.Windows.Forms.Button();
            this.buttonFilesUpload = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.buttonFilesRun = new System.Windows.Forms.Button();
            this.listBoxElements = new System.Windows.Forms.ListBox();
            this.buttonFilesGet = new System.Windows.Forms.Button();
            this.buttonFilesUpdate = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelSelectedItem = new System.Windows.Forms.Label();
            this.buttonStartControl = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenMain)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panelFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewClients
            // 
            this.listViewClients.Location = new System.Drawing.Point(12, 96);
            this.listViewClients.Name = "listViewClients";
            this.listViewClients.Size = new System.Drawing.Size(386, 359);
            this.listViewClients.TabIndex = 3;
            this.listViewClients.UseCompatibleStateImageBehavior = false;
            this.listViewClients.View = System.Windows.Forms.View.SmallIcon;
            this.listViewClients.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1137, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStripMain";
            // 
            // ServerToolStripMenuItem
            // 
            this.ServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestartToolStripMenuItem});
            this.ServerToolStripMenuItem.Name = "ServerToolStripMenuItem";
            this.ServerToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.ServerToolStripMenuItem.Text = "Сервер";
            // 
            // RestartToolStripMenuItem
            // 
            this.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem";
            this.RestartToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.RestartToolStripMenuItem.Text = "Перезапуск";
            this.RestartToolStripMenuItem.Click += new System.EventHandler(this.RestartToolStripMenuItem_Click);
            // 
            // labelTextStatus
            // 
            this.labelTextStatus.AutoSize = true;
            this.labelTextStatus.Location = new System.Drawing.Point(9, 35);
            this.labelTextStatus.Name = "labelTextStatus";
            this.labelTextStatus.Size = new System.Drawing.Size(89, 13);
            this.labelTextStatus.TabIndex = 5;
            this.labelTextStatus.Text = "Статус сервера:";
            // 
            // labelServerStatus
            // 
            this.labelServerStatus.AutoSize = true;
            this.labelServerStatus.ForeColor = System.Drawing.Color.Red;
            this.labelServerStatus.Location = new System.Drawing.Point(104, 35);
            this.labelServerStatus.Name = "labelServerStatus";
            this.labelServerStatus.Size = new System.Drawing.Size(45, 13);
            this.labelServerStatus.TabIndex = 6;
            this.labelServerStatus.Text = "OFLINE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(6, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 7;
            // 
            // pictureBoxScreenMain
            // 
            this.pictureBoxScreenMain.Location = new System.Drawing.Point(36, 16);
            this.pictureBoxScreenMain.Name = "pictureBoxScreenMain";
            this.pictureBoxScreenMain.Size = new System.Drawing.Size(654, 339);
            this.pictureBoxScreenMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreenMain.TabIndex = 10;
            this.pictureBoxScreenMain.TabStop = false;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Controls.Add(this.tabPage2);
            this.tabControlMain.Controls.Add(this.tabPage3);
            this.tabControlMain.Controls.Add(this.tabPage4);
            this.tabControlMain.Location = new System.Drawing.Point(404, 27);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(729, 428);
            this.tabControlMain.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonStartControl);
            this.tabPage1.Controls.Add(this.pictureBoxScreenMain);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(721, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Экран";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labelHardwareInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(721, 402);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Информация";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelHardwareInfo
            // 
            this.labelHardwareInfo.AutoSize = true;
            this.labelHardwareInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelHardwareInfo.Location = new System.Drawing.Point(7, 7);
            this.labelHardwareInfo.Name = "labelHardwareInfo";
            this.labelHardwareInfo.Size = new System.Drawing.Size(0, 20);
            this.labelHardwareInfo.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(721, 402);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Нагрузка";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelFiles);
            this.tabPage4.Controls.Add(this.buttonFilesUpdate);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(721, 402);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Файлы";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panelFiles
            // 
            this.panelFiles.Controls.Add(this.labelTextArgs);
            this.panelFiles.Controls.Add(this.textBoxArgs);
            this.panelFiles.Controls.Add(this.labelTextPath);
            this.panelFiles.Controls.Add(this.buttonFilesDelete);
            this.panelFiles.Controls.Add(this.buttonFilesGetElements);
            this.panelFiles.Controls.Add(this.buttonFilesUpload);
            this.panelFiles.Controls.Add(this.textBoxPath);
            this.panelFiles.Controls.Add(this.buttonFilesRun);
            this.panelFiles.Controls.Add(this.listBoxElements);
            this.panelFiles.Controls.Add(this.buttonFilesGet);
            this.panelFiles.Enabled = false;
            this.panelFiles.Location = new System.Drawing.Point(6, 6);
            this.panelFiles.Name = "panelFiles";
            this.panelFiles.Size = new System.Drawing.Size(640, 390);
            this.panelFiles.TabIndex = 11;
            // 
            // labelTextArgs
            // 
            this.labelTextArgs.AutoSize = true;
            this.labelTextArgs.Location = new System.Drawing.Point(231, 47);
            this.labelTextArgs.Name = "labelTextArgs";
            this.labelTextArgs.Size = new System.Drawing.Size(107, 13);
            this.labelTextArgs.TabIndex = 10;
            this.labelTextArgs.Text = "Аргументы запуска";
            // 
            // textBoxArgs
            // 
            this.textBoxArgs.Location = new System.Drawing.Point(231, 63);
            this.textBoxArgs.Name = "textBoxArgs";
            this.textBoxArgs.Size = new System.Drawing.Size(396, 20);
            this.textBoxArgs.TabIndex = 9;
            // 
            // labelTextPath
            // 
            this.labelTextPath.AutoSize = true;
            this.labelTextPath.Location = new System.Drawing.Point(3, 1);
            this.labelTextPath.Name = "labelTextPath";
            this.labelTextPath.Size = new System.Drawing.Size(29, 13);
            this.labelTextPath.TabIndex = 3;
            this.labelTextPath.Text = "Path";
            // 
            // buttonFilesDelete
            // 
            this.buttonFilesDelete.Location = new System.Drawing.Point(231, 317);
            this.buttonFilesDelete.Name = "buttonFilesDelete";
            this.buttonFilesDelete.Size = new System.Drawing.Size(70, 70);
            this.buttonFilesDelete.TabIndex = 8;
            this.buttonFilesDelete.Text = "Del";
            this.buttonFilesDelete.UseVisualStyleBackColor = true;
            this.buttonFilesDelete.Click += new System.EventHandler(this.ButtonFilesDelete_Click);
            // 
            // buttonFilesGetElements
            // 
            this.buttonFilesGetElements.Location = new System.Drawing.Point(3, 47);
            this.buttonFilesGetElements.Name = "buttonFilesGetElements";
            this.buttonFilesGetElements.Size = new System.Drawing.Size(219, 35);
            this.buttonFilesGetElements.TabIndex = 0;
            this.buttonFilesGetElements.Text = "Получить список файлов и папок";
            this.buttonFilesGetElements.UseVisualStyleBackColor = true;
            this.buttonFilesGetElements.Click += new System.EventHandler(this.ButtonFilesGetElements_Click);
            // 
            // buttonFilesUpload
            // 
            this.buttonFilesUpload.Location = new System.Drawing.Point(231, 241);
            this.buttonFilesUpload.Name = "buttonFilesUpload";
            this.buttonFilesUpload.Size = new System.Drawing.Size(70, 70);
            this.buttonFilesUpload.TabIndex = 7;
            this.buttonFilesUpload.Text = "Add File";
            this.buttonFilesUpload.UseVisualStyleBackColor = true;
            this.buttonFilesUpload.Click += new System.EventHandler(this.ButtonFilesUpload_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(3, 21);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(624, 20);
            this.textBoxPath.TabIndex = 1;
            // 
            // buttonFilesRun
            // 
            this.buttonFilesRun.Location = new System.Drawing.Point(231, 89);
            this.buttonFilesRun.Name = "buttonFilesRun";
            this.buttonFilesRun.Size = new System.Drawing.Size(70, 70);
            this.buttonFilesRun.TabIndex = 6;
            this.buttonFilesRun.Text = "Run";
            this.buttonFilesRun.UseVisualStyleBackColor = true;
            this.buttonFilesRun.Click += new System.EventHandler(this.ButtonFilesRun_Click);
            // 
            // listBoxElements
            // 
            this.listBoxElements.FormattingEnabled = true;
            this.listBoxElements.Location = new System.Drawing.Point(6, 97);
            this.listBoxElements.Name = "listBoxElements";
            this.listBoxElements.Size = new System.Drawing.Size(219, 290);
            this.listBoxElements.TabIndex = 2;
            this.listBoxElements.SelectedIndexChanged += new System.EventHandler(this.ListBoxElements_SelectedIndexChanged);
            this.listBoxElements.DoubleClick += new System.EventHandler(this.ListBoxElements_DoubleClick);
            // 
            // buttonFilesGet
            // 
            this.buttonFilesGet.Location = new System.Drawing.Point(231, 165);
            this.buttonFilesGet.Name = "buttonFilesGet";
            this.buttonFilesGet.Size = new System.Drawing.Size(70, 70);
            this.buttonFilesGet.TabIndex = 5;
            this.buttonFilesGet.Text = "Get";
            this.buttonFilesGet.UseVisualStyleBackColor = true;
            this.buttonFilesGet.Click += new System.EventHandler(this.ButtonFilesGet_Click);
            // 
            // buttonFilesUpdate
            // 
            this.buttonFilesUpdate.Location = new System.Drawing.Point(652, 27);
            this.buttonFilesUpdate.Name = "buttonFilesUpdate";
            this.buttonFilesUpdate.Size = new System.Drawing.Size(60, 60);
            this.buttonFilesUpdate.TabIndex = 10;
            this.buttonFilesUpdate.Text = "Update";
            this.buttonFilesUpdate.UseVisualStyleBackColor = true;
            this.buttonFilesUpdate.Click += new System.EventHandler(this.ButtonFilesUpdate_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelSelectedItem
            // 
            this.labelSelectedItem.AutoSize = true;
            this.labelSelectedItem.Location = new System.Drawing.Point(9, 65);
            this.labelSelectedItem.Name = "labelSelectedItem";
            this.labelSelectedItem.Size = new System.Drawing.Size(49, 13);
            this.labelSelectedItem.TabIndex = 12;
            this.labelSelectedItem.Text = "Выбран:";
            // 
            // buttonStartControl
            // 
            this.buttonStartControl.Location = new System.Drawing.Point(329, 361);
            this.buttonStartControl.Name = "buttonStartControl";
            this.buttonStartControl.Size = new System.Drawing.Size(97, 35);
            this.buttonStartControl.TabIndex = 11;
            this.buttonStartControl.Text = "Управление";
            this.buttonStartControl.UseVisualStyleBackColor = true;
            this.buttonStartControl.Click += new System.EventHandler(this.ButtonStartControl_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 464);
            this.Controls.Add(this.labelSelectedItem);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.labelServerStatus);
            this.Controls.Add(this.labelTextStatus);
            this.Controls.Add(this.listViewClients);
            this.Controls.Add(this.menuStrip1);
            this.Icon = global::NetMonitorServer.Properties.Resources.Main;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "NetMonitor Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenMain)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.panelFiles.ResumeLayout(false);
            this.panelFiles.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ListView listViewClients;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ServerToolStripMenuItem;
        private System.Windows.Forms.Label labelTextStatus;
        private System.Windows.Forms.Label labelServerStatus;
        private System.Windows.Forms.ToolStripMenuItem RestartToolStripMenuItem;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox pictureBoxScreenMain;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelSelectedItem;
        public System.Windows.Forms.Label labelHardwareInfo;
        private System.Windows.Forms.TabPage tabPage4;
        public System.Windows.Forms.Panel panelFiles;
        private System.Windows.Forms.Label labelTextArgs;
        private System.Windows.Forms.TextBox textBoxArgs;
        private System.Windows.Forms.Label labelTextPath;
        private System.Windows.Forms.Button buttonFilesDelete;
        private System.Windows.Forms.Button buttonFilesGetElements;
        private System.Windows.Forms.Button buttonFilesUpload;
        public System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Button buttonFilesRun;
        public System.Windows.Forms.ListBox listBoxElements;
        private System.Windows.Forms.Button buttonFilesGet;
        private System.Windows.Forms.Button buttonFilesUpdate;
        private System.Windows.Forms.Button buttonStartControl;
    }
}

