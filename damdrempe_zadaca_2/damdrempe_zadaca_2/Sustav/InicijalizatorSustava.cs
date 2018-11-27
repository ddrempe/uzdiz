using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Pomagaci.Entiteti;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            InicijalizatorOtpada.IspisiOtpadKorisnikaPoUlicama(Program.Ulice);
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

            string datotekaPodrucja = Pomocno.DohvatiPutanjuDatoteke(Program.Parametri.DohvatiParametar("područja"));
            Popis podrucjePopis = new PodrucjePopis();
            List<PodrucjeRedak> podrucjaPopisRetci = podrucjePopis.UcitajRetke(datotekaPodrucja).Cast<PodrucjeRedak>().ToList();

            Program.PripremljeneUlice = PripremateljPrototype.PripremiUlice(ulicaPopisRetci.Cast<UlicaRedak>().ToList());
            Program.PripremljeniSpremnici = PripremateljPrototype.PripremiSpremnike(spremnikPopisRetci.Cast<SpremnikRedak>().ToList());
            Program.Podrucja = PripremateljPodrucja.PripremiPodrucja(podrucjaPopisRetci);
        }

        private static void StvoriKonacnePodatkeSustava()
        {
            Program.Ulice = GeneratorEntiteta.StvoriKorisnike(Program.PripremljeneUlice);
            Program.Spremnici = GeneratorEntiteta.StvoriSpremnike(Program.PripremljeneUlice, Program.PripremljeniSpremnici);
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
    }
}
