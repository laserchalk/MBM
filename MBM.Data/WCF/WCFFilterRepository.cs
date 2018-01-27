using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBM.BL;
using MBM.DL.WCFFilterService;

namespace MBM.DL
{
    /// <summary>Used for retrieving filter information from a web service</summary>
    public class WCFFilterRepository : IFilterRepository
    {
        /// <summary>Initialises an instance of WCFFilterRepository</summary>
        public WCFFilterRepository()
        {
            Client = new FilterServiceClient();
        }

        /// <summary>Connection to web service</summary>
        private FilterServiceClient Client;


        /// <summary>Gets filter values from a web service</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve filter values from a web service</exception>
        public Filter GetFilter()
        {
            try
            {
                Filter filter = new Filter();

                filter = GetMinMaxValues();
                filter.Symbols = GetSymbols() as List<string>;

                return filter;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve filter values from web service", ex);
            }
        }


        /// <summary>Gets the min and max values from a web service</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve min & max values from a web service</exception>
        public Filter GetMinMaxValues()
        {
            try
            {
                Filter response = new Filter();

                response = Client.GetMinMaxValues();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve min & max values from web service", ex);
            }
        }

        /// <summary>Gets the list of symbols from a web service</summary>
        /// <exception cref="Exception">Thrown when failed to retrieve symbol values from a web service</exception>
        public IEnumerable<string> GetSymbols()
        {
            try
            {
                List<string> response = new List<string>();

                response = Client.GetSymbols().ToList<string>();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve symbol values from web service", ex);
            }
        }
    }
}
