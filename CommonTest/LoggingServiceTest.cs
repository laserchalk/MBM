using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BL;
using Common;

namespace CommonTest
{
    [TestClass]
    public class LoggingServiceTest
    {
        [TestMethod]
        public void LogTest()
        {
            StockEntry stock = new StockEntry();
            LoggingService.Log(stock, "Log.txt");
        }
    }
}
