using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Win7FTP
{
    public partial class frmLogInSetting : Form
    {
        String Login_pass;
        int LogType;
        public frmLogInSetting(int log_type)
        {
            InitializeComponent();
            LogType = log_type;
            if (LogType == 1)
                lblTitle.Text = "Setting secure log in!";
            else
                if (LogType == 3)
                    lblTitle.Text = "Manual convert log in!";
                else
                lblTitle.Text = "Close program confirm!";
        }

        private void btnLogInSetting_Click(object sender, EventArgs e)
        {
            Login_pass = ConfigurationManager.AppSettings["Login_pass"];
            if (LogType == 1)
            {
                if (Login_pass == txtLogIn.Text)
                {
                    //pnlNonTransparent.Enabled = true;
                    txtLogIn.Text = "";
                    txtLogIn.Enabled = false;
                    btnLogInSetting.Enabled = false;
                    FrmSetting setting = new FrmSetting();
                    setting.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid password");
                }
            }
            else
                if (LogType == 3)
                {
                    if (Login_pass == txtLogIn.Text)
                    {
                        //pnlNonTransparent.Enabled = true;
                        txtLogIn.Text = "";
                        txtLogIn.Enabled = false;
                        btnLogInSetting.Enabled = false;
                        frmManualConvert frm_MC = new frmManualConvert();
                        frm_MC.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid password");
                    }
                }
                else
                if (LogType == 2)
                {
                    if (Login_pass == txtLogIn.Text)
                    {
                        //pnlNonTransparent.Enabled = true;
                        txtLogIn.Text = "";
                        txtLogIn.Enabled = false;
                        //btnLogInSetting.Enabled = false;
                        //FrmSetting setting = new FrmSetting();
                        //setting.Show();
                        //GlobalVar.isClose = true;
                        this.DialogResult = DialogResult.OK;
                        //this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid password");
                    }
                }
        }

        private void txtLogIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogInSetting_Click(this, new EventArgs());
            }
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
        private void frmLogInSetting_Load(object sender, EventArgs e)
        {

            ReallyCenterToScreen();
            txtLogIn.TabIndex = 0;
            btnLogInSetting.TabIndex = 1;
        }
    }
}
