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
                VolumeMin = uint.Parse("0"),
                VolumeMax = uint.Parse("242106500"),

            };
            expected.OpenMin.Amount = 0.35m;
            expected.OpenMax.Amount = 183.50m;
            expected.CloseMin.Amount = 0.35m;
            expected.CloseMax.Amount = 182.62m;
            expected.CloseAdjustedMin.Amount = 0.35m;
            expected.CloseAdjustedMax.Amount = 134.92m;
            expected.HighMin.Amount = 0.40m;
            expected.HighMax.Amount = 185.50m;
            expected.LowMin.Amount = 0.32m;
            expected.LowMax.Amount = 170.00m;

            SQLFilterRepository filterRepo = new SQLFilterRepository();
            actual = filterRepo.GetMinMaxValues();

            Assert.AreEqual(expected.DateStart, actual.DateStart);
            Assert.AreEqual(expected.DateEnd, actual.DateEnd);
            Assert.AreEqual(expected.VolumeMin, actual.VolumeMin);
            Assert.AreEqual(expected.VolumeMax, actual.VolumeMax);
            Assert.AreEqual(expected.OpenMin.Amount, actual.OpenMin.Amount);
            Assert.AreEqual(expected.OpenMax.Amount, actual.OpenMax.Amount);
            Assert.AreEqual(expected.CloseMin.Amount, actual.CloseMin.Amount);
            Assert.AreEqual(expected.CloseMax.Amount, actual.CloseMax.Amount);
            Assert.AreEqual(expected.CloseAdjustedMin.Amount, actual.CloseAdjustedMin.Amount);
            Assert.AreEqual(expected.CloseAdjustedMax.Amount, actual.CloseAdjustedMax.Amount);
            Assert.AreEqual(expected.HighMin.Amount, actual.HighMin.Amount);
            Assert.AreEqual(expected.HighMax.Amount, actual.HighMax.Amount);
            Assert.AreEqual(expected.LowMin.Amount, actual.LowMin.Amount);
            Assert.AreEqual(expected.LowMax.Amount, actual.LowMax.Amount);
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
