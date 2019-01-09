using damdrempe_zadaca_3.Citaci;
using damdrempe_zadaca_3.Podaci.Modeli;
using damdrempe_zadaca_3.Pomagaci.Entiteti;
using damdrempe_zadaca_3.Sustav;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static damdrempe_zadaca_3.Pomagaci.Entiteti.PodrucjaComposite;

namespace damdrempe_zadaca_3
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

        public static List<Vozac> NoviVozaci = new List<Vozac>();
        public static int BrojGornjihRedaka;
        public static int BrojDonjihRedaka;

        static void Main(string[] args)
        {
            if (args.Length != 5)
            {
                Pomocno.ZavrsiProgram("Broj argumenata mora biti jednak 1.", false);
            }
            DatotekaParametara = args[0];
            BrojGornjihRedaka = int.Parse(args[2]);
            BrojDonjihRedaka = int.Parse(args[4]);

            InicijalizatorSustava.Pokreni();

            PodjelaEkrana podjelaEkrana = new PodjelaEkrana(BrojGornjihRedaka);

            foreach (Spremnik s in Spremnici)
            {
                podjelaEkrana.Ispis(s.ID + s.Vrsta.ToString());
            }

            Pomocno.ZavrsiProgram("Program izvrsen do kraja.", true);
        }
    }
}
