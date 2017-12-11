using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Data;
using MBM.BL;
using System.Data.SqlClient;
using System.Collections.Generic;
using MBM.DL;

namespace MBM.DLTest
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

            };
            expected.OpenStart.Amount = 0.35m;
            expected.OpenEnd.Amount = 183.50m;
            expected.CloseStart.Amount = 0.35m;
            expected.CloseEnd.Amount = 182.62m;
            expected.CloseAdjustedStart.Amount = 0.35m;
            expected.CloseAdjustedEnd.Amount = 134.92m;
            expected.HighStart.Amount = 0.40m;
            expected.HighEnd.Amount = 185.50m;
            expected.LowStart.Amount = 0.32m;
            expected.LowEnd.Amount = 170.00m;

            SQLFilterRepository filterRepo = new SQLFilterRepository();
            actual = filterRepo.GetMinMaxValues();

            Assert.AreEqual(expected.DateStart, actual.DateStart);
            Assert.AreEqual(expected.DateEnd, actual.DateEnd);
            Assert.AreEqual(expected.VolumeStart, actual.VolumeStart);
            Assert.AreEqual(expected.VolumeEnd, actual.VolumeEnd);
            Assert.AreEqual(expected.OpenStart.Amount, actual.OpenStart.Amount);
            Assert.AreEqual(expected.OpenEnd.Amount, actual.OpenEnd.Amount);
            Assert.AreEqual(expected.CloseStart.Amount, actual.CloseStart.Amount);
            Assert.AreEqual(expected.CloseEnd.Amount, actual.CloseEnd.Amount);
            Assert.AreEqual(expected.CloseAdjustedStart.Amount, actual.CloseAdjustedStart.Amount);
            Assert.AreEqual(expected.CloseAdjustedEnd.Amount, actual.CloseAdjustedEnd.Amount);
            Assert.AreEqual(expected.HighStart.Amount, actual.HighStart.Amount);
            Assert.AreEqual(expected.HighEnd.Amount, actual.HighEnd.Amount);
            Assert.AreEqual(expected.LowStart.Amount, actual.LowStart.Amount);
            Assert.AreEqual(expected.LowEnd.Amount, actual.LowEnd.Amount);
        }

        [TestMethod]
        public void GetSymbolsTest()
        {
            List<string> actual = new List<string>();
            List<string> expected = new List<string>();
            expected.Add("all symbols");
            expected.Add("AMG");
            expected.Add("ATT");
            expected.Add("AMP");
            expected.Add("AFG");
            expected.Add("ATU");
            expected.Add("AVB");
            expected.Add("ARG");
            expected.Add("AEO");
            expected.Add("ASI");
            expected.Add("ADI");
            expected.Add("ARM");
            expected.Add("ARD");
            expected.Add("AGD");
            expected.Add("ASP");
            expected.Add("AGM");
            expected.Add("ABB");
            expected.Add("ASF");
            expected.Add("AHS");
            expected.Add("ARK");
            expected.Add("AKP");
            expected.Add("APF");
            expected.Add("APL");
            expected.Add("AEA");
            expected.Add("ALV");
            expected.Add("ATV");
            expected.Add("AOL");
            expected.Add("AHD");
            expected.Add("ADY");
            expected.Add("ATE");
            expected.Add("ASG");
            expected.Add("ALZ");
            expected.Add("AVA");
            expected.Add("AXE");
            expected.Add("AHT");
            expected.Add("AEG");
            expected.Add("AIT");
            expected.Add("AKT");
            expected.Add("AVK");
            expected.Add("AVT");
            expected.Add("AMX");
            expected.Add("ACM");
            expected.Add("AEB");
            expected.Add("ABK");
            expected.Add("ABM");
            expected.Add("AFB");
            expected.Add("AWH");
            expected.Add("APD");
            expected.Add("ACH");
            expected.Add("ARO");
            expected.Add("ASA");
            expected.Add("AA");

            SQLFilterRepository filter = new SQLFilterRepository();
            actual = filter.GetSymbols() as List<string>;

            
            foreach (string symbol in actual)
            {
                Console.WriteLine(symbol);
            }

            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
