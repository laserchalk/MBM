using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MBM.BL;
using MBM.DL;

namespace MBM.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FilterService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FilterService.svc or FilterService.svc.cs at the Solution Explorer and start debugging.
    public class FilterService : IFilterService
    {
        public Filter GetMinMaxValues()
        {
            Filter response = new Filter();
            SQLFilterRepository filterRepo = new SQLFilterRepository();

            response = filterRepo.GetMinMaxValues();

            return response;
        }

        public IEnumerable<string> GetSymbols()
        {
            List<string> response = new List<string>();
            SQLFilterRepository filterRepo = new SQLFilterRepository();

            response = filterRepo.GetSymbols() as List<string>;

            return response;
        }
    }
}
