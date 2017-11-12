using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BL;

namespace MBM.BLTest
{
    [TestClass]
    public class StockTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PriceHighOutOfRangeTest()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            decimal input = -1.00m;

            //-- Act
            try
            {
                stock.PriceHigh = input;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("PriceHigh cannot be negative", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PriceLowOutOfRangeTest()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            decimal input = -1.00m;

            //-- Act
            try
            {
                stock.PriceLow = input;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("PriceLow cannot be negative", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PriceOpenOutOfRangeTest()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            decimal input = -1.00m;

            //-- Act
            try
            {
                stock.PriceOpen = input;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("PriceOpen cannot be negative", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PriceCloseOutOfRangeTest()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            decimal input = -1.00m;

            //-- Act
            try
            {
                stock.PriceClose = input;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("PriceClose cannot be negative", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PriceCloseAdjustedOutOfRangeTest()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            decimal input = -1.00m;

            //-- Act
            try
            {
                stock.PriceCloseAdjusted = input;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("PriceCloseAdjusted cannot be negative", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
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
        [ExpectedException(typeof(ArgumentException))]
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

        [TestMethod]
        public void IDTest()
        {
            //-- Arrange
            StockEntry stock = new StockEntry();
            uint expected = 1;

            //-- Act
            stock.ID = expected;
            var actual = stock.ID;

            //-- Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
