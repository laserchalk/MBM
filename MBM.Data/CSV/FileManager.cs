using MBM.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM.DL
{
    public class FileManager
    {
        public static void Save(IEnumerable<object> objects, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (object line in objects)
                    {
                        sw.WriteLine(line.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message.ToString());
            }

        }


        public static IEnumerable<string> Load(string path)
        {
            List<string> objects = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        objects.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
                throw new ArgumentException(ex.Message.ToString());
            }

            return objects;
        }
    }
}
