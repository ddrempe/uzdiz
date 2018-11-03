using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_1.Podaci.Enumeracije;

namespace damdrempe_zadaca_1.Podaci.Modeli
{
    public enum Kategorija
    {
        Mali,
        Srednji,
        Veliki
    }

    class Korisnik
    {
        public int ID { get; set; }

        public Kategorija Kategorija { get; set; }

        public Dictionary<VrstaOtpada, float> Otpad { get; set; }

        public float OtpadStaklo { get; set; }

        public float OtpadPapir { get; set; }

        public float OtpadMetal { get; set; }

        public float OtpadBio { get; set; }

        public float OtpadMijesano { get; set; }

        public Korisnik()
        {
            Otpad = new Dictionary<VrstaOtpada, float>();
        }
    }
}
