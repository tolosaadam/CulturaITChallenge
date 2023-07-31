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
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void CalcularAlimentoSoloCarnivoros()
        {
            _animales.AddRange(MockFactoryCarnivoros());
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(result, 22.5);
        }

        [Test]
        public void CalcularAlimentoSoloReptiles()
        {
            _animales.AddRange(MockFactoryReptiles());
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(result, 22.5);
        }

        [Test]
        public void CalcularAlimentoSoloHerbivoros()
        {
            _animales.AddRange(MockFactoryHerbivoros());
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(result, 185);
        }

        [Test]
        public void CalcularAlimentoTodos()
        {
            _animales.AddRange(MockFactoryTodos());
            var result = _animales.Sum(a => a.CalcularAlimento());
            Assert.AreEqual(result, 207.5);
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

        private List<Carnivoro> MockFactoryReptiles()
        {
            return new List<Carnivoro>() {
                new Reptil{
                    Peso = 100,
                    Porcentaje = 0.05,
                    DiasCambioDePiel = 3
                },
                new Reptil{
                    Peso = 80,
                    Porcentaje = 0.1,
                    DiasCambioDePiel = 4
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