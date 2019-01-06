using damdrempe_zadaca_3.Podaci.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static damdrempe_zadaca_3.Podaci.Enumeracije;

namespace damdrempe_zadaca_3.Pomagaci.Entiteti
{
    class PodrucjaComposite
    {
        public abstract class PodrucjeComponent
        {
            public string PodrucjeID;

            public string Naziv;

            public PodrucjeComponent(string id, string naziv)
            {
                PodrucjeID = id;
                Naziv = naziv;
            }

            public abstract void Dodijeli(PodrucjeComponent podrucje);

            public abstract void Obrisi(PodrucjeComponent podrucje);
        }

        public class UlicaPodrucja : PodrucjeComponent
        {
            public Ulica ReferencaUlice { get; set; }

            public UlicaPodrucja(string id, string naziv, Ulica ulica) : base(id, naziv)
            {
                ReferencaUlice = ulica;
            }

            public override void Dodijeli(PodrucjeComponent podrucje)
            {
                //TODO: implementirati
            }

            public override void Obrisi(PodrucjeComponent podrucje)
            {
                //TODO: implementirati
            }
        }

        public class Podrucje : PodrucjeComponent
        {
            public List<PodrucjeComponent> podrucja = new List<PodrucjeComponent>();

            public Podrucje(string id, string naziv) : base (id, naziv)
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
