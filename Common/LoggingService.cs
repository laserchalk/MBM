using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class LoggingService
    {
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
                throw new ArgumentException(ex.Message.ToString());
            }
        }

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
                throw new ArgumentException(ex.Message.ToString());
            }
        }

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
                throw new ArgumentException(ex.Message.ToString());
            }
        }

    }
}
