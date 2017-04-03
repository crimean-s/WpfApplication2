using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication2.Model;

namespace WpfApplication2
{
    public class Suggest
    {
        public int Id { get; set; }

        public Command Command { get; set; }

        public string Query { get; set; }

        public string Text { get; set; }

    }
}
