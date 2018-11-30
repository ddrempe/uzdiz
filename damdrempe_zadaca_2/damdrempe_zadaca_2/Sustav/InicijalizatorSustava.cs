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
                Dictionary<VrstaOtpada, float> otpad = PripremateljPodrucja.IzracunajUkupanOtpadPodrucja(podrucje.podrucja);

                Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkCyan);
                Program.Ispisivac.Koristi($"[{podrucje.PodrucjeID}] {podrucje.Naziv}");
                foreach (PodrucjeComponent podrucjeComponent in podrucje.podrucja)
                {
                    Program.Ispisivac.Koristi($"_[{podrucjeComponent.PodrucjeID}] {podrucjeComponent.Naziv}");
                }

                Program.Ispisivac.Koristi("");
                Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkGreen);
                Program.Ispisivac.Koristi($"Ispis ukupne kolicine otpada za podrucje {podrucje.Naziv}");                
                foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                {
                    Program.Ispisivac.Koristi($"{vrsta}: {otpad[vrsta]}kg");
                }
                Program.Ispisivac.Koristi(Tekstovi.HorizontalniRazmak);
                Program.Ispisivac.Koristi("");
            }

            Program.Ispisivac.ResetirajPostavkeBoja();
        }

        private static void IspisiTumacIspisaOtpadaPodrucja()
        {
            Program.Ispisivac.Koristi("");
            Program.Ispisivac.Koristi("ISPIS OTPADA PO PODRUCJIMA I PODPODRUCJIMA");
            Program.Ispisivac.Koristi(Tekstovi.HorizontalniRazmak);
            Program.Ispisivac.Koristi("STRUKTURA ISPISA:");
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.Cyan);
            Program.Ispisivac.Koristi("- Ispis svih ulica u podrucju");
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkCyan);
            Program.Ispisivac.Koristi("- Ispis podrucja i njegovih podpodrucja");
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkGreen);
            Program.Ispisivac.Koristi("- Ispis ukupne kolicine otpada za podrucje");
            Program.Ispisivac.Koristi(Tekstovi.HorizontalniRazmak);
        }
    }
}
