using damdrempe_zadaca_2.Citaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Podaci.Modeli
{
    class Vozilo
    {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public TipVozila Tip { get; set; }

        public VrstaOtpada VrstaOtpada { get; set; }

        public int Nosivost { get; set; }

        public List<string> Vozaci { get; set; }

        public List<int> RedoslijedUlica { get; set; }

        public Vozilo(VoziloRedak voziloRedak)
        {
            ID = voziloRedak.ID;
            Naziv = voziloRedak.Naziv;
            Tip = voziloRedak.Tip;
            VrstaOtpada = voziloRedak.VrstaOtpada;
            Nosivost = voziloRedak.Nosivost;
            Vozaci = voziloRedak.Vozaci;
        }
    }
}
