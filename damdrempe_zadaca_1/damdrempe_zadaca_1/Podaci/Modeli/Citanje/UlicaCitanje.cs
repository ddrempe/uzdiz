using damdrempe_zadaca_1.Podaci.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1.Modeli
{
    class UlicaCitanje
    {
        public string Naziv { get; set; }

        public int BrojMjesta { get; set; }

        public int UdioMalih { get; set; }

        public int UdioSrednjih { get; set; }

        public int UdioVelikih { get; set; }

        public List<Korisnik> KorisniciMali { get; set; }

        public List<Korisnik> KorisniciSrednji { get; set; }

        public List<Korisnik> KorisniciVeliki { get; set; }

        public UlicaCitanje()
        {
            KorisniciMali = new List<Korisnik>();
            KorisniciSrednji = new List<Korisnik>();
            KorisniciVeliki = new List<Korisnik>();
        }
    }
}
