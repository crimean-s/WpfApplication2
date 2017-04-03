using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.Model;

namespace WpfApplication2.ViewModel
{
    public class CommandViewModel
    {
        public Command CurrentCommand { get; set; }

        public ObservableCollection<Command> CommandList { get; private set; }

        public CommandViewModel()
        {
            CurrentCommand = new Command
            {
                Name = "web"
            }; 

            CommandList = new ObservableCollection<Command>
            {
                new Command() { Name = "web"},
                new Command() { Name = "file"},
                new Command() { Name = "wiki"}
            };
        }
        
        public string getCommandName()
        {
           return this.CurrentCommand.Name;
        }

        public void setCommandName(string name)
        {
            CurrentCommand.Name = name;
        }

    }
}
