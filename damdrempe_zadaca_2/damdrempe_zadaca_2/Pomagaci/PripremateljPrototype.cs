using System.Collections.Generic;
using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;

namespace damdrempe_zadaca_2.Pomagaci
{
    class PripremateljPrototype
    {
        public static List<Ulica> PripremiUlice(List<UlicaRedak> uliceRetci)
        {
            List<Ulica> ulice = new List<Ulica>();
            foreach (UlicaRedak ulicaRedak in uliceRetci)
            {
                ulice.Add(new Ulica(ulicaRedak));
            }

            return ulice;
        }

        public static List<Spremnik> PripremiSpremnike(List<SpremnikRedak> spremnikRetci)
        {
            List<Spremnik> spremnici = new List<Spremnik>();
            foreach (SpremnikRedak spremnikRedak in spremnikRetci)
            {
                spremnici.Add(new Spremnik(spremnikRedak));
            }

            return spremnici;
        }
    }
}
