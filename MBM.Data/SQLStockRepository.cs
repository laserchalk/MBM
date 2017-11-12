using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;
using System.Configuration;
using System.Data.SqlClient;

namespace MBM.DL
{
    public class SQLStockRepository : IStockRepository
    {
        public void AddStockEntry(StockEntry stock)
        {
            throw new NotImplementedException();
        }

        public void DeleteStock(uint id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockEntry> GetStockEntries(Filter filter)
        {
            throw new NotImplementedException();
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
                    cmd.CommandText = "DECLARE @id int = {0}; EXEC NyseGetByID @id;";
                    cmd.CommandText = string.Format(cmd.CommandText, id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        stockEntry.ID = uint.Parse(reader["stock_id"].ToString());
                        stockEntry.Exchange = reader["exchange"].ToString();
                        stockEntry.Symbol = reader["stock_symbol"].ToString();
                        stockEntry.Date = DateTime.Parse(reader["date"].ToString());
                        stockEntry.Volume = uint.Parse(reader["stock_volume"].ToString());
                        stockEntry.PriceHigh = decimal.Parse(reader["stock_price_high"].ToString());
                        stockEntry.PriceLow = decimal.Parse(reader["stock_price_low"].ToString());
                        stockEntry.PriceOpen = decimal.Parse(reader["stock_price_open"].ToString());
                        stockEntry.PriceClose = decimal.Parse(reader["stock_price_close"].ToString());
                        stockEntry.PriceCloseAdjusted = decimal.Parse(reader["stock_price_adj_close"].ToString());
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
