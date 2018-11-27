using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Pomagaci.Entiteti;
using damdrempe_zadaca_2.Sustav;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static damdrempe_zadaca_2.Pomagaci.Entiteti.PodrucjaComposite;

namespace damdrempe_zadaca_2
{
    class Program
    {
        public static string DatotekaParametara;
        public static string PutanjaDatoteka;
        public static IspisivacSingleton Ispisivac = IspisivacSingleton.DohvatiInstancu();

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Pomocno.ZavrsiProgram("Broj argumenata mora biti jednak 1.", false);
            }

            DatotekaParametara = args[0];
            if(!File.Exists(DatotekaParametara))
            {
                Pomocno.ZavrsiProgram("Datoteka s parametrima ne postoji!", false);
            }
            ParametriSingleton parametri = ParametriSingleton.DohvatiInstancu(DatotekaParametara);
            PutanjaDatoteka = Path.GetDirectoryName(DatotekaParametara);

            string datotekaUlice = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("ulice"));
            Popis ulicaPopis = new UlicaPopis();
            List<Redak> ulicaPopisRetci = ulicaPopis.UcitajRetke(datotekaUlice);

            string datotekaSpremnika = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("spremnici"));
            Popis spremnikPopis = new SpremnikPopis();
            List<Redak> spremnikPopisRetci = spremnikPopis.UcitajRetke(datotekaSpremnika);

            string datotekaVozila = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("vozila"));
            Popis voziloPopis = new VoziloPopis();
            List<Redak> voziloPopisRetci = voziloPopis.UcitajRetke(datotekaVozila);

            List<Ulica> pripremljeneUlice = PripremateljPrototype.PripremiUlice(ulicaPopisRetci.Cast<UlicaRedak>().ToList());
            List<Spremnik> pripremljeniSpremnici = PripremateljPrototype.PripremiSpremnike(spremnikPopisRetci.Cast<SpremnikRedak>().ToList());

            List<Ulica> ulice = GeneratorEntiteta.StvoriKorisnike(pripremljeneUlice);
            List<Spremnik> spremnici = GeneratorEntiteta.StvoriSpremnike(pripremljeneUlice, pripremljeniSpremnici);

            ulice = InicijalizatorOtpada.OdrediOtpadKorisnicima(ulice, DatotekaParametara);
            spremnici = InicijalizatorOtpada.OdloziOtpadKorisnika(ulice, spremnici);
                        
            InicijalizatorOtpada.IspisiOtpadPoUlicama(ulice);

            //testPodrucja();

            string datotekaPodrucja = Pomocno.DohvatiPutanjuDatoteke(parametri.DohvatiParametar("područja"));
            Popis podrucjePopis = new PodrucjePopis();
            List<PodrucjeRedak> podrucjaPopisRetci = podrucjePopis.UcitajRetke(datotekaPodrucja).Cast<PodrucjeRedak>().ToList();

            List<Podrucje> podrucja = PripremateljPodrucja.PripremiPodrucja(podrucjaPopisRetci);


            Pomocno.ZavrsiProgram("Program izvrsen do kraja.", true);
        }
    }
}
