using MBM.BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    public class ServerStatsRepository
    {
        public IEnumerable<ServerStat> GetServerStats()
        {
            List<ServerStat> serverStats = new List<ServerStat>();
            SqlConnection conn = MbmSqlConnection.GetSqlConnection();

            using (conn)
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"spServerStats";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ServerStat serverStat = new ServerStat(reader);
                        serverStats.Add(serverStat);
                    }
                }
            }

            return serverStats;
        }

        public ServerStat GetMostRecent()
        {
            ServerStat serverStat = new ServerStat();
            SqlConnection conn = MbmSqlConnection.GetSqlConnection();

            using (conn)
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"spServerStats";
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();
                    serverStat = new ServerStat(reader);
                }
            }

            return serverStat;
        }
    }
}
