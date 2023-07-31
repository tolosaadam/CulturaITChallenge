using CodeChallenge.Entities.Enums;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Entities.Models
{
    public class Herbivoro : Animal
    {
        public override AnimalTiposEnum Tipo { get; } = AnimalTiposEnum.Herbivoro;
        public double Kilos { get; set; }
        public override double CalcularAlimento()
        {
            return 2 * Peso + Kilos;
        }
        public override ValidationResult Validate() => new HerbivoroValidator().Validate(this);
    }

    public class HerbivoroValidator : AbstractValidator<Herbivoro>
    {
        public HerbivoroValidator()
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

            RuleFor(m => m.Kilos)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Debe especificar los {PropertyName} de comida que come el animal")
                .NotNull().WithMessage("Debe especificar los {PropertyName} de comida que come el animal")
                .GreaterThan(0).WithMessage("Los {PropertyName} que come el animal no pueden ser menor a 0");
        }
    }
}
