using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.BL;

namespace MBM.BLTest
{
    [TestClass]
    public class ServerStatTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SetInvalidCpuIdle()
        {
            ServerStat stats = new ServerStat();

            try
            {
                stats.CpuIdle = 101;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CpuIdle cannot be above 100", ex.Message);
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SetInvalidCpuOther()
        {
            ServerStat stats = new ServerStat();

            try
            {
                stats.CpuOther = 101;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CpuOther cannot be above 100", ex.Message);
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SetInvalidCpuSql()
        {
            ServerStat stats = new ServerStat();

            try
            {
                stats.CpuSql = 101;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("CpuSql cannot be above 100", ex.Message);
                throw;
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SetInvalidmemoryUtilization()
        {
            ServerStat stats = new ServerStat();

            try
            {
                stats.MemoryUtilization = 101;
            }
            catch (Exception ex)
            {
                Assert.AreEqual("MemoryUtilization cannot be above 100", ex.Message);
                throw;
            }

        }

        [TestMethod]
        public void SetValidCpuIdle()
        {
            ServerStat stats = new ServerStat();


            stats.CpuIdle = 100;

            Assert.AreEqual(uint.Parse("100"), stats.CpuIdle);
        }

        [TestMethod]
        public void SetValidCpuOther()
        {
            ServerStat stats = new ServerStat();


            stats.CpuOther = 100;

            Assert.AreEqual(uint.Parse("100"), stats.CpuOther);
        }

        [TestMethod]
        public void SetValidCpuSql()
        {
            ServerStat stats = new ServerStat();


            stats.CpuSql = 100;

            Assert.AreEqual(uint.Parse("100"), stats.CpuSql);
        }

        [TestMethod]
        public void SetValidMemoryUtilization()
        {
            ServerStat stats = new ServerStat();


            stats.MemoryUtilization = 100;

            Assert.AreEqual(uint.Parse("100"), stats.MemoryUtilization);
        }


        [TestMethod]
        public void ToStringTest()
        {
            string expected = "10,20,30,40,50,60";

            ServerStat stats = new ServerStat();
            stats.CpuIdle = 10;
            stats.CpuOther = 20;
            stats.CpuSql = 30;
            stats.MemoryUtilization = 40;
            stats.TotalSpace = 50;
            stats.AvailableSpace = 60;

            Assert.AreEqual(expected, stats.ToString());
        }

    }
}
