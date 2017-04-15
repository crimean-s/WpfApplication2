using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.Interfaces;
using LinqToWiki.Download;
using LinqToWiki.Generated;
using WpfApplication2.Modules.Wiki;

namespace WpfApplication2.ViewModel
{
    class WikiSearchViewModel : ISearch
    {
        public Wiki WikiObject { get; private set; }

        List<WikiSearchResult> Pages { get; set; }

        public void Search(string query)
        {
            try
            {
                //    Pages = (from cm in WikiObject.Query.categorymembers()
                //             where cm.title == query
                //             select cm.title)
                //.ToEnumerable();

                var s = WikiObject.Query.search(query).ToList();                

                var a = 1;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Setup(object ob)
        {
            WikiObject = new Wiki("DesktopSearchBot (https://github.com/; DesktopSearchBot@test.org) LINQ-to-Wiki", "https://ru.wikipedia.org", "/w/api.php");
        }
    }
}
