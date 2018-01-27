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
        
        /// <summary>Gets a list of server statistics from database</summary>
        /// <exception cref="Exception">Thrown when failed to get server statistics</exception>
        public IEnumerable<ServerStat> GetServerStats()
        {
            try
            {
                List<ServerStat> serverStats = new List<ServerStat>();
                SqlConnection Connection = MbmSqlConnection.GetSqlConnection();

                using (Connection)
                {
                    using (SqlCommand cmd = Connection.CreateCommand())
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
            catch (Exception ex)
            {
                throw new Exception("Failed to get server statistics", ex);
            }
        }


        /// <summary>Gets most recent server statistics from database</summary>
        /// <exception cref="Exception">Thrown when failed to get server statistics</exception>
        public ServerStat GetMostRecent()
        {
            try
            {
                ServerStat serverStat = new ServerStat();
                SqlConnection Connection = MbmSqlConnection.GetSqlConnection();

                using (Connection)
                {
                    using (SqlCommand cmd = Connection.CreateCommand())
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
            catch (Exception ex)
            {
                throw new Exception("Failed to getmost recent server statistics", ex);
            }
        }
    }
}
