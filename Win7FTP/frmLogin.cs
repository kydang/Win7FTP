using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Win7FTP.Library;

namespace Win7FTP
{
    public partial class frmLogin : GlassForm
    {
        #region Members
        public static Win7FTP.Library.FTPclient objFtp;
        private frmMain Main;
        #endregion

        #region Contructor
        public frmLogin()
        {
            //Init Form
            InitializeComponent();

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
        }
        #endregion

        #region Events
        void Form1_AeroGlassCompositionChanged(object sender, AeroGlassCompositionChangedEvenArgs e)
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            //Below code will never fire....But I have it for Development purposes..
            //in case someone decides to make it resizable
            Rectangle panelRect = ClientRectangle;
            panelRect.Inflate(-30, -30);
            pnlNonTransparent.Bounds = panelRect;
            ExcludeControlFromAeroGlass(pnlNonTransparent);
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                //Set FTP
                FTPclient objFtp = new FTPclient(txtHostName.Text, txtUserName.Text, txtPassword.Text);
                objFtp.CurrentDirectory = "/";
                Main = new frmMain();
           
                //Set FTP Client in MAIN form
                Main.SetFtpClient(objFtp);

                //Show MAIN form and HIDE this one
                Main.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                //Display Error
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtHostName_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUserName_Click(object sender, EventArgs e)
        {

        }

        private void pnlNonTransparent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
