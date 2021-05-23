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
    class BirthPlaceValidationTest
    {
        private Mock<IBaseRepository<Guid, BirthPlace>> _serviceBirthPlace;
        private IBaseValidation<Guid, BirthPlace> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceBirthPlace = new Mock<IBaseRepository<Guid, BirthPlace>>();
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
                isExistBirthPlace: true,
                isExistBirthPlaceById: true
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
                isExistBirthPlace: true,
                isExistBirthPlaceById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace), string.Format(BaseValidation.ObjectNotExistById, nameof(BirthPlace), id) }
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
                isExistBirthPlace: false,
                isExistBirthPlaceById: false
            );
            var entity = GetBirthPlace(
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
                isExistBirthPlace: true,
                isExistBirthPlaceById: false
            );
            var entity = GetBirthPlace(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace), string.Format(BaseValidation.ObjectExistWithThisData, nameof(BirthPlace)) }
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
                isExistBirthPlace: true,
                isExistBirthPlaceById: false
            );
            BirthPlace entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(BirthPlace)) }
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
                isExistBirthPlace: false,
                isExistBirthPlaceById: false
            );
            var entity = GetBirthPlace(
                id: Guid.NewGuid(),
                place: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace.Place), string.Format(BaseValidation.FieldInvalidLength, nameof(BirthPlace.Place), 3, 512) }
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
                isExistBirthPlace: false,
                isExistBirthPlaceById: false
            );
            var entity = GetBirthPlace(
                id: Guid.NewGuid(),
                place: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace.Place), string.Format(BaseValidation.FieldNotCanBeNull, nameof(BirthPlace.Place)) }
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
                isExistBirthPlace: false,
                isExistBirthPlaceById: true
            );
            var entity = GetBirthPlace(
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
                isExistBirthPlace: false,
                isExistBirthPlaceById: false
            );
            var entity = GetBirthPlace(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace), string.Format(BaseValidation.ObjectNotExistById, nameof(BirthPlace), entity.Id) }
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
                isExistBirthPlace: true,
                isExistBirthPlaceById: true
            );
            var entity = GetBirthPlace(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace), string.Format(BaseValidation.ObjectExistWithThisData, nameof(BirthPlace)) }
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
                isExistBirthPlace: true,
                isExistBirthPlaceById: true
            );
            BirthPlace entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(BirthPlace)) }
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
                isExistBirthPlace: false,
                isExistBirthPlaceById: true
            );
            var entity = GetBirthPlace(
                id: Guid.NewGuid(),
                place: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace.Place), string.Format(BaseValidation.FieldInvalidLength, nameof(BirthPlace.Place), 3, 512) }
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
                isExistBirthPlace: false,
                isExistBirthPlaceById: true
            );
            var entity = GetBirthPlace(
                id: Guid.NewGuid(),
                place: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace.Place), string.Format(BaseValidation.FieldNotCanBeNull, nameof(BirthPlace.Place)) }
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
                isExistBirthPlace: true,
                isExistBirthPlaceById: true
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
                isExistBirthPlace: true,
                isExistBirthPlaceById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(BirthPlace), string.Format(BaseValidation.ObjectNotExistById, nameof(BirthPlace), id) }
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
            bool isExistBirthPlace = default,
            bool isExistBirthPlaceById = default
            )
        {
            _serviceBirthPlace.Setup(x => x.ExistAsync(It.IsAny<BirthPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistBirthPlace);
            _serviceBirthPlace.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistBirthPlaceById);

            _validation = new BirthPlaceValidation(_serviceBirthPlace.Object);
        }

        private BirthPlace GetBirthPlace(
            Guid id = default,
            string place = default
            )
        {
            return new BirthPlace()
            {
                Id = id,
                Place = place
            };
        }

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
