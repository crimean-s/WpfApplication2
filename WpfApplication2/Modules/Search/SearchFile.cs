using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://msdn.microsoft.com/en-us/library/416y61e6(v=vs.100).aspx

namespace WpfApplication2.Modules
{
    public static class SearchFile
    {
        public static List<string> GetLogicalDrives()
        {
            List<string> drives = new List<string>();

            try
            {
                drives = System.IO.Directory.GetLogicalDrives().ToList<string>();

                foreach (string str in drives)
                {
                    System.Console.WriteLine(str);
                }
            }
            catch (System.IO.IOException)
            {
                System.Console.WriteLine("An I/O error occurs.");
            }
            catch (System.Security.SecurityException)
            {
                System.Console.WriteLine("The caller does not have the " +
                    "required permission.");
            }
            
            return drives;
        }
    }
}
