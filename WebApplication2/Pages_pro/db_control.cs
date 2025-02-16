﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebApplication2.Pages_pro
{
    public class db_control:Inter_connect_database
    {
        SqlConnection sqlCON;
        SqlCommand sqlcmd;
        SqlDataAdapter Adapter = new SqlDataAdapter();

        public db_control()
        {
            connect_db();//initial for class 
            
        }
        //1.connect database       
        static private string GetConnectionString()
        {
            string str = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\Albasheq\\Documents\\Visual Studio 2010\\Projects\\WebApplication1\\WebApplication1\\App_Data\\DB_PROJECT1.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
            
          return  str;
        }
        public void connect_db()
        {
            string con_str = GetConnectionString();
            sqlCON = new SqlConnection(con_str);
            sqlCON.Open();
        }
        //2.disconnect connection
        public void disconnect_db()
        {
            string con_str = GetConnectionString();
            sqlCON = new SqlConnection(con_str);
            if (sqlCON.State==ConnectionState.Open)
            {
                sqlCON.Close();
            }           
        }
        /************************************************************/             
        //2.insert third way (better) here 
        public int Execute1(string s)//orginal
        {
            try
            {
                if (sqlCON.State == ConnectionState.Closed)
                    sqlCON.Open();
                SqlCommand com = new SqlCommand(s, sqlCON);
                int res = com.ExecuteNonQuery();
                sqlCON.Close();
                return res;
            }
            catch (Exception)
            {
                sqlCON.Close();
                return 0;
            }
        }

        /***********************************************************/

















        public SqlDataReader Select(string s)
        {
            try
            {
                if (sqlCON.State == ConnectionState.Closed)
                    sqlCON.Open();
                SqlCommand MySelectCommand = new SqlCommand();
                MySelectCommand.Connection = sqlCON;
                MySelectCommand.CommandText = s;
                // Execute the SqlCommand and get the SqlDataReader.
                SqlDataReader DReader = MySelectCommand.ExecuteReader();
                return DReader;
            }
            catch (Exception)
            {
                sqlCON.Close();
                return null;
            }
        }
        /***********************************************************/
        public object Scalar_Select(string s)
        {
            try
            {
                if (sqlCON.State == ConnectionState.Closed)
                    sqlCON.Open();
                SqlCommand MySelectCommand = new SqlCommand(s, sqlCON);
                object x = MySelectCommand.ExecuteScalar();
                sqlCON.Close();
                return x;
            }
            catch (Exception)
            {
                sqlCON.Close();
                return null;
            }
        }
        /***********************************************************/
        public void Execute(string s)
        {            
            try
            {
                if (sqlCON.State == ConnectionState.Closed)
                    sqlCON.Open();
                SqlCommand cmd = sqlCON.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = s;
                                                                                //Adapter.InsertCommand=new SqlCommand(s,sqlCON);
                                                                                //  Adapter.InsertCommand.ExecuteNonQuery();
                int res = cmd.ExecuteNonQuery();
                                                                                 //com.Dispose();
                sqlCON.Close();               
            }
            catch (Exception)
            {
                sqlCON.Close();
                //return 0;
            }
        }
       
        /***********************************************************/
        public DataSet Dset_Select(string s)//الوضع المنفصل
        {
            try
            {
                if (sqlCON.State == ConnectionState.Closed)
                    sqlCON.Open();
                SqlDataAdapter dAdabter = new SqlDataAdapter(s, sqlCON);
                DataSet dSet = new DataSet();
                dAdabter.Fill(dSet);  // put dataAdapter in dataset.
                sqlCON.Close();
                return dSet;
            }
            catch (Exception)
            {
                sqlCON.Close();
                return null;
            }
        }
    }
}