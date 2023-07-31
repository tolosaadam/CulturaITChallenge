using AutoMapper;
using CodeChallenge.Entities.DTOS;
using CodeChallenge.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.MapperProfiles
{
    public class CarnivoroProfile : Profile
    {
        public CarnivoroProfile()
        {
            CreateMap<AnimalFormDTO, Carnivoro>();
            CreateMap<Carnivoro, AnimalFormDTO>();
        }
    }
}
