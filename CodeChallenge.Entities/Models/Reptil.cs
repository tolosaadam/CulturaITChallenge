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
        public double DiasSinComer { get; } = 3;
        public override double CalcularAlimento()
        {
            double daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            double daysFoodCicle = daysInMonth / (DiasCambioDePiel + DiasSinComer);
            double daysFeeding = daysFoodCicle * DiasCambioDePiel;
            double amountFeedingWeek = Porcentaje * Peso;
            double amountFeedingDay = amountFeedingWeek / 7;
            return (amountFeedingDay * daysFeeding) / daysInMonth;
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

            RuleFor(m => m.Porcentaje)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe seleccionar el {PropertyName} del animal")
                .NotNull().WithMessage("Debe seleccionar el {PropertyName} del animal")
                .GreaterThan(0).WithMessage("El {PropertyName} del animal no puede ser menor a 0");

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
