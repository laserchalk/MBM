using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    ///<summary>Interface for ise in LoggingService</summary>
    public interface ILoggable
    {
        ///<summary>Returns a string representing the log information</summary>
        string Log();
    }
}
