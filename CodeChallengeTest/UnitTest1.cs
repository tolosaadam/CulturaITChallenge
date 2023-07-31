using AutoMapper;
using CodeChallenge.Entities.DTOS;
using CodeChallenge.Entities.Enums;
using CodeChallenge.Entities.Models;
using CodeChallenge.Interfaces;
using CodeChallenge.MapperProfiles;
using CodeChallenge.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallengeTest
{
    public class Tests
    {
        private List<Animal> _animales;
        private List<AnimalFormDTO> _animalesFormDTO;
        private readonly IMapper _mapper;
        public Tests() 
        {
            var mapperConfiguration = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<AnimalProfile>();
                cfg.AddProfile<ReptilProfile>();
                cfg.AddProfile<CarnivoroProfile>();
                cfg.AddProfile<HerbivoroProfile>();
            });
            _mapper = mapperConfiguration.CreateMapper();
        }

        [SetUp]
        public void Setup()
        {
            _animales = new List<Animal>();
            _animalesFormDTO = new List<AnimalFormDTO>();
        }

        [Test]
        public void CalcularAlimentoMensualTotal()
        {
            this._animales.AddRange(MockFactoryTodos());
            var zoologicoService = new ZoologicoServicio(_mapper)
            {
                _animales = this._animales
            };
            var result = zoologicoService.CalcularAlimentoMensualTotal();
            Assert.IsTrue(result.Success);
            Assert.AreEqual(6432.5, result.Data.TotalCarne + result.Data.TotalHierba);
            Assert.AreEqual(697.5, result.Data.TotalCarne);
            Assert.AreEqual(5735, result.Data.TotalHierba);
        }

        [Test]
        public void ObtenerTiposAnimales()
        {
            var zoologicoService = new ZoologicoServicio(_mapper);
            var result = zoologicoService.ObtenerTiposAnimales();
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void ObtenerAnimales()
        {
            this._animales.AddRange(MockFactoryTodos());
            var zoologicoService = new ZoologicoServicio(_mapper)
            {
                _animales = this._animales
            };
            var result = zoologicoService.ObtenerAnimales();
            Assert.IsInstanceOf<List<Animal>>(result.Data);
            Assert.AreEqual(this._animales, result.Data);
        }

        [Test]
        public void AgregarAnimal()
        {
            var zoologicoService = new ZoologicoServicio(_mapper);
            _animalesFormDTO.AddRange(MockFactoryAnimalFormDTO());            
            var result = zoologicoService.AgregarAnimal(_animalesFormDTO.FirstOrDefault());
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.ValidationResult.IsValid);
        }

        [Test]
        public void CalcularAlimentoSinAnimales()
        {
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(0, result);
        }

        [Test]
        public void CalcularAlimentoSoloCarnivoros()
        {
            _animales.AddRange(MockFactoryCarnivoros());
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(22.5, result);
        }

        [Test]
        public void CalcularAlimentoSoloReptiles()
        {
            _animales.AddRange(MockFactoryReptiles());
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(1.6326530612244901, result);
        }

        [Test]
        public void CalcularAlimentoSoloHerbivoros()
        {
            _animales.AddRange(MockFactoryHerbivoros());
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(185, result);
        }

        [Test]
        public void CalcularAlimentoTodos()
        {
            _animales.AddRange(MockFactoryTodos());
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(207.5, result);
        }

        #region Mock Factory       
        private List<Carnivoro> MockFactoryCarnivoros()
        {
            return new List<Carnivoro>() {
                new Carnivoro{
                    Peso = 100,
                    PorcentajeCarne = 0.05
                },
                new Carnivoro{
                    Peso = 80,
                    PorcentajeCarne = 0.1
                },
                new Carnivoro{
                    Peso = 95,
                    PorcentajeCarne = 0.1
                }
            };
        }

        private List<Reptil> MockFactoryReptiles()
        {
            return new List<Reptil>() {
                new Reptil{
                    Peso = 100,
                    PorcentajeCarne = 0.1,
                    PorcentajeHierba = 0.1,
                    DiasCambioDePiel = 7
                },
                new Reptil{
                    Peso = 50,
                    PorcentajeCarne = 0.1,
                    PorcentajeHierba = 0.1,
                    DiasCambioDePiel = 3
                }
            };
        }

        private List<Herbivoro> MockFactoryHerbivoros()
        {
            return new List<Herbivoro>() {
                new Herbivoro{
                    Peso = 30,
                    Kilos = 10
                },
                new Herbivoro{
                    Peso = 50,
                    Kilos = 15
                }
            };
        }

        private List<Animal> MockFactoryTodos()
        {
            return new List<Animal>() {
                new Carnivoro{
                    Peso = 100,
                    PorcentajeCarne = 0.05
                },
                new Carnivoro{
                    Peso = 80,
                    PorcentajeCarne = 0.1
                },
                new Carnivoro{
                    Peso = 95,
                    PorcentajeCarne = 0.1
                },
                new Herbivoro{
                    Peso = 30,
                    Kilos = 10
                },
                new Herbivoro{
                    Peso = 50,
                    Kilos = 15
                }
            };
        }

        private List<AnimalFormDTO> MockFactoryAnimalFormDTO()
        {
            return new List<AnimalFormDTO>() {
                new AnimalFormDTO{
                    Tipo = AnimalTiposEnum.Carnivoro,
                    Edad = 15,
                    Especie = "Dummy",
                    LugarOrigen = "Zabana",
                    Peso = 100,
                    PorcentajeCarne = 0.05
                },
                new AnimalFormDTO{
                    Tipo = AnimalTiposEnum.Carnivoro,
                    Edad = 15,
                    Especie = "Dummy",
                    LugarOrigen = "Zabana",
                    Peso = 80,
                    PorcentajeCarne = 0.1
                },
                new AnimalFormDTO{
                    Tipo = AnimalTiposEnum.Carnivoro,
                    Edad = 15,
                    Especie = "Dummy",
                    LugarOrigen = "Zabana",
                    Peso = 95,
                    PorcentajeCarne = 0.1
                },
                new AnimalFormDTO{
                    Tipo = AnimalTiposEnum.Herbivoro,
                    Edad = 15,
                    Especie = "Dummy",
                    LugarOrigen = "Zabana",
                    Peso = 30,
                    Kilos = 10
                },
                new AnimalFormDTO{
                    Tipo = AnimalTiposEnum.Herbivoro,
                    Edad = 15,
                    Especie = "Dummy",
                    LugarOrigen = "Zabana",
                    Peso = 50,
                    Kilos = 15
                }
            };
        }
        #endregion
    }
}