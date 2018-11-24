using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_2.Pomagaci
{
    public interface IIspisivac
    {
        void Koristi();

        void Koristi(string redakTeksta);
    }

    class IspisivacSingleton : IIspisivac
    {
        private static IspisivacSingleton instanca;
        private static Ispisivac ispisivac;
        private static IspisZaslonProxy ispisZaslonProxy;

        private IspisivacSingleton()
        {
            
        }

        public static IspisivacSingleton DohvatiInstancu()
        {
            if (instanca == null)
            {
                instanca = new IspisivacSingleton();
                ispisivac = new Ispisivac();
                ispisZaslonProxy = new IspisZaslonProxy();
            }

            return instanca;
        }

        public void Koristi(string redakTeksta)
        {            
            ispisivac.IspisiTekst(ispisZaslonProxy, redakTeksta);
        }

        public void Koristi()
        {
            Koristi("\n");
        }
    }
}
