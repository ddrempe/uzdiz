using damdrempe_zadaca_2.Podaci.Modeli;
using System.Collections;

namespace damdrempe_zadaca_2.Sustav
{
    namespace damdrempe_zadaca_2.Sustav
    {
        /// <summary>
        /// The 'Aggregate' interface
        /// </summary>
        interface IApstraktnaKolekcijaU
        {
            IteratorU StvoriIterator();
        }

        /// <summary>
        /// The 'ConcreteAggregate' class
        /// </summary>
        class KolekcijaU : IApstraktnaKolekcijaU
        {
            private ArrayList _stavke = new ArrayList();

            public IteratorU StvoriIterator()
            {
                return new IteratorU(this);
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
        interface IApstraktniIteratorU
        {
            Ulica Prvi();

            Ulica Sljedeci();

            bool Kraj { get; }

            Ulica Trenutni { get; }
        }

        /// <summary>
        /// The 'ConcreteIterator' class
        /// </summary>
        class IteratorU : IApstraktniIteratorU
        {
            private KolekcijaU _kolekcija;
            private int _trenutni = 0;
            private int _korak = 1;

            public IteratorU(KolekcijaU kolekcija)
            {
                this._kolekcija = kolekcija;
            }

            public Ulica Prvi()
            {
                _trenutni = 0;
                return _kolekcija[_trenutni] as Ulica;
            }

            public Ulica Sljedeci()
            {
                _trenutni += _korak;
                if (!Kraj)
                    return _kolekcija[_trenutni] as Ulica;
                else
                    return null;
            }

            public int Korak
            {
                get { return _korak; }
                set { _korak = value; }
            }

            public Ulica Trenutni
            {
                get { return _kolekcija[_trenutni] as Ulica; }
            }

            public bool Kraj
            {
                get { return _trenutni >= _kolekcija.BrojElemenata - 1; }   //TODO: provjeriti da li ide -1
            }
        }
    }
}
