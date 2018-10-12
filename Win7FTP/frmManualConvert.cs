using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Win7FTP.Class;

namespace Win7FTP
{
    public partial class frmManualConvert : Form
    {
        public frmManualConvert()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();
            //ofd1.InitialDirectory = @"F:\";
            if(ofd1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd1.FileName;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Fbd1 = new FolderBrowserDialog();
            //Fbd1.SelectedPath = @"F:\";
            if(Fbd1.ShowDialog()== DialogResult.OK)
            {
                textBox2.Text = Fbd1.SelectedPath;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox3.Enabled = true;

            }
            else
                textBox3.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String orgFile = textBox1.Text;
            String destinationFile;
            var onlyFileName = System.IO.Path.GetFileName(orgFile);
            try
            {
                if (checkBox1.Checked)
                {
                    destinationFile = textBox2.Text + "\\" + textBox3.Text;
                }
                else
                    destinationFile = textBox2.Text + "\\C_" + onlyFileName;

                ConvertCsv(orgFile, destinationFile);
                MessageBox.Show("File: " + onlyFileName + "Converted to " + System.IO.Path.GetFileName(destinationFile));
            } 
            catch(Exception eee)
            {
                MessageBox.Show("Some error! \n " + eee.ToString());
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

                        string col0 = row[0].Remove(0, 2);//remove first 2 zero.
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
                            for (i = 2; i < 16; i++)
                            {
                                string myStr = row[i];
                                myStr = myStr.TrimStart('0');
                                myStr = myStr.Length > 0 ? myStr : "0";
                                row[i] = myStr;
                            }
                        }
                        catch
                        {
                            row[i] = "1";
                            _error = true;
                        }
                        writer.WriteRow(row);
                    }
                    if (_error)
                        MessageBox.Show("File " + OriginalFilePath + " Some data fail to be converted!");
                    //rtbConsole.Text += DateTime.Now.ToString("MM/dd/yyyy hh:MM:ss tt") + ": File " + OriginalFilePath + " converted to " + DestinationFilePath + "\r\n";
                }
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

        private void frmManualConvert_Load(object sender, EventArgs e)
        {
            ReallyCenterToScreen();
        }

    }
}
