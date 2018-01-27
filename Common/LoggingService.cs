using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    ///<summary>Used for logging information to a file</summary>
    public class LoggingService
    {
        ///<summary>Logs a list of objects to a file</summary>
        /// <exception cref="Exception">Thrown when failed to log data to file.</exception>
        public static void Log(IEnumerable<ILoggable> objects, string path)
        {
            try
            {
                string logLine;

                using (StreamWriter sw = new StreamWriter(path, true))
                {

                    foreach (ILoggable item in objects)
                    {
                        logLine = DateTime.Now.ToString() + ", " + item.GetType().ToString() + ", " + item.Log();
                        sw.WriteLine(logLine);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to log data to file.", ex);
            }
        }

        ///<summary>Logs a single object to a file</summary>
        ///<exception cref="Exception">Thrown when failed to log data to file.</exception>
        public static void Log(ILoggable item, string path)
        {
            try
            {
                string logLine;

                using (StreamWriter sw = new StreamWriter(path, true))
                {
                        logLine = DateTime.Now.ToString() + ", " + item.GetType().ToString() + ", " + item.Log();
                        sw.WriteLine(logLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to log data to file.", ex);
            }
        }

        ///<summary>Logs a single line of text to a file using the input parameter</summary>
        /// <exception cref="Exception">Thrown when failed to log data to file.</exception>
        public static void Log(string eventName, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    string logLine = DateTime.Now.ToString() + ", " + "Event: " + eventName;
                    sw.WriteLine(logLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to log data to file.", ex);
            }
        }

        ///<summary>Logs exception details to a file using the input parameters</summary>
        /// <exception cref="Exception">Thrown when failed to log data to file.</exception>
        public static void Log(Exception exception, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    string logLine = DateTime.Now.ToString() + ", " + "Exception: " + exception.ToString();
                    sw.WriteLine(logLine);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to log data to file.", ex);
            }
        }

    }
}
