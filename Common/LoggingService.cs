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
        /// <exception cref="ArgumentException">Thrown when failed to log data to file.</exception>
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
            catch (Exception)
            {
                throw new ArgumentException("Failed to log data to file.");
            }
        }

        ///<summary>Logs a single object to a file</summary>
        ///<exception cref="ArgumentException">Thrown when failed to log data to file.</exception>
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
            catch (Exception)
            {
                throw new ArgumentException("Failed to log data to file.");
            }
        }

        ///<summary>Logs a single single line of text to a file using the input parameter</summary>
        /// <exception cref="ArgumentException">Thrown when failed to log data to file.</exception>
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
            catch (Exception)
            {
                throw new ArgumentException("Failed to log data to file.");
            }
        }

    }
}
