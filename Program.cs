using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LidijaDrzave
{
    class Program
    {
        static void Main(string[] args)
        {
            CountryCityExtraction cs = new CountryCityExtraction();
            cs.otvoriSranicu();
            cs.extract();
        }
    }
}
