using CodeChallenge.Entities.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Entities.Models
{
    public class Animal
    {
        public virtual AnimalTiposEnum Tipo { get; }
        public string Especie { get; set; }
        public int Edad { get; set; }
        public string LugarOrigen { get; set; }
        public double Peso { get; set; }
        public DateTime FechaIngreso { get; } = DateTime.Now;

        public virtual double CalcularAlimento()
        {
            return 0;
        }
        public virtual ValidationResult Validate() => new AnimalValidator().Validate(this);
    }

    public class AnimalValidator : AbstractValidator<Animal>
    {
        public AnimalValidator()
        {
            RuleFor(m => m.Edad)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar la {PropertyName} del animal")
                .NotNull().WithMessage("Debe especificar la {PropertyName} del animal")
                .GreaterThan(0).WithMessage("La {PropertyName} del animal no puede ser menor a 0");

            RuleFor(m => m.Especie)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar la {PropertyName} del animal")
                .NotNull().WithMessage("Debe especificar la {PropertyName} del animal");

            RuleFor(m => m.LugarOrigen)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar el {PropertyName} del animal")
                .NotNull().WithMessage("Debe especificar el {PropertyName} del animal")
                .WithName("Lugar de origen");

            RuleFor(m => m.Peso)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar el {PropertyName} del animal")
                .NotNull().WithMessage("Debe especificar el {PropertyName} del animal")
                .GreaterThan(0).WithMessage("El {PropertyName} del animal no puede ser menor a 0");
        }
    }
}
