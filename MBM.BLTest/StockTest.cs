using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BL;

namespace MBM.BLTest
{
    [TestClass]
    public class StockTest
    {

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ExchangeSetTooLongTest()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            string input = "ABCDE";

            //-- Act
            try
            {
                stock.Exchange = input;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Exchange must have less than 5 characters", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void ExchangeSetFourOrLess()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            string input = "ABCD";

            //-- Act
            stock.Exchange = input;

            Assert.AreEqual("ABCD", stock.Exchange);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SymbolSetTooLongTest()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            string input = "ABCD";

            //-- Act
            try
            {
                stock.Symbol = input;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Symbol must have less than 4 characters", ex.Message);
                throw;
            }
        }


        [TestMethod]
        public void SymbolSetThreeOrLess()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            string input = "ABC";

            //-- Act
            stock.Symbol = input;

            Assert.AreEqual("ABC", stock.Symbol);

        }

        [TestMethod]
        public void ToStringTest()
        {
            string expected = "7,ABCD,ABC,1,01/01/2000,2,3,4,5,6";

            StockEntry stock = new StockEntry();
            stock.ID = 7;
            stock.Exchange = "ABCD";
            stock.Symbol = "ABC";
            stock.Volume = 1;
            stock.Date = DateTime.Parse("01/01/2000");
            stock.PriceHigh.Amount = 2;
            stock.PriceLow.Amount = 3;
            stock.PriceOpen.Amount = 4;
            stock.PriceClose.Amount = 5;
            stock.PriceCloseAdjusted.Amount = 6;

            Assert.AreEqual(expected, stock.ToString());
        }

        [TestMethod]
        public void ConstructerString()
        {

            StockEntry stock = new StockEntry("7,ABCD,ABC,1,01/01/2000,2,3,4,5,6");

            Assert.AreEqual(uint.Parse("7"), stock.ID);
            Assert.AreEqual("ABCD", stock.Exchange);
            Assert.AreEqual("ABC", stock.Symbol);
            Assert.AreEqual(uint.Parse("1"), stock.Volume);
            Assert.AreEqual(DateTime.Parse("01/01/2000"), stock.Date);
            Assert.AreEqual(uint.Parse("2"), stock.PriceHigh.Amount);
            Assert.AreEqual(uint.Parse("3"), stock.PriceLow.Amount);
            Assert.AreEqual(uint.Parse("4"), stock.PriceOpen.Amount);
            Assert.AreEqual(uint.Parse("5"), stock.PriceClose.Amount);
            Assert.AreEqual(uint.Parse("6"), stock.PriceCloseAdjusted.Amount);
        }


    }
}
