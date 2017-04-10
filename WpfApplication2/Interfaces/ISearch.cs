using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication2.Interfaces
{
    interface ISearch
    {
        void Search(string query);

        void Setup(object ob);
    }
}
