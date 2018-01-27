using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BL;

namespace MBM.BLTest
{
    [TestClass]
    public class PriceTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PriceInvalidTest()
        {
            //-- Arrange
            Price price = new Price();
            decimal inputAmount = -1.99m;
            string ExpectedResult = "Price cannot be negative";

            //-- Act
            try
            {    
                price.Amount = inputAmount;
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ExpectedResult, ex.Message.ToString());
                throw;
            }
        }
    }
}
