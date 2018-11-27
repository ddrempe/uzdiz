using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Pomagaci;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static damdrempe_zadaca_2.Pomagaci.PodrucjaComposite;

namespace damdrempe_zadaca_2
{
    class Program
    {
        public static string DatotekaParametara;
        public static string PutanjaDatoteka;
        public static IspisivacSingleton Ispisivac = IspisivacSingleton.DohvatiInstancu();

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Pomocno.ZavrsiProgram("Broj argumenata mora biti jednak 1.", false);
            }

            DatotekaParametara = args[0];
            if(!File.Exists(DatotekaParametara))
            {
                Pomocno.ZavrsiProgram("Datoteka s parametrima ne postoji!", false);
            }
            ParametriSingleton parametri = ParametriSingleton.DohvatiInstancu(DatotekaParametara);
            PutanjaDatoteka = Path.GetDirectoryName(DatotekaParametara);

            string datotekaUlice = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("ulice"));
            Popis ulicaPopis = new UlicaPopis();
            List<Redak> ulicaPopisRetci = ulicaPopis.UcitajRetke(datotekaUlice);

            string datotekaSpremnika = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("spremnici"));
            Popis spremnikPopis = new SpremnikPopis();
            List<Redak> spremnikPopisRetci = spremnikPopis.UcitajRetke(datotekaSpremnika);

            string datotekaVozila = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("vozila"));
            Popis voziloPopis = new VoziloPopis();
            List<Redak> voziloPopisRetci = voziloPopis.UcitajRetke(datotekaVozila);

            List<Ulica> pripremljeneUlice = PripremateljPrototype.PripremiUlice(ulicaPopisRetci.Cast<UlicaRedak>().ToList());
            List<Spremnik> pripremljeniSpremnici = PripremateljPrototype.PripremiSpremnike(spremnikPopisRetci.Cast<SpremnikRedak>().ToList());

            List<Ulica> ulice = GeneratorEntiteta.StvoriKorisnike(pripremljeneUlice);
            List<Spremnik> spremnici = GeneratorEntiteta.StvoriSpremnike(pripremljeneUlice, pripremljeniSpremnici);

            ulice = Inicijalizator.OdrediOtpadKorisnicima(ulice, DatotekaParametara);
            spremnici = Inicijalizator.OdloziOtpadKorisnika(ulice, spremnici);
                        
            Inicijalizator.IspisiOtpadPoUlicama(ulice);

            //testPodrucja();

            string datotekaPodrucja = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("područja"));
            Popis podrucjePopis = new PodrucjePopis();
            List<PodrucjeRedak> podrucjaPopisRetci = podrucjePopis.UcitajRetke(datotekaPodrucja).Cast<PodrucjeRedak>().ToList();

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
                    podrucje.Dodijeli(podrucja.FirstOrDefault(p => p.PodrucjeID == dioID));
                }
            }

            Pomocno.ZavrsiProgram("Program izvrsen do kraja.", true);
        }

        private static void testPodrucja()
        {
            Podrucje podrucje1 = new Podrucje("p1");
            Podrucje podrucje2 = new Podrucje("p2");
            Podrucje podrucje3 = new Podrucje("p3");
            Podrucje podrucje4 = new Podrucje("p4");
            Podrucje podrucje5 = new Podrucje("p5");
            Podrucje podrucje6 = new Podrucje("p6");
            Podrucje podrucje7 = new Podrucje("p7");
            Podrucje podrucje8 = new Podrucje("p8");

            podrucje1.Dodijeli(podrucje2);
            podrucje1.Dodijeli(podrucje3);

            podrucje2.Dodijeli(podrucje4);
            podrucje2.Dodijeli(podrucje5);
            podrucje2.Dodijeli(podrucje6);

            podrucje3.Dodijeli(podrucje7);
            podrucje3.Dodijeli(podrucje8);

            podrucje4.Dodijeli(new UlicaPodrucja("u1"));
            podrucje4.Dodijeli(new UlicaPodrucja("u2"));
            podrucje4.Dodijeli(new UlicaPodrucja("u3"));

            podrucje5.Dodijeli(new UlicaPodrucja("u7"));
            podrucje5.Dodijeli(new UlicaPodrucja("u8"));

            podrucje6.Dodijeli(new UlicaPodrucja("u4"));
            podrucje6.Dodijeli(new UlicaPodrucja("u5"));
            podrucje6.Dodijeli(new UlicaPodrucja("u6"));

            podrucje7.Dodijeli(new UlicaPodrucja("u11"));
            podrucje7.Dodijeli(new UlicaPodrucja("u12"));
        }
    }
}
