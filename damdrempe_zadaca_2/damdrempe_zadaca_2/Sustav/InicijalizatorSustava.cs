using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Pomagaci.Entiteti;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static damdrempe_zadaca_2.Podaci.Enumeracije;
using static damdrempe_zadaca_2.Pomagaci.Entiteti.PodrucjaComposite;

namespace damdrempe_zadaca_2.Sustav
{
    class InicijalizatorSustava
    {

        public static void Pokreni()
        {
            UcitajParametre();
            UcitajZapiseIzDatoteka();
            StvoriKonacnePodatkeSustava();
            StvoriOtpad();
            IzracunajOtpadPoUlicama();

            IspisiOtpadPodrucja();
            IspisiOtpadPodrucjaTablicno();

            StvoriRasporedOdvozaVozilima();

            //IspisiStatistiku();
        }

        private static void UcitajParametre()
        {
            if (!File.Exists(Program.DatotekaParametara))
            {
                Pomocno.ZavrsiProgram("Datoteka s parametrima ne postoji!", false);
            }
            Program.Parametri = ParametriSingleton.DohvatiInstancu(Program.DatotekaParametara);
            Program.PutanjaDatoteka = Path.GetDirectoryName(Program.DatotekaParametara);
        }

        private static void UcitajZapiseIzDatoteka()
        {
            string datotekaUlice = Pomocno.DohvatiPutanjuDatoteke(Program.Parametri.DohvatiParametar("ulice"));
            Popis ulicaPopis = new UlicaPopis();
            List<Redak> ulicaPopisRetci = ulicaPopis.UcitajRetke(datotekaUlice);

            string datotekaSpremnika = Pomocno.DohvatiPutanjuDatoteke(Program.Parametri.DohvatiParametar("spremnici"));
            Popis spremnikPopis = new SpremnikPopis();
            List<Redak> spremnikPopisRetci = spremnikPopis.UcitajRetke(datotekaSpremnika);

            string datotekaVozila = Pomocno.DohvatiPutanjuDatoteke(Program.Parametri.DohvatiParametar("vozila"));
            Popis voziloPopis = new VoziloPopis();
            List<Redak> voziloPopisRetci = voziloPopis.UcitajRetke(datotekaVozila);           

            Program.PripremljeneUlice = PripremateljPrototype.PripremiUlice(ulicaPopisRetci.Cast<UlicaRedak>().ToList());
            Program.PripremljeniSpremnici = PripremateljPrototype.PripremiSpremnike(spremnikPopisRetci.Cast<SpremnikRedak>().ToList());
            Program.Vozila = PripremateljPrototype.PripremiVozila(voziloPopisRetci.Cast<VoziloRedak>().ToList());
        }

        private static void StvoriKonacnePodatkeSustava()
        {
            Program.Ulice = GeneratorEntiteta.StvoriKorisnike(Program.PripremljeneUlice);
            Program.Spremnici = GeneratorEntiteta.StvoriSpremnike(Program.PripremljeneUlice, Program.PripremljeniSpremnici);

            string datotekaPodrucja = Pomocno.DohvatiPutanjuDatoteke(Program.Parametri.DohvatiParametar("područja"));
            Popis podrucjePopis = new PodrucjePopis();
            List<PodrucjeRedak> podrucjaPopisRetci = podrucjePopis.UcitajRetke(datotekaPodrucja).Cast<PodrucjeRedak>().ToList();
            Program.Podrucja = PripremateljPodrucja.PripremiPodrucja(podrucjaPopisRetci);
        }

        private static void StvoriOtpad()
        {
            Program.Ulice = InicijalizatorOtpada.OdrediOtpadKorisnicima(Program.Ulice, Program.DatotekaParametara);
            Program.Spremnici = InicijalizatorOtpada.OdloziOtpadKorisnika(Program.Ulice, Program.Spremnici);
        }

        private static void IzracunajOtpadPoUlicama()
        {
            foreach (Ulica ulica in Program.Ulice)
            {
                List<Spremnik> spremniciUlice = Program.Spremnici.Where(s => s.UlicaID == ulica.ID).ToList();

                Dictionary<VrstaOtpada, float> otpadUlice = new Dictionary<VrstaOtpada, float>();
                foreach (Spremnik spremnik in spremniciUlice)
                {
                    otpadUlice[spremnik.NazivPremaOtpadu] = 0;
                }

                foreach (Spremnik spremnik in spremniciUlice)
                {
                    otpadUlice[spremnik.NazivPremaOtpadu] += spremnik.KolicinaOtpada;
                }

                ulica.Otpad = otpadUlice;
            }
        }

        private static void IspisiOtpadPodrucja()
        {
            IspisiTumacIspisaOtpadaPodrucja();

            foreach (Podrucje podrucje in Program.Podrucja)
            {                
                Dictionary<VrstaOtpada, float> otpad = PripremateljPodrucja.IzracunajUkupanOtpadPodrucjaSIspisom(podrucje.podrucja, true);

                Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkCyan);
                Program.Ispisivac.ObavljeniPosao($"[{podrucje.PodrucjeID}] {podrucje.Naziv}");
                foreach (PodrucjeComponent podrucjeComponent in podrucje.podrucja)
                {
                    Program.Ispisivac.ObavljeniPosao($"_[{podrucjeComponent.PodrucjeID}] {podrucjeComponent.Naziv}");
                }

                Program.Ispisivac.ObavljeniPosao("");
                Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkGreen);
                Program.Ispisivac.ObavljeniPosao($"Ispis ukupne kolicine otpada za podrucje {podrucje.Naziv}");                
                foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                {
                    Program.Ispisivac.ObavljeniPosao($"{vrsta}: {otpad[vrsta]}kg");
                }
                Program.Ispisivac.ObavljeniPosao(Tekstovi.HorizontalniRazmak);
                Program.Ispisivac.ObavljeniPosao("");
            }

            Program.Ispisivac.ResetirajPostavkeBoja();
        }

        private static void IspisiTumacIspisaOtpadaPodrucja()
        {
            Program.Ispisivac.ObavljeniPosao("");
            Program.Ispisivac.ObavljeniPosao("ISPIS OTPADA PO PODRUCJIMA I PODPODRUCJIMA");
            Program.Ispisivac.ObavljeniPosao(Tekstovi.HorizontalniRazmak);
            Program.Ispisivac.ObavljeniPosao("STRUKTURA ISPISA:");
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Cyan);
            Program.Ispisivac.ObavljeniPosao("- Ispis svih ulica u podrucju");
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkCyan);
            Program.Ispisivac.ObavljeniPosao("- Ispis podrucja i njegovih podpodrucja");
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkGreen);
            Program.Ispisivac.ObavljeniPosao("- Ispis ukupne kolicine otpada za podrucje");
            Program.Ispisivac.ObavljeniPosao(Tekstovi.HorizontalniRazmak);
        }

        private static void IspisiOtpadPodrucjaTablicno()
        {
            string zaglavlje = 
                String.Format("|{0,10}|{1,30}|{2,20}|{3,10}|{4,10}|{5,10}|{6,10}|{7,10}|", 
                "ID", "Naziv", "Podpodrucja", "Staklo", "Papir", "Metal", "Bio", "Mjesano");
            Program.Ispisivac.Koristi(zaglavlje);

            foreach (Podrucje podrucje in Program.Podrucja)
            {
                List<string> redak = new List<string>();
                Dictionary<VrstaOtpada, float> otpad = PripremateljPodrucja.IzracunajUkupanOtpadPodrucjaSIspisom(podrucje.podrucja, false);

                redak.Add(podrucje.PodrucjeID);
                redak.Add(podrucje.Naziv);

                string podpodrucja = "";
                foreach (PodrucjeComponent podrucjeComponent in podrucje.podrucja)
                {
                    podpodrucja += podrucjeComponent.PodrucjeID + " ";
                }
                redak.Add(podpodrucja);

                foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                {
                    redak.Add($"{otpad[vrsta]}kg");
                }
                string redakZaIspis = 
                    String.Format("|{0,10}|{1,30}|{2,20}|{3,10}|{4,10}|{5,10}|{6,10}|{7,10}|", 
                    redak[0], redak[1], redak[2], redak[3], redak[4], redak[5], redak[6], redak[7]);
                Program.Ispisivac.Koristi(redakZaIspis);
            }
        }        

        private static void StvoriRasporedOdvozaVozilima()
        {
            bool posebanRasporedZaSvakoVozilo = Program.Parametri.DohvatiParametarBool("preuzimanje");

            List<int> zajednickiRaspored = StvoriNasumicniRedoslijedUlica();
            foreach (Vozilo vozilo in Program.Vozila)
            {
                vozilo.RedoslijedUlica = posebanRasporedZaSvakoVozilo ? StvoriNasumicniRedoslijedUlica() : zajednickiRaspored;
            }              
        }

        private static List<int> StvoriNasumicniRedoslijedUlica()
        {
            int sjemeGeneratora = Program.Parametri.DohvatiParametarInt("sjemeGeneratora");
            GeneratorBrojevaSingleton generatorBrojeva = GeneratorBrojevaSingleton.DohvatiInstancu(sjemeGeneratora);

            int brojUlica = Program.Ulice.Count;
            List<int> redoslijedUlica = new List<int>();
            for (int i=0; i < brojUlica; i++)
            {
                int slucajniBroj = generatorBrojeva.DajSlucajniBrojInt(0, brojUlica);
                while (redoslijedUlica.Contains(slucajniBroj))
                {
                    slucajniBroj = generatorBrojeva.DajSlucajniBrojInt(0, brojUlica);
                }

                redoslijedUlica.Add(slucajniBroj);
            }

            return redoslijedUlica;
        }
    }
}
