using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Sustav
{
    class Dispecer
    {
        public static void ObradiKomanduPripremi(KomandaRedak komanda)
        {
            foreach (string voziloID in komanda.Vozila)
            {
                Vozilo vozilo = Program.Vozila.FirstOrDefault(v => v.ID == voziloID);
                vozilo.PromijeniStanje(VrstaStanja.Pripremljeno);
            }

            foreach (Vozilo trenutnoVozilo in Program.Vozila)
            {
                Console.WriteLine(trenutnoVozilo.TrenutnoStanje);
            }
            Console.WriteLine();

            List<Vozilo> pripremljenaVozila = Program.Vozila.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Parkirano)).ToList();
        }
    }
}
