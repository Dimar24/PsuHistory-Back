using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Validations
{
    [TestFixture]
    class BurialValidationTest
    {
        private Mock<IBaseRepository<Guid, Burial>> _serviceBurial;
        private Mock<IBaseRepository<Guid, TypeBurial>> _serviceTypeBurial;
        private IBaseValidation<Guid, Burial> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceBurial = new Mock<IBaseRepository<Guid, Burial>>();
            _serviceTypeBurial = new Mock<IBaseRepository<Guid, TypeBurial>>();
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
                isExistBurial: true,
                isExistBurialById: true,
                isExistTypeBurialById: true
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
            MockData(
                isExistBurial: true,
                isExistBurialById: false,
                isExistTypeBurialById: true
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial), string.Format(BaseValidation.ObjectNotExistById, nameof(Burial), id) }
            };

            // Act
            var result = await _validation.GetValidationAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task InsertValidationAsync_Succes()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(result.Errors);
                Assert.IsTrue(result.IsValid);
            });
        }

        [Test]
        public async Task InsertValidationAsync_BurialExist_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: true,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial), string.Format(BaseValidation.ObjectExistWithThisData, nameof(Burial)) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task InsertValidationAsync_TypeBurialExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: false
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.TypeBurial), string.Format(BaseValidation.ObjectNotExistById, nameof(Burial.TypeBurial), entity.TypeBurialId) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task InsertValidationAsync_NumberBurialInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: -10,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.NumberBurial), string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.NumberBurial)) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task InsertValidationAsync_LocationInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: null,
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Location), string.Format(BaseValidation.FieldNotCanBeNull, nameof(Burial.Location)) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [TestCase(2)]
        [TestCase(555)]
        public async Task InsertValidationAsync_LocationInvalid_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: GetString(length),
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Location), string.Format(BaseValidation.FieldInvalidLength, nameof(Burial.Location), 3, 512) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task InsertValidationAsync_KnownNumberInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: -1,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.KnownNumber), string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.KnownNumber)) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task InsertValidationAsync_UnknownNumberInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: -1,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.UnknownNumber), string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.UnknownNumber)) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [TestCase(1899)]
        [TestCase(2022)]
        public async Task InsertValidationAsync_YearInvalid_UnSucces(int year)
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: year,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Year), string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Year), 1900, DateTime.Now.Year) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [TestCase(-90.1)]
        [TestCase(90.1)]
        public async Task InsertValidationAsync_LatitudeInvalid_UnSucces(double latitude)
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: latitude,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Latitude), string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Latitude), -90.0, 90.0) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [TestCase(-180.1)]
        [TestCase(180.1)]
        public async Task InsertValidationAsync_LongitudeInvalid_UnSucces(double longitude)
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: longitude,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Longitude), string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Longitude), -180.0, 180.0) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            Burial entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Burial)) }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task UpdateValidationAsync_Succes()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(result.Errors);
                Assert.IsTrue(result.IsValid);
            });
        }

        [Test]
        public async Task UpdateValidationAsync_BurialExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: false,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial), string.Format(BaseValidation.ObjectNotExistById, nameof(Burial), entity.Id) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task UpdateValidationAsync_BurialExist_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: true,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial), string.Format(BaseValidation.ObjectExistWithThisData, nameof(Burial)) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task UpdateValidationAsync_TypeBurialExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: false
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.TypeBurial), string.Format(BaseValidation.ObjectNotExistById, nameof(Burial.TypeBurial), entity.TypeBurialId) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task UpdateValidationAsync_NumberBurialInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: -10,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.NumberBurial), string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.NumberBurial)) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task UpdateValidationAsync_LocationInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: null,
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Location), string.Format(BaseValidation.FieldNotCanBeNull, nameof(Burial.Location)) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [TestCase(2)]
        [TestCase(555)]
        public async Task UpdateValidationAsync_LocationInvalid_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: GetString(length),
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Location), string.Format(BaseValidation.FieldInvalidLength, nameof(Burial.Location), 3, 512) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task UpdateValidationAsync_KnownNumberInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: -1,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.KnownNumber), string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.KnownNumber)) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task UpdateValidationAsync_UnknownNumberInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: -1,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.UnknownNumber), string.Format(BaseValidation.FieldNotCanBeNegative, nameof(Burial.UnknownNumber)) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [TestCase(1899)]
        [TestCase(2022)]
        public async Task UpdateValidationAsync_YearInvalid_UnSucces(int year)
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: year,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Year), string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Year), 1900, DateTime.Now.Year) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [TestCase(-90.1)]
        [TestCase(90.1)]
        public async Task UpdateValidationAsync_LatitudeInvalid_UnSucces(double latitude)
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: latitude,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Latitude), string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Latitude), -90.0, 90.0) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        [TestCase(-180.1)]
        [TestCase(180.1)]
        public async Task UpdateValidationAsync_LongitudeInvalid_UnSucces(double longitude)
        {
            // Arrange
            MockData(
                isExistBurial: false,
                isExistBurialById: true,
                isExistTypeBurialById: true
            );
            var entity = GetBurial(
                id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: longitude,
                description: "Test description",
                typeBurialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial.Longitude), string.Format(BaseValidation.FieldInvalidNumber, nameof(Burial.Longitude), -180.0, 180.0) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
            MockData(
                isExistBurial: true,
                isExistBurialById: true
            );
            Burial entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Burial)) }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
        public async Task DeleteValidationAsync_Succes()
        {
            // Arrange
            MockData(
                isExistBurial: true,
                isExistBurialById: true,
                isExistTypeBurialById: true
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
            MockData(
                isExistBurial: true,
                isExistBurialById: false,
                isExistTypeBurialById: true
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(Burial), string.Format(BaseValidation.ObjectNotExistById, nameof(Burial), id) }
            };

            // Act
            var result = await _validation.DeleteValidationAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        private void MockData(
            bool isExistBurial = default,
            bool isExistBurialById = default,
            bool isExistTypeBurialById = default
            )
        {
            _serviceBurial.Setup(x => x.ExistAsync(It.IsAny<Burial>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistBurial);
            _serviceBurial.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistBurialById);

            _serviceTypeBurial.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistTypeBurialById);

            _validation = new BurialValidation(_serviceBurial.Object, _serviceTypeBurial.Object);
        }

        private Burial GetBurial(
            Guid id = default,
            int numberBurial = default,
            string location = default,
            int knownNumber = default,
            int unknownNumber = default,
            int year = default,
            double latitude = default,
            double longitude = default,
            string description = default,
            Guid typeBurialId = default
            )
        {
            return new Burial()
            {
                Id = id,
                NumberBurial = numberBurial,
                Location = location,
                KnownNumber = knownNumber,
                UnknownNumber = unknownNumber,
                Year = year,
                Latitude = latitude,
                Longitude = longitude,
                Description = description,
                TypeBurialId = typeBurialId
            };
        }

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
