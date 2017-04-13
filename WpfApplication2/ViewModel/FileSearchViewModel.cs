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
using WpfApplication2.Modules.Search;

namespace WpfApplication2.ViewModel
{
    class FileSearchViewModel : ISearch
    {
        public OleDbConnection Connection { get; set; }

        public FileSearchViewModel()
        {
            try
            {
                Connection = new OleDbConnection(@"Provider=Search.CollatorDSO;Extended Properties=""Application=Windows""");
                Connection.Open();
            }
            catch
            { // fails if desktop search is disabled or not installed
                Connection = null;
                MessageBox.Show("Desktop search is disabled");
            }

            
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

            var query5 = @"SELECT top 1000 System.ItemNameDisplay, System.ItemPathDisplay, System.Kind, System.Search.Rank, System.FileAttributes FROM SYSTEMINDEX WHERE scope ='file:C:\' AND System.ItemName LIKE '%" + query + "%'";

            var query6 = @"select top 1000 
		  System.ItemNameDisplay, System.ItemPathDisplay, System.Kind, System.Search.Rank, System.FileAttributes 
		  from systemindex 
		  where CONTAINS(""System.ItemNameDisplay"", '""*{0}*""')
		  and System.FileAttributes <> ALL BITWISE 0x4 and System.FileAttributes <> ALL BITWISE 0x2 order by System.Search.Rank";

            var query7 = @"select top 20 System.ItemNameDisplay, System.ItemPathDisplay, System.Kind, System.Search.Rank, System.FileAttributes 
            from systemindex 	  
            where CONTAINS(""System.ItemNameDisplay"", '" + query + @"')
            and System.FileAttributes <> ALL BITWISE 0x4 and System.FileAttributes <> ALL BITWISE 0x2 order by System.Search.Rank";

            var command = new OleDbCommand(query7, Connection);
            List<FileSearchResult> resultList = new List<FileSearchResult>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var result = new FileSearchResult()
                    {
                        FileName = reader["System.ItemNameDisplay"].ToString(),
                        FilePath = reader["System.ItemPathDisplay"].ToString(),
                        Rank = (int)reader["System.Search.Rank"]
                    };


                    String[] kinds = reader["System.Kind"] as String[];
                    if (kinds != null && kinds.Length >= 1)
                    {
                        foreach (var i in reader["System.Kind"] as String[])
                        {
                            result.Kind |= (FileKind)Enum.Parse(typeof(FileKind), i);
                        }
                    }
                    else
                    {
                        result.Kind = FileKind.file;
                    }

                    resultList.Add(result);
                }
            }

            Connection.Close();

            //IEnumerable<string> st = GetFileList(query, @"D:\alitop\");
            //SearchFile.GetLogicalDrives();

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
