using Moq;
using NUnit.Framework;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Services;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Services
{
    class BirthPlaceServiceTest
    {
        private IBaseService<Guid, BirthPlace> _service;
        private Mock<IBaseRepository<Guid, BirthPlace>> _dataBirthPlace;
        private Mock<IBaseValidation<Guid, BirthPlace>> _birthPlaceValidation;

        [SetUp]
        public void Setup()
        {
            _dataBirthPlace = new Mock<IBaseRepository<Guid, BirthPlace>>();
            _birthPlaceValidation = new Mock<IBaseValidation<Guid, BirthPlace>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var birthPlace = GetBirthPlace(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var validationModel = GetValidationModel<BirthPlace>(
                errors: null
            );
            MockData(
                birthPlace: birthPlace,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.GetAsync(birthPlace.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(birthPlace.Id, result.Result.Id);
                Assert.AreEqual(birthPlace.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var birthPlace = GetBirthPlace();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<BirthPlace>(
                errors: errorList
            );
            MockData(
                birthPlace: birthPlace,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.GetAsync(birthPlace.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
            });
        }

        [Test]
        public async Task GetAllAsync_Succes()
        {
            // Arrange
            var birthPlace = GetBirthPlace();
            var birthPlaceList = new List<BirthPlace>() {
                GetBirthPlace(id: Guid.NewGuid(),
                place: "test data" )
            };
            var validationModel = GetValidationModel<BirthPlace>(
                errors: null
            );
            MockData(
                birthPlace: birthPlace,
                birthPlaceList: birthPlaceList,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.IsNotNull(result.Result);
                Assert.IsNotEmpty(result.Result);
            });
        }

        [Test]
        public async Task InsertAsync_Succes()
        {
            // Arrange
            var birthPlace = GetBirthPlace();
            var validationModel = GetValidationModel<BirthPlace>(
                errors: null
            );
            MockData(
                birthPlace: birthPlace,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.InsertAsync(birthPlace);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(birthPlace.Id, result.Result.Id);
                Assert.AreEqual(birthPlace.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var birthPlace = GetBirthPlace(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<BirthPlace>(
                errors: errorList
            );
            MockData(
                birthPlace: birthPlace,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.InsertAsync(birthPlace);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
            });
        }

        [Test]
        public async Task UpdateAsync_Succes()
        {
            // Arrange
            var birthPlace = GetBirthPlace();
            var validationModel = GetValidationModel<BirthPlace>(
                errors: null
            );
            MockData(
                birthPlace: birthPlace,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(birthPlace);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(birthPlace.Id, result.Result.Id);
                Assert.AreEqual(birthPlace.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var birthPlace = GetBirthPlace(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<BirthPlace>(
                errors: errorList
            );
            MockData(
                birthPlace: birthPlace,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(birthPlace);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
            });
        }

        [Test]
        public async Task DeleteAsync_Succes()
        {
            // Arrange
            var birthPlace = GetBirthPlace(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var validationModel = GetValidationModel<BirthPlace>(
                errors: null
            );
            MockData(
                birthPlace: birthPlace,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(birthPlace.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNull(result.Errors);
            });
        }

        [Test]
        public async Task DeleteAsync_UnSucces()
        {
            // Arrange
            var birthPlace = GetBirthPlace();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<BirthPlace>(
                errors: errorList
            );
            MockData(
                birthPlace: birthPlace,
                validationBirthPlace: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(birthPlace.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
            });
        }

        private void MockData(
            BirthPlace birthPlace = default,
            IEnumerable<BirthPlace> birthPlaceList = default,
            ValidationModel<BirthPlace> validationBirthPlace = default
            )
        {
            _dataBirthPlace.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(birthPlace);
            _dataBirthPlace.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(birthPlaceList);
            _dataBirthPlace.Setup(x => x.InsertAsync(It.IsAny<BirthPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(birthPlace);
            _dataBirthPlace.Setup(x => x.UpdateAsync(It.IsAny<BirthPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(birthPlace);
            _dataBirthPlace.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _birthPlaceValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationBirthPlace);
            _birthPlaceValidation.Setup(x => x.InsertValidationAsync(It.IsAny<BirthPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationBirthPlace);
            _birthPlaceValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<BirthPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationBirthPlace);
            _birthPlaceValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationBirthPlace);

            _service = new BirthPlaceService(_dataBirthPlace.Object, _birthPlaceValidation.Object);
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

        private ValidationModel<TResult> GetValidationModel<TResult>(
            Dictionary<string, string> errors = default
            )
        {
            return new ValidationModel<TResult>()
            {
                Errors = errors
            };
        }
    }
}
