using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfApplication2.Interfaces;
using WpfApplication2.Modules;

namespace WpfApplication2.ViewModel
{
    class FileSearchViewModel : ISearch
    {
        public OleDbConnection Connection { get; set; }

        public FileSearchViewModel()
        {
            Connection = new OleDbConnection(@"Provider=Search.CollatorDSO;Extended Properties=""Application=Windows""");
        }
        
        public void Search(string query)
        {
            var query1 = @"SELECT System.ItemName FROM SystemIndex " +
                @"WHERE scope ='file:D:/' AND System.ItemName LIKE '%" + query + "%'";
            var query2 = @"SELECT System.ItemName, System.ItemUrl FROM SystemIndex WHERE CONTAINS('Microsoft')";
            var query3 = @"SELECT System.ItemName FROM SystemIndex " +
   @"WHERE scope ='file:" + System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "'";
            var query4 = @"SELECT System.ItemNameDisplay FROM SystemIndex " +
   @"WHERE scope ='file:D:/shara'";


            //Connection.Open();

            //var command = new OleDbCommand(query4, Connection);
            //List<string> result = new List<string>();

            //using (var r = command.ExecuteReader())
            //{
            //    while (r.HasRows)
            //    {
            //        //MessageBox.Show(r[0].ToString());
            //        result.Add(r.GetString(0));
            //    }
            //}

            //Connection.Close();

            //IEnumerable<string> st = GetFileList(query, @"D:\alitop\");
            SearchFile.GetLogicalDrives();

            MessageBox.Show(query);
        }

        public void Setup(object ob)
        {
           
        }

        public IEnumerable<string> GetFileList(string query, string rootFolderPath)
        {
            List<string> result = new List<string>();
            Queue<string> pending = new Queue<string>();
            pending.Enqueue(rootFolderPath);
            string[] tmp;
            while (pending.Count > 0)
            {
                rootFolderPath = pending.Dequeue();
                try
                {
                    // LINQ query for all files containing the word 'Europe'.
                    var files = from file in
                       Directory.EnumerateFiles(rootFolderPath, "*", SearchOption.AllDirectories)
                                select file;

                    // Show results.
                    foreach (var file in files)
                    {
                        result.Add(file);
                    }
                    Console.WriteLine("{0} files found.",
                        files.Count<string>().ToString());
                }
                catch (UnauthorizedAccessException UAEx)
                {
                    Console.WriteLine(UAEx.Message);
                }
                catch (PathTooLongException PathEx)
                {
                    Console.WriteLine(PathEx.Message);
                }
            }
            return result;
        }
    }

}
