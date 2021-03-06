﻿using System;

namespace carShopDllProject
{

    [Serializable()]
    public class Auto : Veicolo
    {

        private int numAirbag;

        public Auto() : base(
            "Mercedes",
            "GLX",
            "Nero",
            2100,
            175.20,
            DateTime.Now,
            false,
            false,
            30000,
            0)
        {
            NumAirbag = 6;
        }

        public Auto(string marca, string modello, string colore,
            int cilindrata, double potenzaKw, DateTime immatricolazione,
            bool isUsato, bool isKmZero, double prezzo, int kmPercorsi, int numAirbag) 
            : base(
                marca, 
                modello, 
                colore,
                cilindrata, 
                potenzaKw, 
                immatricolazione,
                isUsato, 
                isKmZero, 
                prezzo,
                kmPercorsi)
        {
            this.NumAirbag = numAirbag;
        }

        public int NumAirbag { get => numAirbag; set => numAirbag = value; }

        public override string ToString()
        {
            return $"Auto: {base.ToString()} - {this.NumAirbag} Airbag" ;
        }

    }
}
