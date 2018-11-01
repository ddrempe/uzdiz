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
                Console.WriteLine("Broj argumenata mora biti jednak 1!");
            }

            string nazivDatotekeParametara = args[0];
            SingletonParametri singletonParametri = SingletonParametri.DohvatiInstancu(nazivDatotekeParametara);
            Console.WriteLine(singletonParametri.DohvatiParametar("spremnici"));

            Console.Read();
        }
    }
}
