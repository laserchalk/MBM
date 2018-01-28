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

    /// <summary>Interface for filter web service</summary> 
    [ServiceContract]
    public interface IFilterService
    {
        /// <summary>Get all filter values</summary> 
        [OperationContract]
        Filter GetFilter();

        /// <summary>Get min and max values</summary> 
        [OperationContract]
        Filter GetMinMaxValues();

        /// <summary>Get list of symbols</summary> 
        [OperationContract]
        IEnumerable<string> GetSymbols();
    }
}
