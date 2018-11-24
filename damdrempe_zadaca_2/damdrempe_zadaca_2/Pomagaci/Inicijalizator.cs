
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
        public static List<Ulica> OdrediOtpadKorisnicima(List<Ulica> ulice, string datotekaParametara)
        {
            ParametriSingleton parametri = ParametriSingleton.DohvatiInstancu(datotekaParametara);
            int sjemeGeneratora = int.Parse(parametri.DohvatiParametar("sjemeGeneratora"));
            GeneratorBrojevaSingleton generatorBrojeva = GeneratorBrojevaSingleton.DohvatiInstancu(sjemeGeneratora);

            int brojDecimala = parametri.DohvatiParametarInt("brojDecimala");

            foreach (Ulica ulica in ulice)
            {
                Dictionary<VrstaOtpada, float> sumaOtpada = new Dictionary<VrstaOtpada, float>();

                foreach (Korisnik korisnik in ulica.KorisniciMali)
                {
                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        string korisnikKategorija = korisnik.Kategorija.ToString().ToLower();
                        int minParametar = parametri.DohvatiParametarInt(korisnikKategorija + "Min");
                        int gornjaVrijednost = parametri.DohvatiParametarInt(korisnikKategorija + vrsta.ToString());

                        float minVrijednost = (float)minParametar / 100 * gornjaVrijednost;
                        float maxVrijednost = gornjaVrijednost;
                        korisnik.Otpad[vrsta] = generatorBrojeva.DajSlucajniBrojFloat(minVrijednost, maxVrijednost, brojDecimala);

                        sumaOtpada[vrsta] = 0;
                        sumaOtpada[vrsta] += korisnik.Otpad[vrsta];
                    }
                }

                foreach (Korisnik korisnik in ulica.KorisniciSrednji)
                {
                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        string korisnikKategorija = korisnik.Kategorija.ToString().ToLower();
                        int minParametar = parametri.DohvatiParametarInt(korisnikKategorija + "Min");
                        int gornjaVrijednost = parametri.DohvatiParametarInt(korisnikKategorija + vrsta.ToString());

                        float minVrijednost = (float)minParametar / 100 * gornjaVrijednost;
                        float maxVrijednost = gornjaVrijednost;
                        korisnik.Otpad[vrsta] = generatorBrojeva.DajSlucajniBrojFloat(minVrijednost, maxVrijednost, brojDecimala);

                        sumaOtpada[vrsta] += korisnik.Otpad[vrsta];
                    }
                }

                foreach (Korisnik korisnik in ulica.KorisniciVeliki)
                {
                    foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
                    {
                        string korisnikKategorija = korisnik.Kategorija.ToString().ToLower();
                        int minParametar = parametri.DohvatiParametarInt(korisnikKategorija + "Min");
                        int gornjaVrijednost = parametri.DohvatiParametarInt(korisnikKategorija + vrsta.ToString());

                        float minVrijednost = (float)minParametar / 100 * gornjaVrijednost;
                        float maxVrijednost = gornjaVrijednost;
                        korisnik.Otpad[vrsta] = generatorBrojeva.DajSlucajniBrojFloat(minVrijednost, maxVrijednost, brojDecimala);

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

            return ulice;
        }
    }
}
