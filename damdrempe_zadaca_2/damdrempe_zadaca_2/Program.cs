using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Pomagaci;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace damdrempe_zadaca_2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                ZavrsiProgram("Broj argumenata mora biti jednak 1.", false);
            }

            string datotekaParametara = args[0];
            if(!File.Exists(datotekaParametara))
            {
                ZavrsiProgram("Datoteka s parametrima ne postoji!", false);
            }

            ParametriSingleton parametri = ParametriSingleton.DohvatiInstancu(datotekaParametara);
            string putanjaDatoteka = Path.GetDirectoryName(datotekaParametara);            

            string datotekaUlice = Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("ulice"));
            Popis ulicaPopis = new UlicaPopis();
            List<Redak> ulicaPopisRetci = ulicaPopis.UcitajRetke(datotekaUlice);

            string datotekaSpremnika = Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("spremnici"));
            Popis spremnikPopis = new SpremnikPopis();
            List<Redak> spremnikPopisRetci = spremnikPopis.UcitajRetke(datotekaSpremnika);

            string datotekaVozila = Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("vozila"));
            Popis voziloPopis = new VoziloPopis();
            List<Redak> voziloPopisRetci = voziloPopis.UcitajRetke(datotekaVozila);

            List<Ulica> pripremljeneUlice = PripremateljPrototype.PripremiUlice(ulicaPopisRetci.Cast<UlicaRedak>().ToList());
            List<Spremnik> pripremljeniSpremnici = PripremateljPrototype.PripremiSpremnike(spremnikPopisRetci.Cast<SpremnikRedak>().ToList());

            List<Ulica> ulice = GeneratorEntiteta.StvoriKorisnike(pripremljeneUlice);
            List<Spremnik> spremnici = GeneratorEntiteta.StvoriSpremnike(pripremljeneUlice, pripremljeniSpremnici);

            ulice = Inicijalizator.OdrediOtpadKorisnicima(ulice, datotekaParametara);

            ZavrsiProgram("Program izvrsen do kraja.", true);
        }

        private static void ZavrsiProgram(string sadrzajPoruke, bool uspjeh)
        {
            string porukaZavrsetka = uspjeh ? "USPJEH! " : "GRESKA! ";
            porukaZavrsetka += sadrzajPoruke;

            Console.WriteLine(porukaZavrsetka);
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
