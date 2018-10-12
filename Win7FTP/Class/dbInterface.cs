using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Win7FTP.Class
{
    public class dbInterface
    {
        public SqlConnection conn;
        public SqlTransaction transaction;
        //constructor
        public dbInterface()
        {
            string strProject = "PC3356721\\SQLEXPRESS"; //Enter your SQL server instance name
            string strDatabase = "MyTestDB"; //Enter your database name
            string strUserID = "user1"; // Enter your SQL Server User Name
            string strPassword = "abc123"; // Enter your SQL Server Password
            string strconn = "data source=" + strProject + ";Persist Security Info=false;database=" + strDatabase + ";user id=" + strUserID + ";password=" + strPassword + ";Connection Timeout = 0";
            conn = new SqlConnection(strconn);
        }

        public bool openConnection()
        {
            try
            {
                conn.Close();
                conn.Open();
                transaction = conn.BeginTransaction();
                return true;

            }
            catch(Exception)
            {
                return false;
            }
           
        }

        public void closeConnection()
        {
            transaction.Commit();
            conn.Close();
        }

        public void errorTransaction()
        {
            transaction.Rollback();
            conn.Close();
        }

        protected void ExecuteSQL(string sSQL)
        {
            SqlCommand cmdDate = new SqlCommand(" SET DATEFORMAT dmy", conn, transaction);
            cmdDate.ExecuteNonQuery();
            SqlCommand cmd = new SqlCommand(sSQL, conn, transaction);
            cmd.ExecuteNonQuery();
        }

        protected void OnlyExecuteSQL(string sSQL)
        {
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.ExecuteNonQuery();
        }

        protected DataSet FillDataSet(DataSet dset, string sSQL, string tbl)
        {
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            try
            {
                adapter.Fill(dset, tbl);
            }
            finally
            {
                conn.Close();
            }
            return dset;

        }

        protected DataSet FillData(string sSQL, string sTable)
        {
            SqlCommand cmd = new SqlCommand(sSQL, conn, transaction);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, sTable);
            return ds;
        }

        protected SqlDataReader setDataReader(string sSQL)
        {
            SqlCommand cmd = new SqlCommand(sSQL, conn, transaction);
            cmd.CommandTimeout = 300;
            SqlDataReader rtnReader;
            rtnReader = cmd.ExecuteReader();
            return rtnReader;
        }

        /// <summary>
        /// Get year
        /// </summary>
        /// <returns></returns>
        public List<string> Select_Year()
        {

            //String thisYear = ConfigurationManager.AppSettings["ThisYear"];

            

            //Create a list to store the result
            List<string> list = new List<string>();//@size


            //Open connection
            if (this.openConnection() == true)
            {
                //Create Command
                string query = "SELECT * FROM RegLogUser where UserName = @user and Password = @pass";
                SqlCommand cmd = new SqlCommand(query, conn,transaction);
                //Create a data reader and Execute the command
                cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = "user1";
                cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = "abc123";
                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(dataReader["UserName"] + "");

                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.closeConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }



        /// <summary>
        /// Get period and id for this year
        /// </summary>
        /// <returns></returns>
        public List<string>[] Select_Period_id(String thisYear)
        {

            //String thisYear = ConfigurationManager.AppSettings["ThisYear"];

            string query = "SELECT * FROM period_tab where year_grp ='" + thisYear + "' order by id";

            //Create a list to store the result
            List<string>[] list = new List<string>[3];//@size
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.openConnection() == true)
            {
                //Create Command
                SqlCommand cmd = new SqlCommand(query, conn);
                //Create a data reader and Execute the command

                SqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["period_id"] + "");
                    list[2].Add(dataReader["seri_order"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.closeConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        public void Insert_Data(String Table,String names, String user,String pass,bool type)
        {
            
            //INSERT INTO `dprcs_db`.`data_rec_tab` (`id`, `period`, `date_of_rec`, `item_no`, `bill_id`, `sender_id`, `receiver_id`, `part_id`, `quantity`, `trans_code`, `product_type`, `operation_code`, `note`) VALUES ('0', 'T4-2018', '2018-04-21', 'AM', '000261', 'QH0001', 'QH0002', 'TRU_CAN_SP', '20', 'LTP', 'TRU', 'TRU_CAN', 'na');
            //open connection
            if (this.openConnection() == true)
            {


                SqlCommand cmdDate = new SqlCommand(" SET DATEFORMAT dmy", conn, transaction);
                cmdDate.ExecuteNonQuery();
                
                string query = "INSERT INTO " + Table + " (" + names + ") VALUES(@username,@pass,@logType )";
                //create command and assign the query and connection from the constructor
                SqlCommand cmd = new SqlCommand(query, conn, transaction);
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = user;
                cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = pass;
                cmd.Parameters.Add("@logtype", SqlDbType.Bit).Value = type;



                //Execute command
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception eee)
                {
                    MessageBox.Show("Data exist!");
                }
                //close connection
                this.closeConnection();
            }
        }
    }
}
