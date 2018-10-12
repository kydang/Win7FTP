using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Win7FTP.Class
{
    class dbProcess:dbConnection
    {
        public bool isRecordExists(string fldName, string tblName, string param)
        {
            string sSQL = "SELECT " + fldName + " From " + tblName + " WHERE " + fldName + "= '" + param + "'";
            SqlDataReader dr = setDataReader(sSQL);
            dr.Read();
            bool isExists = ((dr.HasRows == true) ? true : false);
            dr.Close();
            dr.Dispose();
            return isExists;
        }

        public SqlDataReader getFields(string fldName, string tblName, string condition)
        {
            string sSQL = "SELECT " + fldName + " FROM " + tblName + " " + condition;
            return setDataReader(sSQL);
        }

        public void addRecord(string tblName, string values)
        {
            string sSQL = "INSERT INTO " + tblName + " VALUES(" + values + ")";
            ExecuteSQL(sSQL);
        }

        public void UpdateRecord(string tblName, string values)
        {
            string sSQL = "UPDATE " + tblName + " SET " + values;
            ExecuteSQL(sSQL);
        }

        public void DeleteRecord(string tblName, string values)
        {
            string sSQL = "DELETE FROM " + tblName + " " + values;
            ExecuteSQL(sSQL);
        }

        public void executeSP(string SPName, string condition)
        {
            string sSQL = "EXEC " + SPName + " " + condition;
            ExecuteSQL(sSQL);
        }

        public SqlDataReader getSP_Record(string SPName, string condition)
        {
            string sSQL = "EXEC " + SPName + " " + condition;
            return setDataReader(sSQL);
        }

        public void finishSave()
        {
            closeConnection();
            MessageBox.Show("Data Saved Successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public string getValue(string val, string tbl, string condition)
        {
            string sSQL = "SELECT " + val + " FROM " + tbl + " WHERE " + condition;
            SqlDataReader dr = setDataReader(sSQL);
            string sValue = "";
            dr.Read();
            if (dr.HasRows == true)
            {
                sValue = ((dr[0].ToString().Trim() == "Null" || dr[0].ToString().Trim() == "") ? "" : dr[0].ToString().Trim());
            }
            else sValue = "";
            dr.Close();
            dr.Dispose();
            return sValue;
        }

        public DataSet FillDataWithOpenConn(string sSQL, string sTable)
        {
            return FillData(sSQL, sTable);
        }         

    }
}
