using AutoMapper;
using CodeChallenge.Entities.DTOS;
using CodeChallenge.Entities.Models;
using System;

namespace CodeChallenge.MapperProfiles
{
    public class ReptilProfile : Profile
    {
        public ReptilProfile()
        {
            CreateMap<AnimalFormDTO, Reptil>();
            CreateMap<Reptil, AnimalFormDTO>();
        }
    }
}
