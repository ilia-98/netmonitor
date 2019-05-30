using System.Windows.Forms;

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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.listViewClients = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTextStatus = new System.Windows.Forms.Label();
            this.labelServerStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelSelectedItem = new System.Windows.Forms.Label();
            this.tabPageProcess = new System.Windows.Forms.TabPage();
            this.buttonGetProcess = new System.Windows.Forms.Button();
            this.listViewProcess = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCPU = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderRAM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonStartControl = new System.Windows.Forms.Button();
            this.chartMonitor = new System.Windows.Forms.DataVisualization.Charting.Chart();
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
            this.tabPageMonitoring = new System.Windows.Forms.TabPage();
            this.tabPageInfo = new System.Windows.Forms.TabPage();
            this.labelHardwareInfo = new System.Windows.Forms.Label();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.pictureBoxScreenMain = new System.Windows.Forms.PictureBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.labelTextDBStatus = new System.Windows.Forms.Label();
            this.labelServerDBStatus = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.labelTextNotifyTitle = new System.Windows.Forms.Label();
            this.textBoxNotifyTitle = new System.Windows.Forms.TextBox();
            this.textBoxNotifyText = new System.Windows.Forms.TextBox();
            this.labelTextNotifyText = new System.Windows.Forms.Label();
            this.comboBoxNotifyIconType = new System.Windows.Forms.ComboBox();
            this.labelTextNotifyIconType = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.tabPageProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonitor)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.panelFiles.SuspendLayout();
            this.tabPageMonitoring.SuspendLayout();
            this.tabPageInfo.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenMain)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewClients
            // 
            this.listViewClients.Location = new System.Drawing.Point(18, 179);
            this.listViewClients.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewClients.Name = "listViewClients";
            this.listViewClients.Size = new System.Drawing.Size(577, 519);
            this.listViewClients.TabIndex = 3;
            this.listViewClients.UseCompatibleStateImageBehavior = false;
            this.listViewClients.View = System.Windows.Forms.View.SmallIcon;
            this.listViewClients.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(1706, 35);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStripMain";
            // 
            // ServerToolStripMenuItem
            // 
            this.ServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestartToolStripMenuItem});
            this.ServerToolStripMenuItem.Name = "ServerToolStripMenuItem";
            this.ServerToolStripMenuItem.Size = new System.Drawing.Size(89, 29);
            this.ServerToolStripMenuItem.Text = "Сервер";
            // 
            // RestartToolStripMenuItem
            // 
            this.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem";
            this.RestartToolStripMenuItem.Size = new System.Drawing.Size(209, 34);
            this.RestartToolStripMenuItem.Text = "Перезапуск";
            this.RestartToolStripMenuItem.Click += new System.EventHandler(this.RestartToolStripMenuItem_Click);
            // 
            // labelTextStatus
            // 
            this.labelTextStatus.AutoSize = true;
            this.labelTextStatus.Location = new System.Drawing.Point(14, 54);
            this.labelTextStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextStatus.Name = "labelTextStatus";
            this.labelTextStatus.Size = new System.Drawing.Size(205, 20);
            this.labelTextStatus.TabIndex = 5;
            this.labelTextStatus.Text = "Статус главного сервера:";
            // 
            // labelServerStatus
            // 
            this.labelServerStatus.AutoSize = true;
            this.labelServerStatus.ForeColor = System.Drawing.Color.Red;
            this.labelServerStatus.Location = new System.Drawing.Point(227, 54);
            this.labelServerStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelServerStatus.Name = "labelServerStatus";
            this.labelServerStatus.Size = new System.Drawing.Size(67, 20);
            this.labelServerStatus.TabIndex = 6;
            this.labelServerStatus.Text = "OFLINE";
            // 
            // timer1
            // 
            this.timer1.Interval = 1300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelSelectedItem
            // 
            this.labelSelectedItem.AutoSize = true;
            this.labelSelectedItem.Location = new System.Drawing.Point(14, 145);
            this.labelSelectedItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSelectedItem.Name = "labelSelectedItem";
            this.labelSelectedItem.Size = new System.Drawing.Size(71, 20);
            this.labelSelectedItem.TabIndex = 12;
            this.labelSelectedItem.Text = "Выбран:";
            // 
            // tabPageProcess
            // 
            this.tabPageProcess.Controls.Add(this.buttonGetProcess);
            this.tabPageProcess.Controls.Add(this.listViewProcess);
            this.tabPageProcess.Location = new System.Drawing.Point(4, 29);
            this.tabPageProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageProcess.Name = "tabPageProcess";
            this.tabPageProcess.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageProcess.Size = new System.Drawing.Size(1086, 625);
            this.tabPageProcess.TabIndex = 4;
            this.tabPageProcess.Text = "Процессы";
            this.tabPageProcess.UseVisualStyleBackColor = true;
            // 
            // buttonGetProcess
            // 
            this.buttonGetProcess.Location = new System.Drawing.Point(9, 9);
            this.buttonGetProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonGetProcess.Name = "buttonGetProcess";
            this.buttonGetProcess.Size = new System.Drawing.Size(112, 35);
            this.buttonGetProcess.TabIndex = 1;
            this.buttonGetProcess.Text = "Обновить";
            this.buttonGetProcess.UseVisualStyleBackColor = true;
            this.buttonGetProcess.Click += new System.EventHandler(this.ButtonGetProcess_Click);
            // 
            // listViewProcess
            // 
            this.listViewProcess.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderDescription,
            this.columnHeaderCPU,
            this.columnHeaderRAM});
            this.listViewProcess.FullRowSelect = true;
            this.listViewProcess.GridLines = true;
            this.listViewProcess.Location = new System.Drawing.Point(9, 49);
            this.listViewProcess.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listViewProcess.Name = "listViewProcess";
            this.listViewProcess.Size = new System.Drawing.Size(1062, 558);
            this.listViewProcess.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewProcess.TabIndex = 0;
            this.listViewProcess.UseCompatibleStateImageBehavior = false;
            this.listViewProcess.View = System.Windows.Forms.View.Details;
            this.listViewProcess.SelectedIndexChanged += new System.EventHandler(this.ListViewProcess_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Название";
            this.columnHeaderName.Width = 150;
            // 
            // columnHeaderDescription
            // 
            this.columnHeaderDescription.Text = "Описание";
            this.columnHeaderDescription.Width = 150;
            // 
            // columnHeaderCPU
            // 
            this.columnHeaderCPU.Text = "CPU";
            this.columnHeaderCPU.Width = 150;
            // 
            // columnHeaderRAM
            // 
            this.columnHeaderRAM.Text = "RAM";
            this.columnHeaderRAM.Width = 150;
            // 
            // buttonStartControl
            // 
            this.buttonStartControl.Location = new System.Drawing.Point(494, 555);
            this.buttonStartControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStartControl.Name = "buttonStartControl";
            this.buttonStartControl.Size = new System.Drawing.Size(146, 54);
            this.buttonStartControl.TabIndex = 11;
            this.buttonStartControl.Text = "Управление";
            this.buttonStartControl.UseVisualStyleBackColor = true;
            this.buttonStartControl.Click += new System.EventHandler(this.ButtonStartControl_Click);
            // 
            // chartMonitor
            // 
            chartArea3.AxisY.Maximum = 100D;
            chartArea3.AxisY.Minimum = 0D;
            chartArea3.Name = "TempMonitoring";
            chartArea4.AxisY.Maximum = 100D;
            chartArea4.AxisY.Minimum = 0D;
            chartArea4.Name = "LoadMonitoring";
            this.chartMonitor.ChartAreas.Add(chartArea3);
            this.chartMonitor.ChartAreas.Add(chartArea4);
            legend2.Name = "Legend";
            this.chartMonitor.Legends.Add(legend2);
            this.chartMonitor.Location = new System.Drawing.Point(9, 9);
            this.chartMonitor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chartMonitor.Name = "chartMonitor";
            this.chartMonitor.Size = new System.Drawing.Size(1064, 605);
            this.chartMonitor.TabIndex = 8;
            this.chartMonitor.Text = "chart1";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelFiles);
            this.tabPage4.Controls.Add(this.buttonFilesUpdate);
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage4.Size = new System.Drawing.Size(1086, 625);
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
            this.panelFiles.Location = new System.Drawing.Point(9, 9);
            this.panelFiles.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelFiles.Name = "panelFiles";
            this.panelFiles.Size = new System.Drawing.Size(960, 600);
            this.panelFiles.TabIndex = 11;
            // 
            // labelTextArgs
            // 
            this.labelTextArgs.AutoSize = true;
            this.labelTextArgs.Location = new System.Drawing.Point(346, 72);
            this.labelTextArgs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextArgs.Name = "labelTextArgs";
            this.labelTextArgs.Size = new System.Drawing.Size(154, 20);
            this.labelTextArgs.TabIndex = 10;
            this.labelTextArgs.Text = "Аргументы запуска";
            // 
            // textBoxArgs
            // 
            this.textBoxArgs.Location = new System.Drawing.Point(346, 97);
            this.textBoxArgs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxArgs.Name = "textBoxArgs";
            this.textBoxArgs.Size = new System.Drawing.Size(592, 26);
            this.textBoxArgs.TabIndex = 9;
            // 
            // labelTextPath
            // 
            this.labelTextPath.AutoSize = true;
            this.labelTextPath.Location = new System.Drawing.Point(4, 2);
            this.labelTextPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTextPath.Name = "labelTextPath";
            this.labelTextPath.Size = new System.Drawing.Size(42, 20);
            this.labelTextPath.TabIndex = 3;
            this.labelTextPath.Text = "Path";
            // 
            // buttonFilesDelete
            // 
            this.buttonFilesDelete.Location = new System.Drawing.Point(346, 488);
            this.buttonFilesDelete.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFilesDelete.Name = "buttonFilesDelete";
            this.buttonFilesDelete.Size = new System.Drawing.Size(105, 108);
            this.buttonFilesDelete.TabIndex = 8;
            this.buttonFilesDelete.Text = "Del";
            this.buttonFilesDelete.UseVisualStyleBackColor = true;
            this.buttonFilesDelete.Click += new System.EventHandler(this.ButtonFilesDelete_Click);
            // 
            // buttonFilesGetElements
            // 
            this.buttonFilesGetElements.Location = new System.Drawing.Point(4, 72);
            this.buttonFilesGetElements.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFilesGetElements.Name = "buttonFilesGetElements";
            this.buttonFilesGetElements.Size = new System.Drawing.Size(328, 54);
            this.buttonFilesGetElements.TabIndex = 0;
            this.buttonFilesGetElements.Text = "Получить список файлов и папок";
            this.buttonFilesGetElements.UseVisualStyleBackColor = true;
            this.buttonFilesGetElements.Click += new System.EventHandler(this.ButtonFilesGetElements_Click);
            // 
            // buttonFilesUpload
            // 
            this.buttonFilesUpload.Location = new System.Drawing.Point(346, 371);
            this.buttonFilesUpload.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFilesUpload.Name = "buttonFilesUpload";
            this.buttonFilesUpload.Size = new System.Drawing.Size(105, 108);
            this.buttonFilesUpload.TabIndex = 7;
            this.buttonFilesUpload.Text = "Add File";
            this.buttonFilesUpload.UseVisualStyleBackColor = true;
            this.buttonFilesUpload.Click += new System.EventHandler(this.ButtonFilesUpload_Click);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(4, 32);
            this.textBoxPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(934, 26);
            this.textBoxPath.TabIndex = 1;
            // 
            // buttonFilesRun
            // 
            this.buttonFilesRun.Location = new System.Drawing.Point(346, 137);
            this.buttonFilesRun.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFilesRun.Name = "buttonFilesRun";
            this.buttonFilesRun.Size = new System.Drawing.Size(105, 108);
            this.buttonFilesRun.TabIndex = 6;
            this.buttonFilesRun.Text = "Run";
            this.buttonFilesRun.UseVisualStyleBackColor = true;
            this.buttonFilesRun.Click += new System.EventHandler(this.ButtonFilesRun_Click);
            // 
            // listBoxElements
            // 
            this.listBoxElements.FormattingEnabled = true;
            this.listBoxElements.ItemHeight = 20;
            this.listBoxElements.Location = new System.Drawing.Point(9, 149);
            this.listBoxElements.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxElements.Name = "listBoxElements";
            this.listBoxElements.Size = new System.Drawing.Size(326, 444);
            this.listBoxElements.TabIndex = 2;
            this.listBoxElements.SelectedIndexChanged += new System.EventHandler(this.ListBoxElements_SelectedIndexChanged);
            this.listBoxElements.DoubleClick += new System.EventHandler(this.ListBoxElements_DoubleClick);
            // 
            // buttonFilesGet
            // 
            this.buttonFilesGet.Location = new System.Drawing.Point(346, 254);
            this.buttonFilesGet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFilesGet.Name = "buttonFilesGet";
            this.buttonFilesGet.Size = new System.Drawing.Size(105, 108);
            this.buttonFilesGet.TabIndex = 5;
            this.buttonFilesGet.Text = "Get";
            this.buttonFilesGet.UseVisualStyleBackColor = true;
            this.buttonFilesGet.Click += new System.EventHandler(this.ButtonFilesGet_Click);
            // 
            // buttonFilesUpdate
            // 
            this.buttonFilesUpdate.Location = new System.Drawing.Point(978, 42);
            this.buttonFilesUpdate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFilesUpdate.Name = "buttonFilesUpdate";
            this.buttonFilesUpdate.Size = new System.Drawing.Size(90, 92);
            this.buttonFilesUpdate.TabIndex = 10;
            this.buttonFilesUpdate.Text = "Update";
            this.buttonFilesUpdate.UseVisualStyleBackColor = true;
            this.buttonFilesUpdate.Click += new System.EventHandler(this.ButtonFilesUpdate_Click);
            // 
            // tabPageMonitoring
            // 
            this.tabPageMonitoring.Controls.Add(this.chartMonitor);
            this.tabPageMonitoring.Location = new System.Drawing.Point(4, 29);
            this.tabPageMonitoring.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMonitoring.Name = "tabPageMonitoring";
            this.tabPageMonitoring.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMonitoring.Size = new System.Drawing.Size(1086, 625);
            this.tabPageMonitoring.TabIndex = 2;
            this.tabPageMonitoring.Text = "Нагрузка";
            this.tabPageMonitoring.UseVisualStyleBackColor = true;
            this.tabPageMonitoring.Click += new System.EventHandler(this.TabPageMonitoring_Click);
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.labelHardwareInfo);
            this.tabPageInfo.Location = new System.Drawing.Point(4, 29);
            this.tabPageInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageInfo.Size = new System.Drawing.Size(1086, 625);
            this.tabPageInfo.TabIndex = 1;
            this.tabPageInfo.Text = "Информация";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // labelHardwareInfo
            // 
            this.labelHardwareInfo.AutoSize = true;
            this.labelHardwareInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.labelHardwareInfo.Location = new System.Drawing.Point(10, 11);
            this.labelHardwareInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHardwareInfo.Name = "labelHardwareInfo";
            this.labelHardwareInfo.Size = new System.Drawing.Size(0, 29);
            this.labelHardwareInfo.TabIndex = 0;
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.buttonStartControl);
            this.tabPageMain.Controls.Add(this.pictureBoxScreenMain);
            this.tabPageMain.Location = new System.Drawing.Point(4, 29);
            this.tabPageMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPageMain.Size = new System.Drawing.Size(1086, 625);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Экран";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // pictureBoxScreenMain
            // 
            this.pictureBoxScreenMain.Location = new System.Drawing.Point(54, 25);
            this.pictureBoxScreenMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBoxScreenMain.Name = "pictureBoxScreenMain";
            this.pictureBoxScreenMain.Size = new System.Drawing.Size(981, 522);
            this.pictureBoxScreenMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxScreenMain.TabIndex = 10;
            this.pictureBoxScreenMain.TabStop = false;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPageInfo);
            this.tabControlMain.Controls.Add(this.tabPageMonitoring);
            this.tabControlMain.Controls.Add(this.tabPage4);
            this.tabControlMain.Controls.Add(this.tabPageProcess);
            this.tabControlMain.Controls.Add(this.tabPage1);
            this.tabControlMain.Location = new System.Drawing.Point(606, 42);
            this.tabControlMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1094, 658);
            this.tabControlMain.TabIndex = 11;
            // 
            // labelTextDBStatus
            // 
            this.labelTextDBStatus.AutoSize = true;
            this.labelTextDBStatus.Location = new System.Drawing.Point(14, 83);
            this.labelTextDBStatus.Name = "labelTextDBStatus";
            this.labelTextDBStatus.Size = new System.Drawing.Size(159, 20);
            this.labelTextDBStatus.TabIndex = 13;
            this.labelTextDBStatus.Text = "Статус сервера БД:";
            // 
            // labelServerDBStatus
            // 
            this.labelServerDBStatus.AutoSize = true;
            this.labelServerDBStatus.ForeColor = System.Drawing.Color.Red;
            this.labelServerDBStatus.Location = new System.Drawing.Point(180, 83);
            this.labelServerDBStatus.Name = "labelServerDBStatus";
            this.labelServerDBStatus.Size = new System.Drawing.Size(67, 20);
            this.labelServerDBStatus.TabIndex = 14;
            this.labelServerDBStatus.Text = "OFLINE";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.labelTextNotifyIconType);
            this.tabPage1.Controls.Add(this.comboBoxNotifyIconType);
            this.tabPage1.Controls.Add(this.labelTextNotifyText);
            this.tabPage1.Controls.Add(this.textBoxNotifyText);
            this.tabPage1.Controls.Add(this.textBoxNotifyTitle);
            this.tabPage1.Controls.Add(this.labelTextNotifyTitle);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1086, 625);
            this.tabPage1.TabIndex = 5;
            this.tabPage1.Text = "Уведомление";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(156, 576);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 43);
            this.button1.TabIndex = 0;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // labelTextNotifyTitle
            // 
            this.labelTextNotifyTitle.AutoSize = true;
            this.labelTextNotifyTitle.Location = new System.Drawing.Point(7, 12);
            this.labelTextNotifyTitle.Name = "labelTextNotifyTitle";
            this.labelTextNotifyTitle.Size = new System.Drawing.Size(90, 20);
            this.labelTextNotifyTitle.TabIndex = 1;
            this.labelTextNotifyTitle.Text = "Заголовок";
            // 
            // textBoxNotifyTitle
            // 
            this.textBoxNotifyTitle.Location = new System.Drawing.Point(11, 35);
            this.textBoxNotifyTitle.Name = "textBoxNotifyTitle";
            this.textBoxNotifyTitle.Size = new System.Drawing.Size(422, 26);
            this.textBoxNotifyTitle.TabIndex = 2;
            // 
            // textBoxNotifyText
            // 
            this.textBoxNotifyText.Location = new System.Drawing.Point(11, 98);
            this.textBoxNotifyText.Multiline = true;
            this.textBoxNotifyText.Name = "textBoxNotifyText";
            this.textBoxNotifyText.Size = new System.Drawing.Size(422, 258);
            this.textBoxNotifyText.TabIndex = 3;
            // 
            // labelTextNotifyText
            // 
            this.labelTextNotifyText.AutoSize = true;
            this.labelTextNotifyText.Location = new System.Drawing.Point(7, 75);
            this.labelTextNotifyText.Name = "labelTextNotifyText";
            this.labelTextNotifyText.Size = new System.Drawing.Size(52, 20);
            this.labelTextNotifyText.TabIndex = 4;
            this.labelTextNotifyText.Text = "Текст";
            // 
            // comboBoxNotifyIconType
            // 
            this.comboBoxNotifyIconType.FormattingEnabled = true;
            this.comboBoxNotifyIconType.Location = new System.Drawing.Point(11, 404);
            this.comboBoxNotifyIconType.Name = "comboBoxNotifyIconType";
            this.comboBoxNotifyIconType.Size = new System.Drawing.Size(422, 28);
            this.comboBoxNotifyIconType.TabIndex = 5;
            this.comboBoxNotifyIconType.Items.Add(ToolTipIcon.Info);
            this.comboBoxNotifyIconType.Items.Add(ToolTipIcon.Warning);
            this.comboBoxNotifyIconType.Items.Add(ToolTipIcon.Error);
            this.comboBoxNotifyIconType.SelectedIndex = 0;
            // 
            // labelTextNotifyIconType
            // 
            this.labelTextNotifyIconType.AutoSize = true;
            this.labelTextNotifyIconType.Location = new System.Drawing.Point(11, 378);
            this.labelTextNotifyIconType.Name = "labelTextNotifyIconType";
            this.labelTextNotifyIconType.Size = new System.Drawing.Size(92, 20);
            this.labelTextNotifyIconType.TabIndex = 6;
            this.labelTextNotifyIconType.Text = "Тип иконки";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1706, 714);
            this.Controls.Add(this.labelServerDBStatus);
            this.Controls.Add(this.labelTextDBStatus);
            this.Controls.Add(this.labelSelectedItem);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.labelServerStatus);
            this.Controls.Add(this.labelTextStatus);
            this.Controls.Add(this.listViewClients);
            this.Controls.Add(this.menuStrip1);
            this.Icon = global::NetMonitorServer.Properties.Resources.Main;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormMain";
            this.Text = "NetMonitor Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPageProcess.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMonitor)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.panelFiles.ResumeLayout(false);
            this.panelFiles.PerformLayout();
            this.tabPageMonitoring.ResumeLayout(false);
            this.tabPageInfo.ResumeLayout(false);
            this.tabPageInfo.PerformLayout();
            this.tabPageMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenMain)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelSelectedItem;
        private System.Windows.Forms.TabPage tabPageProcess;
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
        private System.Windows.Forms.TabPage tabPageMonitoring;
        private System.Windows.Forms.TabPage tabPageInfo;
        public System.Windows.Forms.Label labelHardwareInfo;
        private System.Windows.Forms.TabPage tabPageMain;
        private System.Windows.Forms.Button buttonStartControl;
        public System.Windows.Forms.PictureBox pictureBoxScreenMain;
        private System.Windows.Forms.TabControl tabControlMain;
        public System.Windows.Forms.ListView listViewProcess;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderDescription;
        private System.Windows.Forms.ColumnHeader columnHeaderCPU;
        private System.Windows.Forms.ColumnHeader columnHeaderRAM;
        private System.Windows.Forms.Button buttonGetProcess;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartMonitor;
        private System.Windows.Forms.Label labelTextDBStatus;
        private System.Windows.Forms.Label labelServerDBStatus;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxNotifyTitle;
        private System.Windows.Forms.Label labelTextNotifyTitle;
        private System.Windows.Forms.Label labelTextNotifyText;
        private System.Windows.Forms.TextBox textBoxNotifyText;
        private System.Windows.Forms.Label labelTextNotifyIconType;
        private System.Windows.Forms.ComboBox comboBoxNotifyIconType;
    }
}

