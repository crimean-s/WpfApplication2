using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2
{
    public class WebSearch
    {
        public string Query { get; set; }

        public string Url { get; set; }

        public string Browser { get; set; }


        public WebSearch()
        {
            Query = "";
            Url = "https://google.com/";
            Browser = "Chrome";
        }
        
    }
}
