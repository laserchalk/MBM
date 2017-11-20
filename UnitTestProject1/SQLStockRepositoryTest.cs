using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.DL;
using MBM.BL;
using System.Collections.Generic;

namespace MBM.DLTest
{
    [TestClass]
    public class SQLStockRepositoryTest
    {

        [TestMethod]
        public void AddStockEntryTest()
        {
            SQLStockRepository stockRepo = new SQLStockRepository();

            StockEntry insertStock = new StockEntry()
            {
                ID = uint.Parse(500.ToString()),
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

            int actual = stockRepo.AddStockEntry(insertStock);

            Assert.AreEqual(1, actual);
        }

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
                Volume = uint.Parse(892800.ToString())
            };
            expected.PriceOpen.Amount = 8.00m;
            expected.PriceClose.Amount = 7.75m;
            expected.PriceHigh.Amount = 8.06m;
            expected.PriceLow.Amount = 7.51m;
            expected.PriceCloseAdjusted.Amount = 6.33m;

            //-- Act
            actual = repository.GetStockEntry(500);

            //-- Assert
            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Exchange, actual.Exchange);
            Assert.AreEqual(expected.Symbol, actual.Symbol);
            Assert.AreEqual(expected.Date, actual.Date);
            Assert.AreEqual(expected.PriceOpen.Amount, actual.PriceOpen.Amount);
            Assert.AreEqual(expected.PriceClose.Amount, actual.PriceClose.Amount);
            Assert.AreEqual(expected.PriceCloseAdjusted.Amount, actual.PriceCloseAdjusted.Amount);
            Assert.AreEqual(expected.PriceHigh.Amount, actual.PriceHigh.Amount);
            Assert.AreEqual(expected.PriceLow.Amount, actual.PriceLow.Amount);
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
