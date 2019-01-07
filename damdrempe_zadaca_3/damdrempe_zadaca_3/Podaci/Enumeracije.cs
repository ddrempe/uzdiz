using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_3.Podaci
{
    class Enumeracije
    {
        public enum TipVozila
        {
            Dizel,
            Elektricni
        }

        public enum VrstaOtpada
        {
            Staklo,
            Papir,
            Metal,
            Bio,
            Mješano
        }

        public enum VrstaSpremnika
        {
            Kanta,
            Kontejner
        }

        public enum VrstaKomande
        {
            PRIPREMI,
            KRENI,
            KRENI_N,
            KVAR,
            KONTROLA,
            ISPRAZNI,
            STATUS,
            VOZAČI,
            IZLAZ,
            OBRADI,
            GODIŠNJI_ODMOR,
            BOLOVANJE,
            OTKAZ,
            PREUZMI,
            NOVI
        }

        public enum VrstaStanja
        {
            Parkirano,
            Skupljanje,
            Pokvareno,
            Kontrola,
            Praznjenje
        }
    }
}
