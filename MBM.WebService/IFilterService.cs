using MBM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MBM.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IFilterService" in both code and config file together.
    [ServiceContract]
    public interface IFilterService
    {
        [OperationContract]
        Filter GetMinMaxValues();

        [OperationContract]
        IEnumerable<string> GetSymbols();
    }
}
