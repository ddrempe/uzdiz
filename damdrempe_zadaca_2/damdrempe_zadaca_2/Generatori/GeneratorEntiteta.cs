using damdrempe_zadaca_2.Podaci.Modeli;
using System;
using System.Collections.Generic;

namespace damdrempe_zadaca_2
{
    class GeneratorEntiteta
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
                    Korisnik korisnik = new Korisnik
                    {
                        ID = korisnikID,
                        Kategorija = Kategorija.Mali,
                    };

                    ulica.KorisniciMali.Add(korisnik);               
                    korisnikID++;
                }

                for (int i = 0; i < brojSrednjih; i++)
                {
                    Korisnik korisnik = new Korisnik
                    {
                        ID = korisnikID,
                        Kategorija = Kategorija.Srednji,
                    };

                    ulica.KorisniciSrednji.Add(korisnik);
                    korisnikID++;
                }

                for (int i = 0; i < brojVelikih; i++)
                {
                    Korisnik korisnik = new Korisnik
                    {
                        ID = korisnikID,
                        Kategorija = Kategorija.Veliki,
                    };

                    ulica.KorisniciVeliki.Add(korisnik);
                    korisnikID++;
                }
            }

            return ulice;
        }

        public static List<Spremnik> StvoriSpremnike(List<Ulica> ulice, List<Spremnik> spremniciVrste)
        {
            int spremnikID = 1;
            int brojacKorisnici;
            List<Spremnik> spremnici = new List<Spremnik>();

            foreach (Ulica ulica in ulice)
            {
                foreach (Spremnik spremnikVrsta in spremniciVrste)
                {                    
                    brojacKorisnici = 0;
                    while (brojacKorisnici < ulica.KorisniciMali.Count)
                    {
                        if (spremnikVrsta.BrojnostMali == 0) break;

                        Spremnik spremnik = new Spremnik();
                        spremnik = new Spremnik();
                        spremnik.ID = spremnikID;
                        spremnikID++;
                        spremnik.Naziv = spremnikVrsta.Naziv;

                        int brojacKorisnikaGrupe = 1;
                        while (brojacKorisnikaGrupe <= spremnikVrsta.BrojnostMali && brojacKorisnici < ulica.KorisniciMali.Count)
                        {
                            Korisnik korisnik = ulica.KorisniciMali[brojacKorisnici];
                            spremnik.Korisnici.Add(korisnik.ID);
                            brojacKorisnikaGrupe++;
                            brojacKorisnici++;
                        }

                        spremnici.Add(spremnik);
                    }

                    brojacKorisnici = 0;
                    while (brojacKorisnici < ulica.KorisniciSrednji.Count)
                    {
                        if (spremnikVrsta.BrojnostSrednji == 0) break;

                        Spremnik spremnik = new Spremnik();
                        spremnik = new Spremnik();
                        spremnik.ID = spremnikID;
                        spremnikID++;
                        spremnik.Naziv = spremnikVrsta.Naziv;

                        int brojacKorisnikaGrupe = 1;
                        while (brojacKorisnikaGrupe <= spremnikVrsta.BrojnostSrednji && brojacKorisnici < ulica.KorisniciSrednji.Count)
                        {
                            Korisnik korisnik = ulica.KorisniciSrednji[brojacKorisnici];
                            spremnik.Korisnici.Add(korisnik.ID);
                            brojacKorisnikaGrupe++;
                            brojacKorisnici++;
                        }

                        spremnici.Add(spremnik);
                    }

                    brojacKorisnici = 0;
                    while (brojacKorisnici < ulica.KorisniciVeliki.Count)
                    {
                        if (spremnikVrsta.BrojnostVeliki == 0) break;

                        Spremnik spremnik = new Spremnik();
                        spremnik = new Spremnik();
                        spremnik.ID = spremnikID;
                        spremnikID++;
                        spremnik.Naziv = spremnikVrsta.Naziv;

                        int brojacKorisnikaGrupe = 1;
                        while (brojacKorisnikaGrupe <= spremnikVrsta.BrojnostVeliki && brojacKorisnici < ulica.KorisniciVeliki.Count)
                        {
                            Korisnik korisnik = ulica.KorisniciVeliki[brojacKorisnici];
                            spremnik.Korisnici.Add(korisnik.ID);
                            brojacKorisnikaGrupe++;
                            brojacKorisnici++;
                        }

                        spremnici.Add(spremnik);
                    }
                }                           
            }

            return spremnici;
        }
    }
}
