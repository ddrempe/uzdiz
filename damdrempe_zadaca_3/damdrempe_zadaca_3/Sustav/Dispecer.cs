using damdrempe_zadaca_3.Citaci;
using damdrempe_zadaca_3.Podaci.Modeli;
using damdrempe_zadaca_3.Sustav.damdrempe_zadaca_2.Sustav;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_3.Podaci.Enumeracije;

namespace damdrempe_zadaca_3.Sustav
{
    class Dispecer
    {
        public static int TrenutniCiklus;

        public static List<PrijevozPutnika> listaPrijevozPutnika = new List<PrijevozPutnika>();

        public static void ObradiKomanduPripremi(KomandaRedak komanda)
        {
            foreach (string voziloID in komanda.Lista)
            {
                Vozilo vozilo = Program.Vozila.FirstOrDefault(v => v.ID == voziloID);
                if(vozilo != null)
                {
                    //provjeri da li vozilo već postoji
                    Vozilo trazenoVoziloUObradi = Program.Vozila.FirstOrDefault(v => v.ID == vozilo.ID);
                    if (trazenoVoziloUObradi.TrenutnoStanje != VrstaStanja.Skupljanje)
                    {
                        Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                        Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozilo {vozilo.ID} je stavljeno u status skupljanja.");
                        Program.Ispisivac.ResetirajPostavkeBoja();
                        vozilo.PromijeniStanje(VrstaStanja.Skupljanje);
                    }
                    else
                    {
                        Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                        Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozilo {vozilo.ID} je vec u stanju skupljanja.");
                        Program.Ispisivac.ResetirajPostavkeBoja();
                    }
                }
                else
                {
                    Program.Ispisivac.Koristi($"Ne postoji vozilo {voziloID} za komandu {komanda.Vrsta}");
                }
            }
        }

        public static void ObradiKomanduKreni(KomandaRedak komanda)
        {
            komanda.Broj = int.MaxValue;
            ObradiKomanduKreniN(komanda);
        }

        public static void ObradiKomanduKreniN(KomandaRedak komanda)
        {
            int brojPotrebnihCiklusa = komanda.Broj;
            int redniBrojVozila = 0;

            List<Vozilo> vozilaKojaSkupljaju = Program.Vozila.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Skupljanje)).ToList();
            List<Vozilo> vozilaZaPraznjenje = Program.Vozila.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Praznjenje)).ToList();
            for (TrenutniCiklus = 1; TrenutniCiklus < brojPotrebnihCiklusa; TrenutniCiklus++)
            {
                if (vozilaKojaSkupljaju.Count == 0 && vozilaZaPraznjenje.Count == 0)
                {
                    break;
                }

                if (vozilaKojaSkupljaju.Count > 0)
                {
                    if (redniBrojVozila >= vozilaKojaSkupljaju.Count) redniBrojVozila = 0;

                    bool voziloIzaslo = false;
                    PokupiOtpad(vozilaKojaSkupljaju[redniBrojVozila], ref voziloIzaslo);
                    if(!voziloIzaslo) redniBrojVozila++;
                }                    

                if (vozilaZaPraznjenje.Count > 0) OdradiCiklusPraznjenja();

                vozilaKojaSkupljaju = Program.Vozila.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Skupljanje)).ToList();
                vozilaZaPraznjenje = Program.Vozila.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Praznjenje)).ToList();
            }

            Program.Ispisivac.Koristi($"C{TrenutniCiklus} Zavrseno izvrsavanje komande {komanda.Vrsta}.");
            Program.Ispisivac.Koristi();
        }

        private static void OdradiCiklusPraznjenja()
        {
            List<Vozilo> vozilaZaPraznjenje = Program.Vozila.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Praznjenje)).ToList();
            foreach (Vozilo vozilo in vozilaZaPraznjenje)
            {
                if(vozilo.BrojPreostalihCiklusa <= 0)
                {
                    vozilo.PromijeniStanje(VrstaStanja.Skupljanje);
                    Statistika.DeponijUkupanOtpad[vozilo.VrstaOtpada] += vozilo.KolicinaOtpada;
                    vozilo.KolicinaOtpada = 0;
                    Program.Ispisivac.Koristi($"C{TrenutniCiklus} Vozilo {vozilo.ID} je zavrsilo s odvozom otpada i spremno je za skupljanje.");
                    //TODO: stavi vozilo na kraj liste

                    PrijevozPutnika prijevozPutnika = listaPrijevozPutnika.FirstOrDefault(p => p.VoziloID == vozilo.ID);
                    if(prijevozPutnika != null)
                    {
                        prijevozPutnika.IskrcajPutnike();
                    }
                }
                
                vozilo.BrojPreostalihCiklusa--;
            }
        }

        private static void PokupiOtpad(Vozilo vozilo, ref bool voziloIzaslo)
        {
            voziloIzaslo = false;
            //preskoči prazne spremnike
            while(!vozilo.IteratorS.Kraj && vozilo.IteratorS.Trenutni.KolicinaOtpada == 0)
            {
                vozilo.IteratorS.Sljedeci();                
            }

            if (vozilo.IteratorS.Kraj)
            {
                Program.Ispisivac.Koristi($"C{TrenutniCiklus} Nema vise otpada za vozilo {vozilo.ID}");
                vozilo.PromijeniStanje(VrstaStanja.Parkirano);  //vozilo za koje vise nema otpada se vraća na prakiralište
                voziloIzaslo = true;
                return;
            }

            Spremnik spremnik = vozilo.IteratorS.Trenutni;

            float preostaliKapacitetVozila = vozilo.Nosivost - vozilo.KolicinaOtpada;
            float kolicinaUzetogOtpadaSpremnika = spremnik.KolicinaOtpada;
            if (kolicinaUzetogOtpadaSpremnika > preostaliKapacitetVozila)
            {
                float kolicinaOtpadaViska = kolicinaUzetogOtpadaSpremnika - preostaliKapacitetVozila;
                kolicinaUzetogOtpadaSpremnika = kolicinaUzetogOtpadaSpremnika - kolicinaOtpadaViska;
                Program.Ispisivac.Koristi($"ODVOZ Vozilo {vozilo.ID} ({vozilo.VrstaOtpada}) je puno ({vozilo.Nosivost}kg) i mora na odvoz.");
                Program.Ispisivac.Koristi($"ODVOZ Spremnik {spremnik.ID} ima jos {kolicinaOtpadaViska}kg otpada vrste {spremnik.NazivPremaOtpadu}.");
                Statistika.VoziloBrojOdlazakaNaDeponij[vozilo.ID]++;
                PrijevozPutnika prijevozPutnika = new PrijevozPutnika(vozilo, vozilo.ID);
                prijevozPutnika.UkrcajPutnika($"Putnik{TrenutniCiklus}");
                listaPrijevozPutnika.Add(prijevozPutnika);

                //ako je vozilo puno promijeni stanje, posalji ga na praznjenje n ciklusa nakon kojih se vraca na kraj liste
                vozilo.PromijeniStanje(VrstaStanja.Praznjenje);
                vozilo.BrojPreostalihCiklusa = Program.Parametri.DohvatiParametarInt("brojRadnihCiklusaZaOdvoz");
                voziloIzaslo = true;
            }

            vozilo.KolicinaOtpada += kolicinaUzetogOtpadaSpremnika;
            spremnik.KolicinaOtpada -= kolicinaUzetogOtpadaSpremnika;
            Statistika.VoziloBrojPreuzetihSpremnika[vozilo.ID]++;
            Statistika.VoziloKolicinaPreuzetogOtpada[vozilo.ID] += kolicinaUzetogOtpadaSpremnika;

            Program.Ispisivac.Koristi($"C{TrenutniCiklus} Vozilo {vozilo.ID} ({vozilo.VrstaOtpada}) trenutno ima {vozilo.KolicinaOtpada}kg otpada, preostali kapacitet je {vozilo.Nosivost - vozilo.KolicinaOtpada}kg.");
            vozilo.IteratorS.Sljedeci(); //TODO: provjeri ako je iduci spremnik u iducoj ulici i iteriraj ulicu                 
        }

        public static void ObradiKomanduKontrola(KomandaRedak komanda)
        {
            foreach (string voziloID in komanda.Lista)
            {
                Vozilo vozilo = Program.Vozila.FirstOrDefault(v => v.ID == voziloID);
                if (vozilo != null)
                {
                    vozilo.PromijeniStanje(VrstaStanja.Kontrola);
                    Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                    Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozilo {vozilo.ID} je u stanju kontrole.");
                    Program.Ispisivac.ResetirajPostavkeBoja();
                }
                else
                {
                    Program.Ispisivac.Koristi($"Ne postoji vozilo {voziloID} za komandu {komanda.Vrsta}");
                }
            }
        }

        public static void ObradiKomanduIsprazni(KomandaRedak komanda)
        {
            foreach (string voziloID in komanda.Lista)
            {
                Vozilo vozilo = Program.Vozila.FirstOrDefault(v => v.ID == voziloID);
                if (vozilo != null)
                {
                    vozilo.PromijeniStanje(VrstaStanja.Praznjenje);
                    vozilo.BrojPreostalihCiklusa = Program.Parametri.DohvatiParametarInt("brojRadnihCiklusaZaOdvoz");
                    Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                    Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozilo {vozilo.ID} je poslano na odvoz.");
                    Program.Ispisivac.ResetirajPostavkeBoja();

                    Statistika.VoziloBrojOdlazakaNaDeponij[vozilo.ID]++;
                }
                else
                {
                    Program.Ispisivac.Koristi($"Ne postoji vozilo {voziloID} za komandu {komanda.Vrsta}");
                }
            }
        }

        public static void ObradiKomanduKvar(KomandaRedak komanda)
        {
            foreach (string voziloID in komanda.Lista)
            {
                Vozilo vozilo = Program.Vozila.FirstOrDefault(v => v.ID == voziloID);
                if (vozilo != null)
                {
                    vozilo.PromijeniStanje(VrstaStanja.Pokvareno);
                    Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                    Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozilo {vozilo.ID} je u stanju kvara.");
                    Program.Ispisivac.ResetirajPostavkeBoja();
                }
                else
                {
                    Program.Ispisivac.Koristi($"Ne postoji vozilo {voziloID} za komandu {komanda.Vrsta}");
                }
            }
        }

        public static void ObradiKomanduStatus(KomandaRedak komanda)
        {
            List<Vozilo> vozilaKojaNisuUKvaru = Program.Vozila.Where(v => !v.TrenutnoStanje.Equals(VrstaStanja.Pokvareno)).ToList();

            string redakZaIspis = String.Format("|{0,5}|{1,15}|{2,15}|{3,10}|{4,20}|{5,20}|",
                    "ID", "Naziv", "Vrsta", "Nosivost", "Kolicina otpada", "Stanje");
            Program.Ispisivac.Koristi(redakZaIspis);

            foreach (Vozilo vozilo in vozilaKojaNisuUKvaru)
            {
                redakZaIspis =
                    String.Format("|{0,5}|{1,15}|{2,15}|{3,10}|{4,20}|{5,20}|",
                    vozilo.ID, vozilo.Naziv, vozilo.VrstaOtpada, vozilo.Nosivost, vozilo.KolicinaOtpada, vozilo.TrenutnoStanje);
                Program.Ispisivac.Koristi(redakZaIspis);
            }
            Program.Ispisivac.Koristi();
        }

        public static void ObradiKomanduVozaca(KomandaRedak komanda, StatusVozaca statusVozaca)
        {
            foreach (Vozilo vozilo in Program.Vozila)
            {
                foreach (string imeVozaca in komanda.Lista)
                {
                    Vozac vozac = vozilo.Vozaci.FirstOrDefault(v => v.Ime == imeVozaca);
                    if (vozac != null)
                    {
                        vozac.Status = statusVozaca;
                        Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                        Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozac {vozac.Ime} vozila {vozilo.ID} je u sada u statusu {vozac.Status}.");
                        Program.Ispisivac.ResetirajPostavkeBoja();
                    }
                }
            }
        }

        public static void ObradiKomanduGodisnjiOdmor(KomandaRedak komanda)
        {
            ObradiKomanduVozaca(komanda, StatusVozaca.Godisnji);        
        }

        public static void ObradiKomanduBolovanje(KomandaRedak komanda)
        {
            ObradiKomanduVozaca(komanda, StatusVozaca.Bolovanje);
        }

        public static void ObradiKomanduOtkaz(KomandaRedak komanda)
        {
            ObradiKomanduVozaca(komanda, StatusVozaca.Otkaz);
        }

        public static void ObradiKomanduNovi(KomandaRedak komanda)
        {
            foreach (string imeVozaca in komanda.Lista)
            {
                Vozac vozac = new Vozac(imeVozaca);
                Program.NoviVozaci.Add(vozac);
                Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozac {vozac.Ime} je novi vozac.");
                Program.Ispisivac.ResetirajPostavkeBoja();
            }
        }

        public static void ObradiKomanduIzlaz(KomandaRedak komanda)
        {
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
            Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Izlaz iz programa.");
            Program.Ispisivac.ResetirajPostavkeBoja();
            //Environment.Exit(0); // TODO: odkomentirati
        }

        public static void ObradiKomanduPreuzmi(KomandaRedak komanda)
        {
            foreach (Vozilo dosadasnjeVozilo in Program.Vozila)
            {
                Vozac vozac = dosadasnjeVozilo.Vozaci.FirstOrDefault(v => v.Ime == komanda.Vozac);
                if (vozac != null)
                {
                    dosadasnjeVozilo.Vozaci.Remove(vozac);
                    Vozilo novoVozilo = Program.Vozila.FirstOrDefault(v => v.ID == komanda.Vozilo);
                    if (novoVozilo != null)
                    {
                        novoVozilo.Vozaci.Add(vozac);
                        vozac.IDVozila = novoVozilo.ID;
                    }

                    Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                    Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozac {vozac.Ime} vozila {dosadasnjeVozilo.ID} preuzima vozilo {novoVozilo.ID}.");
                    Program.Ispisivac.ResetirajPostavkeBoja();
                    return;
                }
            }

            Vozac noviVozac = Program.NoviVozaci.FirstOrDefault(v => v.Ime == komanda.Vozac);
            if (noviVozac != null)
            {
                Vozilo novoVozilo = Program.Vozila.FirstOrDefault(v => v.ID == komanda.Vozilo);
                if (novoVozilo != null)
                {
                    novoVozilo.Vozaci.Add(noviVozac);
                    noviVozac.IDVozila = novoVozilo.ID;
                }

                Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
                Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Vozac {noviVozac.Ime} bez dosadasnjeg vozila preuzima vozilo {novoVozilo.ID}.");
                Program.Ispisivac.ResetirajPostavkeBoja();
            }
        }

        private static List<Vozac> DohvatiSveVozace()
        {
            List<Vozac> sviVozaci = new List<Vozac>();

            foreach (Vozilo vozilo in Program.Vozila)
            {                
                sviVozaci.AddRange(vozilo.Vozaci.ToList());
            }
            sviVozaci.AddRange(Program.NoviVozaci);

            return sviVozaci;
        }

        public static void ObradiKomanduVozaci(KomandaRedak komanda)
        {
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Blue);
            Program.Ispisivac.Koristi($"KOMANDA {komanda.Vrsta}. Ispis statusa i ostalih podataka vozaca.");
            Program.Ispisivac.ResetirajPostavkeBoja();

            List<Vozac> sviVozaci = DohvatiSveVozace();

            string redakZaIspis = String.Format("|{0,5}|{1,10}|{2,15}|{3,5}|",
                    "#","Ime", "Status", "Vozilo");
            Program.Ispisivac.Koristi(redakZaIspis);

            for (int i=0; i < sviVozaci.Count; i++)
            {
                Vozac vozac = sviVozaci[i];
                redakZaIspis =
                    String.Format("|{0,5}|{1,10}|{2,15}|{3,5}|",
                    i+1, vozac.Ime, vozac.Status, vozac.IDVozila);
                Program.Ispisivac.Koristi(redakZaIspis);
            }
            Program.Ispisivac.Koristi();
        }

    }
}
