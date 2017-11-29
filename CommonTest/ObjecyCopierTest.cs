using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BL;
using Common;

namespace CommonTest
{
    [TestClass]
    public class ObjecyCopierTest
    {
        [TestMethod]
        public void CloneTest()
        {
            StockEntry stockOriginal = new StockEntry();
            stockOriginal.ID = 1;
            stockOriginal.Exchange = "NYSE";
            stockOriginal.Symbol = "AEA";
            stockOriginal.Date = DateTime.Parse("13/1/2010");
            stockOriginal.Volume = 205500;
            stockOriginal.PriceOpen.Amount = 4.42m;
            stockOriginal.PriceClose.Amount = 4.42m;
            stockOriginal.PriceCloseAdjusted.Amount = 4.42m;
            stockOriginal.PriceHigh.Amount = 4.42m;
            stockOriginal.PriceLow.Amount = 4.42m;

            StockEntry stockClone = new StockEntry();

            stockClone = ObjectCopier.Clone<StockEntry>(stockClone);

            Assert.IsFalse(Object.ReferenceEquals(stockOriginal, stockClone));
        }
    }
}
