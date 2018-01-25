using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    ///<summary>Interface for logging information</summary>
    public interface ILoggable
    {
        ///<summary>Returns a string representing the log information</summary>
        string Log();
    }
}
