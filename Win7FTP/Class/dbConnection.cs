using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Win7FTP.Class
{
    // make this class abstract so that only dbProcess and dbUser class can access this class
    public abstract class dbConnection
    {
        public SqlConnection conn;
        public SqlTransaction transaction;

        public dbConnection()
        {
            string strProject = "PC3356721\\SQLEXPRESS"; //Enter your SQL server instance name
            string strDatabase = "MyTestDB"; //Enter your database name
            string strUserID = "user1"; // Enter your SQL Server User Name
            string strPassword = "abc123"; // Enter your SQL Server Password
            string strconn = "data source=" + strProject + ";Persist Security Info=false;database=" + strDatabase + ";user id=" + strUserID + ";password=" + strPassword + ";Connection Timeout = 0";
            conn = new SqlConnection(strconn);
        }

        public void openConnection()
        {
            conn.Close();
            conn.Open();
            transaction = conn.BeginTransaction();
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
    }
}
