using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    public class StockRepositoryFactory
    {
        /// <summary>
        /// Gets a stock repository. Values "WCF", "SQL", "CSV"
        /// </summary>
        public static IStockRepository GetRepository(string repositoryType)
        {
            IStockRepository repo = null;

            switch (repositoryType)
            {
                case "WCF":
                    repo = new WCFStockRepository();
                    break;
                case "SQL":
                    repo = new SQLStockRepository();
                    break;
                case "CSV":
                    repo = new CSVStockRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid repository type");
            }

            return repo;
        }
    }
}
