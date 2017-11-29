﻿using MBM.BL;
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
    public class SQLFilterRepository : IFilterRepository
    {
        public Filter GetMinMaxValues()
        {
            Filter filter = new Filter();
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

            return filter;
        }

        public IEnumerable<string> GetSymbols()
        {
            List<string> symbols = new List<string>();
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

            return symbols;
        }
    }
}
