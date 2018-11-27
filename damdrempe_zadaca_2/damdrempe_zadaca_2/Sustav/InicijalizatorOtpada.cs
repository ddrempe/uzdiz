
using damdrempe_zadaca_2.Podaci.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Sustav
{
    class InicijalizatorOtpada
    {
        /// <summary>
        /// Za sve ulice i svakog pojedinog korisnika nasumično generira količinu otpada po pojedinoj vrsti otpada.
        /// </summary>
        /// <param name="ulice"></param>
        /// <param name="datotekaParametara"></param>
        /// <returns></returns>
        public static List<Ulica> OdrediOtpadKorisnicima(List<Ulica> ulice, string datotekaParametara)
        {
            ParametriSingleton parametri = ParametriSingleton.DohvatiInstancu(datotekaParametara);
            int sjemeGeneratora = int.Parse(parametri.DohvatiParametar("sjemeGeneratora"));
            GeneratorBrojevaSingleton generatorBrojeva = GeneratorBrojevaSingleton.DohvatiInstancu(sjemeGeneratora);            

            foreach (Ulica ulica in ulice)
            {                
                foreach (Korisnik korisnik in ulica.KorisniciMali)
                {
                    korisnik.Otpad = OdrediOtpadKorisnikuPoVrsti(parametri, generatorBrojeva, Kategorija.Mali);
                }

                foreach (Korisnik korisnik in ulica.KorisniciSrednji)
                {
                    korisnik.Otpad = OdrediOtpadKorisnikuPoVrsti(parametri, generatorBrojeva, Kategorija.Srednji);
                }

                foreach (Korisnik korisnik in ulica.KorisniciVeliki)
                {
                    korisnik.Otpad = OdrediOtpadKorisnikuPoVrsti(parametri, generatorBrojeva, Kategorija.Veliki);
                }
            }

            return ulice;
        }

        /// <summary>
        /// Nasumično generira količine otpada po vrsti otpada i prema kategoriji korisnika.
        /// </summary>
        /// <param name="parametri"></param>
        /// <param name="generatorBrojeva"></param>
        /// <param name="kategorija"></param>
        /// <returns></returns>
        private static Dictionary<VrstaOtpada, float> OdrediOtpadKorisnikuPoVrsti(ParametriSingleton parametri, 
                                                                                GeneratorBrojevaSingleton generatorBrojeva, 
                                                                                Kategorija kategorija)
        {
            string korisnikKategorija = kategorija.ToString().ToLower();
            int minParametar = parametri.DohvatiParametarInt(korisnikKategorija + "Min");
            int brojDecimala = parametri.DohvatiParametarInt("brojDecimala");            

            Dictionary<VrstaOtpada, float> otpadiKorisnika = new Dictionary<VrstaOtpada, float>();
            foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
            {
                int gornjaVrijednost = parametri.DohvatiParametarInt(korisnikKategorija + vrsta.ToString());
                float minVrijednost = (float)minParametar / 100 * gornjaVrijednost;
                float maxVrijednost = gornjaVrijednost;

                otpadiKorisnika[vrsta] = generatorBrojeva.DajSlucajniBrojFloat(minVrijednost, maxVrijednost, brojDecimala);
            }

            return otpadiKorisnika;
        }        

        /// <summary>
        /// Ispisuje otpad po ulicama.
        /// </summary>
        public static void IspisiOtpadPoUlicama(List<Ulica> ulice)
        {
            foreach (Ulica ulica in ulice)
            {
                Dictionary<VrstaOtpada, float> sumaOtpada = new Dictionary<VrstaOtpada, float>();

                foreach (Korisnik korisnik in ulica.KorisniciMali)
                {
                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        sumaOtpada[vrsta] = 0;
                        sumaOtpada[vrsta] += korisnik.Otpad[vrsta];
                    }
                }

                foreach (Korisnik korisnik in ulica.KorisniciSrednji)
                {
                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        sumaOtpada[vrsta] += korisnik.Otpad[vrsta];
                    }
                }

                foreach (Korisnik korisnik in ulica.KorisniciVeliki)
                {
                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        sumaOtpada[vrsta] += korisnik.Otpad[vrsta];
                    }
                }

                Program.Ispisivac.Koristi();
                Program.Ispisivac.Koristi(ulica.Naziv);
                foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                {
                    Program.Ispisivac.Koristi($"{vrsta}: {sumaOtpada[vrsta]}kg");
                }
            }            
        }

        /// <summary>
        /// Za svaki spremnik pronalazi korisnike kojima pripada.
        /// Otpad svakog korisnika se pohranjuje u spremnik a visak odbacuje.
        /// </summary>
        /// <param name="ulice"></param>
        /// <param name="spremnici"></param>
        /// <returns></returns>
        public static List<Spremnik> OdloziOtpadKorisnika(List<Ulica> ulice, List<Spremnik> spremnici)
        {
            foreach(Spremnik spremnik in spremnici)
            {
                foreach (int korisnikID in spremnik.Korisnici)
                {
                    Korisnik korisnik = NadjiKorisnika(korisnikID, ulice);
                    float preostaliKapacitetSpremnika = spremnik.Nosivost - spremnik.KolicinaOtpada;
                    float kolicinaOtpadaKorisnika = korisnik.Otpad[spremnik.NazivPremaOtpadu];

                    if (kolicinaOtpadaKorisnika > preostaliKapacitetSpremnika)
                    {
                        float kolicinaOtpadaViska = kolicinaOtpadaKorisnika - preostaliKapacitetSpremnika;
                        kolicinaOtpadaKorisnika = kolicinaOtpadaKorisnika - kolicinaOtpadaViska;
                        Program.Ispisivac.Koristi($"Korisnik {korisnikID} ima {kolicinaOtpadaViska}kg otpada vrste {spremnik.NazivPremaOtpadu} viška.");
                        Program.Ispisivac.Koristi($"Spremnik {spremnik.ID} ({spremnik.NazivPremaOtpadu}) je pun ({spremnik.Nosivost}kg)");
                    }
                    
                    spremnik.KolicinaOtpada += kolicinaOtpadaKorisnika;
                }
            }

            return spremnici;
        }

        /// <summary>
        /// Pronalazi i vraca objekt korisnika prema identifikatoru.
        /// </summary>
        /// <param name="korisnikID"></param>
        /// <param name="ulice"></param>
        /// <returns></returns>
        private static Korisnik NadjiKorisnika(int korisnikID, List<Ulica> ulice)
        {
            Korisnik korisnik = new Korisnik();
            foreach (Ulica ulica in ulice)
            {
                if(ulica.KorisniciMali.Any(k => k.ID == korisnikID))
                {
                    korisnik = ulica.KorisniciMali.FirstOrDefault(k => k.ID == korisnikID);
                }

                if (ulica.KorisniciSrednji.Any(k => k.ID == korisnikID))
                {
                    korisnik = ulica.KorisniciSrednji.FirstOrDefault(k => k.ID == korisnikID);
                }

                if (ulica.KorisniciVeliki.Any(k => k.ID == korisnikID))
                {
                    korisnik = ulica.KorisniciVeliki.FirstOrDefault(k => k.ID == korisnikID);
                }
            }

            return korisnik;
        }
    }
}
