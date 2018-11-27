using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Pomagaci.Entiteti;
using damdrempe_zadaca_2.Sustav;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static damdrempe_zadaca_2.Pomagaci.Entiteti.PodrucjaComposite;

namespace damdrempe_zadaca_2.Pomagaci.Entiteti
{
    class PripremateljPodrucja
    {
        public static List<Podrucje> PripremiPodrucja(List<PodrucjeRedak> podrucjaPopisRetci)
        {
            List<Podrucje> podrucja = new List<Podrucje>();
            foreach (PodrucjeRedak podrucjeRedak in podrucjaPopisRetci)
            {
                Podrucje novoPodrucje = new Podrucje(podrucjeRedak.ID);
                podrucja.Add(novoPodrucje);
            }

            foreach (Podrucje podrucje in podrucja)
            {
                PodrucjeRedak podrucjeRedak = podrucjaPopisRetci.FirstOrDefault(p => p.ID == podrucje.PodrucjeID);

                foreach (string dioID in podrucjeRedak.Dijelovi)
                {
                    if (Pomocno.DioPodrucjaJeUlica(dioID))
                    {
                        UlicaPodrucja ulicaPodrucja = new UlicaPodrucja(dioID);
                        podrucje.Dodijeli(ulicaPodrucja);
                    }
                    else
                    {
                        Podrucje podPodrucje = podrucja.FirstOrDefault(p => p.PodrucjeID == dioID);
                        podrucje.Dodijeli(podPodrucje);
                    }
                }
            }

            return podrucja;
        }
    }
}
