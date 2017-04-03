using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.Model;

namespace WpfApplication2
{
    public class SuggestViewModel
    {
        
        private string Name { get; set; }
        public ObservableCollection<Suggest> SuggestItems { get; private set; }



        public SuggestViewModel()
        {
            Name = "First List";
            SuggestItems = new ObservableCollection<Suggest>
            {
                new Suggest() { Command = new Command() { Name = "web" } , Id = 0, Query = "", Text = ""},
                new Suggest() { Command = new Command() { Name = "file" }, Id = 1, Query = "", Text = "" },
                new Suggest() { Command = new Command() { Name = "wiki" }, Id = 2, Query = "", Text = "" }
            };
        }

        public void updateSuggestViewModel(ObservableCollection<Suggest> list)
        {
            SuggestItems = list;
        }

    }
}
