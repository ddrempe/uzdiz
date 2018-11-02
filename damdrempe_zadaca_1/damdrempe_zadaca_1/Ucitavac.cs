using damdrempe_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1
{
    class Ucitavac
    {
        public static List<Ulica> UcitajUlice(string datoteka)
        {
            List<Ulica> ulice = new List<Ulica>();

            CitacPopisa citacPopisa = new CitacPopisa(datoteka);
            citacPopisa.ProcitajRetke();
            for (int brojRetka = 0; brojRetka < citacPopisa.VratiRetke().Length; brojRetka++)
            {
                try
                {
                    citacPopisa.ProcitajElementeRetka(brojRetka, ';');
                    Ulica ulica = new Ulica();
                    ulica.Naziv = citacPopisa.VratiElementRetka(0);
                    ulica.BrojMjesta = citacPopisa.VratiElementRetkaInt(1);
                    ulica.UdioMalih = citacPopisa.VratiElementRetkaInt(2);
                    ulica.UdioSrednjih = citacPopisa.VratiElementRetkaInt(3);
                    ulica.UdioVelikih = citacPopisa.VratiElementRetkaInt(4);

                    ulice.Add(ulica);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Neispravan redak " + brojRetka);
                }                
            }

            return ulice;
        }
    }
}
