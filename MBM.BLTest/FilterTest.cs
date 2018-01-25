using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BL;

namespace MBM.BLTest
{
    [TestClass]
    public class FilterTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateInvalidDateTest()
        {
            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2001");
            filter.DateEnd = DateTime.Parse("1/1/2000");

            try
            {
                filter.Validate();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid date range. First date can't be greater than second date.", ex.Message);
                throw;
            }
        }
    }
}
