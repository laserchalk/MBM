using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.DL;
using MBM.BL;

namespace UnitTestProject1
{
    [TestClass]
    public class SQLRepositoryTest
    {

        [TestMethod]
        public void GetStockEntryTest()
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
    }
}
