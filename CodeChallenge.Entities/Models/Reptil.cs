using CodeChallenge.Entities.DTOS;
using CodeChallenge.Entities.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeChallenge.Entities.Models
{
    public class Reptil : Carnivoro
    {
        public override AnimalTiposEnum Tipo { get; } = AnimalTiposEnum.Reptil;
        public double DiasCambioDePiel { get; set; }
        public double PorcentajeHierba { get; set; }
        public double DiasSinComer { get; } = 3;
        public double ComidaPorDiaHierba => CalcularHierbaPorDia();
        public double ComidaPorDiaCarne => CalcularCarnePorDia();

        private double CalcularCarnePorDia()
        {
            double daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            double totalCambioDePiel = daysInMonth /  DiasCambioDePiel; // Cuántas veces ocurre el cambio de piel en el mes
            double totalDiasSinAlimentarse = totalCambioDePiel * 3; // Total de días sin alimentarse debido al cambio de piel
            double totalDiasAlimentacion = daysInMonth - totalDiasSinAlimentarse;

            double cantidadCarnePorSemana = PorcentajeCarne * Peso;
            double cantidadComidaPorMes = cantidadCarnePorSemana * (daysInMonth / 7);

            double cantidadComidaPorMesMenosDiasCambioDePiel = (totalDiasAlimentacion * cantidadComidaPorMes) / daysInMonth;
            double promedioAlimentacionDiaria = cantidadComidaPorMesMenosDiasCambioDePiel / daysInMonth;

            return promedioAlimentacionDiaria;
        }
        private double CalcularHierbaPorDia()
        {           
            double daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            double totalCambioDePiel = daysInMonth / DiasCambioDePiel; // Cuántas veces ocurre el cambio de piel en el mes
            double totalDiasSinAlimentarse = totalCambioDePiel * 3; // Total de días sin alimentarse debido al cambio de piel
            double totalDiasAlimentacion = daysInMonth - totalDiasSinAlimentarse;

            double cantidadHierbaPorSemana = PorcentajeHierba * Peso;
            double cantidadComidaPorMes = cantidadHierbaPorSemana * (daysInMonth / 7);

            double cantidadComidaPorMesMenosDiasCambioDePiel = (totalDiasAlimentacion * cantidadComidaPorMes) / daysInMonth;
            double promedioAlimentacionDiaria = cantidadComidaPorMesMenosDiasCambioDePiel / daysInMonth;

            return promedioAlimentacionDiaria;          
        }
        public override double CalcularAlimento()
        {
            return ComidaPorDiaHierba + ComidaPorDiaCarne;
        }

        public double CalcularAlimentoSinCambioDePiel()
        {
            double daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            double cantidadCarnePorDia = PorcentajeCarne * Peso;
            double cantidadHierbasPorDia = PorcentajeHierba * Peso;
            double cantidadComidaPorMes = (daysInMonth / 7) * (cantidadCarnePorDia + cantidadHierbasPorDia);
            return cantidadComidaPorMes / daysInMonth;
        }
        public override ValidationResult Validate() => new ReptilValidator().Validate(this);
    }

    public class ReptilValidator : AbstractValidator<Reptil>
    {
        public ReptilValidator()
        {
            RuleFor(m => m.Especie)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar la {PropertyName} del animal")
                .NotNull().WithMessage("Debe especificar la {PropertyName} del animal");

            RuleFor(m => m.LugarOrigen)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar el {PropertyName} del animal")
                .NotNull().WithMessage("Debe especificar el {PropertyName} del animal")
                .WithName("Lugar origen");

            RuleFor(m => m.Peso)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar el {PropertyName} del animal")
                .NotNull().WithMessage("Debe especificar el {PropertyName} del animal")
                .GreaterThan(0).WithMessage("El {PropertyName} del animal no puede ser menor a 0");

            RuleFor(m => m.PorcentajeCarne)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe seleccionar el {PropertyName} del animal")
                .NotNull().WithMessage("Debe seleccionar el {PropertyName} del animal")
                .GreaterThan(0).WithMessage("El {PropertyName} del animal no puede ser menor a 0")
                .WithName("Porcentaje de carne");

            RuleFor(m => m.PorcentajeHierba)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe seleccionar el {PropertyName} del animal")
                .NotNull().WithMessage("Debe seleccionar el {PropertyName} del animal")
                .GreaterThan(0).WithMessage("El {PropertyName} del animal no puede ser menor a 0")
                .WithName("Porcentaje de hierbas");

            RuleFor(m => m.Edad)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar la {PropertyName} del animal")
                .NotNull().WithMessage("Debe especificar la {PropertyName} del animal")
                .GreaterThan(0).WithMessage("La {PropertyName} del animal no puede ser menor a 0");

            RuleFor(m => m.DiasCambioDePiel)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar cada cuantos días el animal tiene un {PropertyName}")
                .NotNull().WithMessage("Debe especificar cada cuantos días el animal tiene un {PropertyName}")
                .GreaterThan(0).WithMessage("Los días de {PropertyName} del animal no pueden ser menor a 0")
                .WithName("Cambio de piel");
        }
    }
}
