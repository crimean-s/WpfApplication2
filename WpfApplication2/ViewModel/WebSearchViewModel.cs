using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApplication2.Interfaces;

namespace WpfApplication2.ViewModel
{
    public class WebSearchViewModel : ISearch
    {
        public WebSearch WebSearchObject = new WebSearch();
        

        public void Search(string query)
        {
            WebSearchObject.Query = query;

            Process myProcess = new Process();

            WebSearchObject.Url = WebSearchObject.Url + "search?q=" + WebSearchObject.Query;

            try
            {
                myProcess.StartInfo.UseShellExecute = true;

                if (WebSearchObject.Browser != "")
                {
                    myProcess.StartInfo.FileName = getBrowserPath(WebSearchObject.Browser);
                    myProcess.StartInfo.Arguments = "\"" + WebSearchObject.Url + "\"";
                }
                else
                {
                    myProcess.StartInfo.FileName = WebSearchObject.Url;
                }
                myProcess.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("A handled exception just occurred: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void Setup(object ob)
        {
            WebSearchObject.Browser = (string)ob;
        }

        private string getBrowserPath(string browser)
        {
            string browserPath = "";

            switch (browser)
            {
                case "firefox":
                    browserPath = "firefox.exe";
                    break;
                case "chrome":
                    browserPath = "chrome.exe";
                    break;
                case "ie":
                    browserPath = "iexplore.exe";
                    break;
            }

            return browserPath;
        }
    }
}
