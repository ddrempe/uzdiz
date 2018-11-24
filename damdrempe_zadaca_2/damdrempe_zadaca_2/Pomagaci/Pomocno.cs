using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_2.Pomagaci
{
    class Pomocno
    {
        public static void ZavrsiProgram(string sadrzajPoruke, bool uspjeh)
        {
            string porukaZavrsetka = uspjeh ? "USPJEH! " : "GRESKA! ";
            porukaZavrsetka += sadrzajPoruke;

            Console.WriteLine(porukaZavrsetka);

            Console.ReadKey();
            Environment.Exit(0);
        }

        public static string DohvatiPutanjuDatoteke(string nazivDatoteke)
        {
            return Path.Combine(Program.PutanjaDatoteka, nazivDatoteke);
        }
    }
}
