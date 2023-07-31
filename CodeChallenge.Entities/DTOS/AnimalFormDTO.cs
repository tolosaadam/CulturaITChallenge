using CodeChallenge.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Entities.DTOS
{
    public class AnimalFormDTO
    {
        public virtual AnimalTiposEnum Tipo { get; set; }
        public string Especie { get; set; }
        public int Edad { get; set; }
        public string LugarOrigen { get; set; }
        public double Peso { get; set; }
        public double Porcentaje { get; set; }
        public double Kilos { get; set; }
        public int DiasCambioDePiel { get; set; }
    }
}
