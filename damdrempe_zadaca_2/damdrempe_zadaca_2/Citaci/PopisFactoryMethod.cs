using System;
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

    class PodrucjeRedak : Redak
    {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public List<string> Dijelovi { get; set; }

        public PodrucjeRedak(CitacPopisaBuilder citacPopisa)
        {
            ID = citacPopisa.VratiElementRetka(0);
            Naziv = citacPopisa.VratiElementRetka(1);

            Dijelovi = new List<string>();
            string[] dijelovi = citacPopisa.VratiElementRetka(2).Split(',');
            foreach (string dio in dijelovi)
            {
                Dijelovi.Add(dio.Trim());
            }
        }
    }

    class KomandaRedak : Redak
    {
        public VrstaKomande Vrsta { get; set; }

        public int Broj { get; set; }

        public List<string> Vozila { get; set; }

        public KomandaRedak(CitacPopisaBuilder citacPopisa)
        {
            string prviDioKomande = citacPopisa.VratiElementRetka(0);
            if (prviDioKomande.StartsWith(VrstaKomande.KRENI.ToString()))
            {
                string[] prviDioKomandeSplit = prviDioKomande.Split(' ');
                Vrsta = (VrstaKomande)Enum.Parse(typeof(VrstaKomande), prviDioKomandeSplit[0], true);

                if(prviDioKomandeSplit.Length == 2)
                {
                    Vrsta = VrstaKomande.KRENI_N;
                    Broj = int.Parse(prviDioKomandeSplit[1]);
                }
            }
            else
            {
                Vrsta = (VrstaKomande)Enum.Parse(typeof(VrstaKomande), prviDioKomande, true);
                if(Vrsta != VrstaKomande.STATUS)
                {
                    Vozila = new List<string>();
                    string[] vozila = citacPopisa.VratiElementRetka(1).Split(',');
                    foreach (string vozilo in vozila)
                    {
                        Vozila.Add(vozilo.Trim());
                    }
                }
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

    class PodrucjePopis : Popis
    {
        public override List<Redak> UcitajRetke(string datoteka)
        {
            List<Redak> podrucja = new List<Redak>();

            CitacPopisaBuilder citacPopisa = new CitacPopisaBuilder(datoteka);
            citacPopisa.ProcitajRetke();
            // počinje se od retka 1 jer je redak indeksa 0 zaglavlje
            for (int brojRetka = 1; brojRetka < citacPopisa.VratiBrojRedaka(); brojRetka++)
            {
                try
                {
                    citacPopisa.ProcitajElementeRetka(brojRetka, ';');
                    if (citacPopisa.VratiBrojElemenataRetka() != 3)
                    {
                        Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
                        continue;
                    }

                    PodrucjeRedak podrucje = new PodrucjeRedak(citacPopisa);
                    podrucja.Add(podrucje);
                }
                catch (FormatException)
                {
                    Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
                }
            }

            return podrucja;
        }
    }

    class KomandaPopis : Popis
    {
        public override List<Redak> UcitajRetke(string datoteka)
        {
            List<Redak> komande = new List<Redak>();

            CitacPopisaBuilder citacPopisa = new CitacPopisaBuilder(datoteka);
            citacPopisa.ProcitajRetke();
            // počinje se od retka 1 jer je redak indeksa 0 zaglavlje
            for (int brojRetka = 1; brojRetka < citacPopisa.VratiBrojRedaka(); brojRetka++)
            {
                try
                {
                    citacPopisa.ProcitajElementeRetka(brojRetka, ';');
                    if (!(citacPopisa.VratiBrojElemenataRetka() == 1 || citacPopisa.VratiBrojElemenataRetka() == 2))
                    {
                        Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
                        continue;
                    }

                    KomandaRedak podrucje = new KomandaRedak(citacPopisa);
                    komande.Add(podrucje);
                }
                catch (FormatException)
                {
                    Program.Ispisivac.Koristi($"Neispravan redak {brojRetka} u datoteci {datoteka}.");
                }
            }

            return komande;
        }
    }
}
