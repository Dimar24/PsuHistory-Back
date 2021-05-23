using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
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
    class DutyStationValidationTest
    {
        private Mock<IBaseRepository<Guid, DutyStation>> _serviceDutyStation;
        private IBaseValidation<Guid, DutyStation> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceDutyStation = new Mock<IBaseRepository<Guid, DutyStation>>();
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
                isExistDutyStation: true,
                isExistDutyStationById: true
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
                isExistDutyStation: true,
                isExistDutyStationById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), string.Format(BaseValidation.ObjectNotExistById, nameof(DutyStation), id) }
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
                isExistDutyStation: false,
                isExistDutyStationById: false
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: "Test Place"
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
        public async Task InsertValidationAsync_Exist_UnSucces()
        {
            // Arrange
            MockData(
                isExistDutyStation: true,
                isExistDutyStationById: false
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), string.Format(BaseValidation.ObjectExistWithThisData, nameof(DutyStation)) }
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
                isExistDutyStation: true,
                isExistDutyStationById: false
            );
            DutyStation entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(DutyStation)) }
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
        public async Task InsertValidationAsync_InvalidPlace_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistDutyStation: false,
                isExistDutyStationById: false
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation.Place), string.Format(BaseValidation.FieldInvalidLength, nameof(DutyStation.Place), 3, 512) }
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
        public async Task InsertValidationAsync_PlaceIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistDutyStation: false,
                isExistDutyStationById: false
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation.Place), string.Format(BaseValidation.FieldNotCanBeNull, nameof(DutyStation.Place)) }
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
                isExistDutyStation: false,
                isExistDutyStationById: true
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: "Test Place"
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
        public async Task UpdateValidationAsync_ExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistDutyStation: false,
                isExistDutyStationById: false
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), string.Format(BaseValidation.ObjectNotExistById, nameof(DutyStation), entity.Id) }
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
        public async Task UpdateValidationAsync_Exist_UnSucces()
        {
            // Arrange
            MockData(
                isExistDutyStation: true,
                isExistDutyStationById: true
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), string.Format(BaseValidation.ObjectExistWithThisData, nameof(DutyStation)) }
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
                isExistDutyStation: true,
                isExistDutyStationById: true
            );
            DutyStation entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(DutyStation)) }
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
        public async Task UpdateValidationAsync_InvalidPlace_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistDutyStation: false,
                isExistDutyStationById: true
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation.Place), string.Format(BaseValidation.FieldInvalidLength, nameof(DutyStation.Place), 3, 512) }
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
        public async Task UpdateValidationAsync_PlaceIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistDutyStation: false,
                isExistDutyStationById: true
            );
            var entity = GetDutyStation(
                id: Guid.NewGuid(),
                place: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation.Place), string.Format(BaseValidation.FieldNotCanBeNull, nameof(DutyStation.Place)) }
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
                isExistDutyStation: true,
                isExistDutyStationById: true
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
                isExistDutyStation: true,
                isExistDutyStationById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(DutyStation), string.Format(BaseValidation.ObjectNotExistById, nameof(DutyStation), id) }
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
            bool isExistDutyStation = default,
            bool isExistDutyStationById = default
            )
        {
            _serviceDutyStation.Setup(x => x.ExistAsync(It.IsAny<DutyStation>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistDutyStation);
            _serviceDutyStation.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistDutyStationById);

            _validation = new DutyStationValidation(_serviceDutyStation.Object);
        }

        private DutyStation GetDutyStation(
            Guid id = default,
            string place = default
            )
        {
            return new DutyStation()
            {
                Id = id,
                Place = place
            };
        }

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
