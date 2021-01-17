using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace workArrange.App_Start
{
    public class SqlHelper
    {
        private static readonly string mystr = System.Configuration.ConfigurationManager.ConnectionStrings["workArrange"].ConnectionString;
        private SqlConnection con = new SqlConnection(mystr);
        SqlDataReader sdr;
        public SqlHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        public SqlConnection GetCon()
        {
            return con;
        }

        public SqlDataReader QueryOperationProc(String StrQueryCommand)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand
            {
                Connection = con,
                CommandText = StrQueryCommand
            };
            if (sdr != null)
                sdr.Close();
            sdr = cmd.ExecuteReader();
            return sdr;
        }
        public DataTable Query(String StrQueryCommand)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(StrQueryCommand, con);
            sda.Fill(dt);
            return dt;
        }
        public bool ExeNoQuery(String StrCmd)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand
            {
                Connection = con,
                CommandText = StrCmd
            };
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;

        }
        public void CloseConn()
        {
            if (con.State != System.Data.ConnectionState.Closed)
                con.Close();
        }

        public bool ExeNoQueryProc(String cmdName, SqlParameter[] ps)
        {
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand
            {
                Connection = con,
                CommandText = cmdName,
                CommandType = System.Data.CommandType.StoredProcedure
            };
            foreach (SqlParameter p in ps)
            {
                cmd.Parameters.Add(p);
            }
            try
            {
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public DataTable QueryProc(String cmdStr, SqlParameter[] ps)
        {
            DataTable dt = new DataTable();
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            SqlCommand cmd = new SqlCommand
            {
                Connection = con,
                CommandType = CommandType.StoredProcedure,
                CommandText = cmdStr
            };
            if (ps != null)
            {
                foreach (SqlParameter p in ps)
                {
                    cmd.Parameters.Add(p);
                }
            }
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            con.Close();
            return dt;
        }
    }
}