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
    [ServiceContract]
    public interface IStockService
    {
        [OperationContract]
        string AddStockEntry(StockEntry stock);

        [OperationContract]
        StockEntry GetStockEntry(uint id);

        [OperationContract]
        IEnumerable<StockEntry> GetStockEntries(Filter filter);

        [OperationContract]
        string UpdateStockEntry(StockEntry stock);

        [OperationContract]
        string DeleteStock(uint id);
    }
}
