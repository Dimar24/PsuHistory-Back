﻿using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Service.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Validations
{
    class TypeVictimValidationTest
    {
        private Mock<IBaseService<Guid, TypeVictim>> _service;
        private IBaseValidation<Guid, TypeVictim> _validation;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IBaseService<Guid, TypeVictim>>();
        }

        [TearDown]
        public void Teardown()
        {
            //_dbContext.Dispose();
        }

        [Test]
        public async Task GetValidationAsync_Succes()
        {
            // Arrange
            await MockData(
                typeVictim: new TypeVictim()
                );
            var id = Guid.NewGuid();

            // Act
            var result = await _validation.GetValidationAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(result.Errors);
                Assert.IsTrue(result.IsValid);
            });
        }

        [Test]
        public async Task GetValidationAsync_UnSucces()
        {
            // Arrange
            await MockData();
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), BaseValidation.ObjectNotExistById }
            };

            // Act
            var result = await _validation.GetValidationAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [Test]
        public async Task InsertValidationAsync_Succes()
        {
            // Arrange
            await MockData();
            var entity = new TypeVictim()
            {
                Name = "InsertValidationAsync_Succes"
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(result.Errors);
                Assert.IsTrue(result.IsValid);
            });
        }

        [TestCase(2, "FieldInvalidLength")]
        [TestCase(555, "FieldInvalidLength")]
        public async Task InsertValidationAsync_InvalidPlace_UnSucces(int length, string nameError)
        {
            // Arrange
            await MockData();
            var entity = new TypeVictim()
            {
                Name = GetString(length)
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources(nameError), error.Value);
                }
            });
        }

        [Test]
        public async Task InsertValidationAsync_PlaceIsNull_UnSucces()
        {
            // Arrange
            await MockData();
            var entity = new TypeVictim()
            {
                Name = null
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources("FieldNotCanBeNull"), error.Value);
                }
            });
        }

        [Test]
        public async Task InsertValidationAsync_ExistAsync_UnSucces()
        {
            // Arrange
            await MockData(
                isExist: true
                );
            var entity = new TypeVictim()
            {
                Name = "1234"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), BaseValidation.ObjectExistWithThisData }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [Test]
        public async Task InsertValidationAsync_Null_UnSucces()
        {
            // Arrange
            await MockData();

            // Act
            var result = await _validation.InsertValidationAsync(null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources("ObjectNotCanBeNull"), error.Value);
                }
            });
        }

        [Test]
        public async Task UpdateValidationAsync_Succes()
        {
            // Arrange
            await MockData(
                typeVictim: new TypeVictim()
                );
            var entity = new TypeVictim()
            {
                Name = "1234"
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(result.Errors);
                Assert.IsTrue(result.IsValid);
            });
        }

        [TestCase(2, "FieldInvalidLength")]
        [TestCase(555, "FieldInvalidLength")]
        public async Task UpdateValidationAsync_InvalidPlace_UnSucces(int length, string nameError)
        {
            // Arrange
            await MockData(
                typeVictim: new TypeVictim()
                );
            var entity = new TypeVictim()
            {
                Name = GetString(length)
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources(nameError), error.Value);
                }
            });
        }

        [Test]
        public async Task UpdateValidationAsync_PlaceIsNull_UnSucces()
        {
            // Arrange
            await MockData(
                typeVictim: new TypeVictim()
                );
            var entity = new TypeVictim()
            {
                Name = null
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources("FieldNotCanBeNull"), error.Value);
                }
            });
        }

        [Test]
        public async Task UpdateValidationAsync_ExistAsync_UnSucces()
        {
            // Arrange
            await MockData(
                typeVictim: new TypeVictim(),
                isExist: true
                );
            var entity = new TypeVictim()
            {
                Name = "1234"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), BaseValidation.ObjectExistWithThisData }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [Test]
        public async Task UpdateValidationAsync_GetAsync_UnSucces()
        {
            // Arrange
            await MockData(
                );
            var entity = new TypeVictim()
            {
                Name = "1234"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), BaseValidation.ObjectNotExistById }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [Test]
        public async Task UpdateValidationAsync_Null_UnSucces()
        {
            // Arrange
            await MockData();

            // Act
            var result = await _validation.UpdateValidationAsync(null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources("ObjectNotCanBeNull"), error.Value);
                }
            });
        }

        [Test]
        public async Task DeleteValidationAsync_Succes()
        {
            // Arrange
            await MockData(
                typeVictim: new TypeVictim()
                );
            var id = Guid.NewGuid();

            // Act
            var result = await _validation.DeleteValidationAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(result.Errors);
                Assert.IsTrue(result.IsValid);
            });
        }

        [Test]
        public async Task DeleteValidationAsync_UnSucces()
        {
            // Arrange
            await MockData();
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), BaseValidation.ObjectNotExistById }
            };

            // Act
            var result = await _validation.DeleteValidationAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        private async Task MockData(
            TypeVictim typeVictim = null,
            bool isExist = false
            )
        {
            _service.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(typeVictim);
            _service.Setup(x => x.ExistAsync(It.IsAny<TypeVictim>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExist);

            _validation = new TypeVictimValidation(_service.Object);
        }

        private static string GetString(int length) => new Randomizer().GetString(length);

        private string GetBaseValidationResources(string name)
        {
            switch (name)
            {
                case "ObjectExistWithThisData": return BaseValidation.ObjectExistWithThisData; break;
                case "FieldNotCanBeNull": return BaseValidation.FieldNotCanBeNull; break;
                case "FieldInvalidLength": return BaseValidation.FieldInvalidLength; break;
                case "ObjectNotExistById": return BaseValidation.ObjectNotExistById; break;
                case "ObjectNotCanBeNull": return BaseValidation.ObjectNotCanBeNull; break;
            }
            return null;
        }
    }
}
