using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            public abstract void Remove(PodrucjeComponent podrucje);
        }

        public class UlicaPodrucja : PodrucjeComponent
        {
            public UlicaPodrucja(string id) : base(id)
            {

            }

            public override void Dodijeli(PodrucjeComponent podrucje)
            {
                throw new NotImplementedException();    //TODO: unallowed exception
            }

            public override void Remove(PodrucjeComponent podrucje)
            {
                throw new NotImplementedException();    //TODO: unallowed exception
            }
        }

        public class Podrucje : PodrucjeComponent
        {
            private List<PodrucjeComponent> podrucja = new List<PodrucjeComponent>();

            public Podrucje(string id) : base (id)
            {

            }

            public override void Dodijeli(PodrucjeComponent podrucje)
            {
                podrucja.Add(podrucje);
            }

            public override void Remove(PodrucjeComponent podrucje)
            {
                podrucje.Remove(podrucje);
            }
        }
    }
}
