using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci.Modeli;
using damdrempe_zadaca_2.Sustav.damdrempe_zadaca_2.Sustav;
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
                if(vozilo != null)
                {
                    vozilo.PromijeniStanje(VrstaStanja.Pripremljeno);

                    Program.VozilaUObradi.Add(vozilo);
                }
                else
                {
                    Program.Ispisivac.ObavljeniPosao($"Ne postoji vozilo {voziloID} za komandu {komanda.Vrsta}");
                }
            }
        }

        public static void ObradiKomanduKreni(KomandaRedak komanda)
        {
            /*TODO: od vozila u obradi uzmi samo one koji su u statusu SKUPLJA
            Obrada vozila temelji se na listi koja sadrži sva vozila koja su u procesu preuzimanja otpada i
            trenutno ne voze otpad na mjesto za zbrinjavanje i nisu u kvaru i ne čakaju odlazak na pražnjenje otpada*/

            foreach (Vozilo vozilo in Program.VozilaUObradi)
            {
                
            }
        }

        public static void ObradiKomanduKreniN(KomandaRedak komanda)
        {
            int trenutniCiklus = 1;
            int brojPotrebnihCiklusa = komanda.Broj;

            foreach (Vozilo vozilo in Program.VozilaUObradi)
            {
                //TODO: provjeri ako je trenutni ciklus zadnji, tako dugo dok ne obradi n ciklusa

                PokupiOtpad(vozilo, ref trenutniCiklus);
            }
            Program.Ispisivac.ObavljeniPosao();
        }

        private static void PokupiOtpad(Vozilo vozilo, ref int trenutniCiklus)
        {
            //TODO: provjeri ako ima što u spremniku, ako nema idi na iduci

            Spremnik spremnik = vozilo.IteratorS.Trenutni;
            //vozilo.KolicinaOtpada += spremnik.KolicinaOtpada;

            float preostaliKapacitetVozila = vozilo.Nosivost - vozilo.KolicinaOtpada;
            float kolicinaUzetogOtpadaSpremnika = spremnik.KolicinaOtpada;

            if (kolicinaUzetogOtpadaSpremnika > preostaliKapacitetVozila)
            {
                float kolicinaOtpadaViska = kolicinaUzetogOtpadaSpremnika - preostaliKapacitetVozila;
                kolicinaUzetogOtpadaSpremnika = kolicinaUzetogOtpadaSpremnika - kolicinaOtpadaViska;
                Program.Ispisivac.ObavljeniPosao($"Vozilo {vozilo.ID} ({vozilo.VrstaOtpada}) je puno ({vozilo.Nosivost}kg)");
                Program.Ispisivac.ObavljeniPosao($"Spremnik {spremnik.ID} ima jos {kolicinaOtpadaViska}kg otpada vrste {spremnik.NazivPremaOtpadu}.");

                //TODO: ako je vozilo puno izaci ga iz liste i promijeni stanje, posalji ga na praznjenje n ciklusa nakon kojih se vraca na kraj liste
            }

            vozilo.KolicinaOtpada += kolicinaUzetogOtpadaSpremnika;
            spremnik.KolicinaOtpada -= kolicinaUzetogOtpadaSpremnika;

            Program.Ispisivac.ObavljeniPosao($"Vozilo {vozilo.ID} ({vozilo.VrstaOtpada}) trenutno ima {vozilo.KolicinaOtpada}kg otpada, preostali kapacitet je {vozilo.Nosivost - vozilo.KolicinaOtpada}kg.");
            vozilo.IteratorS.Sljedeci();
            trenutniCiklus++;
            
            //TODO: provjeri ako je iduci spremnik u iducoj ulici i iteriraj ulicu
        }
    }
}
