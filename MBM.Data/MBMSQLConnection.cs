using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    public class MbmSqlConnection
    {
        public static SqlConnection GetSqlConnection()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString);
                sqlConnection.Open();

                return sqlConnection;
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to connect to database.");
            }
        }

        public string DataSource
        {
            get
            {
                string configConnectionString = ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString;
                SqlConnectionStringBuilder connStringbuilder = new SqlConnectionStringBuilder(configConnectionString);

                return connStringbuilder.DataSource.ToString();
            }
            set
            {
                string configConnectionString = ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString;
                SqlConnectionStringBuilder connStringbuilder = new SqlConnectionStringBuilder(configConnectionString);
                connStringbuilder.DataSource = value;

                ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString = connStringbuilder.ConnectionString;
            }
        }
        public string InitialCatalog
        {
            get
            {
                string configConnectionString = ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString;
                SqlConnectionStringBuilder connStringbuilder = new SqlConnectionStringBuilder(configConnectionString);

                return connStringbuilder.InitialCatalog.ToString();
            }
            set
            {
                string configConnectionString = ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString;
                SqlConnectionStringBuilder connStringbuilder = new SqlConnectionStringBuilder(configConnectionString);
                connStringbuilder.InitialCatalog = value;

                ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString = connStringbuilder.ConnectionString;
            }
        }
    }
}
