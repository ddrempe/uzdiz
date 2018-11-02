using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1.Modeli
{
    enum VrstaSpremnika
    {
        Kanta,
        Kontejner
    }

    class Spremnik
    {
        public string Naziv { get; set; }

        public VrstaSpremnika Vrsta { get; set; }

        public int BrojnostMali { get; set; }

        public int BrojnostSrednji { get; set; }

        public int BrojnostVeliki { get; set; }

        public int Nosivost { get; set; }
    }
}
