using System;
using System.Collections.Generic;
using System.IO;

namespace Common
{
    ///<summary>Used for writing, reading and deleting files</summary>
    public class FileManager
    {
        ///<summary>Writes a list of objects as strings to the specified file path</summary>
        /// <exception cref="Exception">Thrown when saving failed</exception>
        public static void Save(IEnumerable<object> objects, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    foreach (object item in objects)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception("Failed to save file");
            }
        }


        ///<summary>Returns a list of strings that represent the lines from a file at the specified path</summary>
        /// <exception cref="Exception">Thrown when loading failed</exception>
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
            catch (Exception)
            {
                throw new Exception("Failed to load file");
            }

            return objects;
        }

        ///<summary>Deletes the file at the specified path.</summary>
        /// <exception cref="Exception">Thrown when deleting failed</exception>
        public static void Delete(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete file", ex);
            }
        }
    }
}
