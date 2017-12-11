using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBM.DL;
using System.Data.SqlClient;
using System.Data;

namespace MBM.DLTest
{
    [TestClass]
    public class MbmSqlConnectionTest
    {

        [TestMethod]
        public void SqlConnectionTest()
        {
            ConnectionState actual;
            ConnectionState expected = ConnectionState.Open;

            SqlConnection conn = MbmSqlConnection.GetSqlConnection();

            using (conn)
            {
                actual = conn.State;
            }

            Assert.AreEqual(expected, actual);
        }
    }
}
