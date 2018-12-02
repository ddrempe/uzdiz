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
        public static Dictionary<VrstaOtpada, float> DeponijUkupanOtpad { get; set; }

        public static Dictionary<string, int> VoziloBrojPreuzetihSpremnika { get; set; }

        public static Dictionary<string, int> VoziloBrojPreuzetihMjesta { get; set; }

        public static Dictionary<string, float> VoziloKolicinaPreuzetogOtpada { get; set; }

        public static Dictionary<string, int> VoziloBrojOdlazakaNaDeponij { get; set; }
    }
}
