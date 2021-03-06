﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace MBM.DL
{
    /// <summary>Used for retrieving stock information from a server</summary>
    public class SQLStockRepository : IStockRepository
    {
        /// <summary>Adds a stock entry to a server</summary>
        /// <exception cref="Exception">Thrown when failed to add stock entry to database</exception>
        public string AddStockEntry(StockEntry stock)
        {
            try
            {
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();
                SqlParameter serverResponse = new SqlParameter();

                using (conn)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = @"spInsertStockEntry";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("exchange", stock.Exchange);
                        cmd.Parameters.AddWithValue("stock_symbol", stock.Symbol);
                        cmd.Parameters.AddWithValue("date", stock.Date);
                        cmd.Parameters.AddWithValue("stock_volume", int.Parse(stock.Volume.ToString()));
                        cmd.Parameters.AddWithValue("stock_price_open", stock.PriceOpen.Amount);
                        cmd.Parameters.AddWithValue("stock_price_close", stock.PriceClose.Amount);
                        cmd.Parameters.AddWithValue("stock_price_adj_close", stock.PriceCloseAdjusted.Amount);
                        cmd.Parameters.AddWithValue("stock_price_high", stock.PriceHigh.Amount);
                        cmd.Parameters.AddWithValue("stock_price_low", stock.PriceLow.Amount);

                        serverResponse = new SqlParameter("@response", SqlDbType.NVarChar, 1000);
                        serverResponse.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(serverResponse);

                        cmd.ExecuteNonQuery();
                        Console.WriteLine(serverResponse.Value.ToString());
                    }
                }

                return serverResponse.Value.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add stock entry to database", ex);
            }
        }

        /// <summary>Deletes a stock entry from a server</summary>
        /// <exception cref="Exception">Thrown when failed to add stock entry to database</exception>
        public string DeleteStock(uint id)
        {
            try
            {
                int numberOfRowsAffected;
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();
                string serverResponse;

                using (conn)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = @"spDeleteStockEntry";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("id", int.Parse(id.ToString()));

                        numberOfRowsAffected = cmd.ExecuteNonQuery();

                        if (numberOfRowsAffected == 1)
                        {
                            serverResponse = "Stock entry " + id + " deleted";
                        }
                        else
                        {
                            serverResponse = "Failed to delete because entry doesn't exist";
                        }
                    }
                }

                return serverResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete stock entry to database", ex);
            }
        }

        /// <summary>Gets a list of stock entries from a server</summary>
        /// <exception cref="Exception">Thrown when failed to get stock entries to database</exception>
        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            try
            {
                List<StockEntry> stockEntries = new List<StockEntry>();
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();

                using (conn)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = @"GetStockEntriesFilter";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("DateStart", filter.DateStart);
                        cmd.Parameters.AddWithValue("DateEnd", filter.DateEnd);
                        cmd.Parameters.AddWithValue("Symbol", filter.SelectedSymbol);
                        cmd.Parameters.AddWithValue("VolumeStart", int.Parse(filter.VolumeMin.ToString()));
                        cmd.Parameters.AddWithValue("VolumeEnd", int.Parse(filter.VolumeMax.ToString()));
                        cmd.Parameters.AddWithValue("OpenStart", filter.OpenMin.Amount);
                        cmd.Parameters.AddWithValue("OpenEnd", filter.OpenMax.Amount);
                        cmd.Parameters.AddWithValue("CloseStart", filter.CloseMin.Amount);
                        cmd.Parameters.AddWithValue("CloseEnd", filter.CloseMax.Amount);
                        cmd.Parameters.AddWithValue("CloseAdjustedStart", filter.CloseAdjustedMin.Amount);
                        cmd.Parameters.AddWithValue("CloseAdjustedEnd", filter.CloseAdjustedMax.Amount);
                        cmd.Parameters.AddWithValue("HighStart", filter.HighMin.Amount);
                        cmd.Parameters.AddWithValue("HighEnd", filter.HighMax.Amount);
                        cmd.Parameters.AddWithValue("LowStart", filter.LowMin.Amount);
                        cmd.Parameters.AddWithValue("LowEnd", filter.LowMax.Amount);


                        SqlDataReader reader = cmd.ExecuteReader();


                        while (reader.Read())
                        {
                            StockEntry stockEntry = new StockEntry(reader);
                            stockEntries.Add(stockEntry);
                        }
                    }
                }

                return stockEntries;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stock entries from database", ex);
            }
        }

        /// <summary>Gets a stock entry from a server</summary>
        /// <exception cref="Exception">Thrown when failed to get stock entry from database</exception>
        public StockEntry GetStockEntry(uint id)
        {
            try
            {
                StockEntry stockEntry = new StockEntry();
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();

                using (conn)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "NyseGetByID";
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter stock_id = new SqlParameter("id", SqlDbType.BigInt);
                        stock_id.Value = id;
                        cmd.Parameters.Add(stock_id);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            stockEntry = new StockEntry(reader);
                        }

                    }
                }

                return stockEntry;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get stock entry from database", ex);
            }
        }

        /// <summary>Updates values for a stock entry on a server</summary>
        /// <exception cref="Exception">Thrown when failed to upadte stock entry in database</exception>
        public string UpdateStockEntry(StockEntry stock)
        {
            try
            {
                SqlConnection conn = MbmSqlConnection.GetSqlConnection();
                string serverResponse;
                SqlParameter response = new SqlParameter();

                using (conn)
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = @"spUpdateStockEntry";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("stock_id", int.Parse(stock.ID.ToString()));
                        cmd.Parameters.AddWithValue("exchange", stock.Exchange);
                        cmd.Parameters.AddWithValue("stock_symbol", stock.Symbol);
                        cmd.Parameters.AddWithValue("date", stock.Date);
                        cmd.Parameters.AddWithValue("stock_volume", int.Parse(stock.Volume.ToString()));
                        cmd.Parameters.AddWithValue("stock_price_open", stock.PriceOpen.Amount);
                        cmd.Parameters.AddWithValue("stock_price_close", stock.PriceClose.Amount);
                        cmd.Parameters.AddWithValue("stock_price_adj_close", stock.PriceCloseAdjusted.Amount);
                        cmd.Parameters.AddWithValue("stock_price_high", stock.PriceHigh.Amount);
                        cmd.Parameters.AddWithValue("stock_price_low", stock.PriceLow.Amount);

                        response = new SqlParameter("@response", SqlDbType.NVarChar, 1000);
                        response.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(response);

                        cmd.ExecuteNonQuery();

                        serverResponse = response.Value.ToString();
                    }
                }

                return serverResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upadte stock entry in database", ex);
            }
        }
    }
}
