using AutoMapper;
using CodeChallenge.Entities.DTOS;
using CodeChallenge.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.MapperProfiles
{
    public class AnimalProfile : Profile
    {
        public AnimalProfile()
        {
            CreateMap<AnimalFormDTO, Animal>();
            CreateMap<Animal, AnimalFormDTO>();
        }
    }
}
