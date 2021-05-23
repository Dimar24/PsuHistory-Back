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
    [TestFixture]
    class ConscriptionPlaceValidationTest
    {
        private Mock<IBaseService<Guid, ConscriptionPlace>> _serviceConscriptionPlace;
        private IBaseValidation<Guid, ConscriptionPlace> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceConscriptionPlace = new Mock<IBaseService<Guid, ConscriptionPlace>>();
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
                isExistConscriptionPlace: true,
                isExistConscriptionPlaceById: true
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
                isExistConscriptionPlace: true,
                isExistConscriptionPlaceById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace), string.Format(BaseValidation.ObjectNotExistById, nameof(ConscriptionPlace), id) }
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
                isExistConscriptionPlace: false,
                isExistConscriptionPlaceById: false
            );
            var entity = GetConscriptionPlace(
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
                isExistConscriptionPlace: true,
                isExistConscriptionPlaceById: false
            );
            var entity = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace), string.Format(BaseValidation.ObjectExistWithThisData, nameof(ConscriptionPlace)) }
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
                isExistConscriptionPlace: true,
                isExistConscriptionPlaceById: false
            );
            ConscriptionPlace entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(ConscriptionPlace)) }
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
                isExistConscriptionPlace: false,
                isExistConscriptionPlaceById: false
            );
            var entity = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace.Place), string.Format(BaseValidation.FieldInvalidLength, nameof(ConscriptionPlace.Place), 3, 512) }
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
                isExistConscriptionPlace: false,
                isExistConscriptionPlaceById: false
            );
            var entity = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace.Place), string.Format(BaseValidation.FieldNotCanBeNull, nameof(ConscriptionPlace.Place)) }
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
                isExistConscriptionPlace: false,
                isExistConscriptionPlaceById: true
            );
            var entity = GetConscriptionPlace(
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
                isExistConscriptionPlace: false,
                isExistConscriptionPlaceById: false
            );
            var entity = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace), string.Format(BaseValidation.ObjectNotExistById, nameof(ConscriptionPlace), entity.Id) }
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
                isExistConscriptionPlace: true,
                isExistConscriptionPlaceById: true
            );
            var entity = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: "Test Place"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace), string.Format(BaseValidation.ObjectExistWithThisData, nameof(ConscriptionPlace)) }
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
                isExistConscriptionPlace: true,
                isExistConscriptionPlaceById: true
            );
            ConscriptionPlace entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(ConscriptionPlace)) }
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
                isExistConscriptionPlace: false,
                isExistConscriptionPlaceById: true
            );
            var entity = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace.Place), string.Format(BaseValidation.FieldInvalidLength, nameof(ConscriptionPlace.Place), 3, 512) }
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
                isExistConscriptionPlace: false,
                isExistConscriptionPlaceById: true
            );
            var entity = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace.Place), string.Format(BaseValidation.FieldNotCanBeNull, nameof(ConscriptionPlace.Place)) }
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
                isExistConscriptionPlace: true,
                isExistConscriptionPlaceById: true
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
                isExistConscriptionPlace: true,
                isExistConscriptionPlaceById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(ConscriptionPlace), string.Format(BaseValidation.ObjectNotExistById, nameof(ConscriptionPlace), id) }
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
            bool isExistConscriptionPlace = default,
            bool isExistConscriptionPlaceById = default
            )
        {
            _serviceConscriptionPlace.Setup(x => x.ExistAsync(It.IsAny<ConscriptionPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistConscriptionPlace);
            _serviceConscriptionPlace.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistConscriptionPlaceById);

            _validation = new ConscriptionPlaceValidation(_serviceConscriptionPlace.Object);
        }

        private ConscriptionPlace GetConscriptionPlace(
            Guid id = default,
            string place = default
            )
        {
            return new ConscriptionPlace()
            {
                Id = id,
                Place = place
            };
        }

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
