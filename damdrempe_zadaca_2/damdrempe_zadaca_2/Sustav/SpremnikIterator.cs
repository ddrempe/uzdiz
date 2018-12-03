using damdrempe_zadaca_2.Podaci.Modeli;
using System.Collections;

namespace damdrempe_zadaca_2.Sustav
{
    namespace damdrempe_zadaca_2.Sustav
    {
        /// <summary>
        /// The 'Aggregate' interface
        /// </summary>
        interface IApstraktnaKolekcijaS
        {
            IteratorS StvoriIterator();
        }

        /// <summary>
        /// The 'ConcreteAggregate' class
        /// </summary>
        class KolekcijaS : IApstraktnaKolekcijaS
        {
            private ArrayList _stavke = new ArrayList();

            public IteratorS StvoriIterator()
            {
                return new IteratorS(this);
            }

            public int BrojElemenata
            {
                get { return _stavke.Count; }
            }

            public object this[int index]
            {
                get { return _stavke[index]; }
                set { _stavke.Add(value); }
            }
        }

        /// <summary>
        /// The 'Iterator' interface
        /// </summary>
        interface IApstraktniIteratorS
        {
            Spremnik Prvi();

            Spremnik Sljedeci();

            bool Kraj { get; }

            Spremnik Trenutni { get; }
        }

        /// <summary>
        /// The 'ConcreteIterator' class
        /// </summary>
        class IteratorS : IApstraktniIteratorS
        {
            private KolekcijaS _kolekcija;
            private int _trenutni = 0;
            private int _korak = 1;

            public IteratorS(KolekcijaS kolekcija)
            {
                this._kolekcija = kolekcija;
            }

            public Spremnik Prvi()
            {
                _trenutni = 0;
                return _kolekcija[_trenutni] as Spremnik;
            }

            public Spremnik Sljedeci()
            {
                _trenutni += _korak;
                if (!Kraj)
                    return _kolekcija[_trenutni] as Spremnik;
                else
                    return null;
            }

            public int Korak
            {
                get { return _korak; }
                set { _korak = value; }
            }

            public Spremnik Trenutni
            {
                get { return _kolekcija[_trenutni] as Spremnik; }
            }

            public bool Kraj
            {
                get { return _trenutni >= _kolekcija.BrojElemenata; }
            }
        }
    }
}
