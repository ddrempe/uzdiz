using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1.Modeli
{
    public enum Kategorija
    {
        Mali,
        Srednji,
        Veliki
    }

    class Korisnik
    {
        public int ID { get; set; }

        public Kategorija Kategorija { get; set; }
    }
}
