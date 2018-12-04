using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_2.Sustav
{
    /// <summary>
    /// The 'Component' abstract class
    /// 'ConcreteComponent' class je vozilo
    /// </summary>
    abstract class PrijevoznoSredstvo
    {
        private int _brojMjesta;

        public int BrojMjesta
        {
            get { return _brojMjesta; }
            set { _brojMjesta = value; }
        }

        public abstract void IspisiPutnike();
    }

    /// <summary>
    /// The 'Decorator' abstract class
    /// </summary>
    abstract class Decorator : PrijevoznoSredstvo
    {
        protected PrijevoznoSredstvo prijevoznoSredstvo;

        public Decorator(PrijevoznoSredstvo prijevoznoSredstvo)
        {
            this.prijevoznoSredstvo = prijevoznoSredstvo;
        }

        public override void IspisiPutnike()
        {
            prijevoznoSredstvo.IspisiPutnike();
        }
    }

    /// <summary>
    /// The 'ConcreteDecorator' class
    /// </summary>
    class PrijevozPutnika : Decorator
    {
        public string VoziloID { get; set; }

        protected List<string> putnici = new List<string>();

        public PrijevozPutnika(PrijevoznoSredstvo prijevoznoSredstvo, string id) : base(prijevoznoSredstvo)
        {
            VoziloID = id;
        }

        public void UkrcajPutnika(string ime)
        {
            if(prijevoznoSredstvo.BrojMjesta > 0)
            {
                putnici.Add(ime);
                prijevoznoSredstvo.BrojMjesta--;
                IspisiObavijest($"Ukrcan je putnik {ime} u {VoziloID}.");
            }
            else
            {
                IspisiObavijest($"Nema vise mjesta za ukrcavanje u {VoziloID}.");
            }
        }

        public void IskrcajPutnike()
        {            
            if (putnici.Count > 0)
            {
                prijevoznoSredstvo.BrojMjesta += putnici.Count;
                putnici.Clear();
                IspisiObavijest($"Iskrcani svi putnici iz {VoziloID}.");
            }
            else
            {
                IspisiObavijest($"Nema putnika za iskrcavanje iz {VoziloID}.");
            }            
        }

        public void IspisiObavijest(string tekst)
        {
            Program.Ispisivac.PromijeniBojuTeksta(ConsoleColor.DarkYellow);
            Program.Ispisivac.ObavljeniPosao(tekst);
            Program.Ispisivac.ResetirajPostavkeBoja();
        }

        public override void IspisiPutnike()
        {
            base.IspisiPutnike();

            foreach (string putnik in putnici)
            {
                Console.WriteLine(" putnik: " + putnik);
            }
        }
    }
}
