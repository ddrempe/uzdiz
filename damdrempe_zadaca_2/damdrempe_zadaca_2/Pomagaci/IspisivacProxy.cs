using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_2.Pomagaci
{
    /// <summary>
    /// Client
    /// </summary>
    public class Ispisivac
    {
        public void IspisiTekst(Ispis ispis, string redakTeksta)
        {
            ispis.ObaviIspis(redakTeksta);
        }
    }

    /// <summary>
    /// Subject
    /// </summary>
    public abstract class Ispis
    {
        public abstract void ObaviIspis(string redakTeksta);
    }

    /// <summary>
    /// Real Subject
    /// </summary>
    class IspisZaslon : Ispis
    {
        public override void ObaviIspis( string redakTeksta)
        {
            Console.WriteLine(redakTeksta);
        }
    }

    /// <summary>
    /// Proxy
    /// </summary>
    class IspisZaslonProxy : Ispis
    {
        IspisZaslon ispisZaslon;
        string datotekaIzlaza;

        public override void ObaviIspis(string redakTeksta)
        {
            ParametriSingleton parametri = ParametriSingleton.DohvatiInstancu(Program.DatotekaParametara);
            datotekaIzlaza = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("izlaz"));

            if (this.provjeriPostojanjeDatoteke())
            {
                ispisZaslon = new IspisZaslon();
                ispisZaslon.ObaviIspis(redakTeksta);

                this.ispisUDatoteku(redakTeksta);
            }
        }

        public bool provjeriPostojanjeDatoteke()
        {         
            if(!File.Exists(datotekaIzlaza))
            {
                using (StreamWriter sw = File.CreateText(datotekaIzlaza)){} // TODO: maknuti using?
                Console.WriteLine($"Stvorena datoteka izlaza {datotekaIzlaza}.");
            }

            return true;
        }

        public void ispisUDatoteku(string redakTeksta)
        {
            using (StreamWriter sw = File.AppendText(datotekaIzlaza))
            {
                sw.WriteLine(redakTeksta);
            }
        }
    }

}
