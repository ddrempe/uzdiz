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

            string nazivDatotekeParametara = args[0];
            if(!File.Exists(nazivDatotekeParametara))
            {
                ZavrsiProgram("Datoteka s parametrima ne postoji!", false);
            }

            SingletonParametri parametri = SingletonParametri.DohvatiInstancu(nazivDatotekeParametara);
            int sjemeGeneratora = int.Parse(parametri.DohvatiParametar("sjemeGeneratora"));
            SingletonGeneratorBrojeva generatorBrojeva = SingletonGeneratorBrojeva.DohvatiInstancu(sjemeGeneratora);

            Ucitavac.UcitajUlice(parametri.DohvatiParametar("ulice"));

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
