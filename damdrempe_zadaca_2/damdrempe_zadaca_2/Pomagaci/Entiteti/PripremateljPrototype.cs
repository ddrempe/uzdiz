using System.Collections.Generic;
using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;

namespace damdrempe_zadaca_2.Pomagaci.Entiteti
{
    class PripremateljPrototype
    {
        public static List<Ulica> PripremiUlice(List<UlicaRedak> ulicaRetci)
        {
            List<Ulica> ulice = new List<Ulica>();
            foreach (UlicaRedak ulicaRedak in ulicaRetci)
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

        public static List<Vozilo> PripremiVozila(List<VoziloRedak> voziloRetci)
        {
            List<Vozilo> vozila = new List<Vozilo>();
            foreach (VoziloRedak voziloRedak in voziloRetci)
            {
                vozila.Add(new Vozilo(voziloRedak));
            }

            return vozila;
        }
    }
}
