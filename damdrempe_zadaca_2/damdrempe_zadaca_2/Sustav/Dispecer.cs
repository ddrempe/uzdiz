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
        public static int TrenutniCiklus;

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
            int brojPotrebnihCiklusa = komanda.Broj;
            TrenutniCiklus = 1;

            List<Vozilo> vozilaKojaSkupljaju = Program.VozilaUObradi.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Pripremljeno)).ToList();
            while (TrenutniCiklus < brojPotrebnihCiklusa && vozilaKojaSkupljaju.Count > 0)
            {
                //TODO: provjeri da li jos ima vozila za obradu

                vozilaKojaSkupljaju = Program.VozilaUObradi.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Pripremljeno)).ToList();
                foreach (Vozilo vozilo in vozilaKojaSkupljaju)
                {
                    //provjeri ako je trenutni ciklus zadnji, tako dugo dok ne obradi n ciklusa
                    if (TrenutniCiklus >= brojPotrebnihCiklusa)
                    {
                        Program.Ispisivac.ObavljeniPosao($"Obavljeno je {TrenutniCiklus} ciklusa i izvrsavanje komande {komanda.Vrsta}.");
                        break;
                    }

                    PokupiOtpad(vozilo);
                }
                TrenutniCiklus++;
            }
            Program.Ispisivac.ObavljeniPosao($"Obavljeno je {TrenutniCiklus} ciklusa i izvrsavanje komande {komanda.Vrsta}.");
            Program.Ispisivac.ObavljeniPosao();
        }

        private static void PokupiOtpad(Vozilo vozilo)
        {
            //preskoči prazne spremnike
            while(vozilo.IteratorS.Trenutni.KolicinaOtpada == 0 && !vozilo.IteratorS.Kraj)
            {
                if(!vozilo.IteratorS.Kraj)
                {
                    vozilo.IteratorS.Sljedeci();
                }
            }
            Spremnik spremnik = vozilo.IteratorS.Trenutni;

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

            if (!vozilo.IteratorS.Kraj)
            {
                vozilo.IteratorS.Sljedeci();
            }
            else
            {
                Program.Ispisivac.ObavljeniPosao($"CIKLUS {TrenutniCiklus} Nema vise otpada za vozilo {vozilo.ID}");
                vozilo.PromijeniStanje(VrstaStanja.Parkirano);
            }
            TrenutniCiklus++;
            
            //TODO: provjeri ako je iduci spremnik u iducoj ulici i iteriraj ulicu
        }
    }
}
