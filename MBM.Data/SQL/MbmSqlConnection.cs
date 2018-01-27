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
        /// <summary>Initialises a new instance of MbmSqlConnection with default values</summary>
        /// <exception cref="Exception">Thrown when failed to instance of MbmSqlConnection</exception>
        public MbmSqlConnection()
        {
            try
            {
                string configConnectionString = ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString;
                SqlConnectionStringBuilder ConnectionStringBuilder = new SqlConnectionStringBuilder(configConnectionString);

                _datasource = ConnectionStringBuilder.DataSource.ToString();
                _initialCatalogue = ConnectionStringBuilder.InitialCatalog.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to initialise MbmSqlConnection", ex);
            }
        }


        /// <summary>Gets or sets DataSource using connection string in config file</summary>
        /// <exception cref="Exception">Thrown when failed to set DataSource</exception>
        public string DataSource
        {
            get
            {
                return _datasource;
            }
            set
            {
                try
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
                catch (Exception ex)
                {
                    throw new Exception("Failed to set DataSource", ex);
                }
            }
        }
        string _datasource;

        /// <summary>Gets or sets InitialCatalog using connection string in config file</summary>
        /// <exception cref="Exception">Thrown when failed to set InitialCatalog</exception>
        public string InitialCatalog
        {
            get
            {
                return _initialCatalogue;
            }
            set
            {
                try
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
                catch (Exception ex)
                {
                    throw new Exception("Failed to set InitialCatalog", ex);
                }
            }
        }
        string _initialCatalogue;

        /// <summary>Gets filter values from a server</summary>
        /// <exception cref="Exception">Thrown when failed to connect to database</exception>
        public static SqlConnection GetSqlConnection()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["MBMconnection"].ConnectionString);
                sqlConnection.Open();

                return sqlConnection;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to connect to database.", ex);
            }
        }
    }
}
