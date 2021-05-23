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
        public async Task InsertValidationAsync_Null_UnSucces()
        {
            // Arrange
            MockData(
                isExistBurial: true,
                isExistBurialById: false
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
