using damdrempe_zadaca_1.Citaci;
using damdrempe_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1
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
            int sjemeGeneratora = int.Parse(parametri.DohvatiParametar("sjemeGeneratora"));
            GeneratorBrojevaSingleton generatorBrojeva = GeneratorBrojevaSingleton.DohvatiInstancu(sjemeGeneratora);

            string putanjaDatoteka = Path.GetDirectoryName(datotekaParametara);

            Popis ulicaPopis = new UlicaPopis();
            List<Redak> retciUlice = ulicaPopis.UcitajRetke(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("ulice")));

            Popis voziloPopis = new VoziloPopis();
            List<Redak> retciVozila = voziloPopis.UcitajRetke(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("vozila")));

            Popis spremnikPopis = new SpremnikPopis();
            List<Redak> retciSpremnici = spremnikPopis.UcitajRetke(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("spremnici")));

            // Refaktorirano za korištenje Abstract Factory
            //List<UlicaCitanje> ulice = Ucitavac.UcitajUlice(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("ulice")));
            //List<VoziloCitanje> vozila = Ucitavac.UcitajVozila(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("vozila")));
            //List<SpremnikCitanje> spremnici = Ucitavac.UcitajSpremnike(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("spremnici")));

            //ulice = GeneratorEntiteta.StvoriKorisnike(ulice);
            //GeneratorEntiteta.StvoriSpremnike(ulice, spremnici);

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
