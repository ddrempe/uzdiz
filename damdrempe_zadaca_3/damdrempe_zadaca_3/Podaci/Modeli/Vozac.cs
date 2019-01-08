using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_3.Podaci.Modeli
{
    public enum StatusVozaca
    {
        Raspoloziv,
        Vozi,
        Godisnji,
        Bolovanje,
        Otkaz
    }

    class Vozac
    {
        public string Ime { get; set; }

        public StatusVozaca Status { get; set; }

        public string IDVozila { get; set; }

        public Vozac(string ime)
        {
            Ime = ime;
            Status = StatusVozaca.Raspoloziv;
            IDVozila = "";
        }
    }
}
