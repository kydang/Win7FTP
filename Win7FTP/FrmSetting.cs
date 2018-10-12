using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Win7FTP.Library;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Configuration;
using Win7FTP.Class;

namespace Win7FTP
{
    public partial class FrmSetting : Form
    {
        public FrmSetting()
        {
            InitializeComponent();

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

        private void btnSavePara_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem == null)
            {
                return;
            }
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Conn1":
                    SetSetting("Conn1_Name", txtConnName.Text);
                    SetSetting("Conn1_username", txtUserName.Text);
                    SetSetting("Conn1_pass", txtPassword.Text);
                    SetSetting("Conn1_host", txtHostName.Text);
                    SetSetting("Conn1_RemotePath", txtRemotePath.Text);
                    SetSetting("Conn1_LocalPath", txtLocalPath.Text);
                    SetSetting("Conn1_LocalPathConverted", txtLocalPath2.Text);
                    SetSetting("Conn1_interval", txtInterval.Text);
                    SetSetting("Conn1_enable", Convert.ToString(chkbxEnableConn.Checked));
                    SetSetting("Conn1_enable", Convert.ToString(chkbxEnableConn.Checked));
                    SetSetting("Conn1_index", "0");
                    SetSetting("Conn1_Filter_Char", txtFilterChar.Text);
                    SetSetting("Conn1_FilterEnable", Convert.ToString(chkbFilter.Checked));
                    
                    break;
                case "Conn2":
                    SetSetting("Conn2_Name", txtConnName.Text);
                    SetSetting("Conn2_username", txtUserName.Text);
                    SetSetting("Conn2_pass", txtPassword.Text);
                    SetSetting("Conn2_host", txtHostName.Text);
                    SetSetting("Conn2_RemotePath", txtRemotePath.Text);
                    SetSetting("Conn2_LocalPath", txtLocalPath.Text);
                    SetSetting("Conn2_LocalPathConverted", txtLocalPath2.Text);
                    SetSetting("Conn2_interval", txtInterval.Text);
                    SetSetting("Conn2_enable", Convert.ToString(chkbxEnableConn.Checked));
                    SetSetting("Conn2_index", "1");
                    SetSetting("Conn2_Filter_Char", txtFilterChar.Text);
                    SetSetting("Conn2_FilterEnable", Convert.ToString(chkbFilter.Checked));
                    break;
                case "Conn3":
                    SetSetting("Conn3_Name", txtConnName.Text);
                    SetSetting("Conn3_username", txtUserName.Text);
                    SetSetting("Conn3_pass", txtPassword.Text);
                    SetSetting("Conn3_host", txtHostName.Text);
                    SetSetting("Conn3_RemotePath", txtRemotePath.Text);
                    SetSetting("Conn3_LocalPath", txtLocalPath.Text);
                    SetSetting("Conn3_LocalPathConverted", txtLocalPath2.Text);
                    SetSetting("Conn3_interval", txtInterval.Text);
                    SetSetting("Conn3_enable", Convert.ToString(chkbxEnableConn.Checked));
                    SetSetting("Conn3_index", "2");
                    SetSetting("Conn3_Filter_Char", txtFilterChar.Text);
                    SetSetting("Conn3_FilterEnable", Convert.ToString(chkbFilter.Checked));
                    break;
                case "Conn4":
                    SetSetting("Conn4_Name", txtConnName.Text);
                    SetSetting("Conn4_username", txtUserName.Text);
                    SetSetting("Conn4_pass", txtPassword.Text);
                    SetSetting("Conn4_host", txtHostName.Text);
                    SetSetting("Conn4_RemotePath", txtRemotePath.Text);
                    SetSetting("Conn4_LocalPath", txtLocalPath.Text);
                    SetSetting("Conn4_LocalPathConverted", txtLocalPath2.Text);
                    SetSetting("Conn4_interval", txtInterval.Text);
                    SetSetting("Conn4_enable", Convert.ToString(chkbxEnableConn.Checked));
                    SetSetting("Conn4_index", "3");
                    SetSetting("Conn4_Filter_Char", txtFilterChar.Text);
                    SetSetting("Conn4_FilterEnable", Convert.ToString(chkbFilter.Checked));
                    break;
            }
            SetSetting("LogPath", txtLogpath.Text);
            ConfigurationManager.RefreshSection("appSettings");
            
        }

        private void btnSavePass_Click(object sender, EventArgs e)
        {
            DialogResult rst = MessageBox.Show("You want to change password login for Setting? ", "Please confirm:", MessageBoxButtons.YesNo);
            if (rst == DialogResult.No)
            {
                return;

            }
                

            SetSetting("Login_pass", txtLogInPass.Text);
            //Login_pass = txtLogInPass.Text;
            txtLogInPass.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Ftp_service.OnMyEvent(this);
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Conn1":
                    txtConnName.Text = ConfigurationManager.AppSettings["Conn1_Name"];
                    txtUserName.Text = ConfigurationManager.AppSettings["Conn1_username"];
                    txtPassword.Text = ConfigurationManager.AppSettings["Conn1_pass"];
                    txtHostName.Text = ConfigurationManager.AppSettings["Conn1_host"];
                    txtRemotePath.Text = ConfigurationManager.AppSettings["Conn1_RemotePath"];
                    txtLocalPath.Text = ConfigurationManager.AppSettings["Conn1_LocalPath"];
                    txtLocalPath2.Text = ConfigurationManager.AppSettings["Conn1_LocalPathConverted"];
                    txtInterval.Text = ConfigurationManager.AppSettings["Conn1_interval"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn1_enable"]);
                    txtFilterChar.Text = ConfigurationManager.AppSettings["Conn1_Filter_Char"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn1_enable"]);
                    chkbFilter.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn1_FilterEnable"]);
                    
                    break;
                case "Conn2":
                    txtConnName.Text = ConfigurationManager.AppSettings["Conn2_Name"];
                    txtUserName.Text = ConfigurationManager.AppSettings["Conn2_username"];
                    txtPassword.Text = ConfigurationManager.AppSettings["Conn2_pass"];
                    txtHostName.Text = ConfigurationManager.AppSettings["Conn2_host"];
                    txtRemotePath.Text = ConfigurationManager.AppSettings["Conn2_RemotePath"];
                    txtLocalPath.Text = ConfigurationManager.AppSettings["Conn2_LocalPath"];
                    txtLocalPath2.Text = ConfigurationManager.AppSettings["Conn2_LocalPathConverted"];
                    txtInterval.Text = ConfigurationManager.AppSettings["Conn2_interval"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn2_enable"]);
                    txtFilterChar.Text = ConfigurationManager.AppSettings["Conn2_Filter_Char"];
                    chkbFilter.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn2_FilterEnable"]);
                    break;
                case "Conn3":
                    txtConnName.Text = ConfigurationManager.AppSettings["Conn3_Name"];
                    txtUserName.Text = ConfigurationManager.AppSettings["Conn3_username"];
                    txtPassword.Text = ConfigurationManager.AppSettings["Conn3_pass"];
                    txtHostName.Text = ConfigurationManager.AppSettings["Conn3_host"];
                    txtRemotePath.Text = ConfigurationManager.AppSettings["Conn3_RemotePath"];
                    txtLocalPath.Text = ConfigurationManager.AppSettings["Conn3_LocalPath"];
                    txtLocalPath2.Text = ConfigurationManager.AppSettings["Conn3_LocalPathConverted"];
                    txtInterval.Text = ConfigurationManager.AppSettings["Conn3_interval"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn3_enable"]);
                    txtFilterChar.Text = ConfigurationManager.AppSettings["Conn3_Filter_Char"];
                    chkbFilter.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn3_FilterEnable"]);
                    
                    break;
                case "Conn4":
                    txtConnName.Text = ConfigurationManager.AppSettings["Conn4_Name"];
                    txtUserName.Text = ConfigurationManager.AppSettings["Conn4_username"];
                    txtPassword.Text = ConfigurationManager.AppSettings["Conn4_pass"];
                    txtHostName.Text = ConfigurationManager.AppSettings["Conn4_host"];
                    txtRemotePath.Text = ConfigurationManager.AppSettings["Conn4_RemotePath"];
                    txtLocalPath.Text = ConfigurationManager.AppSettings["Conn4_LocalPath"];
                    txtLocalPath2.Text = ConfigurationManager.AppSettings["Conn4_LocalPathConverted"];
                    txtInterval.Text = ConfigurationManager.AppSettings["Conn4_interval"];
                    chkbxEnableConn.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn4_enable"]);
                    txtFilterChar.Text = ConfigurationManager.AppSettings["Conn4_Filter_Char"];
                    chkbFilter.Checked = Convert.ToBoolean(ConfigurationManager.AppSettings["Conn4_FilterEnable"]);
                    break;
            }
            if (chkbxEnableConn.Checked)
                chkbxEnableConn.BackColor = System.Drawing.Color.Green;
            else chkbxEnableConn.BackColor = System.Drawing.Color.Red;

            txtLogpath.Text = ConfigurationManager.AppSettings["LogPath"];
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
        private void FrmSetting_Load(object sender, EventArgs e)
        {
            ReallyCenterToScreen();
            comboBox1.TabIndex = 0;
            btnSavePara.TabIndex = 1;
        }
    }
}
