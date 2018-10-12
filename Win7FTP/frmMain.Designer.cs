using System.Windows.Forms;
namespace Win7FTP
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mMain = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.gbCommands = new System.Windows.Forms.GroupBox();
            this.lstMessages = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbFileSystem = new System.Windows.Forms.GroupBox();
            this.tvFileSystem = new Raccoom.Windows.Forms.TreeViewFolderBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtLocalFolderName = new System.Windows.Forms.ToolStripTextBox();
            this.gbRemoteSite = new System.Windows.Forms.GroupBox();
            this.lstRemoteSiteFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.txtRemoteDirectory = new System.Windows.Forms.ToolStripTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnNewDir = new System.Windows.Forms.ToolStripButton();
            this.btnRename = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnUpload = new System.Windows.Forms.ToolStripButton();
            this.btnDownload = new System.Windows.Forms.ToolStripButton();
            this.btnRemoteBack = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.btnOpenDialog = new System.Windows.Forms.ToolStripButton();
            this.btnGo = new System.Windows.Forms.ToolStripButton();
            this.gbCommands.SuspendLayout();
            this.gbFileSystem.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.gbRemoteSite.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mMain
            // 
            this.mMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem2,
            this.menuItem3,
            this.menuItem4,
            this.menuItem5,
            this.menuItem6});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.Text = "File";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Edit";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "View";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 3;
            this.menuItem4.Text = "Server";
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "Bookmarks";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 5;
            this.menuItem6.Text = "Help";
            // 
            // gbCommands
            // 
            this.gbCommands.Controls.Add(this.lstMessages);
            this.gbCommands.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbCommands.Location = new System.Drawing.Point(0, 313);
            this.gbCommands.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbCommands.Name = "gbCommands";
            this.gbCommands.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbCommands.Size = new System.Drawing.Size(1000, 162);
            this.gbCommands.TabIndex = 3;
            this.gbCommands.TabStop = false;
            this.gbCommands.Text = "FTP Server Messages";
            // 
            // lstMessages
            // 
            this.lstMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11});
            this.lstMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstMessages.Location = new System.Drawing.Point(3, 19);
            this.lstMessages.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(994, 139);
            this.lstMessages.TabIndex = 0;
            this.lstMessages.UseCompatibleStateImageBehavior = false;
            this.lstMessages.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Date";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Message Type";
            this.columnHeader8.Width = 120;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Message";
            this.columnHeader9.Width = 200;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Message Code";
            this.columnHeader10.Width = 150;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Current Directory";
            this.columnHeader11.Width = 200;
            // 
            // gbFileSystem
            // 
            this.gbFileSystem.Controls.Add(this.tvFileSystem);
            this.gbFileSystem.Controls.Add(this.toolStrip1);
            this.gbFileSystem.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbFileSystem.Location = new System.Drawing.Point(376, 0);
            this.gbFileSystem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbFileSystem.Name = "gbFileSystem";
            this.gbFileSystem.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbFileSystem.Size = new System.Drawing.Size(624, 313);
            this.gbFileSystem.TabIndex = 5;
            this.gbFileSystem.TabStop = false;
            this.gbFileSystem.Text = "Local Filesystem";
            // 
            // tvFileSystem
            // 
            this.tvFileSystem.AllowDrop = true;
            this.tvFileSystem.CheckboxBehaviorMode = Raccoom.Windows.Forms.CheckboxBehaviorMode.None;
            this.tvFileSystem.DataSource = null;
            this.tvFileSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvFileSystem.DriveTypes = ((Raccoom.Windows.Forms.DriveTypes)((Raccoom.Windows.Forms.DriveTypes.RemovableDisk | Raccoom.Windows.Forms.DriveTypes.LocalDisk)));
            this.tvFileSystem.HideSelection = false;
            this.tvFileSystem.Location = new System.Drawing.Point(3, 46);
            this.tvFileSystem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvFileSystem.Name = "tvFileSystem";
            this.tvFileSystem.SelectedDirectories = ((System.Collections.Specialized.StringCollection)(resources.GetObject("tvFileSystem.SelectedDirectories")));
            this.tvFileSystem.Size = new System.Drawing.Size(618, 263);
            this.tvFileSystem.TabIndex = 1;
            this.tvFileSystem.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvFileSystem_DragDrop);
            this.tvFileSystem.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvFileSystem_DragEnter);
            this.tvFileSystem.DragOver += new System.Windows.Forms.DragEventHandler(this.tvFileSystem_DragOver);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenDialog,
            this.toolStripSeparator1,
            this.txtLocalFolderName,
            this.btnGo});
            this.toolStrip1.Location = new System.Drawing.Point(3, 19);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(618, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // txtLocalFolderName
            // 
            this.txtLocalFolderName.Name = "txtLocalFolderName";
            this.txtLocalFolderName.Size = new System.Drawing.Size(524, 27);
            // 
            // gbRemoteSite
            // 
            this.gbRemoteSite.Controls.Add(this.lstRemoteSiteFiles);
            this.gbRemoteSite.Controls.Add(this.toolStrip3);
            this.gbRemoteSite.Controls.Add(this.toolStrip2);
            this.gbRemoteSite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbRemoteSite.Location = new System.Drawing.Point(0, 0);
            this.gbRemoteSite.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbRemoteSite.Name = "gbRemoteSite";
            this.gbRemoteSite.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbRemoteSite.Size = new System.Drawing.Size(376, 313);
            this.gbRemoteSite.TabIndex = 6;
            this.gbRemoteSite.TabStop = false;
            this.gbRemoteSite.Text = "Remote Site";
            // 
            // lstRemoteSiteFiles
            // 
            this.lstRemoteSiteFiles.AllowDrop = true;
            this.lstRemoteSiteFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lstRemoteSiteFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRemoteSiteFiles.Location = new System.Drawing.Point(3, 73);
            this.lstRemoteSiteFiles.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstRemoteSiteFiles.MultiSelect = false;
            this.lstRemoteSiteFiles.Name = "lstRemoteSiteFiles";
            this.lstRemoteSiteFiles.Size = new System.Drawing.Size(370, 236);
            this.lstRemoteSiteFiles.TabIndex = 4;
            this.lstRemoteSiteFiles.UseCompatibleStateImageBehavior = false;
            this.lstRemoteSiteFiles.View = System.Windows.Forms.View.Details;
            this.lstRemoteSiteFiles.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lstRemoteSiteFiles_ItemDrag);
            this.lstRemoteSiteFiles.SelectedIndexChanged += new System.EventHandler(this.lstRemoteSiteFiles_SelectedValueChanged);
            this.lstRemoteSiteFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstRemoteSiteFiles_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "FileType";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Path";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Permission";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Date";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Size";
            // 
            // toolStrip3
            // 
            this.toolStrip3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewDir,
            this.toolStripSeparator3,
            this.btnRename,
            this.btnDelete,
            this.toolStripSeparator4,
            this.btnUpload,
            this.btnDownload});
            this.toolStrip3.Location = new System.Drawing.Point(3, 46);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(370, 27);
            this.toolStrip3.TabIndex = 3;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRemoteBack,
            this.toolStripSeparator2,
            this.txtRemoteDirectory,
            this.toolStripButton5});
            this.toolStrip2.Location = new System.Drawing.Point(3, 19);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(370, 27);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // txtRemoteDirectory
            // 
            this.txtRemoteDirectory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtRemoteDirectory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtRemoteDirectory.Name = "txtRemoteDirectory";
            this.txtRemoteDirectory.Size = new System.Drawing.Size(291, 27);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // btnNewDir
            // 
            this.btnNewDir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewDir.Image = global::Win7FTP.Properties.Resources.NewFolderHS;
            this.btnNewDir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewDir.Name = "btnNewDir";
            this.btnNewDir.Size = new System.Drawing.Size(24, 24);
            this.btnNewDir.Text = "New Folder...";
            this.btnNewDir.Click += new System.EventHandler(this.btnNewDir_Click);
            // 
            // btnRename
            // 
            this.btnRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRename.Image = global::Win7FTP.Properties.Resources.RenameFolderHS;
            this.btnRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(24, 24);
            this.btnRename.Text = "Rename";
            this.btnRename.ToolTipText = "Rename";
            this.btnRename.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::Win7FTP.Properties.Resources.DeleteHS;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(24, 24);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUpload.Image = global::Win7FTP.Properties.Resources.FillUpHS;
            this.btnUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(24, 24);
            this.btnUpload.Text = "Upload File";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDownload.Image = global::Win7FTP.Properties.Resources.FillDownHS;
            this.btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(24, 24);
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // btnRemoteBack
            // 
            this.btnRemoteBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoteBack.Image = global::Win7FTP.Properties.Resources.GoRtlHS;
            this.btnRemoteBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoteBack.Name = "btnRemoteBack";
            this.btnRemoteBack.Size = new System.Drawing.Size(24, 24);
            this.btnRemoteBack.Text = "toolStripButton1";
            this.btnRemoteBack.Click += new System.EventHandler(this.btnRemoteBack_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::Win7FTP.Properties.Resources.FormRunHS;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(24, 24);
            this.toolStripButton5.Text = "toolStripButton1";
            // 
            // btnOpenDialog
            // 
            this.btnOpenDialog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenDialog.Image = global::Win7FTP.Properties.Resources.openfolderHS;
            this.btnOpenDialog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenDialog.Name = "btnOpenDialog";
            this.btnOpenDialog.Size = new System.Drawing.Size(24, 24);
            this.btnOpenDialog.Text = "toolStripButton1";
            this.btnOpenDialog.Click += new System.EventHandler(this.btnOpenDialog_Click);
            // 
            // btnGo
            // 
            this.btnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGo.Image = global::Win7FTP.Properties.Resources.GoLtrHS;
            this.btnGo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(24, 24);
            this.btnGo.Text = "toolStripButton1";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 475);
            this.Controls.Add(this.gbRemoteSite);
            this.Controls.Add(this.gbFileSystem);
            this.Controls.Add(this.gbCommands);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.Text = "Win7 FTP Client";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.gbCommands.ResumeLayout(false);
            this.gbFileSystem.ResumeLayout(false);
            this.gbFileSystem.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.gbRemoteSite.ResumeLayout(false);
            this.gbRemoteSite.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MainMenu mMain;
        private MenuItem menuItem1;
        private MenuItem menuItem2;
        private MenuItem menuItem3;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private GroupBox gbCommands;
        private GroupBox gbFileSystem;
        private GroupBox gbRemoteSite;
        private ToolStrip toolStrip1;
        private ToolStripButton btnOpenDialog;
        private ToolStrip toolStrip2;
        private ToolStripButton btnRemoteBack;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripTextBox txtRemoteDirectory;
        private ToolStripButton toolStripButton5;
        private ToolTip toolTip1;
        public ListView lstRemoteSiteFiles;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ToolStrip toolStrip3;
        private ToolStripButton btnNewDir;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnRename;
        private ToolStripButton btnDelete;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnUpload;
        private ToolStripButton btnDownload;
        private Raccoom.Windows.Forms.TreeViewFolderBrowser tvFileSystem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripTextBox txtLocalFolderName;
        private ToolStripButton btnGo;
        private ListView lstMessages;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;

    }
}