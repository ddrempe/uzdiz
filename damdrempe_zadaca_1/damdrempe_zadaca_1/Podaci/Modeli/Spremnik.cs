using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using damdrempe_zadaca_1.Citaci;
using static damdrempe_zadaca_1.Podaci.Enumeracije;

namespace damdrempe_zadaca_1.Podaci.Modeli
{
    class Spremnik
    {
        public int ID { get; set; }

        public string Naziv { get; set; }

        public VrstaSpremnika Vrsta { get; set; }

        public int BrojnostMali { get; set; }

        public int BrojnostSrednji { get; set; }

        public int BrojnostVeliki { get; set; }

        public int Nosivost { get; set; }

        public List<int> Korisnici { get; set; }

        public Spremnik()
        {
            Korisnici = new List<int>();
        }

        public Spremnik(SpremnikRedak spremnikRedak)
        {
            Naziv = spremnikRedak.Naziv;
            Vrsta = spremnikRedak.Vrsta;
            BrojnostMali = spremnikRedak.BrojnostMali;
            BrojnostSrednji = spremnikRedak.BrojnostSrednji;
            BrojnostVeliki = spremnikRedak.BrojnostVeliki;
            Nosivost = spremnikRedak.Nosivost;
            Korisnici = new List<int>();
        }
    }
}
