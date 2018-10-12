namespace Win7FTP
{
    partial class frmLogInSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogInSetting));
            this.txtLogIn = new System.Windows.Forms.TextBox();
            this.btnLogInSetting = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtLogIn
            // 
            this.txtLogIn.Location = new System.Drawing.Point(149, 46);
            this.txtLogIn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtLogIn.Name = "txtLogIn";
            this.txtLogIn.PasswordChar = '*';
            this.txtLogIn.Size = new System.Drawing.Size(96, 22);
            this.txtLogIn.TabIndex = 24;
            this.txtLogIn.UseSystemPasswordChar = true;
            this.txtLogIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLogIn_KeyDown);
            // 
            // btnLogInSetting
            // 
            this.btnLogInSetting.BackColor = System.Drawing.Color.Transparent;
            this.btnLogInSetting.Location = new System.Drawing.Point(149, 76);
            this.btnLogInSetting.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogInSetting.Name = "btnLogInSetting";
            this.btnLogInSetting.Size = new System.Drawing.Size(87, 28);
            this.btnLogInSetting.TabIndex = 23;
            this.btnLogInSetting.Text = "Enter";
            this.btnLogInSetting.UseVisualStyleBackColor = false;
            this.btnLogInSetting.Click += new System.EventHandler(this.btnLogInSetting_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Password:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(91, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(190, 19);
            this.lblTitle.TabIndex = 26;
            this.lblTitle.Text = "Setting Secure Log In! ";
            // 
            // frmLogInSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 117);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtLogIn);
            this.Controls.Add(this.btnLogInSetting);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLogInSetting";
            this.Text = "Password Confirm:";
            this.Load += new System.EventHandler(this.frmLogInSetting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLogIn;
        private System.Windows.Forms.Button btnLogInSetting;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTitle;
    }
}