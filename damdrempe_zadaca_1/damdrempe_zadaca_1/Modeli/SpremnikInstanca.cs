using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1.Modeli
{
    class SpremnikInstanca
    {
        public int ID { get; set; }

        public string Naziv { get; set; }

        public VrstaSpremnika Vrsta { get; set; }

        public int Nosivost { get; set; }

        public List<int> Korisnici { get; set; }

        public SpremnikInstanca() => Korisnici = new List<int>();
    }
}
