using damdrempe_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1.Citaci
{
    abstract class Redak { }

    //TODO: extract to separate class
    enum VrstaSpremnika
    {
        Kanta,
        Kontejner
    }

    class SpremnikRedak : Redak {
        public string Naziv { get; set; }

        public VrstaSpremnika Vrsta { get; set; }

        public int BrojnostMali { get; set; }

        public int BrojnostSrednji { get; set; }

        public int BrojnostVeliki { get; set; }

        public int Nosivost { get; set; }
    }

    class UlicaRedak : Redak {
        public string Naziv { get; set; }

        public int BrojMjesta { get; set; }

        public int UdioMalih { get; set; }

        public int UdioSrednjih { get; set; }

        public int UdioVelikih { get; set; }

        //TODO: extract users outside this class
        public List<Korisnik> KorisniciMali { get; set; }

        public List<Korisnik> KorisniciSrednji { get; set; }

        public List<Korisnik> KorisniciVeliki { get; set; }

        public UlicaRedak()
        {
            KorisniciMali = new List<Korisnik>();
            KorisniciSrednji = new List<Korisnik>();
            KorisniciVeliki = new List<Korisnik>();
        }
    }

    enum TipVozila
    {
        Dizel,
        Elektricni
    }

    enum VrstaOtpada
    {
        Staklo,
        Papir,
        Metal,
        Bio,
        Mijesano
    }

    class VoziloRedak : Redak
    {
        public string Naziv { get; set; }

        public TipVozila Tip { get; set; }

        public VrstaOtpada VrstaOtpada { get; set; }

        public int Nosivost { get; set; }

        public List<string> Vozaci { get; set; }

        public VoziloRedak() => Vozaci = new List<string>();
    }

    abstract class Popis
    {
        public Popis()
        {

        }

        public abstract List<Redak> UcitajRetke(string datoteka);
    }

    class UlicaPopisF : Popis
    {
        public override List<Redak> UcitajRetke(string datoteka)
        {
            List<Redak> ulice = new List<Redak>();

            CitacPopisaBuilder citacPopisa = new CitacPopisaBuilder(datoteka);
            citacPopisa.ProcitajRetke();
            for (int brojRetka = 0; brojRetka < citacPopisa.VratiBrojRedaka(); brojRetka++)
            {
                try
                {
                    citacPopisa.ProcitajElementeRetka(brojRetka, ';');
                    if (citacPopisa.VratiBrojElemenataRetka() != 5)
                    {
                        Console.WriteLine("Neispravan redak " + brojRetka);
                        continue;
                    }

                    UlicaRedak ulica = new UlicaRedak();
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
