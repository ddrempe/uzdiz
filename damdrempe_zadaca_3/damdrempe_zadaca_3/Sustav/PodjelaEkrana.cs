using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_3.Sustav
{
    class PodjelaEkrana
    {
        private static int BrojRedakaGore = 0;
        private static int BrojacLinija = 1;
        private const String ASCIIEscapeChar = "\x1B[";

        public PodjelaEkrana(int brojRedakaGore)
        {
            BrojRedakaGore = brojRedakaGore;
            Console.Clear();
        }

        private void IzvrsiKontrolu(string nastavakKontrole)
        {
            Console.WriteLine($"\x1B[{nastavakKontrole}");
        }

        private void IzvrsiKontroluKursorURedak(int redak)
        {
            IzvrsiKontrolu($"{redak};0H");
        }

        private void IzvrsiKontroluBrisiGore()
        {
            IzvrsiKontrolu("1J");
        }

        private void IzvrsiKontroluBrisiDolje()
        {
            IzvrsiKontrolu("J");
        }

        public void Ispis(string redakTeksta)
        {
            IzvrsiKontroluKursorURedak(BrojacLinija);
            Console.WriteLine(redakTeksta);
            BrojacLinija++;

            if (BrojacLinija >= BrojRedakaGore + 1)
            {
                string unos;
                do
                {
                    IzvrsiKontroluKursorURedak(BrojRedakaGore + 1);
                    IzvrsiKontroluBrisiDolje();
                    IzvrsiKontroluKursorURedak(BrojRedakaGore + 1);

                    for (int i = 0; i < 80; i++)
                    {
                        Console.Write("-");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Za nastavak pritisnite n/N");

                    unos = Console.ReadLine();
                } while (unos != "N" && unos != "n");

                IzvrsiKontroluKursorURedak(BrojacLinija);
                IzvrsiKontroluBrisiGore();
                BrojacLinija = 1;
            }            
        }
    }
}
