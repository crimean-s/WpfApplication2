using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Modules.Search
{
    class FileSearchResult
    {
        public String FileName { get; set; }
        public String FilePath { get; set; }
        public FileKind Kind { get; set; }
        public int Rank { get; set; }

        public bool Touched { get; set; }

        public FileSearchResult()
        {

        }
    }
}
