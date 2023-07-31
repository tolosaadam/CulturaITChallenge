using AutoMapper;
using CodeChallenge.Entities.DTOS;
using CodeChallenge.Entities.Enums;
using CodeChallenge.Entities.Models;
using CodeChallenge.Entities.Responses;
using CodeChallenge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge.Services
{
    public class ZoologicoServicio : IZoologicoServicio
    {
        public List<Animal> _animales;
        private readonly IMapper _mapper;
        public ZoologicoServicio(IMapper mapper)
        {
            _animales = new List<Animal>();
            _mapper = mapper;
        }

        /// <summary>
        /// Devuelve una lista de los tipos de animales.
        /// </summary>
        public List<AnimalTiposEnum> ObtenerTiposAnimales() => new List<AnimalTiposEnum>() { AnimalTiposEnum.Herbivoro, AnimalTiposEnum.Reptil, AnimalTiposEnum.Carnivoro, };

        /// <summary>
        /// Agrega un animal al zoologico, y verifica si  el alimento de todos los animales sumados no supera los 1500Kg por mes.
        /// </summary>
        /// <param name="animalFormDTO">AnimalFormDTO object</param>
        public ServiceResponse AgregarAnimal(AnimalFormDTO animalFormDTO)
        {
            Animal animal = new Animal();
            switch (animalFormDTO.Tipo)
            {
                case AnimalTiposEnum.Reptil:
                    animal = _mapper.Map<AnimalFormDTO, Reptil> (animalFormDTO);
                    break;
                case AnimalTiposEnum.Carnivoro:
                    animal = _mapper.Map<AnimalFormDTO, Carnivoro>(animalFormDTO);
                    break;
                case AnimalTiposEnum.Herbivoro:
                    animal = _mapper.Map<AnimalFormDTO, Herbivoro>(animalFormDTO);
                    break;
            }

            #region Validate Model
            var validatorResponse = animal.Validate();
            if (!validatorResponse.IsValid) return new ServiceResponse(false, validatorResponse);
            #endregion
            double daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            var alimentoTotalMensual = CalcularAlimentoMensualTotal().Data;
            if ((alimentoTotalMensual.TotalCarne + alimentoTotalMensual.TotalHierba) + (animal.CalcularAlimento() * daysInMonth) > 1500) return new ServiceResponse(false, validatorResponse);
            _animales.Add(animal);
            return new ServiceResponse(true, validatorResponse);
        }

        /// <summary>
        /// Devuelve una lista de todos los animales del zoologico.
        /// </summary>
        public ServiceDataResponse<List<Animal>> ObtenerAnimales()
        {
            return new ServiceDataResponse<List<Animal>>(true, _animales);
        }

        /// <summary>
        /// Calcula el alimento mensual total en Kg que se necesita, diferenciando entre Carnes y hierbas.
        /// </summary>
        public ServiceDataResponse<TotalAlimentoMensualDTO> CalcularAlimentoMensualTotal()
        {
            double daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            double alimentoTotal = _animales.Sum(animal =>
            {
                return animal.CalcularAlimento() * daysInMonth;
            });

            double totalConsumoCarnivoros = _animales.Where(animal => animal.Tipo == AnimalTiposEnum.Carnivoro).Sum(animal => animal.CalcularAlimento() * daysInMonth);
            double totalConsumoHebivoros = _animales.Where(animal => animal.Tipo == AnimalTiposEnum.Herbivoro).Sum(animal => animal.CalcularAlimento() * daysInMonth);

            // Calcular el total de consumo de carne para los reptiles
            double totalConsumoReptilesCarne = _animales
                .Where(animal => animal is Reptil reptil)
                .Sum(reptil => ((Reptil)reptil).ComidaPorDiaCarne * daysInMonth);

            // Calcular el total de consumo de hierba para los reptiles
            double totalConsumoReptilesHierba = _animales
                .Where(animal => animal is Reptil reptil)
                .Sum(reptil => ((Reptil)reptil).ComidaPorDiaHierba * daysInMonth);

            return new ServiceDataResponse<TotalAlimentoMensualDTO>(true, new TotalAlimentoMensualDTO{ TotalCarne = totalConsumoCarnivoros + totalConsumoReptilesCarne , TotalHierba = totalConsumoHebivoros  + totalConsumoReptilesHierba });
        }

        /// <summary>
        /// Calcula los dias del mes en curso en el que el animal consumio. Si no es el mes en curso calcula el total.
        /// </summary>
        /// <param name="animal">Animal object</param>
        public double CalcularDiasConsumidos(Animal animal)
        {
            DateTime fechaIngresoAnimal = animal.FechaIngreso;
            DateTime fechaActual = DateTime.Now;

            // Verificar si es el mes en curso
            if (fechaIngresoAnimal.Year == fechaActual.Year && fechaIngresoAnimal.Month == fechaActual.Month)
            {
                // Es el mes en curso, calcular días desde el ingreso hasta hoy
                double diasDesdeIngresoHastaHoy = (fechaActual - fechaIngresoAnimal).TotalDays + 1;
                return Math.Max(diasDesdeIngresoHastaHoy, 0);
            }
            else
            {
                // No es el mes en curso, calcular días desde el primer día del mes hasta hoy
                DateTime primerDiaMesActual = new DateTime(fechaActual.Year, fechaActual.Month, 1);
                double diasDesdePrimerDiaHastaHoy = (fechaActual - primerDiaMesActual).TotalDays + 1;
                return Math.Max(diasDesdePrimerDiaHastaHoy, 0);
            }
        }
    }
}
