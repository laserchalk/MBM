using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.DL;
using MBM.BL;
using System.Collections.Generic;

namespace MBM.DLTest
{
    [TestClass]
    public class CSVStockRepositoryTest
    {
        [TestMethod]
        public void CSVGetStockEntriesTest()
        {
            CSVStockRepository stockRepo = new CSVStockRepository();
            SQLFilterRepository filterRepo = new SQLFilterRepository();
            Filter filter = new Filter();

            filter = filterRepo.GetMinMaxValues();
            stockRepo.GetStockEntries(filter);
        }


        [TestMethod]
        public void CSVAddStockEntryTest()
        {
            CSVStockRepository stockRepo = new CSVStockRepository();
            string actualResponse;
            string expectedResponse = "Stock entry saved";
            StockEntry insertStock = new StockEntry()
            {
                ID = uint.Parse(0.ToString()),
                Exchange = "NYSE",
                Symbol = "AEA",
                Date = DateTime.Parse("12/10/2017"),
                Volume = uint.Parse(892800.ToString())
            };
            insertStock.PriceOpen.Amount = 8.00m;
            insertStock.PriceClose.Amount = 7.75m;
            insertStock.PriceHigh.Amount = 8.06m;
            insertStock.PriceLow.Amount = 7.51m;
            insertStock.PriceCloseAdjusted.Amount = 6.33m;

            actualResponse = stockRepo.AddStockEntry(insertStock);

            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestMethod]
        public void CSVAddStockEntriesTest()
        {
            CSVStockRepository stockRepo = new CSVStockRepository();
            List<StockEntry> stockEntries = new List<StockEntry>();
            SQLStockRepository sqlStockRepo = new SQLStockRepository();
            SQLFilterRepository filterRepo = new SQLFilterRepository();
            Filter filter = new Filter();
            string actualResponse;
            string expectedResponse = "Stock entries saved";

            filter = filterRepo.GetMinMaxValues();
            stockEntries = sqlStockRepo.GetStockEntries(filter) as List<StockEntry>;

            actualResponse = stockRepo.AddStockEntries(stockEntries);

            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}
