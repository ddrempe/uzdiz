﻿using damdrempe_zadaca_1.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_1.Podaci.Enumeracije;

namespace damdrempe_zadaca_1
{
    /// <summary>
    /// Klasa prije refaktoriranja i dodavanja PopisFactoryMethod.
    /// Trenutno se ne koristi, ostavljena samo za usporedbu prije i poslije refaktoriranja.
    /// </summary>
    class Ucitavac
    {
        public static List<UlicaCitanje> UcitajUlice(string datoteka)
        {
            List<UlicaCitanje> ulice = new List<UlicaCitanje>();

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

                    UlicaCitanje ulica = new UlicaCitanje();
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

        public static List<SpremnikCitanje> UcitajSpremnike(string datoteka)
        {
            List<SpremnikCitanje> spremnici = new List<SpremnikCitanje>();

            CitacPopisaBuilder citacPopisa = new CitacPopisaBuilder(datoteka);
            citacPopisa.ProcitajRetke();
            for (int brojRetka = 0; brojRetka < citacPopisa.VratiBrojRedaka(); brojRetka++)
            {
                try
                {
                    citacPopisa.ProcitajElementeRetka(brojRetka, ';');
                    if(citacPopisa.VratiBrojElemenataRetka() != 6)
                    {
                        Console.WriteLine("Neispravan redak " + brojRetka);
                        continue;
                    }

                    SpremnikCitanje spremnik = new SpremnikCitanje();
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
                    Console.WriteLine("Neispravan redak " + brojRetka);
                }
            }

            return spremnici;
        }

        public static List<VoziloCitanje> UcitajVozila(string datoteka)
        {
            List<VoziloCitanje> vozila = new List<VoziloCitanje>();

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

                    VoziloCitanje vozilo = new VoziloCitanje();
                    vozilo.Naziv = citacPopisa.VratiElementRetka(0);
                    vozilo.Tip = (TipVozila)citacPopisa.VratiElementRetkaInt(1);
                    vozilo.VrstaOtpada = (VrstaOtpada)citacPopisa.VratiElementRetkaInt(2);
                    vozilo.Nosivost = citacPopisa.VratiElementRetkaInt(3);

                    string[] vozaci = citacPopisa.VratiElementRetka(4).Split(',');
                    foreach (string vozac in vozaci)
                    {
                        vozilo.Vozaci.Add(vozac.Trim());
                    }

                    vozila.Add(vozilo);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Neispravan redak " + brojRetka);
                }
            }

            return vozila;
        }
    }
}
