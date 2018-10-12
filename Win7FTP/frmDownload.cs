using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using System.Net;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.Threading;
using System.IO;
using Win7FTP.Library;
using Win7FTP.HelperClasses;

namespace Win7FTP
{
    public partial class frmDownload : GlassForm
    {
        #region Members
        //String Variables we will need to use throughout the File.
        string FileName, SaveFilePath, CurrentDirectory;

        //TaskDialog to display Download Completed actions to User
        TaskDialog taskDialogMain = null;

        //FTPClient used to Download File and setup File Download Events
        FTPclient FtpClient;
        #endregion

        #region Constructor
        /// <summary>
        /// frmDownload constructor
        /// </summary>
        /// <param name="Filename">Name of the File to Download</param>
        /// <param name="Current_Directory">Current Directory of the FTPClient; where file will be downloaded from.</param>
        /// <param name="SavePath">Path where the File will be saved.</param>
        /// <param name="Ftpclient">FTPClient from frmMain that will be refrenced here to FtpClient variable.</param>
        public frmDownload(string Filename, string Current_Directory, string SavePath, FTPclient Ftpclient)
        {
            //Init Form
            InitializeComponent();

            //Setup Variables
            FileName = Filename;
            SaveFilePath = SavePath;
            CurrentDirectory = Current_Directory;
            lblDownloadFrom.Text = Ftpclient.Hostname + Current_Directory + FileName;   //ex: ftp://ftp.somesite.com/current_dir/File.exe
            lblSavePath.Text = SaveFilePath;
            FtpClient = Ftpclient;
            TaskBarManager.ClearProgressValue();

            //Aero Composition Event 
            AeroGlassCompositionChanged += new AeroGlassCompositionChangedEvent(Form1_AeroGlassCompositionChanged);

            if (AeroGlassCompositionEnabled)
            {
                //We don't want pnlNonTransparent and the controls in it to be part of AERO
                //but we do want Aero...looks cool ;)
                ExcludeControlFromAeroGlass(pnlNonTransparent);
            }
            else
            {
                this.BackColor = Color.Teal;
            }
            
            //Show Form
            this.Show();

            //Setup our Download Client and Start Downloading
            FtpClient.CurrentDirectory = Current_Directory;
            FtpClient.OnDownloadProgressChanged += new FTPclient.DownloadProgressChangedHandler(FtpClient_OnDownloadProgressChanged);
            FtpClient.OnDownloadCompleted += new FTPclient.DownloadCompletedHandler(FtpClient_OnDownloadCompleted);
            //FtpClient.GetFileSize(FileName);//Huynh
            //FtpClient.Download(FileName, SavePath, true);
            FtpClient.Download(FileName, SavePath, true, FtpClient.GetFileSize(FileName));//Huynh, some ftp server doesn't support Get Size, then this func cannot be used.
        }
        #endregion

        #region TaskDialog
        void ShowCompleteDownloadDialog()
        {
            taskDialogMain = new TaskDialog();
            taskDialogMain.Caption = "File Download Completed Task";
            taskDialogMain.InstructionText = "Select an Action to Perform with the Downloaded File:";
            taskDialogMain.FooterText = FileName + " has successfully downloaded. Please select an action to perform or click Cancel and Return.";
            taskDialogMain.Cancelable = true;

            // Add a close button so user can close our dialog
            taskDialogMain.StandardButtons = TaskDialogStandardButtons.Close;
            taskDialogMain.Closing += new EventHandler<TaskDialogClosingEventArgs>(taskDialogMain_Closing);

            #region Creating and adding command link buttons

            TaskDialogCommandLink btnOpenFile = new TaskDialogCommandLink("cmdOpenFile", "Open Downloaded File");
            btnOpenFile.Click += new EventHandler(btnOpenFile_Click);

            TaskDialogCommandLink btnOpenFolder = new TaskDialogCommandLink("cmdOpenFolder", "Open Folder containing Downloaded File");
            btnOpenFolder.Click += new EventHandler(btnOpenFolder_Click);

            taskDialogMain.Controls.Add(btnOpenFile);
            taskDialogMain.Controls.Add(btnOpenFolder);

            #endregion

            // Show the taskdialog
            TaskDialogResult result = taskDialogMain.Show();
        }
        #endregion

        #region Events
        #region Download Client Events
        bool Happened = false;      //Supposedly, The OnDownloadCompleted repeats.  Well each time, it repeats one more time than
                                    //it previously repeated. Nothing bad about this, except that tyhe ShowCompleteDownloadDialog 
                                    //gets called as well. We don't want that.  So I have this variable to keep track of everything.

        //Event fires when the Download has completed.
        void FtpClient_OnDownloadCompleted(object sender, DownloadCompletedArgs e)
        {
            if (e.DownloadCompleted)
            {
                if (!Happened)
                { 
                    //Display the appropriate information to the User regarding the Download.
                    this.Text = "Download Completed!";
                    lblDownloadStatus.Text = "Downloaded File Successfully!";
                    progressBar1.Value = progressBar1.Maximum;
                    btnCancel.Text = "Exit";
                    //Display the TaskDialog, which will ask the user about what he/she needs to do with the file.
                    ShowCompleteDownloadDialog();
                }
                Happened = true;
            }
            else
            {
                lblDownloadStatus.Text = "Download Status: " + e.DownloadStatus;
                this.Text = "Download Error";
                btnCancel.Text = "Exit";

                HelperClasses.TaskBarManager.SetTaskBarProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Error);
                TaskDialog.Show("Error: " + e.DownloadStatus);
            }
            Happened = true;
        }

        //Event Fires whenever the Download Progress in changed.
        void FtpClient_OnDownloadProgressChanged(object sender, DownloadProgressChangedArgs e)
        {
            //Set Value for Progressbar
            progressBar1.Maximum = Convert.ToInt32(e.TotleBytes);
            progressBar1.Value = Convert.ToInt32(e.BytesDownloaded);

            //Taskbar Progress
            HelperClasses.TaskBarManager.SetProgressValue(Convert.ToInt32(e.BytesDownloaded), Convert.ToInt32(e.TotleBytes));

            // Calculate the download progress in percentages
            Int64 PercentProgress = Convert.ToInt64((progressBar1.Value * 100) / e.TotleBytes);

            //Display Information to the User on Form and on Labels
            this.Text = PercentProgress.ToString() + "% Downloading " + FileName;
            lblDownloadStatus.Text = "Download Status: Downloaded " + GetFileSize(e.BytesDownloaded) + " out of " + GetFileSize(e.TotleBytes) + " (" + PercentProgress.ToString() + "%)";
        }
        #endregion
        
        #region TaskDialog CommandLink Events
        void btnOpenFolder_Click(object sender, EventArgs e)
        {
            taskDialogMain.Close(TaskDialogResult.Close);
            Process.Start(System.IO.Path.GetPathRoot(SaveFilePath));
        }

        void btnOpenFile_Click(object sender, EventArgs e)
        {
            taskDialogMain.Close(TaskDialogResult.Close);
            Process.Start(SaveFilePath);
        }
        #endregion

        //Event fires when taskDialog closes.
        void taskDialogMain_Closing(object sender, TaskDialogClosingEventArgs e)
        {
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text != "Exit")
                FtpClient.CancelDownload();  //This means that the Text is "Cancel" and the User wants to Cancel Download.
                                             //Remember that we are changing text to Exit when Download Finishes.
            this.Close();
        }

        private void frmDownload_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Clear the Progress of the TaskBar Progressbar
            TaskBarManager.ClearProgressValue();

            Happened = false;
            this.taskDialogMain = null;
        }
        
        private void Form1_AeroGlassCompositionChanged(object sender, AeroGlassCompositionChangedEvenArgs e)
        {
            // When the desktop composition mode changes the window exclusion must be changed appropriately.
            if (e.GlassAvailable)
            {
                ExcludeControlFromAeroGlass(pnlNonTransparent);
                Invalidate();
            }
            else
            {
                this.BackColor = Color.Teal;
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Code Below Converts Bytes to KB, MB, GB, or just Bytes.  Makes the App more look :)
        /// Obtained from: http://www.freevbcode.com/ShowCode.Asp?ID=1971
        /// </summary>
        /// <param name="byteCount">Bytes that need to be converted</param>
        /// <returns>Converts the Bytes into its Appropriate form (KB, MB, GB, or just Bytes) and returns them in the form of: ex: 22 KB</returns>
        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount / 1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount / 1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount / 1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount.ToString() + " Bytes";

            return size;
        }
        #endregion
    }
}
