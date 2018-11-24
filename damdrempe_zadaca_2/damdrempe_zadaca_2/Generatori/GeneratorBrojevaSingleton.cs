using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_2
{
    public interface IGeneratorBrojeva
    {
        int DajSlucajniBrojInt(int minVrijednost, int maxVrijednost);

        long DajSlucajniBrojLong(long minVrijednost, long maxVrijednost);

        float DajSlucajniBrojFloat(float minVrijednost, float maxVrijednost, int brojDecimala);
    }

    class GeneratorBrojevaSingleton : IGeneratorBrojeva
    {
        private static GeneratorBrojevaSingleton instanca;

        private static Random random;

        private GeneratorBrojevaSingleton(int sjemeGeneratora)
        {
            random = new Random(sjemeGeneratora);
        }

        public static GeneratorBrojevaSingleton DohvatiInstancu(int sjemeGeneratora)
        {
            if (instanca == null)
            {
                instanca = new GeneratorBrojevaSingleton(sjemeGeneratora);
            }

            return instanca;
        }

        public int DajSlucajniBrojInt(int minVrijednost, int maxVrijednost)
        {
            return random.Next(minVrijednost, maxVrijednost);
        }

        public long DajSlucajniBrojLong(long minVrijednost, long maxVrijednost)
        {
            byte[] spremnik = new byte[8];
            random.NextBytes(spremnik);
            long slucajniBroj = BitConverter.ToInt64(spremnik, 0);

            return (Math.Abs(slucajniBroj % (maxVrijednost - minVrijednost)) + minVrijednost);
        }

        public float DajSlucajniBrojFloat(float minVrijednost, float maxVrijednost, int brojDecimala)
        {
            double slucajniBrojDouble = random.NextDouble() * (maxVrijednost - minVrijednost) + minVrijednost;

            return (float)Math.Round(slucajniBrojDouble, brojDecimala);
        }
    }
}
