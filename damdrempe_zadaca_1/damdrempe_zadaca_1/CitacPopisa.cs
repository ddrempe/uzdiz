using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1
{
    class CitacPopisa
    {
        private string[] retci;

        private string[] elementi;

        private string element;

        public string DatotekaPopisa { get; set; }

        public CitacPopisa(string datoteka)
        {
            DatotekaPopisa = datoteka;
        }        

        public CitacPopisa ProcitajRetke()
        {
            retci = File.ReadAllLines(DatotekaPopisa);
            return this;
        }

        public string[] VratiRetke()
        {
            return retci;
        }

        public string[] ProcitajElementeRetka(string redak, char separator)
        {
            return redak.Split(separator);
        }

        public CitacPopisa ProcitajElementeRetka(int redniBrojRetka, char separator)
        {
            if(redniBrojRetka <0 || redniBrojRetka >= retci.Length)
            {
                return null;
            }

            elementi = ProcitajElementeRetka(retci[redniBrojRetka], separator);
            return this;
        }

        public string VratiElementRetka(int redniBrojElementa)
        {
            element = elementi[redniBrojElementa];
            return element;
        }

        public int VratiElementRetkaInt(int redniBrojElementa)
        {
            VratiElementRetka(redniBrojElementa);
            return int.Parse(element);
        }
    }
}
