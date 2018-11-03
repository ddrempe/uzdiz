using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_1.Podaci.Enumeracije;

namespace damdrempe_zadaca_1.Modeli
{
    class Spremnik
    {
        public int ID { get; set; }

        public string Naziv { get; set; }

        public VrstaSpremnika Vrsta { get; set; }

        public int Nosivost { get; set; }

        public List<int> Korisnici { get; set; }

        public Spremnik() => Korisnici = new List<int>();
    }
}
