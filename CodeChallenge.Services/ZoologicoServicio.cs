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

        public List<AnimalTiposEnum> ObtenerTiposAnimales() => new List<AnimalTiposEnum>() { AnimalTiposEnum.Herbivoro, AnimalTiposEnum.Reptil, AnimalTiposEnum.Carnivoro, };

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
            if (Convert.ToDouble(CalcularAlimento().Data) + (animal.CalcularAlimento() * daysInMonth) > 1500) return new ServiceResponse(false, validatorResponse);
            _animales.Add(animal);
            return new ServiceResponse(true, validatorResponse);
        }

        public ServiceDataResponse<List<Animal>> ObtenerAnimales()
        {
            return new ServiceDataResponse<List<Animal>>(true, _animales);
        }

        public ServiceDataResponse<object> CalcularAlimento()
        {
            double daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            double alimentoTotal = _animales.Sum(animal => animal.CalcularAlimento()) * daysInMonth;
            return new ServiceDataResponse<object>(true, alimentoTotal);
        }
    }
}
