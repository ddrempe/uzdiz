using damdrempe_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1
{
    class Pomocno
    {
        public static List<Ulica> StvoriKorisnike(List<Ulica> ulice)
        {
            int korisnikID = 1;

            foreach (Ulica ulica in ulice)
            {
                int brojMalih = (int)Math.Round(ulica.UdioMalih / 100.00 * ulica.BrojMjesta, MidpointRounding.AwayFromZero);
                int brojSrednjih = (int)Math.Round(ulica.UdioSrednjih / 100.00 * ulica.BrojMjesta, MidpointRounding.AwayFromZero);
                int brojVelikih = (int)Math.Round(ulica.UdioVelikih / 100.00 * ulica.BrojMjesta, MidpointRounding.AwayFromZero);

                for (int i = 0; i < brojMalih; i++)
                {
                    Korisnik korisnik = new Korisnik();
                    korisnik.ID = korisnikID;
                    korisnik.Kategorija = Kategorija.Mali;
                    ulica.Korisnici.Add(korisnik);

                    korisnikID++;
                }

                for (int i = 0; i < brojSrednjih; i++)
                {
                    Korisnik korisnik = new Korisnik();
                    korisnik.ID = korisnikID;
                    korisnik.Kategorija = Kategorija.Srednji;
                    ulica.Korisnici.Add(korisnik);

                    korisnikID++;
                }

                for (int i = 0; i < brojVelikih; i++)
                {
                    Korisnik korisnik = new Korisnik();
                    korisnik.ID = korisnikID;
                    korisnik.Kategorija = Kategorija.Veliki;
                    ulica.Korisnici.Add(korisnik);

                    korisnikID++;
                }
            }

            return ulice;
        }
    }
}
