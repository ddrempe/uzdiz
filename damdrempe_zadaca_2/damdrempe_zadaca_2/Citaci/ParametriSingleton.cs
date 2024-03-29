﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_2
{
    public interface IParametri
    {
        string DohvatiParametar(string nazivParametra);
    }

    class ParametriSingleton : IParametri
    {
        private static ParametriSingleton instanca;

        private Dictionary<string, string> parametri = new Dictionary<string, string>();

        private ParametriSingleton(string nazivDatotekeParametara)
        {
            string[] retci = File.ReadAllLines(nazivDatotekeParametara);
            foreach (string redak in retci)
            {
                string[] rezultat = redak.Split(':');
                parametri.Add(rezultat[0], rezultat[1]);
            }
        }  

        public static ParametriSingleton DohvatiInstancu(string nazivDatotekeParametara)
        {
            if (instanca == null)
            {
                instanca = new ParametriSingleton(nazivDatotekeParametara);
            }

            return instanca;
        }

        public string DohvatiParametar(string nazivParametra)
        {
            return parametri[nazivParametra];
        }

        public int DohvatiParametarInt(string nazivParametra)
        {
            return int.Parse(DohvatiParametar(nazivParametra));
        }

        public bool DohvatiParametarBool(string nazivParametra)
        {
            return DohvatiParametarInt(nazivParametra) == 1 ? true : false;
        }
    }
}
