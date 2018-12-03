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
                    vozilo.PromijeniStanje(VrstaStanja.Skupljanje);
                    //TODO: provjeri da li vozilo već postoji
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

            List<Vozilo> vozilaKojaSkupljaju = Program.VozilaUObradi.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Skupljanje)).ToList();
            List<Vozilo> vozilaZaPraznjenje = Program.VozilaUObradi.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Praznjenje)).ToList();
            while (TrenutniCiklus < brojPotrebnihCiklusa && (vozilaKojaSkupljaju.Count > 0 || vozilaZaPraznjenje.Count > 0))
            {
                //TODO: provjeri da li jos ima vozila za obradu                
                foreach (Vozilo vozilo in vozilaKojaSkupljaju)
                {
                    //provjeri ako je trenutni ciklus zadnji, tako dugo dok ne obradi n ciklusa
                    if (TrenutniCiklus >= brojPotrebnihCiklusa)
                    {
                        Program.Ispisivac.ObavljeniPosao($"C{TrenutniCiklus} Obavljeno je izvrsavanje komande {komanda.Vrsta}.");   //TODO provjeri da li potrebno
                        break;
                    }
                    PokupiOtpad(vozilo);
                    TrenutniCiklus++;

                    if (vozilaZaPraznjenje.Count > 0) ObaviPraznjenje();
                }
                vozilaKojaSkupljaju = Program.VozilaUObradi.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Skupljanje)).ToList();
                vozilaZaPraznjenje = Program.VozilaUObradi.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Praznjenje)).ToList();

                if (vozilaZaPraznjenje.Count > 0) ObaviPraznjenje();
                //Console.WriteLine($"{TrenutniCiklus} beskonacna");
            }
            
            Program.Ispisivac.ObavljeniPosao($"C{TrenutniCiklus} nema vise otpada za vozila u obradi. Zavrseno izvrsavanje komande {komanda.Vrsta}.");
            Program.Ispisivac.ObavljeniPosao();
        }

        private static void ObaviPraznjenje()
        {
            List<Vozilo> vozilaZaPraznjenje = Program.VozilaUObradi.Where(v => v.TrenutnoStanje.Equals(VrstaStanja.Praznjenje)).ToList();
            foreach (Vozilo vozilo in vozilaZaPraznjenje)
            {
                if(vozilo.BrojPreostalihCiklusa <= 0)
                {
                    vozilo.PromijeniStanje(VrstaStanja.Skupljanje);
                    //TODO: stavi ga na kraj liste
                }

                vozilo.KolicinaOtpada = 0; 
                vozilo.BrojPreostalihCiklusa--;
            }
        }

        private static void PokupiOtpad(Vozilo vozilo)
        {
            //preskoči prazne spremnike
            while(vozilo.IteratorS.Trenutni.KolicinaOtpada == 0 && !vozilo.IteratorS.Kraj)
            {
                vozilo.IteratorS.Sljedeci();                
            }

            if (vozilo.IteratorS.Kraj)
            {
                Program.Ispisivac.ObavljeniPosao($"C{TrenutniCiklus} Nema vise otpada za vozilo {vozilo.ID}");
                vozilo.PromijeniStanje(VrstaStanja.Parkirano);  //vozilo za koje vise nema otpada se vraća na prakiralište
                return;
            }

            Spremnik spremnik = vozilo.IteratorS.Trenutni;

            float preostaliKapacitetVozila = vozilo.Nosivost - vozilo.KolicinaOtpada;
            float kolicinaUzetogOtpadaSpremnika = spremnik.KolicinaOtpada;
            if (kolicinaUzetogOtpadaSpremnika > preostaliKapacitetVozila)
            {
                float kolicinaOtpadaViska = kolicinaUzetogOtpadaSpremnika - preostaliKapacitetVozila;
                kolicinaUzetogOtpadaSpremnika = kolicinaUzetogOtpadaSpremnika - kolicinaOtpadaViska;
                Program.Ispisivac.ObavljeniPosao($"Vozilo {vozilo.ID} ({vozilo.VrstaOtpada}) je puno ({vozilo.Nosivost}kg) i mora na odvoz.");
                Program.Ispisivac.ObavljeniPosao($"Spremnik {spremnik.ID} ima jos {kolicinaOtpadaViska}kg otpada vrste {spremnik.NazivPremaOtpadu}.");

                //ako je vozilo puno promijeni stanje, posalji ga na praznjenje n ciklusa nakon kojih se vraca na kraj liste
                vozilo.PromijeniStanje(VrstaStanja.Praznjenje);
                vozilo.BrojPreostalihCiklusa = Program.Parametri.DohvatiParametarInt("brojRadnihCiklusaZaOdvoz");
            }

            vozilo.KolicinaOtpada += kolicinaUzetogOtpadaSpremnika;
            spremnik.KolicinaOtpada -= kolicinaUzetogOtpadaSpremnika;

            Program.Ispisivac.ObavljeniPosao($"C{TrenutniCiklus} Vozilo {vozilo.ID} ({vozilo.VrstaOtpada}) trenutno ima {vozilo.KolicinaOtpada}kg otpada, preostali kapacitet je {vozilo.Nosivost - vozilo.KolicinaOtpada}kg.");
            vozilo.IteratorS.Sljedeci(); //TODO: provjeri ako je iduci spremnik u iducoj ulici i iteriraj ulicu                 
        }
    }
}
