using CodeChallenge.Entities.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallengeTest
{
    public class Tests
    {
        private List<Animal> _animales;

        [SetUp]
        public void Setup()
        {
            _animales = new List<Animal>();
        }

        [Test]
        public void CalcularAlimentoSinAnimales()
        {
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(22.5, result);
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
            Assert.AreEqual(10.333333333333334, result);
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
                    Porcentaje = 0.05
                },
                new Carnivoro{
                    Peso = 80,
                    Porcentaje = 0.1
                },
                new Carnivoro{
                    Peso = 95,
                    Porcentaje = 0.1
                }
            };
        }

        private List<Reptil> MockFactoryReptiles()
        {
            return new List<Reptil>() {
                new Reptil{
                    Peso = 100,
                    Porcentaje = 0.5,
                    DiasCambioDePiel = 7
                },
                new Reptil{
                    Peso = 100,
                    Porcentaje = 0.5,
                    DiasCambioDePiel = 7
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
                    Porcentaje = 0.05
                },
                new Carnivoro{
                    Peso = 80,
                    Porcentaje = 0.1
                },
                new Carnivoro{
                    Peso = 95,
                    Porcentaje = 0.1
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
        #endregion
    }
}