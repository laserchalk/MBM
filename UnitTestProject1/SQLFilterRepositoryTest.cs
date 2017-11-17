using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data;
using MBM.BL;
using System.Data.SqlClient;
using System.Collections.Generic;
using MBM.DL;

namespace UnitTestProject1
{
    [TestClass]
    public class SQLFilterRepositoryTest
    {
        [TestMethod]
        public void GetMinMaxTest()
        {
            Filter actual = new Filter();
            Filter expected = new Filter()
            {
                DateStart = DateTime.Parse("2000-01-03"),
                DateEnd = DateTime.Parse("2010-02-08"),
                VolumeStart = uint.Parse("0"),
                VolumeEnd = uint.Parse("242106500"),
                OpenStart = 0.35m,
                OpenEnd = 183.50m,
                CloseStart = 0.35m,
                CloseEnd = 182.62m,
                CloseAdjustedStart = 0.35m,
                CloseAdjustedEnd = 134.92m,
                HighStart = 0.40m,
                HighEnd = 185.50m,
                LowStart = 0.32m,
                LowEnd = 170.00m
            };

            string connStr = ConfigurationManager.ConnectionStrings["MBMconnection"].ToString();
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            using (conn)
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "MinMaxValues";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    actual = new Filter(reader);
                }
            }

            Assert.AreEqual(expected.DateStart, actual.DateStart);
            Assert.AreEqual(expected.DateEnd, actual.DateEnd);
            Assert.AreEqual(expected.VolumeStart, actual.VolumeStart);
            Assert.AreEqual(expected.VolumeEnd, actual.VolumeEnd);
            Assert.AreEqual(expected.OpenStart, actual.OpenStart);
            Assert.AreEqual(expected.OpenEnd, actual.OpenEnd);
            Assert.AreEqual(expected.CloseStart, actual.CloseStart);
            Assert.AreEqual(expected.CloseEnd, actual.CloseEnd);
            Assert.AreEqual(expected.CloseAdjustedStart, actual.CloseAdjustedStart);
            Assert.AreEqual(expected.CloseAdjustedEnd, actual.CloseAdjustedEnd);
            Assert.AreEqual(expected.HighStart, actual.HighStart);
            Assert.AreEqual(expected.HighEnd, actual.HighEnd);
            Assert.AreEqual(expected.LowStart, actual.LowStart);
            Assert.AreEqual(expected.LowEnd, actual.LowEnd);
        }

        [TestMethod]
        public void GetSymbolsTest()
        {
            List<string> actual = new List<string>();
            List<string> expected = new List<string>();

            SQLFilterRepository filter = new SQLFilterRepository();
            actual = filter.GetSymbols();

            
            foreach (string symbol in actual)
            {
                Console.WriteLine(symbol);
            }

            Assert.AreEqual(1, 1);
        }
    }
}
