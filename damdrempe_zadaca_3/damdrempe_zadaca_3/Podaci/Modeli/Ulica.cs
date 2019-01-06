using damdrempe_zadaca_3.Citaci;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_3.Podaci.Enumeracije;

namespace damdrempe_zadaca_3.Podaci.Modeli
{
    class Ulica
    {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public int BrojMjesta { get; set; }

        public int UdioMalih { get; set; }

        public int UdioSrednjih { get; set; }

        public int UdioVelikih { get; set; }

        public List<Korisnik> KorisniciMali { get; set; }

        public List<Korisnik> KorisniciSrednji { get; set; }

        public List<Korisnik> KorisniciVeliki { get; set; }

        public Dictionary<VrstaOtpada, float> Otpad { get; set; }

        public Ulica()
        {
            KorisniciMali = new List<Korisnik>();
            KorisniciSrednji = new List<Korisnik>();
            KorisniciVeliki = new List<Korisnik>();
            Otpad = new Dictionary<VrstaOtpada, float>();
        }

        public Ulica(UlicaRedak ulicaRedak)
        {
            ID = ulicaRedak.ID;
            Naziv = ulicaRedak.Naziv;
            BrojMjesta = ulicaRedak.BrojMjesta;
            UdioMalih = ulicaRedak.UdioMalih;
            UdioSrednjih = ulicaRedak.UdioSrednjih;
            UdioVelikih = ulicaRedak.UdioVelikih;
            KorisniciMali = new List<Korisnik>();
            KorisniciSrednji = new List<Korisnik>();
            KorisniciVeliki = new List<Korisnik>();
            Otpad = new Dictionary<VrstaOtpada, float>();
        }
    }
}
