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
        public void ValidateInvalidDateRange()
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


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateInvalidVolumeRange()
        {
            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2000");
            filter.DateEnd = DateTime.Parse("1/1/2001");
            filter.VolumeMin = 100;
            filter.VolumeMax = 99;

            try
            {
                filter.Validate();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid volume range. First volume can't be greater than second volume.", ex.Message);
                throw;
            }
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateInvalidOpenRange()
        {
            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2000");
            filter.DateEnd = DateTime.Parse("1/1/2001");
            filter.VolumeMin = 99;
            filter.VolumeMax = 100;
            filter.OpenMin.Amount = 100;
            filter.OpenMax.Amount = 99;

            try
            {
                filter.Validate();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid open price range. First price can't be greater than second price.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateInvalidCloseRange()
        {
            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2000");
            filter.DateEnd = DateTime.Parse("1/1/2001");
            filter.VolumeMin = 99;
            filter.VolumeMax = 100;
            filter.OpenMin.Amount = 99;
            filter.OpenMax.Amount = 100;
            filter.CloseMin.Amount = 100;
            filter.CloseMax.Amount = 99;

            try
            {
                filter.Validate();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid close price range. First price can't be greater than second price.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateInvalidCloseAdjustedRange()
        {
            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2000");
            filter.DateEnd = DateTime.Parse("1/1/2001");
            filter.VolumeMin = 99;
            filter.VolumeMax = 100;
            filter.OpenMin.Amount = 99;
            filter.OpenMax.Amount = 100;
            filter.CloseMin.Amount = 99;
            filter.CloseMax.Amount = 100;
            filter.CloseAdjustedMin.Amount = 100;
            filter.CloseAdjustedMax.Amount = 99;

            try
            {
                filter.Validate();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid close adjusted price range. First price can't be greater than second price.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateInvalidHighRange()
        {
            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2000");
            filter.DateEnd = DateTime.Parse("1/1/2001");
            filter.VolumeMin = 99;
            filter.VolumeMax = 100;
            filter.OpenMin.Amount = 99;
            filter.OpenMax.Amount = 100;
            filter.CloseMin.Amount = 99;
            filter.CloseMax.Amount = 100;
            filter.CloseAdjustedMin.Amount = 99;
            filter.CloseAdjustedMax.Amount = 100;
            filter.HighMin.Amount = 100;
            filter.HighMax.Amount = 99;

            try
            {
                filter.Validate();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid high price range. First price can't be greater than second price.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ValidateInvalidLowRange()
        {
            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2000");
            filter.DateEnd = DateTime.Parse("1/1/2001");
            filter.VolumeMin = 99;
            filter.VolumeMax = 100;
            filter.OpenMin.Amount = 99;
            filter.OpenMax.Amount = 100;
            filter.CloseMin.Amount = 99;
            filter.CloseMax.Amount = 100;
            filter.CloseAdjustedMin.Amount = 99;
            filter.CloseAdjustedMax.Amount = 100;
            filter.HighMin.Amount = 99;
            filter.HighMax.Amount = 100;
            filter.LowMin.Amount = 100;
            filter.LowMax.Amount = 99;

            try
            {
                filter.Validate();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid low price range. First price can't be greater than second price.", ex.Message);
                throw;
            }
        }

        [TestMethod]
        public void ValidateValidFilter()
        {
            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2000");
            filter.DateEnd = DateTime.Parse("1/1/2001");
            filter.VolumeMin = 99;
            filter.VolumeMax = 100;
            filter.OpenMin.Amount = 99;
            filter.OpenMax.Amount = 100;
            filter.CloseMin.Amount = 99;
            filter.CloseMax.Amount = 100;
            filter.CloseAdjustedMin.Amount = 99;
            filter.CloseAdjustedMax.Amount = 100;
            filter.HighMin.Amount = 99;
            filter.HighMax.Amount = 100;
            filter.LowMin.Amount = 99;
            filter.LowMax.Amount = 100;


            Assert.AreEqual(true, filter.Validate());
        }

        [TestMethod]
        public void ToStringTest()
        {
            string expected = "01/01/2000,01/01/2001,ABC,1,2,3,4,5,6,7,8,9,10,11,12";

            Filter filter = new Filter();
            filter.DateStart = DateTime.Parse("1/1/2000");
            filter.DateEnd = DateTime.Parse("1/1/2001");
            filter.SelectedSymbol = "ABC";
            filter.VolumeMin = 1;
            filter.VolumeMax = 2;
            filter.OpenMin.Amount = 3;
            filter.OpenMax.Amount = 4;
            filter.CloseMin.Amount = 5;
            filter.CloseMax.Amount = 6;
            filter.CloseAdjustedMin.Amount = 7;
            filter.CloseAdjustedMax.Amount = 8;
            filter.HighMin.Amount = 9;
            filter.HighMax.Amount = 10;
            filter.LowMin.Amount = 11;
            filter.LowMax.Amount = 12;

            Assert.AreEqual(expected, filter.ToString());
        }

        [TestMethod]
        public void Constructor()
        {
            
        }
    }
}
