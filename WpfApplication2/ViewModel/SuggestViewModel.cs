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
                new Suggest() { Command = new Command() { Name = "web" } , Query = "", Text = ""},
                new Suggest() { Command = new Command() { Name = "file" }, Query = "", Text = "" },
                new Suggest() { Command = new Command() { Name = "wiki" }, Query = "", Text = "" }
            };
        }

        public void updateSuggestViewModel(ObservableCollection<Suggest> list)
        {
            SuggestItems = list;
        }

    }
}
