using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using damdrempe_zadaca_3.Citaci;
using static damdrempe_zadaca_3.Podaci.Enumeracije;

namespace damdrempe_zadaca_3.Podaci.Modeli
{
    class Spremnik
    {
        public int ID { get; set; }

        public VrstaOtpada NazivPremaOtpadu { get; set; }

        public VrstaSpremnika Vrsta { get; set; }

        public int BrojnostMali { get; set; }

        public int BrojnostSrednji { get; set; }

        public int BrojnostVeliki { get; set; }

        public int Nosivost { get; set; }

        public float KolicinaOtpada { get; set; }

        public List<int> Korisnici { get; set; }

        public string UlicaID { get; set; }

        public Spremnik()
        {            
            Korisnici = new List<int>();
            KolicinaOtpada = 0;
        }

        public Spremnik(SpremnikRedak spremnikRedak)
        {
            NazivPremaOtpadu = (VrstaOtpada)Enum.Parse(typeof(VrstaOtpada), spremnikRedak.NazivPremaOtpadu, true);
            Vrsta = spremnikRedak.Vrsta;
            BrojnostMali = spremnikRedak.BrojnostMali;
            BrojnostSrednji = spremnikRedak.BrojnostSrednji;
            BrojnostVeliki = spremnikRedak.BrojnostVeliki;
            Nosivost = spremnikRedak.Nosivost;
            Korisnici = new List<int>();
            KolicinaOtpada = 0;
        }
    }
}
