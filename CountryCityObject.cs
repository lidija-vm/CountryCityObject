using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidijaDrzave
{
    class CountryCityObject
    {

        public int id { get; set; }
        public string skr { get; set; }
        public string naziv { get; set; }
        public List<string> gradovi { get; set; }
    }
}
