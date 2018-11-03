using damdrempe_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1
{
    class GeneratorEntiteta
    {
        public static List<UlicaPopis> StvoriKorisnike(List<UlicaPopis> ulice)
        {
            int korisnikID = 1;

            foreach (UlicaPopis ulica in ulice)
            {
                int brojMalih = (int)Math.Round(ulica.UdioMalih / 100.00 * ulica.BrojMjesta, MidpointRounding.AwayFromZero);
                int brojSrednjih = (int)Math.Round(ulica.UdioSrednjih / 100.00 * ulica.BrojMjesta, MidpointRounding.AwayFromZero);
                int brojVelikih = (int)Math.Round(ulica.UdioVelikih / 100.00 * ulica.BrojMjesta, MidpointRounding.AwayFromZero);

                for (int i = 0; i < brojMalih; i++)
                {
                    Korisnik korisnik = new Korisnik();
                    korisnik.ID = korisnikID;
                    korisnik.Kategorija = Kategorija.Mali;
                    ulica.KorisniciMali.Add(korisnik);

                    korisnikID++;
                }

                for (int i = 0; i < brojSrednjih; i++)
                {
                    Korisnik korisnik = new Korisnik();
                    korisnik.ID = korisnikID;
                    korisnik.Kategorija = Kategorija.Srednji;
                    ulica.KorisniciSrednji.Add(korisnik);

                    korisnikID++;
                }

                for (int i = 0; i < brojVelikih; i++)
                {
                    Korisnik korisnik = new Korisnik();
                    korisnik.ID = korisnikID;
                    korisnik.Kategorija = Kategorija.Veliki;
                    ulica.KorisniciVeliki.Add(korisnik);

                    korisnikID++;
                }
            }

            return ulice;
        }

        public static List<Spremnik> StvoriSpremnike(List<UlicaPopis> ulice, List<SpremnikPopis> spremnici)
        {
            int idSpremnika = 1;
            List<Spremnik> instanceSpremnika = new List<Spremnik>();

            foreach (UlicaPopis ulica in ulice)
            {
                foreach (SpremnikPopis spremnik in spremnici)
                {             
                    int brojacKorisnici = 0;
                    while (brojacKorisnici < ulica.KorisniciMali.Count)
                    {
                        if (spremnik.BrojnostMali == 0) break;

                        Spremnik spremnikInstanca = new Spremnik();
                        spremnikInstanca = new Spremnik();
                        spremnikInstanca.ID = idSpremnika;
                        spremnikInstanca.Naziv = spremnik.Naziv;

                        int brojacKorisnikaGrupe = 1;
                        while (brojacKorisnikaGrupe <= spremnik.BrojnostMali && brojacKorisnici < ulica.KorisniciMali.Count)
                        {
                            Korisnik korisnik = ulica.KorisniciMali[brojacKorisnici];
                            spremnikInstanca.Korisnici.Add(korisnik.ID);
                            brojacKorisnikaGrupe++;
                            brojacKorisnici++;
                        }

                        instanceSpremnika.Add(spremnikInstanca);
                    }                    
                }                           
            }

            return instanceSpremnika;
        }
    }
}
