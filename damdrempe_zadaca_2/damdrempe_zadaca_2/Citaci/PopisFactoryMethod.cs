﻿using System;
using System.Collections.Generic;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Citaci
{
    abstract class Redak { }

    class SpremnikRedak : Redak {        

        public string NazivPremaOtpadu { get; set; }

        public VrstaSpremnika Vrsta { get; set; }

        public int BrojnostMali { get; set; }

        public int BrojnostSrednji { get; set; }

        public int BrojnostVeliki { get; set; }

        public int Nosivost { get; set; }

        public SpremnikRedak(CitacPopisaBuilder citacPopisa)
        {
            NazivPremaOtpadu = citacPopisa.VratiElementRetka(0);
            Vrsta = (VrstaSpremnika)citacPopisa.VratiElementRetkaInt(1);
            BrojnostMali = citacPopisa.VratiElementRetkaInt(2);
            BrojnostSrednji = citacPopisa.VratiElementRetkaInt(3);
            BrojnostVeliki = citacPopisa.VratiElementRetkaInt(4);
            Nosivost = citacPopisa.VratiElementRetkaInt(5);
        }
    }

    class UlicaRedak : Redak {        

        public string ID { get; set; }

        public string Naziv { get; set; }

        public int BrojMjesta { get; set; }

        public int UdioMalih { get; set; }

        public int UdioSrednjih { get; set; }

        public int UdioVelikih { get; set; }

        public UlicaRedak(CitacPopisaBuilder citacPopisa)
        {
            ID = citacPopisa.VratiElementRetka(0);
            Naziv = citacPopisa.VratiElementRetka(1);
            BrojMjesta = citacPopisa.VratiElementRetkaInt(2);
            UdioMalih = citacPopisa.VratiElementRetkaInt(3);
            UdioSrednjih = citacPopisa.VratiElementRetkaInt(4);
            UdioVelikih = citacPopisa.VratiElementRetkaInt(5);
        }
    }

    class VoziloRedak : Redak
    {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public TipVozila Tip { get; set; }

        public VrstaOtpada VrstaOtpada { get; set; }

        public int Nosivost { get; set; }

        public List<string> Vozaci { get; set; }

        public VoziloRedak(CitacPopisaBuilder citacPopisa)
        {            
            ID = citacPopisa.VratiElementRetka(0);
            Naziv = citacPopisa.VratiElementRetka(1);
            Tip = (TipVozila)citacPopisa.VratiElementRetkaInt(2);
            VrstaOtpada = (VrstaOtpada)citacPopisa.VratiElementRetkaInt(3);
            Nosivost = citacPopisa.VratiElementRetkaInt(4);

            Vozaci = new List<string>();
            string[] vozaci = citacPopisa.VratiElementRetka(5).Split(',');
            foreach (string vozac in vozaci)
            {
                Vozaci.Add(vozac.Trim());
            }
        }
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
                        Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
                        continue;
                    }

                    SpremnikRedak spremnik = new SpremnikRedak(citacPopisa);
                    spremnici.Add(spremnik);
                }
                catch (FormatException)
                {
                    Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
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
                        Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
                        continue;
                    }

                    UlicaRedak ulica = new UlicaRedak(citacPopisa);                   
                    ulice.Add(ulica);
                }
                catch (FormatException)
                {
                    Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
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
                        Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
                        continue;
                    }

                    VoziloRedak vozilo = new VoziloRedak(citacPopisa);                 
                    vozila.Add(vozilo);
                }
                catch (FormatException)
                {
                    Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
                }
            }

            return vozila;
        }
    }
}