using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Sustav;
using System;
using System.Collections.Generic;
using System.Linq;
using static damdrempe_zadaca_2.Podaci.Enumeracije;
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
                Podrucje novoPodrucje = new Podrucje(podrucjeRedak.ID, podrucjeRedak.Naziv);
                podrucja.Add(novoPodrucje);
            }

            foreach (Podrucje podrucje in podrucja)
            {
                PodrucjeRedak podrucjeRedak = podrucjaPopisRetci.FirstOrDefault(p => p.ID == podrucje.PodrucjeID);

                foreach (string dioID in podrucjeRedak.Dijelovi)
                {
                    if (Pomocno.DioPodrucjaJeUlica(dioID))
                    {
                        Ulica ulica = Program.Ulice.FirstOrDefault(u => u.ID == dioID);
                        if(ulica != null)
                        {
                            UlicaPodrucja ulicaPodrucja = new UlicaPodrucja(dioID, ulica.Naziv, ulica);
                            podrucje.Dodijeli(ulicaPodrucja);
                        }
                    }
                    else
                    {
                        Podrucje podPodrucje = podrucja.FirstOrDefault(p => p.PodrucjeID == dioID);
                        if(podPodrucje != null)
                        {
                            podrucje.Dodijeli(podPodrucje);
                        }
                    }
                }
            }

            return podrucja;
        }

        public static Dictionary<VrstaOtpada, float> IzracunajUkupanOtpadPodrucjaSIspisom(List<PodrucjeComponent> podpodrucja, bool ispis)
        {           
            Dictionary<VrstaOtpada, float> otpad = new Dictionary<VrstaOtpada, float>();
            foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
            {
                otpad[vrsta] = 0;
            }

            foreach (PodrucjeComponent podrucje in podpodrucja)
            {
                if (podrucje.GetType() == typeof(UlicaPodrucja))
                {
                    UlicaPodrucja ulica = (UlicaPodrucja)podrucje;
                    if (ispis) Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Cyan);
                    if (ispis) Program.Ispisivac.ObavljeniPosao($"ULICA [{ulica.ReferencaUlice.ID}] {ulica.ReferencaUlice.Naziv}");

                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        if (ulica.ReferencaUlice.Otpad.ContainsKey(vrsta))
                        {
                            if (ispis) Program.Ispisivac.ObavljeniPosao($"{vrsta}: {ulica.ReferencaUlice.Otpad[vrsta]}kg");
                            otpad[vrsta] += ulica.ReferencaUlice.Otpad[vrsta];
                        }
                    }
                    if (ispis) Program.Ispisivac.ObavljeniPosao("");
                }

                else if (podrucje.GetType() == typeof(Podrucje))
                {
                    Podrucje podPodrucje = (Podrucje)podrucje;
                    Dictionary<VrstaOtpada, float> otpadPodpodrucja = IzracunajUkupanOtpadPodrucjaSIspisom(podPodrucje.podrucja, ispis);

                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        if (otpadPodpodrucja.ContainsKey(vrsta))
                        {
                            otpad[vrsta] += otpadPodpodrucja[vrsta];
                        }
                    }
                }
            }

            return otpad;
        }
    }
}
