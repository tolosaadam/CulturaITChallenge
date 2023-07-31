using AutoMapper;
using CodeChallenge.Entities.DTOS;
using CodeChallenge.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.MapperProfiles
{
    public class HerbivoroProfile : Profile
    {
        public HerbivoroProfile()
        {
            CreateMap<AnimalFormDTO, Herbivoro>();
            CreateMap<Herbivoro, AnimalFormDTO>();
        }
    }
}
