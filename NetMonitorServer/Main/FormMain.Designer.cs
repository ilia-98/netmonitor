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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.listViewClients = new System.Windows.Forms.ListView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьСписокИзЛокальнойСетиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.клиентыToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьСписокИзЛокальнойСетиToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
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
            this.listViewHardwareInfo = new System.Windows.Forms.ListView();
            this.columnHardwareName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHardwareValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBoxScreenMain = new System.Windows.Forms.PictureBox();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageApps = new System.Windows.Forms.TabPage();
            this.buttonGetApps = new System.Windows.Forms.Button();
            this.listViewApps = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnInstallDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnVendor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageNotify = new System.Windows.Forms.TabPage();
            this.labelTextNotifyIconType = new System.Windows.Forms.Label();
            this.comboBoxNotifyIconType = new System.Windows.Forms.ComboBox();
            this.labelTextNotifyText = new System.Windows.Forms.Label();
            this.textBoxNotifyText = new System.Windows.Forms.TextBox();
            this.textBoxNotifyTitle = new System.Windows.Forms.TextBox();
            this.labelTextNotifyTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelTextDBStatus = new System.Windows.Forms.Label();
            this.labelServerDBStatus = new System.Windows.Forms.Label();
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
            this.tabPageApps.SuspendLayout();
            this.tabPageNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewClients
            // 
            resources.ApplyResources(this.listViewClients, "listViewClients");
            this.listViewClients.Name = "listViewClients";
            this.listViewClients.UseCompatibleStateImageBehavior = false;
            this.listViewClients.View = System.Windows.Forms.View.List;
            this.listViewClients.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServerToolStripMenuItem,
            this.клиентыToolStripMenuItem,
            this.клиентыToolStripMenuItem1});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // ServerToolStripMenuItem
            // 
            this.ServerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RestartToolStripMenuItem});
            this.ServerToolStripMenuItem.Name = "ServerToolStripMenuItem";
            resources.ApplyResources(this.ServerToolStripMenuItem, "ServerToolStripMenuItem");
            // 
            // RestartToolStripMenuItem
            // 
            this.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem";
            resources.ApplyResources(this.RestartToolStripMenuItem, "RestartToolStripMenuItem");
            this.RestartToolStripMenuItem.Click += new System.EventHandler(this.RestartToolStripMenuItem_Click);
            // 
            // клиентыToolStripMenuItem
            // 
            this.клиентыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьСписокИзЛокальнойСетиToolStripMenuItem});
            this.клиентыToolStripMenuItem.Name = "клиентыToolStripMenuItem";
            resources.ApplyResources(this.клиентыToolStripMenuItem, "клиентыToolStripMenuItem");
            // 
            // обновитьСписокИзЛокальнойСетиToolStripMenuItem
            // 
            this.обновитьСписокИзЛокальнойСетиToolStripMenuItem.Name = "обновитьСписокИзЛокальнойСетиToolStripMenuItem";
            resources.ApplyResources(this.обновитьСписокИзЛокальнойСетиToolStripMenuItem, "обновитьСписокИзЛокальнойСетиToolStripMenuItem");
            this.обновитьСписокИзЛокальнойСетиToolStripMenuItem.Click += new System.EventHandler(this.ОбновитьСписокИзЛокальнойСетиToolStripMenuItem_Click);
            // 
            // клиентыToolStripMenuItem1
            // 
            this.клиентыToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.обновитьСписокИзЛокальнойСетиToolStripMenuItem1});
            this.клиентыToolStripMenuItem1.Name = "клиентыToolStripMenuItem1";
            resources.ApplyResources(this.клиентыToolStripMenuItem1, "клиентыToolStripMenuItem1");
            // 
            // обновитьСписокИзЛокальнойСетиToolStripMenuItem1
            // 
            this.обновитьСписокИзЛокальнойСетиToolStripMenuItem1.Name = "обновитьСписокИзЛокальнойСетиToolStripMenuItem1";
            resources.ApplyResources(this.обновитьСписокИзЛокальнойСетиToolStripMenuItem1, "обновитьСписокИзЛокальнойСетиToolStripMenuItem1");
            this.обновитьСписокИзЛокальнойСетиToolStripMenuItem1.Click += new System.EventHandler(this.ОбновитьСписокИзЛокальнойСетиToolStripMenuItem_Click);
            // 
            // labelTextStatus
            // 
            resources.ApplyResources(this.labelTextStatus, "labelTextStatus");
            this.labelTextStatus.Name = "labelTextStatus";
            // 
            // labelServerStatus
            // 
            resources.ApplyResources(this.labelServerStatus, "labelServerStatus");
            this.labelServerStatus.ForeColor = System.Drawing.Color.Red;
            this.labelServerStatus.Name = "labelServerStatus";
            // 
            // timer1
            // 
            this.timer1.Interval = 1300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelSelectedItem
            // 
            resources.ApplyResources(this.labelSelectedItem, "labelSelectedItem");
            this.labelSelectedItem.Name = "labelSelectedItem";
            // 
            // tabPageProcess
            // 
            this.tabPageProcess.Controls.Add(this.buttonGetProcess);
            this.tabPageProcess.Controls.Add(this.listViewProcess);
            resources.ApplyResources(this.tabPageProcess, "tabPageProcess");
            this.tabPageProcess.Name = "tabPageProcess";
            this.tabPageProcess.UseVisualStyleBackColor = true;
            // 
            // buttonGetProcess
            // 
            resources.ApplyResources(this.buttonGetProcess, "buttonGetProcess");
            this.buttonGetProcess.Name = "buttonGetProcess";
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
            resources.ApplyResources(this.listViewProcess, "listViewProcess");
            this.listViewProcess.Name = "listViewProcess";
            this.listViewProcess.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listViewProcess.UseCompatibleStateImageBehavior = false;
            this.listViewProcess.View = System.Windows.Forms.View.Details;
            this.listViewProcess.SelectedIndexChanged += new System.EventHandler(this.ListViewProcess_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderDescription
            // 
            resources.ApplyResources(this.columnHeaderDescription, "columnHeaderDescription");
            // 
            // columnHeaderCPU
            // 
            resources.ApplyResources(this.columnHeaderCPU, "columnHeaderCPU");
            // 
            // columnHeaderRAM
            // 
            resources.ApplyResources(this.columnHeaderRAM, "columnHeaderRAM");
            // 
            // buttonStartControl
            // 
            resources.ApplyResources(this.buttonStartControl, "buttonStartControl");
            this.buttonStartControl.Name = "buttonStartControl";
            this.buttonStartControl.UseVisualStyleBackColor = true;
            this.buttonStartControl.Click += new System.EventHandler(this.ButtonStartControl_Click);
            // 
            // chartMonitor
            // 
            chartArea7.AxisY.Maximum = 100D;
            chartArea7.AxisY.Minimum = 0D;
            chartArea7.Name = "TempMonitoring";
            chartArea8.AxisY.Maximum = 100D;
            chartArea8.AxisY.Minimum = 0D;
            chartArea8.Name = "LoadMonitoring";
            this.chartMonitor.ChartAreas.Add(chartArea7);
            this.chartMonitor.ChartAreas.Add(chartArea8);
            legend4.Name = "Legend";
            this.chartMonitor.Legends.Add(legend4);
            resources.ApplyResources(this.chartMonitor, "chartMonitor");
            this.chartMonitor.Name = "chartMonitor";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelFiles);
            this.tabPage4.Controls.Add(this.buttonFilesUpdate);
            resources.ApplyResources(this.tabPage4, "tabPage4");
            this.tabPage4.Name = "tabPage4";
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
            resources.ApplyResources(this.panelFiles, "panelFiles");
            this.panelFiles.Name = "panelFiles";
            // 
            // labelTextArgs
            // 
            resources.ApplyResources(this.labelTextArgs, "labelTextArgs");
            this.labelTextArgs.Name = "labelTextArgs";
            // 
            // textBoxArgs
            // 
            resources.ApplyResources(this.textBoxArgs, "textBoxArgs");
            this.textBoxArgs.Name = "textBoxArgs";
            // 
            // labelTextPath
            // 
            resources.ApplyResources(this.labelTextPath, "labelTextPath");
            this.labelTextPath.Name = "labelTextPath";
            // 
            // buttonFilesDelete
            // 
            resources.ApplyResources(this.buttonFilesDelete, "buttonFilesDelete");
            this.buttonFilesDelete.Name = "buttonFilesDelete";
            this.buttonFilesDelete.UseVisualStyleBackColor = true;
            this.buttonFilesDelete.Click += new System.EventHandler(this.ButtonFilesDelete_Click);
            // 
            // buttonFilesGetElements
            // 
            resources.ApplyResources(this.buttonFilesGetElements, "buttonFilesGetElements");
            this.buttonFilesGetElements.Name = "buttonFilesGetElements";
            this.buttonFilesGetElements.UseVisualStyleBackColor = true;
            this.buttonFilesGetElements.Click += new System.EventHandler(this.ButtonFilesGetElements_Click);
            // 
            // buttonFilesUpload
            // 
            resources.ApplyResources(this.buttonFilesUpload, "buttonFilesUpload");
            this.buttonFilesUpload.Name = "buttonFilesUpload";
            this.buttonFilesUpload.UseVisualStyleBackColor = true;
            this.buttonFilesUpload.Click += new System.EventHandler(this.ButtonFilesUpload_Click);
            // 
            // textBoxPath
            // 
            resources.ApplyResources(this.textBoxPath, "textBoxPath");
            this.textBoxPath.Name = "textBoxPath";
            // 
            // buttonFilesRun
            // 
            resources.ApplyResources(this.buttonFilesRun, "buttonFilesRun");
            this.buttonFilesRun.Name = "buttonFilesRun";
            this.buttonFilesRun.UseVisualStyleBackColor = true;
            this.buttonFilesRun.Click += new System.EventHandler(this.ButtonFilesRun_Click);
            // 
            // listBoxElements
            // 
            this.listBoxElements.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxElements, "listBoxElements");
            this.listBoxElements.Name = "listBoxElements";
            this.listBoxElements.SelectedIndexChanged += new System.EventHandler(this.ListBoxElements_SelectedIndexChanged);
            this.listBoxElements.DoubleClick += new System.EventHandler(this.ListBoxElements_DoubleClick);
            // 
            // buttonFilesGet
            // 
            resources.ApplyResources(this.buttonFilesGet, "buttonFilesGet");
            this.buttonFilesGet.Name = "buttonFilesGet";
            this.buttonFilesGet.UseVisualStyleBackColor = true;
            this.buttonFilesGet.Click += new System.EventHandler(this.ButtonFilesGet_Click);
            // 
            // buttonFilesUpdate
            // 
            resources.ApplyResources(this.buttonFilesUpdate, "buttonFilesUpdate");
            this.buttonFilesUpdate.Name = "buttonFilesUpdate";
            this.buttonFilesUpdate.UseVisualStyleBackColor = true;
            this.buttonFilesUpdate.Click += new System.EventHandler(this.ButtonFilesUpdate_Click);
            // 
            // tabPageMonitoring
            // 
            this.tabPageMonitoring.Controls.Add(this.chartMonitor);
            resources.ApplyResources(this.tabPageMonitoring, "tabPageMonitoring");
            this.tabPageMonitoring.Name = "tabPageMonitoring";
            this.tabPageMonitoring.UseVisualStyleBackColor = true;
            // 
            // tabPageInfo
            // 
            this.tabPageInfo.Controls.Add(this.listViewHardwareInfo);
            resources.ApplyResources(this.tabPageInfo, "tabPageInfo");
            this.tabPageInfo.Name = "tabPageInfo";
            this.tabPageInfo.UseVisualStyleBackColor = true;
            // 
            // listViewHardwareInfo
            // 
            this.listViewHardwareInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHardwareName,
            this.columnHardwareValue});
            resources.ApplyResources(this.listViewHardwareInfo, "listViewHardwareInfo");
            this.listViewHardwareInfo.Name = "listViewHardwareInfo";
            this.listViewHardwareInfo.UseCompatibleStateImageBehavior = false;
            this.listViewHardwareInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHardwareName
            // 
            resources.ApplyResources(this.columnHardwareName, "columnHardwareName");
            // 
            // columnHardwareValue
            // 
            resources.ApplyResources(this.columnHardwareValue, "columnHardwareValue");
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.button5);
            this.tabPageMain.Controls.Add(this.button4);
            this.tabPageMain.Controls.Add(this.button3);
            this.tabPageMain.Controls.Add(this.button2);
            this.tabPageMain.Controls.Add(this.buttonStartControl);
            this.tabPageMain.Controls.Add(this.pictureBoxScreenMain);
            resources.ApplyResources(this.tabPageMain, "tabPageMain");
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // pictureBoxScreenMain
            // 
            resources.ApplyResources(this.pictureBoxScreenMain, "pictureBoxScreenMain");
            this.pictureBoxScreenMain.Name = "pictureBoxScreenMain";
            this.pictureBoxScreenMain.TabStop = false;
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageMain);
            this.tabControlMain.Controls.Add(this.tabPageInfo);
            this.tabControlMain.Controls.Add(this.tabPageMonitoring);
            this.tabControlMain.Controls.Add(this.tabPageApps);
            this.tabControlMain.Controls.Add(this.tabPage4);
            this.tabControlMain.Controls.Add(this.tabPageProcess);
            this.tabControlMain.Controls.Add(this.tabPageNotify);
            resources.ApplyResources(this.tabControlMain, "tabControlMain");
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.SelectedIndexChanged += new System.EventHandler(this.TabControlMain_SelectedIndexChanged);
            // 
            // tabPageApps
            // 
            this.tabPageApps.Controls.Add(this.buttonGetApps);
            this.tabPageApps.Controls.Add(this.listViewApps);
            resources.ApplyResources(this.tabPageApps, "tabPageApps");
            this.tabPageApps.Name = "tabPageApps";
            this.tabPageApps.UseVisualStyleBackColor = true;
            // 
            // buttonGetApps
            // 
            resources.ApplyResources(this.buttonGetApps, "buttonGetApps");
            this.buttonGetApps.Name = "buttonGetApps";
            this.buttonGetApps.UseVisualStyleBackColor = true;
            this.buttonGetApps.Click += new System.EventHandler(this.ButtonGetApps_Click);
            // 
            // listViewApps
            // 
            this.listViewApps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnInstallDate,
            this.columnVendor,
            this.columnVersion});
            resources.ApplyResources(this.listViewApps, "listViewApps");
            this.listViewApps.Name = "listViewApps";
            this.listViewApps.UseCompatibleStateImageBehavior = false;
            this.listViewApps.View = System.Windows.Forms.View.Details;
            // 
            // columnName
            // 
            resources.ApplyResources(this.columnName, "columnName");
            // 
            // columnInstallDate
            // 
            resources.ApplyResources(this.columnInstallDate, "columnInstallDate");
            // 
            // columnVendor
            // 
            resources.ApplyResources(this.columnVendor, "columnVendor");
            // 
            // columnVersion
            // 
            resources.ApplyResources(this.columnVersion, "columnVersion");
            // 
            // tabPageNotify
            // 
            this.tabPageNotify.Controls.Add(this.labelTextNotifyIconType);
            this.tabPageNotify.Controls.Add(this.comboBoxNotifyIconType);
            this.tabPageNotify.Controls.Add(this.labelTextNotifyText);
            this.tabPageNotify.Controls.Add(this.textBoxNotifyText);
            this.tabPageNotify.Controls.Add(this.textBoxNotifyTitle);
            this.tabPageNotify.Controls.Add(this.labelTextNotifyTitle);
            this.tabPageNotify.Controls.Add(this.button1);
            resources.ApplyResources(this.tabPageNotify, "tabPageNotify");
            this.tabPageNotify.Name = "tabPageNotify";
            this.tabPageNotify.UseVisualStyleBackColor = true;
            // 
            // labelTextNotifyIconType
            // 
            resources.ApplyResources(this.labelTextNotifyIconType, "labelTextNotifyIconType");
            this.labelTextNotifyIconType.Name = "labelTextNotifyIconType";
            // 
            // comboBoxNotifyIconType
            // 
            this.comboBoxNotifyIconType.FormattingEnabled = true;
            this.comboBoxNotifyIconType.Items.AddRange(new object[] {
            ((object)(resources.GetObject("comboBoxNotifyIconType.Items"))),
            ((object)(resources.GetObject("comboBoxNotifyIconType.Items1"))),
            ((object)(resources.GetObject("comboBoxNotifyIconType.Items2")))});
            resources.ApplyResources(this.comboBoxNotifyIconType, "comboBoxNotifyIconType");
            this.comboBoxNotifyIconType.Name = "comboBoxNotifyIconType";
            // 
            // labelTextNotifyText
            // 
            resources.ApplyResources(this.labelTextNotifyText, "labelTextNotifyText");
            this.labelTextNotifyText.Name = "labelTextNotifyText";
            // 
            // textBoxNotifyText
            // 
            resources.ApplyResources(this.textBoxNotifyText, "textBoxNotifyText");
            this.textBoxNotifyText.Name = "textBoxNotifyText";
            // 
            // textBoxNotifyTitle
            // 
            resources.ApplyResources(this.textBoxNotifyTitle, "textBoxNotifyTitle");
            this.textBoxNotifyTitle.Name = "textBoxNotifyTitle";
            // 
            // labelTextNotifyTitle
            // 
            resources.ApplyResources(this.labelTextNotifyTitle, "labelTextNotifyTitle");
            this.labelTextNotifyTitle.Name = "labelTextNotifyTitle";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // labelTextDBStatus
            // 
            resources.ApplyResources(this.labelTextDBStatus, "labelTextDBStatus");
            this.labelTextDBStatus.Name = "labelTextDBStatus";
            // 
            // labelServerDBStatus
            // 
            resources.ApplyResources(this.labelServerDBStatus, "labelServerDBStatus");
            this.labelServerDBStatus.ForeColor = System.Drawing.Color.Red;
            this.labelServerDBStatus.Name = "labelServerDBStatus";
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelServerDBStatus);
            this.Controls.Add(this.labelTextDBStatus);
            this.Controls.Add(this.labelSelectedItem);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.labelServerStatus);
            this.Controls.Add(this.labelTextStatus);
            this.Controls.Add(this.listViewClients);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = global::NetMonitorServer.Properties.Resources.Main;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
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
            this.tabPageMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScreenMain)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageApps.ResumeLayout(false);
            this.tabPageNotify.ResumeLayout(false);
            this.tabPageNotify.PerformLayout();
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
        private System.Windows.Forms.TabPage tabPageNotify;
        private System.Windows.Forms.Button button1;
        private TextBox textBoxNotifyTitle;
        private Label labelTextNotifyTitle;
        private Label labelTextNotifyText;
        private TextBox textBoxNotifyText;
        private Label labelTextNotifyIconType;
        private ComboBox comboBoxNotifyIconType;
        private TabPage tabPageApps;
        public ListView listViewApps;
        private Button buttonGetApps;
        private ColumnHeader columnName;
        private ColumnHeader columnInstallDate;
        private ColumnHeader columnVendor;
        private ColumnHeader columnVersion;
        private ToolStripMenuItem клиентыToolStripMenuItem;
        private ToolStripMenuItem обновитьСписокИзЛокальнойСетиToolStripMenuItem;
        private ToolStripMenuItem клиентыToolStripMenuItem1;
        private ToolStripMenuItem обновитьСписокИзЛокальнойСетиToolStripMenuItem1;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button5;
        public ListView listViewHardwareInfo;
        private ColumnHeader columnHardwareName;
        private ColumnHeader columnHardwareValue;
    }
}

