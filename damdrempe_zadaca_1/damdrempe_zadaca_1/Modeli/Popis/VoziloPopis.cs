﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace damdrempe_zadaca_1.Modeli
{
    enum TipVozila
    {
        Dizel,
        Elektricni
    }

    enum VrstaOtpada
    {
        Staklo,
        Papir,
        Metal,
        Bio,
        Mijesano
    }

    class VoziloPopis
    {
        public string Naziv { get; set; }

        public TipVozila Tip { get; set; }

        public VrstaOtpada VrstaOtpada { get; set; }

        public int Nosivost { get; set; }

        public List<string> Vozaci { get; set; }

        public VoziloPopis() => Vozaci = new List<string>();
    }
}