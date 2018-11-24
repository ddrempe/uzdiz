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

                ulica.KorisniciMali = StvoriKorisnikePoVrsti(ref korisnikID, brojMalih, Kategorija.Mali);
                ulica.KorisniciSrednji = StvoriKorisnikePoVrsti(ref korisnikID, brojSrednjih, Kategorija.Srednji);
                ulica.KorisniciVeliki = StvoriKorisnikePoVrsti(ref korisnikID, brojVelikih, Kategorija.Veliki);
            }

            return ulice;
        }

        private static List<Korisnik> StvoriKorisnikePoVrsti(ref int korisnikID, int brojKorisnika, Kategorija kategorija)
        {
            List<Korisnik> korisnici = new List<Korisnik>();

            for (int i = 0; i < brojKorisnika; i++)
            {
                Korisnik korisnik = new Korisnik
                {
                    ID = korisnikID,
                    Kategorija = kategorija,
                };

                korisnikID++;
                korisnici.Add(korisnik);                
            }

            return korisnici;
        }

        public static List<Spremnik> StvoriSpremnike(List<Ulica> ulice, List<Spremnik> spremniciVrste)
        {
            int spremnikID = 1;
            List<Spremnik> spremnici = new List<Spremnik>();

            foreach (Ulica ulica in ulice)
            {
                foreach (Spremnik spremnikVrsta in spremniciVrste)
                {
                    List<Spremnik> spremniciMali = StvoriSpremnikePoVrsti(ref spremnikID, ulica, spremnikVrsta, Kategorija.Mali);
                    List<Spremnik> spremniciSrednji = StvoriSpremnikePoVrsti(ref spremnikID, ulica, spremnikVrsta, Kategorija.Srednji);
                    List<Spremnik> spremniciVeliki = StvoriSpremnikePoVrsti(ref spremnikID, ulica, spremnikVrsta, Kategorija.Veliki);

                    spremnici.AddRange(spremniciMali);
                    spremnici.AddRange(spremniciSrednji);
                    spremnici.AddRange(spremniciVeliki);
                }                           
            }

            return spremnici;
        }

        private static List<Spremnik> StvoriSpremnikePoVrsti(ref int spremnikID, Ulica ulica, Spremnik spremnikVrsta, Kategorija kategorija)
        {            
            List<Korisnik> korisnici = OdrediListuKorisnika(ulica, kategorija);          
            List<Spremnik> spremnici = new List<Spremnik>();

            for (int i = 0; i < korisnici.Count;)
            {
                if (spremnikVrsta.BrojnostMali == 0) break;

                Spremnik spremnik = new Spremnik();
                spremnik = new Spremnik();
                spremnik.ID = spremnikID;
                spremnikID++;
                spremnik.Naziv = spremnikVrsta.Naziv;

                int brojacKorisnikaGrupe = 1;
                while (brojacKorisnikaGrupe <= spremnikVrsta.BrojnostMali && i < korisnici.Count)
                {
                    Korisnik korisnik = korisnici[i];
                    spremnik.Korisnici.Add(korisnik.ID);
                    brojacKorisnikaGrupe++;
                    i++;
                }

                spremnici.Add(spremnik);
            }

            return spremnici;
        }

        private static List<Korisnik> OdrediListuKorisnika(Ulica ulica, Kategorija kategorija)
        {
            List<Korisnik> korisnici = new List<Korisnik>();

            switch (kategorija)
            {
                case Kategorija.Mali:
                    korisnici = ulica.KorisniciMali;
                    break;
                case Kategorija.Srednji:
                    korisnici = ulica.KorisniciSrednji;
                    break;
                case Kategorija.Veliki:
                    korisnici = ulica.KorisniciVeliki;
                    break;
                default:
                    break;
            }

            return korisnici;
        }
    }
}
