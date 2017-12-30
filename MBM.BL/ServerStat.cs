using System;
using System.Data.SqlClient;

namespace MBM.BL
{
    public class ServerStat
    {
        public ServerStat()
        {

        }

        public ServerStat(SqlDataReader reader)
        {
            this.Time = DateTime.Parse(reader["EventTime"].ToString());
            this.CpuIdle = uint.Parse(reader["SystemIdle"].ToString());
            this.CpuSql = uint.Parse(reader["SQLProcessUtilization"].ToString());
            this.CpuOther = uint.Parse(reader["OtherProcessCPUUtilization"].ToString());
            this.MemoryUtilization = uint.Parse(reader["MemoryUtilization"].ToString());
            this.TotalSpace = uint.Parse(reader["TotalSpaceGigabytes"].ToString());
            this.AvailableSpace = uint.Parse(reader["AvailableSpaceGigabytes"].ToString());
        }


        DateTime Time { get; set; }
        private uint _cpuIdle;
        private uint _cpuSql;
        private uint _cpuOther;
        private uint _memoryUtilization;
        public uint CpuIdle
        {
            get
            {
                return _cpuIdle;
            }

            set
            {
                if (value > 1000) throw new ArgumentException("CpuIdle cannot be above 100");
                _cpuIdle = value;
            }
        }
        public uint CpuSql
        {
            get
            {
                return _cpuSql;
            }

            set
            {
                if (value > 1000) throw new ArgumentException("CpuSql cannot be above 100");
                _cpuSql = value;
            }
        }
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
        public uint TotalSpace { get; set; }
        public uint AvailableSpace { get; set; }
    }
}
