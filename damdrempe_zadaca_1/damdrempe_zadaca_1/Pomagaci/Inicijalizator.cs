
using damdrempe_zadaca_1.Podaci.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_1.Podaci.Enumeracije;

namespace damdrempe_zadaca_1.Pomagaci
{
    class Inicijalizator
    {
        public static List<Ulica> OdrediOtpadKorisnicima(List<Ulica> ulice, ParametriSingleton parametri, GeneratorBrojevaSingleton generatorBrojeva)
        {
            int brojDecimala = parametri.DohvatiParametarInt("brojDecimala");

            foreach (Ulica ulica in ulice)
            {
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
                    }
                }
            }

            return ulice;
        }
    }
}
