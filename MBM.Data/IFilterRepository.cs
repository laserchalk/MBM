using MBM.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    public interface IFilterRepository
    {
        Filter GetMinMaxValues();
        List<string> GetSymbols();
    }
}
