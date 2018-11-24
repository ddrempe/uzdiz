using System;
using System.Collections.Generic;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Citaci
{
    abstract class Redak { }

    class SpremnikRedak : Redak {
        public string Naziv { get; set; }

        public VrstaSpremnika Vrsta { get; set; }

        public int BrojnostMali { get; set; }

        public int BrojnostSrednji { get; set; }

        public int BrojnostVeliki { get; set; }

        public int Nosivost { get; set; }
    }

    class UlicaRedak : Redak {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public int BrojMjesta { get; set; }

        public int UdioMalih { get; set; }

        public int UdioSrednjih { get; set; }

        public int UdioVelikih { get; set; }
    }

    class VoziloRedak : Redak
    {
        public string ID { get; set; }

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

    class SpremnikPopis : Popis
    {
        public override List<Redak> UcitajRetke(string datoteka)
        {
            List<Redak> spremnici = new List<Redak>();

            CitacPopisaBuilder citacPopisa = new CitacPopisaBuilder(datoteka);
            citacPopisa.ProcitajRetke();
            // počinje se od retka 1 jer je redak indeksa 0 zaglavlje
            for (int brojRetka = 1; brojRetka < citacPopisa.VratiBrojRedaka(); brojRetka++)
            {
                try
                {
                    citacPopisa.ProcitajElementeRetka(brojRetka, ';');
                    if (citacPopisa.VratiBrojElemenataRetka() != 6)
                    {
                        Console.WriteLine("Neispravan redak {0} u datoteci {1}.", brojRetka, datoteka);
                        continue;
                    }

                    SpremnikRedak spremnik = new SpremnikRedak();
                    spremnik.Naziv = citacPopisa.VratiElementRetka(0);
                    spremnik.Vrsta = (VrstaSpremnika)citacPopisa.VratiElementRetkaInt(1);
                    spremnik.BrojnostMali = citacPopisa.VratiElementRetkaInt(2);
                    spremnik.BrojnostSrednji = citacPopisa.VratiElementRetkaInt(3);
                    spremnik.BrojnostVeliki = citacPopisa.VratiElementRetkaInt(4);
                    spremnik.Nosivost = citacPopisa.VratiElementRetkaInt(5);

                    spremnici.Add(spremnik);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Neispravan redak {0} u datoteci {1}.", brojRetka, datoteka);
                }
            }

            return spremnici;
        }
    }

    class UlicaPopis : Popis
    {
        public override List<Redak> UcitajRetke(string datoteka)
        {
            List<Redak> ulice = new List<Redak>();

            CitacPopisaBuilder citacPopisa = new CitacPopisaBuilder(datoteka);
            citacPopisa.ProcitajRetke();
            // počinje se od retka 1 jer je redak indeksa 0 zaglavlje
            for (int brojRetka = 1; brojRetka < citacPopisa.VratiBrojRedaka(); brojRetka++)
            {
                try
                {
                    citacPopisa.ProcitajElementeRetka(brojRetka, ';');
                    if (citacPopisa.VratiBrojElemenataRetka() != 6)
                    {
                        Console.WriteLine("Neispravan redak {0} u datoteci {1}.", brojRetka, datoteka);
                        continue;
                    }

                    UlicaRedak ulica = new UlicaRedak();
                    ulica.ID = citacPopisa.VratiElementRetka(0);
                    ulica.Naziv = citacPopisa.VratiElementRetka(1);
                    ulica.BrojMjesta = citacPopisa.VratiElementRetkaInt(2);
                    ulica.UdioMalih = citacPopisa.VratiElementRetkaInt(3);
                    ulica.UdioSrednjih = citacPopisa.VratiElementRetkaInt(4);
                    ulica.UdioVelikih = citacPopisa.VratiElementRetkaInt(5);

                    ulice.Add(ulica);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Neispravan redak {0} u datoteci {1}.", brojRetka, datoteka);
                }
            }

            return ulice;
        }
    }

    class VoziloPopis : Popis
    {
        public override List<Redak> UcitajRetke(string datoteka)
        {
            List<Redak> vozila = new List<Redak>();

            CitacPopisaBuilder citacPopisa = new CitacPopisaBuilder(datoteka);
            citacPopisa.ProcitajRetke();
            // počinje se od retka 1 jer je redak indeksa 0 zaglavlje
            for (int brojRetka = 1; brojRetka < citacPopisa.VratiBrojRedaka(); brojRetka++)
            {
                try
                {
                    citacPopisa.ProcitajElementeRetka(brojRetka, ';');
                    if (citacPopisa.VratiBrojElemenataRetka() != 6)
                    {
                        Console.WriteLine("Neispravan redak {0} u datoteci {1}.", brojRetka, datoteka);
                        continue;
                    }

                    VoziloRedak vozilo = new VoziloRedak();
                    vozilo.ID = citacPopisa.VratiElementRetka(0);
                    vozilo.Naziv = citacPopisa.VratiElementRetka(1);
                    vozilo.Tip = (TipVozila)citacPopisa.VratiElementRetkaInt(2);
                    vozilo.VrstaOtpada = (VrstaOtpada)citacPopisa.VratiElementRetkaInt(3);
                    vozilo.Nosivost = citacPopisa.VratiElementRetkaInt(4);

                    string[] vozaci = citacPopisa.VratiElementRetka(5).Split(',');
                    foreach (string vozac in vozaci)
                    {
                        vozilo.Vozaci.Add(vozac.Trim());
                    }

                    vozila.Add(vozilo);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Neispravan redak {0} u datoteci {1}.", brojRetka, datoteka);
                }
            }

            return vozila;
        }
    }
}
