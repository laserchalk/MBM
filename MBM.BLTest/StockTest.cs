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
        public void ExchangeTooLongTest()
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
        [ExpectedException(typeof(Exception))]
        public void SymbolTooLongTest()
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

    }
}
