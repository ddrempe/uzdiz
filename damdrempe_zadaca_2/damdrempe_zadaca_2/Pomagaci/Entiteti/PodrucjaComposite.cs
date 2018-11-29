using damdrempe_zadaca_2.Podaci.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_2.Podaci.Enumeracije;

namespace damdrempe_zadaca_2.Pomagaci.Entiteti
{
    class PodrucjaComposite
    {
        public abstract class PodrucjeComponent
        {
            public string PodrucjeID;

            public PodrucjeComponent(string id)
            {
                PodrucjeID = id;
            }

            public abstract void Dodijeli(PodrucjeComponent podrucje);

            public abstract void Obrisi(PodrucjeComponent podrucje);
        }

        public class UlicaPodrucja : PodrucjeComponent
        {
            public Ulica ReferencaUlice { get; set; }

            public UlicaPodrucja(string id, Ulica ulica) : base(id)
            {
                ReferencaUlice = ulica;
            }

            public override void Dodijeli(PodrucjeComponent podrucje)
            {
                throw new NotImplementedException();    //TODO: unallowed exception
            }

            public override void Obrisi(PodrucjeComponent podrucje)
            {
                throw new NotImplementedException();    //TODO: unallowed exception
            }
        }

        public class Podrucje : PodrucjeComponent
        {
            public List<PodrucjeComponent> podrucja = new List<PodrucjeComponent>();

            public Podrucje(string id) : base (id)
            {

            }

            public override void Dodijeli(PodrucjeComponent podrucje)
            {
                podrucja.Add(podrucje);
            }

            public override void Obrisi(PodrucjeComponent podrucje)
            {
                podrucja.Remove(podrucje);
            }
        }
    }
}
