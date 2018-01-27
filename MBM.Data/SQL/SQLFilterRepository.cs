using MBM.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    /// <summary>Used for retrieving filter information from a server</summary>
    public class SQLFilterRepository : IFilterRepository
    {

        /// <summary>Gets filter values from a server</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve filter values from a server</exception>
        public Filter GetFilter()
        {
            try
            {
                Filter filter = new Filter();

                filter = GetMinMaxValues();
                filter.Symbols = GetSymbols() as List<string>;

                return filter;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve filter values from server", ex);
            }
        }

        /// <summary>Gets the min and max values from a server</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve min and max values from a server</exception>
        public Filter GetMinMaxValues()
        {
            Filter filter = new Filter();
            try
            {
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();

                using (conn)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "MinMaxValues";
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();
                        filter = new Filter(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve filter values server", ex);
            }

            return filter;
        }

        /// <summary>Gets the list of symbols from a server</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve symbols from a server</exception>
        public IEnumerable<string> GetSymbols()
        {
            List<string> symbols = new List<string>();

            try
            {
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();

                using (conn)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * From symbolsView";

                        SqlDataReader reader = cmd.ExecuteReader();

                        symbols.Add("all symbols");
                        while (reader.Read())
                        {
                            symbols.Add(reader["stock_symbol"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve filter symbols server", ex);
            }

            return symbols;
        }
    }
}
