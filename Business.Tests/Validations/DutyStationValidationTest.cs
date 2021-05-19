using Moq;
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
    class DutyStationValidationTest
    {
        private Mock<IBaseService<Guid, DutyStation>> _service;
        private IBaseValidation<Guid, DutyStation> _validation;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IBaseService<Guid, DutyStation>>();
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
            MockData(
                dutyStation: new DutyStation()
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
            MockData();
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), BaseValidation.ObjectNotExistById }
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
            MockData();
            var entity = new DutyStation()
            {
                Place = "г. Полоцк"
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
            MockData();
            var entity = new DutyStation()
            {
                Place = GetString(length)
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
            MockData();
            var entity = new DutyStation()
            {
                Place = null
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
            MockData(
                isExist: true
                );
            var entity = new DutyStation()
            {
                Place = "1234"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), BaseValidation.ObjectExistWithThisData }
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
            MockData();

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
            MockData(
                dutyStation: new DutyStation()
                );
            var entity = new DutyStation()
            {
                Place = "г. Полоцк"
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
            MockData(
                dutyStation: new DutyStation()
                );
            var entity = new DutyStation()
            {
                Place = GetString(length)
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
            MockData(
                dutyStation: new DutyStation()
                );
            var entity = new DutyStation()
            {
                Place = null
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
            MockData(
                dutyStation: new DutyStation(),
                isExist: true
                );
            var entity = new DutyStation()
            {
                Place = "1234"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), BaseValidation.ObjectExistWithThisData }
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
            MockData(
                );
            var entity = new DutyStation()
            {
                Place = "1234"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), BaseValidation.ObjectNotExistById }
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
            MockData();

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
            MockData(
                dutyStation: new DutyStation()
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
            MockData();
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), BaseValidation.ObjectNotExistById }
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

        private void MockData(
            DutyStation dutyStation = null,
            bool isExist = false
            )
        {
            _service.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(dutyStation);
            _service.Setup(x => x.ExistAsync(It.IsAny<DutyStation>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExist);

            _validation = new DutyStationValidation(_service.Object);
        }

        private static string GetString(int length) => new Randomizer().GetString(length);

        private string GetBaseValidationResources(string name)
        {
            switch (name)
            {
                case "ObjectExistWithThisData": return BaseValidation.ObjectExistWithThisData;
                case "FieldNotCanBeNull": return BaseValidation.FieldNotCanBeNull;
                case "FieldInvalidLength": return BaseValidation.FieldInvalidLength;
                case "ObjectNotExistById": return BaseValidation.ObjectNotExistById;
                case "ObjectNotCanBeNull": return BaseValidation.ObjectNotCanBeNull;
            }
            return null;
        }
    }
}
