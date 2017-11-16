using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.DL;
using MBM.BL;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class SQLStockRepositoryTest
    {

        [TestMethod]
        public void GetStockEntryByIDTest()
        {
            //-- Arrange
            SQLStockRepository repository = new SQLStockRepository();
            StockEntry actual = new StockEntry();
            StockEntry expected = new StockEntry()
            {
                ID = uint.Parse(500.ToString()),
                Exchange = "NYSE",
                Symbol = "AEA",
                Date = DateTime.Parse("2008-02-14"),
                PriceOpen = 8.00m,
                PriceClose = 7.75m,
                PriceHigh = 8.06m,
                PriceLow = 7.51m,
                PriceCloseAdjusted = 6.33m,
                Volume = uint.Parse(892800.ToString())
            };

            //-- Act
            actual = repository.GetStockEntry(500);

            //-- Assert
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Exchange, actual.Exchange);
            Assert.AreEqual(expected.Symbol, actual.Symbol);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.PriceOpen, actual.PriceOpen);
            Assert.AreEqual(expected.PriceClose, actual.PriceClose);
            Assert.AreEqual(expected.PriceCloseAdjusted, actual.PriceCloseAdjusted);
            Assert.AreEqual(expected.PriceHigh, actual.PriceHigh);
            Assert.AreEqual(expected.PriceLow, actual.PriceLow);
            Assert.AreEqual(expected.Volume, actual.Volume);
        }

        [TestMethod]
        public void GetStockEntriesByFilterTest()
        {
            //-- Arrange
            SQLStockRepository stockRepo = new SQLStockRepository();
            List<StockEntry> actual;

            SQLFilterRepository filterRepo = new SQLFilterRepository();
            Filter filter = new Filter();

            int expected = 102580;

            //-- Act
            filter = filterRepo.GetMinMaxValues();
            actual = stockRepo.GetStockEntries(filter) as List<StockEntry>;

            //-- Assert
            Assert.AreEqual(expected, actual.Count);

        }
    }
}
