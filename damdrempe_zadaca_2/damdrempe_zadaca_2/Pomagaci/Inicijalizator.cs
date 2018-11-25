
using damdrempe_zadaca_2.Podaci.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Pomagaci
{
    class Inicijalizator
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
        /// 
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
                        sumaOtpada[vrsta] = 0;
                        sumaOtpada[vrsta] += korisnik.Otpad[vrsta];
                    }
                }

                foreach (Korisnik korisnik in ulica.KorisniciVeliki)
                {
                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        sumaOtpada[vrsta] = 0;
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
    }
}
