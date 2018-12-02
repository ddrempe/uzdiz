using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using damdrempe_zadaca_2.Citaci;
using damdrempe_zadaca_2.Podaci;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Sustav
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
                    //TODO: vec je parkirano                   
                    break;
                case VrstaStanja.Pripremljeno:
                    this.trenutnoStanje = novoStanje;
                    vozilo.StanjeVozila = new Pripremljeno(this);
                    break;
                case VrstaStanja.Skuplja:
                    break;
                case VrstaStanja.Pokvareno:
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
    class Pripremljeno : StanjeVozila
    {
        public Pripremljeno(StanjeVozila stanjeVozila)
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
                case VrstaStanja.Pripremljeno:
                    //TODO: vec je pripremljeno
                    break;
                case VrstaStanja.Skuplja:
                    break;
                case VrstaStanja.Pokvareno:
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
    /// The Context class
    /// </summary>
    class Vozilo
    {
        public string ID { get; set; }

        public string Naziv { get; set; }

        public TipVozila Tip { get; set; }

        public VrstaOtpada VrstaOtpada { get; set; }

        public int Nosivost { get; set; }

        public List<string> Vozaci { get; set; }

        public List<int> RedoslijedUlica { get; set; }        

        private StanjeVozila _stanjeVozila;

        public Vozilo()
        {
            _stanjeVozila = new Parkirano(VrstaStanja.Parkirano, this);
        }

        public Vozilo(VoziloRedak voziloRedak)
        {
            _stanjeVozila = new Parkirano(VrstaStanja.Parkirano, this);

            ID = voziloRedak.ID;
            Naziv = voziloRedak.Naziv;
            Tip = voziloRedak.Tip;
            VrstaOtpada = voziloRedak.VrstaOtpada;
            Nosivost = voziloRedak.Nosivost;
            Vozaci = voziloRedak.Vozaci;
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
    }
}
