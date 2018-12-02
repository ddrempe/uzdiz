using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Pomagaci.Entiteti;
using damdrempe_zadaca_2.Sustav;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static damdrempe_zadaca_2.Pomagaci.Entiteti.PodrucjaComposite;

namespace damdrempe_zadaca_2
{
    class Program
    {
        public static IspisivacSingleton Ispisivac = IspisivacSingleton.DohvatiInstancu();

        public static string DatotekaParametara;
        public static ParametriSingleton Parametri;
        public static string PutanjaDatoteka;

        public static List<Ulica> PripremljeneUlice = new List<Ulica>();
        public static List<Spremnik> PripremljeniSpremnici = new List<Spremnik>();

        public static List<Ulica> Ulice = new List<Ulica>();
        public static List<Spremnik> Spremnici = new List<Spremnik>();
        public static List<Podrucje> Podrucja = new List<Podrucje>();
        public static List<Vozilo> Vozila = new List<Vozilo>();
        public static List<KomandaRedak> Komande = new List<KomandaRedak>();

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Pomocno.ZavrsiProgram("Broj argumenata mora biti jednak 1.", false);
            }
            DatotekaParametara = args[0];            

            InicijalizatorSustava.Pokreni();     
            Pomocno.ZavrsiProgram("Program izvrsen do kraja.", true);
        }
    }
}
