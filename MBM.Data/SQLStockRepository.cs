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
    public class SQLStockRepository : IStockRepository
    {
        int numberOfRowsAffected;
        public int AddStockEntry(StockEntry stock)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MBMconnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

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

                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return numberOfRowsAffected;
        }

        public void DeleteStock(uint id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            List<StockEntry> stockEntries = new List<StockEntry>();

            string connStr = ConfigurationManager.ConnectionStrings["MBMconnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            using (conn)
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"GetStockEntriesFilter";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("DateStart", filter.DateStart);
                    cmd.Parameters.AddWithValue("DateEnd", filter.DateEnd);
                    cmd.Parameters.AddWithValue("Symbol", filter.SelectedSymbol);
                    cmd.Parameters.AddWithValue("VolumeStart", int.Parse(filter.VolumeStart.ToString()));
                    cmd.Parameters.AddWithValue("VolumeEnd", int.Parse(filter.VolumeEnd.ToString()));
                    cmd.Parameters.AddWithValue("OpenStart", filter.OpenStart.Amount);
                    cmd.Parameters.AddWithValue("OpenEnd", filter.OpenEnd.Amount);
                    cmd.Parameters.AddWithValue("CloseStart", filter.CloseStart.Amount);
                    cmd.Parameters.AddWithValue("CloseEnd", filter.CloseEnd.Amount);
                    cmd.Parameters.AddWithValue("CloseAdjustedStart", filter.CloseAdjustedStart.Amount);
                    cmd.Parameters.AddWithValue("CloseAdjustedEnd", filter.CloseAdjustedEnd.Amount);
                    cmd.Parameters.AddWithValue("HighStart", filter.HighStart.Amount);
                    cmd.Parameters.AddWithValue("HighEnd", filter.HighEnd.Amount);
                    cmd.Parameters.AddWithValue("LowStart", filter.LowStart.Amount);
                    cmd.Parameters.AddWithValue("LowEnd", filter.LowEnd.Amount);

                    
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

        public StockEntry GetStockEntry(uint id)
        {
            StockEntry stockEntry = new StockEntry();

            string connStr = ConfigurationManager.ConnectionStrings["MBMconnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

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

        public void UpdateStockEntries(IEnumerable<StockEntry> stockEntries)
        {
            throw new NotImplementedException();
        }

        public void UpdateStockEntry(StockEntry stock)
        {
            throw new NotImplementedException();
        }
    }
}