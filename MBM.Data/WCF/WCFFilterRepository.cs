using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;
using MBM.DL.WCFFilterService;

namespace MBM.DL
{
    public class WCFFilterRepository : IFilterRepository
    {
        public WCFFilterRepository()
        {
            _client = new FilterServiceClient();
        }

        private FilterServiceClient _client;

        public Filter GetMinMaxValues()
        {
            Filter response = new Filter();

            response = _client.GetMinMaxValues();

            return response;
        }

        public IEnumerable<string> GetSymbols()
        {
            List<string> response = new List<string>();

            response = _client.GetSymbols().ToList<string>();

            return response;
        }

        public static implicit operator WCFFilterRepository(SQLFilterRepository v)
        {
            throw new NotImplementedException();
        }
    }
}
