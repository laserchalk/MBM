using Common;
using System;
using System.Data.SqlClient;

namespace MBM.BL
{
    /// <summary>Holds information about server statistics</summary>
    public class ServerStat : EntityBase
    {
        /// <summary>Initialises a new instance of <see cref="ServerStat"/></summary>
        public ServerStat()
        {

        }

        /// <summary>Initialises a new instance of <see cref="ServerStat"/> using an SqlDataReader</summary>
        /// <exception cref="ArgumentException">Thrown if setting a value above 100</exception>
        public ServerStat(SqlDataReader reader)
        {
            try
            {
                this.Time = DateTime.Parse(reader["EventTime"].ToString());
                this.CpuIdle = uint.Parse(reader["SystemIdle"].ToString());
                this.CpuSql = uint.Parse(reader["SQLProcessUtilization"].ToString());
                this.CpuOther = uint.Parse(reader["OtherProcessCPUUtilization"].ToString());
                this.MemoryUtilization = uint.Parse(reader["MemoryUtilization"].ToString());
                this.TotalSpace = uint.Parse(reader["TotalSpaceGigabytes"].ToString());
                this.AvailableSpace = uint.Parse(reader["AvailableSpaceGigabytes"].ToString());
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to initialise ServerStat using SqlDataReader");
            }
        }


        /// <summary>Gets or sets Time</summary>
        DateTime Time { get; set; }
        
        private uint _cpuIdle;
        /// <summary>Gets or sets CpuIdle</summary>
        /// <exception cref="ArgumentException">Thrown if setting a value above 100</exception>
        public uint CpuIdle
        {
            get
            {
                return _cpuIdle;
            }

            set
            {
                if (value > 100) throw new ArgumentException("CpuIdle cannot be above 100");
                _cpuIdle = value;
            }
        }

        private uint _cpuSql;
        /// <summary>Gets or sets CpuSql</summary>
        /// <exception cref="ArgumentException">Thrown if setting a value above 100</exception>
        public uint CpuSql
        {
            get
            {
                return _cpuSql;
            }

            set
            {
                if (value > 100) throw new ArgumentException("CpuSql cannot be above 100");
                _cpuSql = value;
            }
        }

        private uint _cpuOther;
        /// <summary>Gets or sets CpuOther</summary>
        /// <exception cref="ArgumentException">Thrown if setting a value above 100</exception>
        public uint CpuOther
        {
            get
            {
                return _cpuOther;
            }

            set
            {
                if (value > 1000) throw new ArgumentException("CpuOther cannot be above 100");
                _cpuOther = value;
            }
        }

        private uint _memoryUtilization;
        /// <summary>Gets or sets MemoryUtilization</summary>
        /// <exception cref="ArgumentException">Thrown if setting a value above 100</exception>
        public uint MemoryUtilization
        {
            get
            {
                return _memoryUtilization;
            }

            set
            {
                if (value > 1000) throw new ArgumentException("MemoryUtilization cannot be above 100");
                _memoryUtilization = value;
            }
        }

        /// <summary>Gets or sets TotalSpace</summary>
        public uint TotalSpace { get; set; }

        /// <summary>Gets or sets AvailableSpace</summary>
        public uint AvailableSpace { get; set; }

        /// <summary>Returns the ServerStat as a string</summary>
        public override string ToString()
        {
            string serverInformation;

            serverInformation = this.CpuIdle.ToString() + ",";
            serverInformation += this.CpuOther.ToString() + ",";
            serverInformation += this.CpuSql.ToString() + ",";
            serverInformation += this.MemoryUtilization.ToString() + ",";
            serverInformation += this.TotalSpace.ToString() + ",";
            serverInformation += this.AvailableSpace.ToString();

            return serverInformation;
        }
    }
}
