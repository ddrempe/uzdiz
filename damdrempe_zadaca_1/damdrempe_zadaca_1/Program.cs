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
            SingletonParametri parametri = SingletonParametri.DohvatiInstancu(datotekaParametara);
            int sjemeGeneratora = int.Parse(parametri.DohvatiParametar("sjemeGeneratora"));
            SingletonGeneratorBrojeva generatorBrojeva = SingletonGeneratorBrojeva.DohvatiInstancu(sjemeGeneratora);

            string putanjaDatoteka = Path.GetDirectoryName(datotekaParametara);
            Ucitavac.UcitajUlice(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("ulice")));
            Ucitavac.UcitajVozila(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("vozila")));
            Ucitavac.UcitajSpremnike(Path.Combine(putanjaDatoteka, parametri.DohvatiParametar("spremnici")));

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
