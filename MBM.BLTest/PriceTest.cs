using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BL;

namespace MBM.BLTest
{
    [TestClass]
    public class PriceTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PriceInvalidTest()
        {
            //-- Arrange
            Price price = new Price();
            decimal inputAmount = -1.99m;

            //-- Act
            try
            {    
                price.Amount = inputAmount;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Price cannot be negative", ex.Message);
                throw;
            }
        }
    }
}
