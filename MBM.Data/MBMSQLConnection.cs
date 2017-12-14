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
        public MbmSqlConnection()
        {
            string configConnectionString = ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString;
            ConnectionStringBuilder = new SqlConnectionStringBuilder(configConnectionString);

            _datasource = ConnectionStringBuilder.DataSource.ToString();
            _initialCatalogue = ConnectionStringBuilder.InitialCatalog.ToString();
        }

        SqlConnectionStringBuilder ConnectionStringBuilder;
        string _datasource;
        string _initialCatalogue;



        public string DataSource
        {
            get
            {
                return _datasource;
            }
            set
            {
                _datasource = value;

                string configConnectionString = ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString;
                SqlConnectionStringBuilder connStringbuilder = new SqlConnectionStringBuilder(configConnectionString);
                connStringbuilder.DataSource = _datasource;
                

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings["MBMconnection"].ConnectionString = connStringbuilder.ConnectionString;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");


            }
        }
        public string InitialCatalog
        {
            get
            {
                return _initialCatalogue;
            }
            set
            {
                _initialCatalogue = value;

                string configConnectionString = ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString;
                SqlConnectionStringBuilder connStringbuilder = new SqlConnectionStringBuilder(configConnectionString);
                connStringbuilder.InitialCatalog = _initialCatalogue;
                

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings["MBMconnection"].ConnectionString = connStringbuilder.ConnectionString;
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
        }

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
    }
}
