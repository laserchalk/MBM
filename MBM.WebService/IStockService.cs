using MBM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MBM.WebService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.

    /// <summary>Interface for stock service</summary> 
    [ServiceContract]
    public interface IStockService
    {
        /// <summary>Adds a stock entry</summary> 
        [OperationContract]
        string AddStockEntry(StockEntry stock);

        /// <summary>Gets a stock entry</summary> 
        [OperationContract]
        StockEntry GetStockEntry(uint id);

        /// <summary>Gets a list of stock entries</summary> 
        [OperationContract]
        IEnumerable<StockEntry> GetStockEntries(Filter filter);

        /// <summary>Updates a stock entry</summary> 
        [OperationContract]
        string UpdateStockEntry(StockEntry stock);

        /// <summary>Deletes a stock entry</summary> 
        [OperationContract]
        string DeleteStock(uint id);
    }
}
