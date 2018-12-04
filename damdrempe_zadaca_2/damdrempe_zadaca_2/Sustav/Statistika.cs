using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Sustav
{
    class Statistika
    {
        public static Dictionary<string, int> VoziloBrojOdlazakaNaDeponij { get; set; }

        public static Dictionary<string, int> VoziloBrojPreuzetihMjesta { get; set; }

        public static Dictionary<string, int> VoziloBrojPreuzetihSpremnika { get; set; }              

        public static Dictionary<string, float> VoziloKolicinaPreuzetogOtpada { get; set; }

        public static Dictionary<VrstaOtpada, float> DeponijUkupanOtpad { get; set; }

        public static void InicijalizirajStatistiku()
        {
            VoziloBrojOdlazakaNaDeponij = new Dictionary<string, int>();
            VoziloBrojPreuzetihMjesta = new Dictionary<string, int>();
            VoziloBrojPreuzetihSpremnika = new Dictionary<string, int>();
            VoziloKolicinaPreuzetogOtpada = new Dictionary<string, float>();
            DeponijUkupanOtpad = new Dictionary<VrstaOtpada, float>();

            foreach (Vozilo vozilo in Program.Vozila)
            {
                VoziloBrojOdlazakaNaDeponij[vozilo.ID] = 0;
                VoziloBrojPreuzetihMjesta[vozilo.ID] = 0;
                VoziloBrojPreuzetihSpremnika[vozilo.ID] = 0;
                VoziloKolicinaPreuzetogOtpada[vozilo.ID] = 0f;
            }

            foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
            {
                DeponijUkupanOtpad[vrsta] = 0f;
            }
        }    

        public static void IspisiStatistikuVozilaTablicno()
        {
            Program.Ispisivac.Koristi("STATISTIKA - VOZILA");
            string redakZaIspis = String.Format("|{0,5}|{1,25}|{2,25}|{3,25}|",
                    "ID", "Odlasci na deponij", "Preuzeti spremnici", "Preuzeti otpad");
            Program.Ispisivac.Koristi(redakZaIspis);

            foreach (Vozilo vozilo in Program.Vozila)
            {
                redakZaIspis =
                    String.Format("|{0,5}|{1,25}|{2,25}|{3,25}|", vozilo.ID, 
                    VoziloBrojOdlazakaNaDeponij[vozilo.ID], 
                    VoziloBrojPreuzetihSpremnika[vozilo.ID],
                    VoziloKolicinaPreuzetogOtpada[vozilo.ID] + "kg");
                Program.Ispisivac.Koristi(redakZaIspis);
            }
            Program.Ispisivac.Koristi();
        }

        public static void IspisiStatistikuDeponija()
        {
            Program.Ispisivac.Koristi("STATISTIKA - KOLICINA OTPADA NA DEPONIJU");            
            foreach (VrstaOtpada vrsta in Enum.GetValues(typeof(VrstaOtpada)))
            {
                string redakZaIspis = String.Format("|{0,10}|{1,10}|",
                    vrsta.ToString().ToUpper(), DeponijUkupanOtpad[vrsta] + "kg");
                Program.Ispisivac.Koristi(redakZaIspis);
            }

            Program.Ispisivac.Koristi();
        }
    }    
}
