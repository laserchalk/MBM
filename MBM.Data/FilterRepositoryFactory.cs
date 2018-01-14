using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    public class FilterRepositoryFactory
    {
        /// <summary>
        /// Gets a filter repository. Values "WCF", "SQL", "CSV"
        /// </summary>
        public static IFilterRepository GetRepository(string repositoryType)
        {
            IFilterRepository repo = null;

            switch (repositoryType)
            {
                case "WCF":
                    repo = new WCFFilterRepository();
                    break;
                case "SQL":
                    repo = new SQLFilterRepository();
                    break;
                case "CSV":
                    repo = new CSVFilterRepository();
                    break;
                default:
                    throw new ArgumentException("Invalid repository type");
            }

            return repo;
        }
    }
}
