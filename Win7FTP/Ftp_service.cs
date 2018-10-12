using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Win7FTP.Library;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Configuration;
using Win7FTP.Class;
using SecureDongle;



namespace Win7FTP
{
    enum SDCmd : ushort
    {
        SD_FIND = 1,//Find Dongle
        SD_FIND_NEXT = 2,//Find Next Dongle
        SD_OPEN = 3,//Open Dongle
        SD_CLOSE = 4,//Close Dongle
        SD_READ = 5,//Read Dongle
        SD_WRITE = 6,//Write Dongle
        SD_RANDOM = 7,//Generate Random Number
        SD_SEED = 8,//Generate Seed Code
        SD_WRITE_USERID = 9,//Write User ID
        SD_READ_USERID = 10,//Read User ID
        SD_SET_MODULE = 11,//Set Module
        SD_CHECK_MODULE = 12,//Check Module
        SD_WRITE_ARITHMETIC = 13,//Write Arithmetic
        SD_CALCULATE1 = 14,//Calculate 1
        SD_CALCULATE2 = 15,//Calculate 2
        SD_CALCULATE3 = 16,//Calculate 3
        SD_DECREASE = 17,//Decrease Module Unit
        SD_SET_COUNTER = 20,//set counter
        SD_GET_COUNTER = 21,//get counter
        SD_DEC_COUNTER = 22,
        SD_SET_TIMER = 23,//set timer
        SD_GET_TIMER = 24,//get timer
        SD_ADJUST_TIMER = 25,//adjust timer
        SD_SET_RSAKEY_N = 29,//write RSA N
        SD_SET_RSAKEY_D = 30,//write RSA D
        SD_UPDATE_GEN_HEADER = 31,//generate encrypted file header
        SD_UPDATE_GEN = 32,//create encrypted file content
        SD_UPDATE_CHECK = 33,//update cipher file
        SD_UPDATE = 34,//update cipher file
        SD_SET_DES_KEY = 41,//Set DES key
        SD_DES_ENC = 42,//DES encryption
        SD_DES_DEC = 43,//DES decryption
        SD_RSA_ENC = 44,//RSA encryption
        SD_RSA_DEC = 45,//RSA decryption
        SD_READ_EX = 46,//read dongle memory
        SD_WRITE_EX = 47,//write dongle memory
        SD_SET_COUNTER_EX = 0xA0,//set counter value type changed from WORD to DWORD
        SD_GET_COUNTER_EX = 0xA1,//get counter, value type changed from WORD to DWORD
        SD_SET_TIMER_EX = 0xA2,//set timer time value type changed from WORD to DWORD
        SD_GET_TIMER_EX = 0xA3,//get timer time value type changed from WORD to DWORD
        SD_ADJUST_TIMER_EX = 0xA4,//adjust timer, time value type changed from WORD to DWORD
        SD_UPDATE_GEN_HEADER_EX = 0xA5,//generate update file header specialize in updating RSA key pair
        SD_UPDATE_GEN_EX = 0xA6,//generate update file content specialize in updating RSA key pair
        SD_UPDATE_CHECK_EX = 0xA7,//update file checking specialize in updating RSA key pair
        SD_UPDATE_EX = 0xA8,//update cipher file specialize in updating RSA key pair
        SD_SET_UPDATE_KEY = 0xA9,//set update RSA key pair
        SD_ADD_UPDATE_HEADER = 0xAA,//fill head of authorization file
        SD_ADD_UPDATE_CONTENT = 0xAB,//fill content of authorization file
        SD_GET_TIME_DWORD = 0xAC,//get value(DWORD type) based on 2006.1.1.0.0.0
        SD_VERSION = 100,//get COS Version
    };

    enum SDErrCode : uint
    {
        ERR_SUCCESS = 0,							//No error
        ERR_NO_PARALLEL_PORT = 0x80300001,		//(0x80300001)No parallel port
        ERR_NO_DRIVER,							//(0x80300002)No drive
        ERR_NO_DONGLE,							//(0x80300003)No SecureDongle
        ERR_INVALID_pWORD,					//(0x80300004)Invalid pword
        ERR_INVALID_pWORD_OR_ID,				//(0x80300005)Invalid pword or ID
        ERR_SETID,								//(0x80300006)Set id error
        ERR_INVALID_ADDR_OR_SIZE,				//(0x80300007)Invalid address or size
        ERR_UNKNOWN_COMMAND,					//(0x80300008)Unkown command
        ERR_NOTBELEVEL3,						//(0x80300009)Inner error
        ERR_READ,								//(0x8030000A)Read error
        ERR_WRITE,								//(0x8030000B)Write error
        ERR_RANDOM,								//(0x8030000C)Generate random error
        ERR_SEED,								//(0x8030000D)Generate seed error
        ERR_CALCULATE,							//(0x8030000E)Calculate error
        ERR_NO_OPEN,							//(0x8030000F)The SecureDongle is not opened
        ERR_OPEN_OVERFLOW,						//(0x80300010)Open SecureDongle too more(>16)
        ERR_NOMORE,								//(0x80300011)No more SecureDongle
        ERR_NEED_FIND,							//(0x80300012)Need Find before FindNext
        ERR_DECREASE,							//(0x80300013)Dcrease error
        ERR_AR_BADCOMMAND,						//(0x80300014)Band command
        ERR_AR_UNKNOWN_OPCODE,					//(0x80300015)Unkown op code
        ERR_AR_WRONGBEGIN,						//(0x80300016)There could not be constant in first instruction in arithmetic 
        ERR_AR_WRONG_END,						//(0x80300017)There could not be constant in last instruction in arithmetic 
        ERR_AR_VALUEOVERFLOW,					//(0x80300018)The constant in arithmetic overflow
        ERR_UNKNOWN = 0x8030ffff,					//(0x8030FFFF)Unkown error

        ERR_RECEIVE_NULL = 0x80300100,			//(0x80300100)Receive null
        ERR_PRNPORT_BUSY = 0x80300101				//(0x80300101)Parallel port busy

    };
    
   
    
    public partial class Ftp_service : Form
    {
        public FTPclient FtpClient;
        ListViewItem Message;
        String CurrentDir;
        String FileName;
        int count1, count2, count3, count4,count_common;
        bool ready = true;
        int current_priority = 1;
        int multi_factor = 60; // please check this: tmrPolling.Interval = 1000; 
        //String Cancel;
        FTP CurrentFTP = new FTP();
        String Login_pass;
        public static bool checkfilter = false;
        public static event EventHandler MyEvent;
        bool blinkG = false;
        bool blinkR = false;



        //Key detect:
        SecureDonglecom SD = new SecureDonglecom();

        //Declare variable
        byte[] buffer = new byte[1024];
        object obbuffer = new object();
        ushort handle = 0;
        ushort p1 = 0;
        ushort p2 = 0;
        ushort p3 = 0;
        ushort p4 = 0;
        uint lp1 = 0;
        uint lp2 = 0;
        ulong ret = 1;
        

        public struct Ftp_conn
        {
            public int conn_idx;
            public string ConnName;
            public string username;
           public string password;
           public string host;
           public string port;
           public string RemotePath;
           public string LocalPath;
           public string LocalPathConverted;
           public int poll_interval;
           public string Filter_Char;
           public bool Enable;
           public bool retry;
           public int retry_time;
           public bool FilterEnable;
        }


        Ftp_conn Conn1 = new Ftp_conn();
        Ftp_conn Conn2 = new Ftp_conn();
        Ftp_conn Conn3 = new Ftp_conn();
        Ftp_conn Conn4 = new Ftp_conn();

        public Ftp_service()
        {
            InitializeComponent();
            read_setting();
            Ftp_service.MyEvent += new EventHandler(MyEventMethod);
            
        }
        private void MyEventMethod(object sender, EventArgs e)
        {
            //do something here
            read_setting();
            //btnLogOut_Click(this, new EventArgs());
            //LogOut();
        }

        public static void OnMyEvent(Form frm)
        {
            if (MyEvent != null)
                MyEvent(frm, new EventArgs());

        }

        public void read_setting()
        {
            //init connection 1 para
            Conn1.username = ConfigurationManager.AppSettings["Conn1_username"];
            Conn1.password = ConfigurationManager.AppSettings["Conn1_pass"];
            Conn1.host = ConfigurationManager.AppSettings["Conn1_host"];
            Conn1.RemotePath = ConfigurationManager.AppSettings["Conn1_RemotePath"];
            Conn1.LocalPath = ConfigurationManager.AppSettings["Conn1_LocalPath"];
            Conn1.LocalPathConverted = ConfigurationManager.AppSettings["Conn1_LocalPathConverted"];
            Conn1.poll_interval = Convert.ToInt32(ConfigurationManager.AppSettings["Conn1_interval"]);
            Conn1.Enable = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn1_enable"]);
            Conn1.conn_idx = Convert.ToInt32(ConfigurationManager.AppSettings["Conn1_index"]);
            Conn1.Filter_Char = ConfigurationManager.AppSettings["Conn1_Filter_Char"];
            Conn1.FilterEnable = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn1_FilterEnable"]);
            Conn1.ConnName = ConfigurationManager.AppSettings["Conn1_Name"];
            //init connection 2 para
            Conn2.username = ConfigurationManager.AppSettings["Conn2_username"];
            Conn2.password = ConfigurationManager.AppSettings["Conn2_pass"];
            Conn2.host = ConfigurationManager.AppSettings["Conn2_host"];
            Conn2.RemotePath = ConfigurationManager.AppSettings["Conn2_RemotePath"];
            Conn2.LocalPath = ConfigurationManager.AppSettings["Conn2_LocalPath"];
            Conn2.LocalPathConverted = ConfigurationManager.AppSettings["Conn2_LocalPathConverted"];
            Conn2.poll_interval = Convert.ToInt32(ConfigurationManager.AppSettings["Conn2_interval"]);
            Conn2.Enable = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn2_enable"]);
            Conn2.conn_idx = Convert.ToInt32(ConfigurationManager.AppSettings["Conn2_index"]);
            Conn2.Filter_Char = ConfigurationManager.AppSettings["Conn2_Filter_Char"];
            Conn2.FilterEnable = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn2_FilterEnable"]);
            Conn2.ConnName = ConfigurationManager.AppSettings["Conn2_Name"];
            //init connection 3 para
            Conn3.username = ConfigurationManager.AppSettings["Conn3_username"];
            Conn3.password = ConfigurationManager.AppSettings["Conn3_pass"];
            Conn3.host = ConfigurationManager.AppSettings["Conn3_host"];
            Conn3.RemotePath = ConfigurationManager.AppSettings["Conn3_RemotePath"];
            Conn3.LocalPath = ConfigurationManager.AppSettings["Conn3_LocalPath"];
            Conn3.LocalPathConverted = ConfigurationManager.AppSettings["Conn3_LocalPathConverted"];
            Conn3.poll_interval = Convert.ToInt32(ConfigurationManager.AppSettings["Conn3_interval"]);
            Conn3.Enable = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn3_enable"]);
            Conn3.conn_idx = Convert.ToInt32(ConfigurationManager.AppSettings["Conn3_index"]);
            Conn3.Filter_Char = ConfigurationManager.AppSettings["Conn3_Filter_Char"];
            Conn3.FilterEnable = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn3_FilterEnable"]);
            Conn3.ConnName = ConfigurationManager.AppSettings["Conn3_Name"];
            //init connection 4 para
            Conn4.username = ConfigurationManager.AppSettings["Conn4_username"];
            Conn4.password = ConfigurationManager.AppSettings["Conn4_pass"];
            Conn4.host = ConfigurationManager.AppSettings["Conn4_host"];
            Conn4.RemotePath = ConfigurationManager.AppSettings["Conn4_RemotePath"];
            Conn4.LocalPath = ConfigurationManager.AppSettings["Conn4_LocalPath"];
            Conn4.LocalPathConverted = ConfigurationManager.AppSettings["Conn4_LocalPathConverted"];
            Conn4.poll_interval = Convert.ToInt32(ConfigurationManager.AppSettings["Conn4_interval"]);
            Conn4.Enable = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn4_enable"]);
            Conn4.conn_idx = Convert.ToInt32(ConfigurationManager.AppSettings["Conn4_index"]);
            Conn4.Filter_Char = ConfigurationManager.AppSettings["Conn4_Filter_Char"];
            Conn4.FilterEnable = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn4_FilterEnable"]);
            Conn4.ConnName = ConfigurationManager.AppSettings["Conn4_Name"];
            Login_pass = ConfigurationManager.AppSettings["Login_pass"];
            
        }
        public void SetFtpClient(Win7FTP.Library.FTPclient client)
        {
            //Set FtpClient
            FtpClient = client;

            //Display the Welcome Message
            Message = new ListViewItem();
            Message.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString();
            Message.SubItems.Add("Open connection");
            Message.SubItems.Add(FtpClient.WelcomeMessage);
            Message.SubItems.Add("No Code");
            Message.SubItems.Add("/");
            lstMessages.Items.Add(Message);
            if (lstMessages.Items.Count > 100000)
            {
                for (int i = 0; i < 4000; i++)
                {
                    lstMessages.Items.RemoveAt(0);
                }
            }
            //Setup OnMessageReceived Event
            FtpClient.OnNewMessageReceived += new FTPclient.NewMessageHandler(FtpClient_OnNewMessageReceived);

            //RefreshDirectory();
        }

        private void FtpClient_OnNewMessageReceived(object myObject, NewMessageEventArgs e)
        {
            //Display Meesage in lstMessages
            Message = new ListViewItem();
            Message.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString();
            Message.SubItems.Add(e.StatusType);
            Message.SubItems.Add(e.StatusMessage);
            Message.SubItems.Add(e.StatusCode);
            Message.SubItems.Add(CurrentDir);//Huynh
            lstMessages.Items.Add(Message);

            this.lstMessages.EnsureVisible(this.lstMessages.Items.Count - 1);
        }

        //Code Below Converts Bytes to KB, MB, GB, or just Bytes.  Makes the App more look :)
        //Obtained from: http://www.freevbcode.com/ShowCode.Asp?ID=1971
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

        //private void btnLogIn_Click(object sender, EventArgs e)
        //{
        //    //FtpClient = new FTPclient();
        //    //FtpClient.CurrentDirectory = "/";

        //    //CurrentDir = FtpClient.CurrentDirectory;
        //    try
        //    {
        //        //Set FTP
        //        FTPclient objFtp = new FTPclient(Conn1.host, Conn1.username, Conn1.password);
        //        objFtp.CurrentDirectory = "/";
        //        CurrentDir = objFtp.CurrentDirectory;

        //        //Set FTP Client in MAIN form
        //        SetFtpClient(objFtp);

        //        //Show MAIN form and HIDE this one
                
        //    }
        //    catch (Exception ex)
        //    {
        //        //Display Error
        //        MessageBox.Show(ex.Message);
        //    }
        //    ////
        //    //test delete file
        //    //RefreshDirectory();
        //    foreach (FTPfileInfo folder in FtpClient.ListDirectoryDetail(CurrentDir))
        //    {
        //        ListViewItem item = new ListViewItem();
        //        item.Text = folder.Filename;
        //        if (folder.FileType == FTPfileInfo.DirectoryEntryTypes.Directory)
        //        {
        //            //item.SubItems.Add("Folder");

        //        }

        //        else
        //        {
        //            FileName = item.Text;
        //            FtpClient.OnDownloadProgressChanged += new FTPclient.DownloadProgressChangedHandler(FtpClient_OnDownloadProgressChanged);
        //            FtpClient.OnDownloadCompleted += new FTPclient.DownloadCompletedHandler(FtpClient_OnDownloadCompleted);
        //            FtpClient.Download(FileName, "F:\\FTP_Local\\" + FileName, true);
                    
        //            FtpClient.FtpDelete(FileName);
        //        }
        //    }
        //    RefreshDirectory();
        //}
        public void TurnLedOn(int idx)
        {
            switch(idx)
            {
                case 0:
                    picboxConn1Green.Visible = true;
                    picboxConn1Red.Visible = false;
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")+ ": " + Conn1.ConnName + " connected! \r\n";
                    break;
                case 1:
                    picboxConn2Green.Visible = true;
                    picboxConn2Red.Visible = false;
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + Conn2.ConnName + " connected! \r\n";
                    break;
                case 2:
                    picboxConn3Green.Visible = true;
                    picboxConn3Red.Visible = false;
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + Conn3.ConnName + " connected! \r\n";
                    break;
                case 3:
                    picboxConn4Green.Visible = true;
                    picboxConn4Red.Visible = false;
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + Conn4.ConnName + " connected! \r\n";
                    break;
                default:
                    break;
            }

        }
        public void TurnLedOff(int idx)
        {
            switch (idx)
            {
                case 0:
                    picboxConn1Green.Visible = false;
                    picboxConn1Red.Visible = true;
                    Conn1.Enable = false;
                    Conn1.retry = true;
                    Conn1.retry_time = 300; //300 seconds
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + Conn1.ConnName+ " disconnected! \r\n";
                    break;
                case 1:
                    picboxConn2Green.Visible = false;
                    picboxConn2Red.Visible = true;
                    Conn2.Enable = false;
                    Conn2.retry = true;
                    Conn2.retry_time = 300;
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + Conn2.ConnName + " disconnected! \r\n";
                    break;
                case 2:
                    picboxConn3Green.Visible = false;
                    picboxConn3Red.Visible = true;
                    Conn3.Enable = false;
                    Conn3.retry = true;
                    Conn3.retry_time = 300;
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + Conn3.ConnName + " disconnected! \r\n";
                    break;
                case 3:
                    picboxConn4Green.Visible = false;
                    picboxConn4Red.Visible = true;
                    Conn4.Enable = false;
                    Conn4.retry = true;
                    Conn4.retry_time = 300;
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") +": "+ Conn4.ConnName + " disconnected! \r\n";
                    break;
                default:
                    break;
            }

        }
        public void CopyFileAndDelete(Ftp_conn ftpConn)
        {
            if (!ftpConn.Enable) return;
            ready = false;
            comboBox1.SelectedIndex = ftpConn.conn_idx;
            
            try
            {
                //Set FTP
                FTPclient objFtp = new FTPclient(ftpConn.host, ftpConn.username, ftpConn.password);
                objFtp.CurrentDirectory = ftpConn.RemotePath;
                CurrentDir = objFtp.CurrentDirectory;

                //Set FTP Client in MAIN form
                SetFtpClient(objFtp);

                //Show MAIN form and HIDE this one
                //turn indicator for the channel:
                TurnLedOn(ftpConn.conn_idx);
                RefreshDirectory(ftpConn.Filter_Char);
            }
            catch (Exception ex)
            {
                //Display Error

                if (ex.ToString().Contains("221"))
                {
                    ready = true;
                    
                    FtpClient.CancelDownload2();
                    return;
                }
                ftpConn.Enable = false;
                TurnLedOff(ftpConn.conn_idx);
                //MessageBox.Show(ex.Message);
                //should return;
                ready = true;
                return;
            }
            ////
            //test delete file
            //RefreshDirectory();
            int NumOfFile = 0;
            try
            {
                foreach (FTPfileInfo folder in FtpClient.ListDirectoryDetail(CurrentDir))
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = folder.Filename;
                    if (folder.Permission != "rwxrwxrwx")
                    {
                        //MessageBox.Show("test");
                        continue;
                    }
                    long _FilseSize = folder.Size;
                    if (folder.FileType == FTPfileInfo.DirectoryEntryTypes.Directory)
                    {
                        //item.SubItems.Add("Folder");

                    }

                    else
                    {
                        if (_FilseSize == 0)
                        {
                            continue;
                        }
                        
                        

                        FileName = item.Text;

                        if (FileName.StartsWith(ftpConn.Filter_Char))//26-Jun-2018 //  copy only files start with specify letters.
                        {
                            lblDownloadStatus.Text = "Download Status: Checking next file!";
                            //long newsize = FtpClient.GetFileSize(FileName);
                            //if (_FilseSize != newsize)
                            //{
                            //    MessageBox.Show("size dif");
                            //    continue;
                            //}
                            FtpClient.OnDownloadProgressChanged += new FTPclient.DownloadProgressChangedHandler(FtpClient_OnDownloadProgressChanged);
                            FtpClient.OnDownloadCompleted += new FTPclient.DownloadCompletedHandler(FtpClient_OnDownloadCompleted);
                            try
                            {


                                //if (_FilseSize == 0)
                                //{
                                //    continue;
                                //}
                                //FtpClient.GetFileSize(FileName);
                                //

                                if (FtpClient.Download(FileName, ftpConn.LocalPath + FileName, true, _FilseSize))//Huynh
                                //FtpClient.Download(FileName, ftpConn.LocalPath + FileName, true);
                                {
                                    FtpClient.FtpDelete(FileName);
                                    ConvertCsv(ftpConn.LocalPath + FileName, ftpConn.LocalPathConverted + "C_" + FileName);
                                }
                                else
                                {
                                    
                                    FtpClient.CancelDownload2();//silent cancel
                                    TurnLedOff(ftpConn.conn_idx);
                                }
                            }
                            catch (Exception e)
                            {
                                ////Display Error
                                //FtpClient.CancelDownload2();//silent cancel
                                //TurnLedOff(ftpConn.conn_idx);
                                ////ftpConn.Enable = false;

                                ////MessageBox.Show(ex.Message);
                                ////should return;
                                //ready = true;
                                //return;
                            }
                            NumOfFile++;
                        }
                    }
                    //count3 = 0;
                    if (NumOfFile > 5) break;
                }
            }
            catch(Exception)
            {
                //Display Error
                TurnLedOff(ftpConn.conn_idx);
                ftpConn.Enable = false;

                //MessageBox.Show(ex.Message);
                //should return;
                ready = true;
                return;
            }
            //RefreshDirectory(ftpConn.Filter_Char);
            ready = true;
        }

        public void CopyFileAndDelete2(Ftp_conn ftpConn)
        {
            ToggleLedG();
            if (!ftpConn.Enable) return;
            ready = false;
            bool CheckFilter = ftpConn.FilterEnable;
            
            comboBox1.SelectedIndex = ftpConn.conn_idx;
            ////lbltest2.Text = "entry 1";
            try
            {
                
                CurrentFTP.Connect(ftpConn.host, ftpConn.username, ftpConn.password);
                //CurrentFTP.ChangeDirectory(ftpConn.RemotePath);
                //Show MAIN form and HIDE this one
                //turn indicator for the channel:
                TurnLedOn(ftpConn.conn_idx);
                LedG_Off();
                RefreshDirectory2(ftpConn,CurrentFTP);
                LedG_On();
            }
            catch (Exception ex)
            {
                
                TurnLedOff(ftpConn.conn_idx);
                //MessageBox.Show(ex.Message);
                //should return;
                rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": " + ftpConn.ConnName + " error:" + ex.ToString() + "\r\n";
                LedG_Off();
                ready = true;
                //return;
                    return;
             }

            ////lbltest2.Text = "entry 2";
            ////
            //test delete file
            //RefreshDirectory();
            int NumOfFile = 0;
            try
            {
                foreach (FTP.File _file in CurrentFTP.Files)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = _file.FileName;
                    
                    long _FilseSize = _file.FileSize;
                   //DateTime TimeCreated = _file.FileDate.AddMinutes(1);
                    int dif = DateTime.Now.Hour * 60 + DateTime.Now.Minute - (_file.FileDate.Hour * 60 + _file.FileDate.Minute);
                   if (dif < 0)
                       dif = 0 - dif;
                    
                        if (_FilseSize == 0)
                        {
                            continue;
                        }


                        ////lbltest2.Text = "entry 3";
                        FileName = item.Text;
                        if (dif < 2)
                        {
                            ToggleLedG();
                            continue;
                        }
                        else if (CheckFilter)
                        {
                            if (!FileName.StartsWith(ftpConn.Filter_Char))
                            {
                                ToggleLedG();
                                continue;
                            }
                        }
                        //if (FileName.StartsWith(ftpConn.Filter_Char)&&(dif>=2))//26-Jun-2018 //  copy only files start with specify letters.
                        
                            //lblDownloadStatus.Text = "Download Status: Checking next file!";

                            //FTP.FileCollection.
                            //CurrentFTP.
                            //FTP.OnDownloadProgressChanged += new FTP.DownloadProgressChangedHandler(FtpClient_OnDownloadProgressChangedN);
                            //CurrentFTP.OnDownloadCompleted += new FTP.DownloadCompletedHandler(FtpClient_OnDownloadCompletedN);
                            ////lbltest2.Text = "entry 4";
                            try
                            {

                                if (CurrentFTP.Files.Download_H(FileName, ftpConn.LocalPath + FileName, false))
                                {
                                    System.Threading.Thread.Sleep(100);
                                    ToggleLedG();
                                    CurrentFTP.Files.RemoveFile(FileName);
                                    ConvertCsv(ftpConn.LocalPath + FileName, ftpConn.LocalPathConverted + "C_" + FileName);
                                    ToggleLedG();
                                    ////lbltest2.Text = "entry 5";
                                }
                                    
                              
                                //if (FtpClient.Download(FileName, ftpConn.LocalPath + FileName, true, _FilseSize))//Huynh
                                ////FtpClient.Download(FileName, ftpConn.LocalPath + FileName, true);
                                //{
                                //    FtpClient.FtpDelete(FileName);
                                //    ConvertCsv(ftpConn.LocalPath + FileName, ftpConn.LocalPathConverted + "C_" + FileName);
                                //}
                                //else
                                //{

                                //    FtpClient.CancelDownload2();//silent cancel
                                //    TurnLedOff(ftpConn.conn_idx);
                                //}

                            }
                            catch (Exception e)
                            {
                               //MessageBox.Show("File  "+FileName+":  error!\r\n"+ e.ToString());
                                rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": File  " + FileName + ":  error!\r\n" + e.ToString() + "\r\n";
                            }
                            NumOfFile++;
                        
                        ////lbltest2.Text = "entry 6";
                            count_common = 0;
                    if (NumOfFile >= 5) break;
                }
                //RefreshDirectory2(ftpConn, CurrentFTP);
                ////lbltest2.Text = "entry 7";
                System.Threading.Thread.Sleep(100);
                CurrentFTP.Disconnect();
                //ToggleLedG();//Indication only
                LedG_Off();
            }
            catch (Exception)
            {
                //Display Error
                TurnLedOff(ftpConn.conn_idx);

                LedG_Off();
                //MessageBox.Show(ex.Message);
                //should return;
                ready = true;
                return;
            }
            //RefreshDirectory(ftpConn.Filter_Char);
            ready = true;
        }
        /// <summary>
        /// Reload all Directories and Files in Current Directory
        /// </summary>
        private void RefreshDirectory()
        {
            //Clear all items
            lstRemoteSiteFiles.Items.Clear();

            //Open and Display Root Directory
            foreach (FTPfileInfo folder in FtpClient.ListDirectoryDetail(CurrentDir))
            {
                ListViewItem item = new ListViewItem();
                item.Text = folder.Filename;
                if (folder.FileType == FTPfileInfo.DirectoryEntryTypes.Directory)
                {
                    //item.SubItems.Add("Folder");

                }

                else
                {
                    item.SubItems.Add("File");

                    item.SubItems.Add(folder.FullName);
                    item.SubItems.Add(folder.Permission);
                    item.SubItems.Add(folder.FileDateTime.ToShortTimeString() + folder.FileDateTime.ToShortDateString());
                    item.SubItems.Add(folder.Size.ToString());
                    lstRemoteSiteFiles.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Reload all Directories and Files in Current Directory
        /// </summary>
        private void RefreshDirectory(string filter_char)
        {
            //Clear all items
            lstRemoteSiteFiles.Items.Clear();

            //Open and Display Root Directory
            foreach (FTPfileInfo folder in FtpClient.ListDirectoryDetail(CurrentDir))
            {
                ListViewItem item = new ListViewItem();
                item.Text = folder.Filename;
                if (folder.FileType == FTPfileInfo.DirectoryEntryTypes.Directory)
                {
                    //item.SubItems.Add("Folder");

                }

                else
                {
                    if (!folder.Filename.StartsWith(filter_char))
                        continue;
                    item.SubItems.Add("File");

                    item.SubItems.Add(folder.FullName);
                    item.SubItems.Add(folder.Permission);
                    item.SubItems.Add(folder.FileDateTime.ToShortTimeString() + folder.FileDateTime.ToShortDateString());
                    item.SubItems.Add(folder.Size.ToString());
                    lstRemoteSiteFiles.Items.Add(item);
                }
            }
        }

        /// <summary>
        /// Reload all Directories and Files in Current Directory
        /// </summary>
        private void RefreshDirectory2(Ftp_conn _ftp_con,FTP _ftp)
        {
            //Clear all items
            _ftp.ChangeDirectory(_ftp_con.RemotePath);
            ToggleLedG();//for indication only.
            lstRemoteSiteFiles.Items.Clear();
            bool checkFilter = _ftp_con.FilterEnable;
            string filter_char = _ftp_con.Filter_Char;
            //int f = _ftp.Files.Count;
            //Open and Display Root Directory
            for (int i = 0; i < _ftp.Files.Count;i++ )
            {
                ListViewItem item = new ListViewItem();
                item.Text = _ftp.Files[i].FileName;
                int dif = DateTime.Now.Hour * 60 + DateTime.Now.Minute - (_ftp.Files[i].FileDate.Hour * 60 + _ftp.Files[i].FileDate.Minute);
                if (dif < 0) dif = 0 - dif;
                
                
                    if (dif < 2)
                        continue;
                    else
                        if (checkFilter)
                        {
                            if (!_ftp.Files[i].FileName.StartsWith(filter_char))
                                continue;
                        }

                item.SubItems.Add("File");

                item.SubItems.Add(_ftp.Files[i].FileName);
                //item.SubItems.Add(_ftp.Files[i].);
                item.SubItems.Add(_ftp.Files[i].FileDate.ToShortTimeString() + _ftp.Files[i].FileDate.ToShortDateString());
                item.SubItems.Add(_ftp.Files[i].FileSize.ToString());
                lstRemoteSiteFiles.Items.Add(item);
            }
                
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //RefreshDirectory();
            //foreach (FTPfileInfo folder in FtpClient.ListDirectoryDetail(CurrentDir))
            //{
            //    ListViewItem item = new ListViewItem();
            //    item.Text = folder.Filename;
            //    if (folder.FileType == FTPfileInfo.DirectoryEntryTypes.Directory)
            //    {
            //        //item.SubItems.Add("Folder");

            //    }

            //    else
            //    {
            //        FileName = item.Text;
            //        FtpClient.OnDownloadProgressChanged += new FTPclient.DownloadProgressChangedHandler(FtpClient_OnDownloadProgressChanged);
            //        FtpClient.OnDownloadCompleted += new FTPclient.DownloadCompletedHandler(FtpClient_OnDownloadCompleted);
            //        FtpClient.Download(FileName, "F:\\FTP_Local\\" + FileName, true);
            //        FtpClient.FtpDelete(FileName);
            //    }
            //}
            //RefreshDirectory();
            //Conn2.RemotePath = txtRemotePath.Text;
            //Conn2.LocalPath = txtLocalPath.Text;
            //CopyFileAndDelete(Conn2);

            
        }

        bool Happened = false; 
        //Event fires when the Download has completed.
        void FtpClient_OnDownloadCompleted(object sender, DownloadCompletedArgs e)
        {
            if (e.DownloadCompleted)
            {
                if (!Happened)
                {
                    //Display the appropriate information to the User regarding the Download.
                    //this.Text = "Download Completed!";
                    lblDownloadStatus.Text = "Downloaded File Successfully!";
                    //progressBar1.Value = progressBar1.Maximum; 12-07
                    //btnCancel.Text = "Exit";
                    //Cancel = "Exit";
                    //Display the TaskDialog, which will ask the user about what he/she needs to do with the file.
                    //ShowCompleteDownloadDialog();
                }
                Happened = true;
            }
            else
            {
                lblDownloadStatus.Text = "Download Status: " + e.DownloadStatus;
                //this.Text = "Download Error";
                //btnCancel.Text = "Exit";
                //Cancel = "Exit";
                HelperClasses.TaskBarManager.SetTaskBarProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Error);
                //TaskDialog.Show("Error: " + e.DownloadStatus);
                //FtpClient.CancelDownload();
                FtpClient = null;
            }
            Happened = true;
        }
        void FtpClient_OnDownloadCompletedN(object sender, DownloadCompletedArgsN e)
        {
            if (e.DownloadCompleted)
            {
                if (!Happened)
                {
                    //Display the appropriate information to the User regarding the Download.
                    //this.Text = "Download Completed!";
                    lblDownloadStatus.Text = "Downloaded File Successfully!";
                    //progressBar1.Value = progressBar1.Maximum; 12-07
                    //btnCancel.Text = "Exit";
                    //Cancel = "Exit";
                    //Display the TaskDialog, which will ask the user about what he/she needs to do with the file.
                    //ShowCompleteDownloadDialog();
                }
                Happened = true;
            }
            else
            {
                lblDownloadStatus.Text = "Download Status: " + e.DownloadStatus;
                //this.Text = "Download Error";
                //btnCancel.Text = "Exit";
                //Cancel = "Exit";
                HelperClasses.TaskBarManager.SetTaskBarProgressState(Microsoft.WindowsAPICodePack.Taskbar.TaskbarProgressBarState.Error);
                //TaskDialog.Show("Error: " + e.DownloadStatus);
                //FtpClient.CancelDownload();
                FtpClient = null;
            }
            Happened = true;
        }

        //Event Fires whenever the Download Progress in changed.
        void FtpClient_OnDownloadProgressChanged(object sender, DownloadProgressChangedArgs e)
        {
            if (e.TotleBytes == 0)
            {
                //this.Text = "100% Downloading " + FileName;
                lblDownloadStatus.Text = "Download Status: Downloaded " + GetFileSize(e.BytesDownloaded) + " out of " + GetFileSize(e.TotleBytes) + "  (100%)";
            }
            else
            {

                
                //Set Value for Progressbar
                //progressBar1.Maximum = Convert.ToInt32(e.TotleBytes); 12-07
                //progressBar1.Value = Convert.ToInt32(e.BytesDownloaded);12-07

                //Taskbar Progress
                HelperClasses.TaskBarManager.SetProgressValue(Convert.ToInt32(e.BytesDownloaded), Convert.ToInt32(e.TotleBytes));

                // Calculate the download progress in percentages

                //Int64 PercentProgress = Convert.ToInt64((progressBar1.Value * 100) / e.TotleBytes); 12-07

                //Display Information to the User on Form and on Labels
                //this.Text = PercentProgress.ToString() + "% Downloading " + FileName;
                lblDownloadStatus.Text = "Download Status: Downloaded " + GetFileSize(e.BytesDownloaded) + " out of " + GetFileSize(e.TotleBytes);// + " (" + PercentProgress.ToString() + "%)";//12-07
            }

        }

        //Event Fires whenever the Download Progress in changed.
        void FtpClient_OnDownloadProgressChangedN(object sender, DownloadProgressChangedArgsN e)
        {
            if (e.BytesDownloaded==e.TotleBytes)
            {
                //this.Text = "100% Downloading " + FileName;
                lblDownloadStatus.Text = "File:"+e.getFileName+ ": Finish - "+ GetFileSize(e.TotleBytes)+"  ";
                rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": File: " + e.getFileName + " downloaded.\r\n";
            }
            else
            {
                ToggleLedG();//Indication only
                //Set Value for Progressbar
                //progressBar1.Maximum = Convert.ToInt32(e.TotleBytes); 12-07
                //progressBar1.Value = Convert.ToInt32(e.BytesDownloaded);12-07

                //Taskbar Progress
                HelperClasses.TaskBarManager.SetProgressValue(Convert.ToInt32(e.BytesDownloaded), Convert.ToInt32(e.TotleBytes));
                
                // Calculate the download progress in percentages

                //Int64 PercentProgress = Convert.ToInt64((progressBar1.Value * 100) / e.TotleBytes); 12-07

                //Display Information to the User on Form and on Labels
                //this.Text = PercentProgress.ToString() + "% Downloading " + FileName;
                lblDownloadStatus.Text = "File:" + e.getFileName + ": Downloading..." + GetFileSize(e.BytesDownloaded) + " out of " + GetFileSize(e.TotleBytes);// + " (" + PercentProgress.ToString() + "%)";//12-07
            }

        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Set FTP
        //        FTPclient objFtp = new FTPclient(Conn2.host, Conn2.username, Conn2.password);
        //        objFtp.CurrentDirectory = "/";
        //        CurrentDir = objFtp.CurrentDirectory;

        //        //Set FTP Client in MAIN form
        //        SetFtpClient(objFtp);

        //        //Show MAIN form and HIDE this one

        //    }
        //    catch (Exception ex)
        //    {
        //        //Display Error
        //        MessageBox.Show(ex.Message);
        //    }

        //    ////
        //    //test delete file
        //    //RefreshDirectory();
        //    foreach (FTPfileInfo folder in FtpClient.ListDirectoryDetail(CurrentDir))
        //    {
        //        ListViewItem item = new ListViewItem();
        //        item.Text = folder.Filename;
        //        if (folder.FileType == FTPfileInfo.DirectoryEntryTypes.Directory)
        //        {
        //            //item.SubItems.Add("Folder");

        //        }

        //        else
        //        {
        //            FileName = item.Text;
        //            FtpClient.OnDownloadProgressChanged += new FTPclient.DownloadProgressChangedHandler(FtpClient_OnDownloadProgressChanged);
        //            FtpClient.OnDownloadCompleted += new FTPclient.DownloadCompletedHandler(FtpClient_OnDownloadCompleted);
        //            FtpClient.Download(FileName, "F:\\FTP_Local\\" + FileName, true);
        //            FtpClient.FtpDelete(FileName);
        //        }
        //    }
        //    RefreshDirectory();

        //}


        public void checkRTB()
        {
            string logpath =ConfigurationManager.AppSettings["LogPath"]; 
            if(rtbConsole.Lines.Count()>=500)
            {
                string _filename = "log"+DateTime.Now.ToString("ddMMyy")+"_"+DateTime.Now.ToString("HHmmss");
                rtbConsole.SaveFile(logpath+ _filename+ ".txt");
                rtbConsole.Text = "";
                rtbConsole.Text += "Log file saved:   "+logpath+ _filename+ ".txt \r\n";
            }

        }
        /// <summary>
        /// timer tick every 6 seconds.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrPolling_Tick(object sender, EventArgs e)
        {
            count1++; count2++; count3++; count4++; count_common++;
            lbltest.Text = count_common.ToString();
            ToggleLedR();
            checkRTB();
            if (!ready) return;
            
            switch(current_priority)
            {
                case 1:
                    if (count1 >= Conn1.poll_interval * multi_factor)//
                    {

                        if (Conn1.Enable && ready)
                        {
                            count1 = 0;
                            CopyFileAndDelete2(Conn1);
                            current_priority = 2;
                        }
                        else
                        {
                            if(!Conn1.Enable)
                            {
                                current_priority = 2;
                            }
                        }
                    }
                    break;
                case 2:
                    if (count2 >= Conn2.poll_interval * multi_factor)//
                    {

                        if (Conn2.Enable && ready)
                        {
                            count2 = 0;
                            CopyFileAndDelete2(Conn2);
                            current_priority = 3;
                        }
                        else
                        {
                            if (!Conn2.Enable)
                            {
                                current_priority = 3;
                            }
                        }
                    }
                    break;
                case 3:
                    if (count3 >= Conn3.poll_interval * multi_factor)//
                    {

                        if (Conn3.Enable && ready)
                        {
                            count3 = 0;
                            CopyFileAndDelete2(Conn3);
                            current_priority = 4;
                        }
                        else
                        {
                            if (!Conn3.Enable)
                            {
                                current_priority = 4;
                            }
                        }
                    }
                    break;
                case 4:
                    if (count4 >= Conn4.poll_interval * multi_factor)//
                    {

                        if (Conn4.Enable && ready)
                        {
                            count4 = 0;
                            CopyFileAndDelete2(Conn4);
                            current_priority = 1;
                        }
                        else
                        {
                            if (!Conn4.Enable)
                            {
                                current_priority = 1;
                            }
                        }
                    }
                    break;
                default:
                    break;

            }

            if (count1 >= Conn1.poll_interval * multi_factor)//
            {
                
                if (Conn1.Enable && ready)
                {
                    count1 = 0;
                    CopyFileAndDelete2(Conn1);
                }
            }
            if (count2 >= Conn2.poll_interval * multi_factor)//
            {
  
                if (Conn2.Enable && ready)
                {
                    count2 = 0;
                    CopyFileAndDelete2(Conn2);
                    
                }

            }
            if (count3 >= Conn3.poll_interval * multi_factor)//
            {
 
                if (Conn3.Enable && ready)
                {
                    count3 = 0;
                    CopyFileAndDelete2(Conn3);
                }

            }
            if (count4 >= Conn4.poll_interval * multi_factor)//
            {
              if (Conn4.Enable && ready)
                {
                    count4 = 0;
                    CopyFileAndDelete2(Conn4);
                }
 
            }
        }

        public static void SetSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            var entry = config.AppSettings.Settings[key];
            if (entry == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;

            config.Save(ConfigurationSaveMode.Modified);
            //ConfigurationManager.RefreshSection("appSettings");

        }
        protected void ReallyCenterToScreen()
        {
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.X + (workingArea.Width - this.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2)
            };
        }
        private bool Keydetect()
        {
           
            //Find SecureDongle
            //SecureDongle Password
            p1 = 0x2242;
            p2 = 0xC898;
            p3 = 0;     //advance password. Must set to 0 for end user application
            p4 = 0;     //advance password. Must set to 0 for end user application
            ret = SD.SecureDongle((ushort)SDCmd.SD_FIND, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);

            if (ret != 0)
            {
                //listBox1.Items.Add(string.Format("No SecureDongle found, error code: {0}", ret));
                //MessageBox.Show("License key not found!");

                DialogResult rst = MessageBox.Show("          License Key not found,\r\n \r\n                 Search again?", "LICENSE ERROR:", MessageBoxButtons.YesNo);
                if (rst == DialogResult.Yes)
                {
                    timer1.Enabled = true;
                    timer1.Interval = 500;
                    return false;
                }
                else
                {
                    this.Close();
                    return false;
                }
            }
            else
            {
                timer1.Interval = 15000;
                return true;
            }
            //return true;
        }
        private void Ftp_service_Load(object sender, EventArgs e)
        {
            String version = Application.ProductVersion;
            this.Text = "Ftp_Services - ver." + version;
            
            ////////////////////
            timer1.Enabled = false;
        
            ReallyCenterToScreen();
            btnStart.BackColor = System.Drawing.Color.Gray;
            try
            {
                if (Keydetect())
                    timer1.Enabled = true;
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.ToString());
            }

      










            ///////////////
            tmrPolling.Interval = 1000; //1 seconds. (should be 10 seconds)
            FTP.OnDownloadProgressChanged += new FTP.DownloadProgressChangedHandler(FtpClient_OnDownloadProgressChangedN);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedItem.ToString())
            {
                case "Conn1":
                    txtConnName.Text = ConfigurationManager.AppSettings["Conn1_Name"];
                    txtUserName.Text = ConfigurationManager.AppSettings["Conn1_username"];
                    txtPassword.Text = ConfigurationManager.AppSettings["Conn1_pass"];
                    txtHostName.Text = ConfigurationManager.AppSettings["Conn1_host"];
                    txtRemotePath.Text = ConfigurationManager.AppSettings["Conn1_RemotePath"];
                    txtLocalPath.Text = ConfigurationManager.AppSettings["Conn1_LocalPath"];
                    txtInterval.Text = ConfigurationManager.AppSettings["Conn1_interval"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn1_enable"]);
                    chkbFilter.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn1_FilterEnable"]);
                    txtFilterChar.Text = ConfigurationManager.AppSettings["Conn1_Filter_Char"];

                    break;
                case "Conn2":
                    txtConnName.Text = ConfigurationManager.AppSettings["Conn2_Name"];
                    txtUserName.Text = ConfigurationManager.AppSettings["Conn2_username"];
                    txtPassword.Text = ConfigurationManager.AppSettings["Conn2_pass"];
                    txtHostName.Text = ConfigurationManager.AppSettings["Conn2_host"];
                    txtRemotePath.Text = ConfigurationManager.AppSettings["Conn2_RemotePath"];
                    txtLocalPath.Text = ConfigurationManager.AppSettings["Conn2_LocalPath"];
                    txtInterval.Text = ConfigurationManager.AppSettings["Conn2_interval"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn2_enable"]);
                    chkbFilter.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn2_FilterEnable"]);
                    txtFilterChar.Text = ConfigurationManager.AppSettings["Conn2_Filter_Char"];


                    break;
                case "Conn3":
                    txtConnName.Text = ConfigurationManager.AppSettings["Conn3_Name"];
                    txtUserName.Text = ConfigurationManager.AppSettings["Conn3_username"];
                    txtPassword.Text = ConfigurationManager.AppSettings["Conn3_pass"];
                    txtHostName.Text = ConfigurationManager.AppSettings["Conn3_host"];
                    txtRemotePath.Text = ConfigurationManager.AppSettings["Conn3_RemotePath"];
                    txtLocalPath.Text = ConfigurationManager.AppSettings["Conn3_LocalPath"];
                    txtInterval.Text = ConfigurationManager.AppSettings["Conn3_interval"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn3_enable"]);
                    chkbFilter.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn3_FilterEnable"]);
                    txtFilterChar.Text = ConfigurationManager.AppSettings["Conn3_Filter_Char"];
                    break;
                case "Conn4":
                    txtConnName.Text = ConfigurationManager.AppSettings["Conn4_Name"];
                    txtUserName.Text = ConfigurationManager.AppSettings["Conn4_username"];
                    txtPassword.Text = ConfigurationManager.AppSettings["Conn4_pass"];
                    txtHostName.Text = ConfigurationManager.AppSettings["Conn4_host"];
                    txtRemotePath.Text = ConfigurationManager.AppSettings["Conn4_RemotePath"];
                    txtLocalPath.Text = ConfigurationManager.AppSettings["Conn4_LocalPath"];
                    txtInterval.Text = ConfigurationManager.AppSettings["Conn4_interval"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn4_enable"]);
                    chkbFilter.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn4_FilterEnable"]);
                    txtFilterChar.Text = ConfigurationManager.AppSettings["Conn4_Filter_Char"];
                    break;
            }
            if (chkbxEnableConn.Checked)
                chkbxEnableConn.BackColor = System.Drawing.Color.Green;
            else chkbxEnableConn.BackColor = System.Drawing.Color.Red;
           
        }

        //private void btnSavePara_Click(object sender, EventArgs e)
        //{
        //    switch (comboBox1.SelectedItem.ToString())
        //    {
        //        case "Conn1":
        //            SetSetting("Conn1_Name", txtConnName.Text);
        //            SetSetting("Conn1_username", txtUserName.Text);
        //            SetSetting("Conn1_pass", txtPassword.Text);
        //            SetSetting("Conn1_host", txtHostName.Text);
        //            SetSetting("Conn1_RemotePath", txtRemotePath.Text);
        //            SetSetting("Conn1_LocalPath", txtLocalPath.Text);
        //            SetSetting("Conn1_interval", txtInterval.Text);
        //            SetSetting("Conn1_enable", Convert.ToString(chkbxEnableConn.Checked));
        //            SetSetting("Conn1_index", "0");                     
        //            break;
        //        case "Conn2":
        //            SetSetting("Conn2_Name", txtConnName.Text);
        //            SetSetting("Conn2_username", txtUserName.Text);
        //            SetSetting("Conn2_pass", txtPassword.Text);
        //            SetSetting("Conn2_host", txtHostName.Text);
        //            SetSetting("Conn2_RemotePath", txtRemotePath.Text);
        //            SetSetting("Conn2_LocalPath", txtLocalPath.Text);
        //            SetSetting("Conn2_interval", txtInterval.Text);
        //            SetSetting("Conn2_enable", Convert.ToString(chkbxEnableConn.Checked));
        //            SetSetting("Conn2_index", "1");   
        //            break;
        //        case "Conn3":
        //            SetSetting("Conn3_Name", txtConnName.Text);
        //            SetSetting("Conn3_username", txtUserName.Text);
        //            SetSetting("Conn3_pass", txtPassword.Text);
        //            SetSetting("Conn3_host", txtHostName.Text);
        //            SetSetting("Conn3_RemotePath", txtRemotePath.Text);
        //            SetSetting("Conn3_LocalPath", txtLocalPath.Text);
        //            SetSetting("Conn3_interval", txtInterval.Text);
        //            SetSetting("Conn3_enable", Convert.ToString(chkbxEnableConn.Checked));
        //            SetSetting("Conn3_index", "2");
        //            break;
        //        case "Conn4":
        //            SetSetting("Conn4_Name", txtConnName.Text);
        //            SetSetting("Conn4_username", txtUserName.Text);
        //            SetSetting("Conn4_pass", txtPassword.Text);
        //            SetSetting("Conn4_host", txtHostName.Text);
        //            SetSetting("Conn4_RemotePath", txtRemotePath.Text);
        //            SetSetting("Conn4_LocalPath", txtLocalPath.Text);
        //            SetSetting("Conn4_interval", txtInterval.Text);
        //            SetSetting("Conn4_enable", Convert.ToString(chkbxEnableConn.Checked));
        //            SetSetting("Conn4_index", "3");
        //            break;
        //    }
        //    ConfigurationManager.RefreshSection("appSettings");
        //    Message = new ListViewItem();
        //    Message.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString();
        //    Message.SubItems.Add("Setting");
        //    Message.SubItems.Add("Saved");
        //    Message.SubItems.Add("No Code");
        //    Message.SubItems.Add("/");
        //    lstMessages.Items.Add(Message);
            
            
        //    //lstMessages.
        //    read_setting();
            
        //}

        private void chkbxEnableConn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxEnableConn.Checked)
                chkbxEnableConn.BackColor = System.Drawing.Color.Green;
            else chkbxEnableConn.BackColor = System.Drawing.Color.Red;
        }

        private void Ftp_service_FormClosing(object sender, FormClosingEventArgs e)
        {

            //DialogResult rsl =  MessageBox.Show("test", "cap", MessageBoxButtons.YesNo);
            //if(rsl == DialogResult.Yes)
            //{
                tmrPolling.Enabled = false;
                if (FtpClient != null)
                    FtpClient.CancelDownload();  //This means that the Text is "Cancel" and the User wants to Cancel Download.
                CurrentFTP.Disconnect();
            //}
            

               
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmrPolling.Enabled = true;
            read_setting();
            if(tmrPolling.Enabled)
            {
                btnStart.BackColor = System.Drawing.Color.Green;
            }

            //Start immediately.
            count1 = (Conn1.poll_interval * multi_factor)-3;
            count2 = (Conn2.poll_interval * multi_factor)-3;
            count3 = (Conn3.poll_interval * multi_factor)-3;
            count4 = (Conn4.poll_interval * multi_factor)-3;

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrPolling.Enabled = false;
            if(CurrentFTP !=null)
            {
                CurrentFTP.Disconnect();

            }
            if (!tmrPolling.Enabled)
            {
                btnStart.BackColor = System.Drawing.Color.Gray;
            }
            if (FtpClient != null)
                FtpClient.CancelDownload();  //This means that the Text is "Cancel" and the User wants to Cancel Download.

        }

       
       

        

        //private void btnLogOut_Click(object sender, EventArgs e)
        //{
        //    pnlNonTransparent.Enabled = false;
        //    btnLogInSetting.Enabled = true;
        //    txtLogIn.Enabled = true;
        //}

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Test Db
            dbInterface DbInterface = new dbInterface();
            //List<string> str = DbInterface.Select_Year();
            ////lbltest.Text = str[0];
            //try
            //{
            //    DbInterface.Insert_Data("RegLogUser", "UserName,Password,LogType", "user3", "abc123", false);
            //    insertlog("SQL", "SQL write", "user3", "none");
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Write data to sql error: \r\n"+ex.ToString());

            //}
            
            //Test CSV
            //ReadTest();//ok
            
            
           // ConvertCsv("F:\\FTP_Local\\Org\\test.csv", "F:\\FTP_Local\\Dest\\WriteTest.csv");
            
            
            //testRename(Conn3);
            //read_setting();
            //CopyFileAndDelete2(Conn3);
           // RefreshDirectory2(Conn3.Filter_Char,)
            
            //test  log file
            //string logpath = ConfigurationManager.AppSettings["LogPath"];
            //rtbConsole.Text = "test";
            //string _filename = "log" + DateTime.Now.ToString("ddMMyy") + "_" + DateTime.Now.ToString("HHmmss");
            //rtbConsole.SaveFile(logpath + _filename + ".txt");
            //rtbConsole.Text = "";
            //rtbConsole.Text += "Log file saved:   " + logpath + _filename + ".txt \r\n";

        }

        public void insertlog(string messageType,string _message,string _code,string currDir)
        {
            //Display the Welcome Message
            Message = new ListViewItem();
            Message.Text = DateTime.Now.ToLongTimeString() + " " + DateTime.Now.ToShortDateString();
            Message.SubItems.Add(messageType);//Message type
            Message.SubItems.Add(_message);//Message 
            Message.SubItems.Add(_code);//Code
            Message.SubItems.Add(currDir);
            lstMessages.Items.Add(Message);
        }

    

      

        void WriteTest()
        {
            // Write sample data to CSV file
            using (CsvFileWriter writer = new CsvFileWriter("WriteTest.csv"))
            {
                for (int i = 0; i < 100; i++)
                {
                    CsvRow row = new CsvRow();
                    for (int j = 0; j < 5; j++)
                        row.Add(String.Format("Column{0}", j));
                    writer.WriteRow(row);
                }
            }
        }

        void ReadTest()
        {
            int count = 0;
            // Read sample data from CSV file
            using (CsvFileReader reader = new CsvFileReader("test.csv"))
            {
                
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    
                    //if (row[4] == "50")
                    //{
                        //insertlog("CSV", "record read", "test", Test[0] + "|" + Test[1] + "|" + Test[2] + "|" + Test[3] + "|");
                        insertlog(count.ToString(), "record read", "test", row[0] + "|" + row[1] + "|" + row[2] + "|" + row[3] + "|" + row[4] + "|" + row[5] + "|");
                        count++;
                    //}
                }
            }
        }

        void ConvertCsv(string OriginalFilePath, string DestinationFilePath)
        {
            bool _error = false;
            
            // Read sample data from CSV file
          
            using (CsvFileReader reader = new CsvFileReader(OriginalFilePath))
            {
                
                using (CsvFileWriter writer = new CsvFileWriter(DestinationFilePath))
                {
                    CsvRow row = new CsvRow() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Col_11", "Col_12", "Col_13", "Col_14", "Col_15", "Col_16" };

                    row[0] = "Date"; row[1] = "Time"; row[2] = "1_Pressing_time"; row[3] = "2_Open_time"; row[4] = "3_MC_No";
                    row[5] = "4_Item_code"; row[6] = "5_Component"; row[7] = "6_Size"; row[8] = "7_Colour"; row[9] = "8_StandardCT";
                    writer.WriteRow(row);

                    while (reader.ReadRow(row))
                    {
                        //CsvRow Wrt_row = new CsvRow();
                        
                        string col0 = row[0].Remove(0,2);//remove first 2 zero.
                        string col1 = row[1].Remove(0, 2);//remove first 2 zero.
                        try
                        {
                            DateTime date = DateTime.ParseExact(col0, "MMddyy", System.Globalization.CultureInfo.InvariantCulture);
                            row[0] = date.ToString("MM/dd/yyyy");
                        }
                        catch
                        {
                            row[0] = "12/31/2000";
                            _error = true;
                        }
                        try
                        {
                            DateTime Time = DateTime.ParseExact(col1, "HHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            row[1] = Time.ToString("h:mm:ss tt");
                        }
                        catch
                        {
                            row[1] = "12/31/2000";
                            _error = true;
                        }
                        //row.RemoveRange(10, 6);
                        int i = 0;
                        try
                        {
                            for ( i = 2; i < 16; i++)
                            {
                                string myStr = row[i];
                                myStr = myStr.TrimStart('0');
                                myStr = myStr.Length > 0 ? myStr : "0";
                                row[i] = myStr;
                            }
                        }
                        catch
                        {
                            row[i] ="1";
                            _error = true;
                        }
                            writer.WriteRow(row);
                    }
                    if (_error)
                        rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": File " + OriginalFilePath + " Some data fail to be converted!";
                    rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + ": File " + OriginalFilePath + " converted to " + DestinationFilePath + "\r\n";
                }
            }
        }

        

       public void testRename(Ftp_conn ftpConn)
        {
            //Set FTP
            FTPclient objFtp = new FTPclient(ftpConn.host, ftpConn.username, ftpConn.password);
            objFtp.CurrentDirectory = ftpConn.RemotePath;
            CurrentDir = objFtp.CurrentDirectory;

            //Set FTP Client in MAIN form
            SetFtpClient(objFtp);
            //string source = "./MEMCARD/6201631.csv";
            FtpClient.FtpRename("./MEMCARD/TEST1.csv", "./MEMCARD/OK_TEST1.csv");
            
        }

       private void tmrRetry_Tick(object sender, EventArgs e)
       {
           if(Conn1.retry)
           {
               if (Conn1.retry_time>0)
               Conn1.retry_time -= 1;
               if(Conn1.retry_time==0)
               {
                   Conn1.retry = false;
                   Conn1.Enable = true;
               }
           }
           if (Conn2.retry)
           {
               if (Conn2.retry_time > 0)
                   Conn2.retry_time -= 1;
               if (Conn2.retry_time == 0)
               {
                   Conn2.retry = false;
                   Conn2.Enable = true;
               }
           }
           if (Conn3.retry)
           {
               if (Conn3.retry_time > 0)
                   Conn3.retry_time -= 1;
               if (Conn3.retry_time == 0)
               {
                   Conn3.retry = false;
                   Conn3.Enable = true;
               }
           }
           if (Conn4.retry)
           {
               if (Conn4.retry_time > 0)
                   Conn4.retry_time -= 1;
               if (Conn4.retry_time == 0)
               {
                   Conn4.retry = false;
                   Conn4.Enable = true;
               }
           }
       }

       private void chkbCopyAll_CheckedChanged(object sender, EventArgs e)
       {
           if (chkbFilter.Checked)
           {
               chkbFilter.BackColor = System.Drawing.Color.Green;
           }
           else
               chkbFilter.BackColor = System.Drawing.Color.LightGray;
       }

       private void settingToolStripMenuItem_Click(object sender, EventArgs e)
       {
           frmLogInSetting LogInform = new frmLogInSetting(1);
           LogInform.Show();
       }
       
        public void ToggleLedG()
        {
            if(blinkG)
            {
                picboxStatusG.Visible = false;
                //picboxStatusR.Visible = false;
                blinkG = false;
            }
            else
            {
                picboxStatusG.Visible = true;
                //picboxStatusR.Visible = true;
                blinkG = true;
            }
        }
        public void LedG_On()
        {
              picboxStatusG.Visible = true;
                //picboxStatusR.Visible = true;
         }
        public void LedG_Off()
        {
            picboxStatusG.Visible = false;
            //picboxStatusR.Visible = true;
        }
        public void ToggleLedR()
        {
            if (blinkR)
            {
                //picboxStatusG.Visible = true;
                picboxStatusR.Visible = false;
                blinkR = false;
            }
            else
            {
                //picboxStatusG.Visible = false;
                picboxStatusR.Visible = true;
                blinkR = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                if (Keydetect())
                    timer1.Enabled = true;
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.ToString());
            }
        }

        private void Ftp_service_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }  

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false; 
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            //Find SecureDongle
            //SecureDongle Password
            p1 = 0x2242;
            p2 = 0xC898;
            p3 = 0;     //advance password. Must set to 0 for end user application
            p4 = 0;     //advance password. Must set to 0 for end user application
            ret = SD.SecureDongle((ushort)SDCmd.SD_FIND, ref handle, ref lp1, ref lp2, ref p1, ref p2, ref p3, ref p4, ref obbuffer);

            if (ret == 0)
            //if (Keydetect())
            {
                frmLogInSetting logFrm = new frmLogInSetting(2);
                //logFrm.Show();
                if (logFrm.ShowDialog() != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }


        }

        private void rtbConsole_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            rtbConsole.SelectionStart = rtbConsole.Text.Length;
            // scroll it automatically
            rtbConsole.ScrollToCaret();
        }

        //private void btnManualConvert_Click(object sender, EventArgs e)
        //{
        //    frmManualConvert C_form = new frmManualConvert();
        //    C_form.Show();
        //}

        private void manualConvertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogInSetting LogInform = new frmLogInSetting(3);
            LogInform.Show();
        }

    }
    public class GlobalVar
    {
        public static bool isClose = false;
    }
}
