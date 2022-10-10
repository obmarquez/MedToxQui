using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MedToxQui.Data
{
    public class DBConnection
    {
        protected IDbConnection conn;
        private string connSection = "ConnectionStrings";
        private string connString = "DefaultConnection";
        public String error = "";
        private string jsonFile = "appsettings.json";

        public DBConnection(int cnx = 1)
        {
            connString = GetConnString(connSection, connString, jsonFile);
        }

        private string GetConnString(string connSection, string connName, string jsonFile)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile(jsonFile, optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();
            return configuration.GetSection(connSection).GetSection(connName).Value;
        }

        public IDbConnection Open()
        {
            conn = new SqlConnection(connString);
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {

                    conn = null;
                    error += ex.ToString();

                }
            }
            else
            {
                error += "Connection.State";
            }
            return conn;
        }

        public async Task<IDbConnection> OpenAsync()
        {
            conn = new SqlConnection(connString);
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                try
                {
                    await ((SqlConnection)conn).OpenAsync();
                }
                catch (Exception ex)
                {
                    conn = null;
                }
            }
            else
            {
                conn = null;
            }
            return conn;
        }

        public void Close()
        {
            if (conn != null)
            {
                if (this.conn.State == ConnectionState.Open)
                {
                    this.conn.Close();
                }
            }
            conn = null;
        }
    }
}
