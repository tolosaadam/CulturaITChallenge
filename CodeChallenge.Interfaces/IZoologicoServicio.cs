using CodeChallenge.Entities.DTOS;
using CodeChallenge.Entities.Enums;
using CodeChallenge.Entities.Models;
using CodeChallenge.Entities.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeChallenge.Interfaces
{
    public interface IZoologicoServicio
    {
        List<AnimalTiposEnum> ObtenerTiposAnimales();
        ServiceResponse AgregarAnimal(AnimalFormDTO animalFormDTO);
        ServiceDataResponse<List<Animal>> ObtenerAnimales();
        ServiceDataResponse<object> CalcularAlimento();
    }
}