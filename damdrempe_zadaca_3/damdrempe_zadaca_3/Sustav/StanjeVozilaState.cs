using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using damdrempe_zadaca_3.Citaci;
using damdrempe_zadaca_3.Podaci;
using damdrempe_zadaca_3.Sustav.damdrempe_zadaca_2.Sustav;
using static damdrempe_zadaca_3.Podaci.Enumeracije;

namespace damdrempe_zadaca_3.Sustav
{
    /// <summary>
    /// The 'State' abstract class
    /// </summary>
    abstract class StanjeVozila
    {
        protected Vozilo vozilo;

        protected VrstaStanja trenutnoStanje;

        public Vozilo Vozilo
        {
            get { return vozilo; }
            set { vozilo = value; }
        }

        public VrstaStanja TrenutnoStanje
        {
            get { return trenutnoStanje; }
            set { trenutnoStanje = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="novoStanje"></param>
        public abstract void PromijeniStanje(VrstaStanja novoStanje);
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// </summary>
    class Parkirano : StanjeVozila
    {
        public Parkirano(StanjeVozila stanjeVozila) : this(stanjeVozila.TrenutnoStanje, stanjeVozila.Vozilo)
        {

        }

        public Parkirano(VrstaStanja stanje, Vozilo vozilo)
        {
            this.trenutnoStanje = stanje;
            this.vozilo = vozilo;
        }

        public override void PromijeniStanje(VrstaStanja novoStanje)
        {
            switch (novoStanje)
            {
                case VrstaStanja.Parkirano:              
                    break;
                case VrstaStanja.Skupljanje:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Skupljanje(this);
                    break;
                case VrstaStanja.Pokvareno:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Pokvareno(this);
                    break;
                case VrstaStanja.Kontrola:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Kontrola(this);
                    break;
                case VrstaStanja.Praznjenje:
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// </summary>
    class Skupljanje : StanjeVozila
    {     
        public Skupljanje(StanjeVozila stanjeVozila)
        {
            trenutnoStanje = stanjeVozila.TrenutnoStanje;
            vozilo = stanjeVozila.Vozilo;
        }

        public override void PromijeniStanje(VrstaStanja novoStanje)
        {
            switch (novoStanje)
            {
                case VrstaStanja.Parkirano:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Parkirano(this);
                    break;
                case VrstaStanja.Skupljanje:
                    break;
                case VrstaStanja.Pokvareno:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Pokvareno(this);
                    break;
                case VrstaStanja.Kontrola:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Kontrola(this);
                    break;
                case VrstaStanja.Praznjenje:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Parkirano(this);  //TODO: u stanje praznjenje 
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// </summary>
    class Praznjenje : StanjeVozila
    {
        public Praznjenje(StanjeVozila stanjeVozila)
        {
            trenutnoStanje = stanjeVozila.TrenutnoStanje;
            vozilo = stanjeVozila.Vozilo;
        }

        public override void PromijeniStanje(VrstaStanja novoStanje)
        {
            switch (novoStanje)
            {
                case VrstaStanja.Parkirano:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Parkirano(this);
                    break;
                case VrstaStanja.Skupljanje:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Skupljanje(this);
                    break;
                case VrstaStanja.Pokvareno:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Pokvareno(this);
                    break;
                case VrstaStanja.Kontrola:
                    break;
                case VrstaStanja.Praznjenje:
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// </summary>
    class Kontrola : StanjeVozila
    {
        public Kontrola(StanjeVozila stanjeVozila)
        {
            trenutnoStanje = stanjeVozila.TrenutnoStanje;
            vozilo = stanjeVozila.Vozilo;
        }

        public override void PromijeniStanje(VrstaStanja novoStanje)
        {
            switch (novoStanje)
            {
                case VrstaStanja.Parkirano:
                    break;
                case VrstaStanja.Skupljanje:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Skupljanje(this);
                    break;
                case VrstaStanja.Pokvareno:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Pokvareno(this);
                    break;
                case VrstaStanja.Kontrola:
                    break;
                case VrstaStanja.Praznjenje:
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// A 'ConcreteState' class
    /// </summary>
    class Pokvareno : StanjeVozila
    {
        public Pokvareno(StanjeVozila stanjeVozila)
        {
            trenutnoStanje = stanjeVozila.TrenutnoStanje;
            vozilo = stanjeVozila.Vozilo;
        }

        public override void PromijeniStanje(VrstaStanja novoStanje)
        {
            switch (novoStanje)
            {
                case VrstaStanja.Praznjenje:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Praznjenje(this);
                    break;
                default:
                    Program.Ispisivac.ObavljeniPosao($"Vozilo {vozilo.ID} je u kvaru! Ne moze u stanje {novoStanje}, vec samo u stanje {VrstaStanja.Praznjenje}.");
                    break;
            }
        }
    }

    /// <summary>
    /// The Context class
    /// </summary>
    class Vozilo : PrijevoznoSredstvo
    {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public TipVozila Tip { get; set; }

        public VrstaOtpada VrstaOtpada { get; set; }

        public int Nosivost { get; set; }

        public float KolicinaOtpada { get; set; }

        public List<string> Vozaci { get; set; }

        public List<int> RedoslijedUlica { get; set; } 
        
        public IteratorS IteratorS { get; set; }

        public int BrojPreostalihCiklusa { get; set; }

        private StanjeVozila _stanjeVozila;

        public Vozilo()
        {
            this.BrojMjesta = 3;
            _stanjeVozila = new Parkirano(VrstaStanja.Parkirano, this);
            KolicinaOtpada = 0;
        }

        public Vozilo(VoziloRedak voziloRedak)
        {
            this.BrojMjesta = 3;
            _stanjeVozila = new Parkirano(VrstaStanja.Parkirano, this);

            ID = voziloRedak.ID;
            Naziv = voziloRedak.Naziv;
            Tip = voziloRedak.Tip;
            VrstaOtpada = voziloRedak.VrstaOtpada;
            Nosivost = voziloRedak.Nosivost;
            Vozaci = voziloRedak.Vozaci;
            KolicinaOtpada = 0;
        }

        public VrstaStanja TrenutnoStanje
        {
            get { return _stanjeVozila.TrenutnoStanje; }
        }

        public StanjeVozila StanjeVozila
        {
            get { return _stanjeVozila; }
            set { _stanjeVozila = value; }
        }

        public void PromijeniStanje(VrstaStanja novoStanje)
        {
            _stanjeVozila.PromijeniStanje(novoStanje);
        }

        public override void IspisiPutnike()    //TODO: implementirati
        {            
        }
    }
}
