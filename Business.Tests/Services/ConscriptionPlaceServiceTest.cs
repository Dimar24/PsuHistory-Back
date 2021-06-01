using Moq;
using NUnit.Framework;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Services;
using PsuHistory.Common.Models;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Services
{
    class ConscriptionPlaceServiceTest
    {
        private IBaseService<Guid, ConscriptionPlace> _service;
        private Mock<IBaseRepository<Guid, ConscriptionPlace>> _dataConscriptionPlace;
        private Mock<IBaseValidation<Guid, ConscriptionPlace>> _conscriptionPlaceValidation;

        [SetUp]
        public void Setup()
        {
            _dataConscriptionPlace = new Mock<IBaseRepository<Guid, ConscriptionPlace>>();
            _conscriptionPlaceValidation = new Mock<IBaseValidation<Guid, ConscriptionPlace>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var conscriptionPlace = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: null
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                validationConscriptionPlace: validationModel
            );

            // Act
            var result = await _service.GetAsync(conscriptionPlace.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(conscriptionPlace.Id, result.Result.Id);
                Assert.AreEqual(conscriptionPlace.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var conscriptionPlace = GetConscriptionPlace();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: errorList
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                validationConscriptionPlace: validationModel
            );

            // Act
            var result = await _service.GetAsync(conscriptionPlace.Id);

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
            var conscriptionPlace = GetConscriptionPlace();
            var conscriptionPlaceList = new List<ConscriptionPlace>() {
                GetConscriptionPlace(id: Guid.NewGuid(),
                place: "test data" )
            };
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: null
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                conscriptionPlaceList: conscriptionPlaceList,
                validationConscriptionPlace: validationModel
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
            var conscriptionPlace = GetConscriptionPlace();
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: null
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                validationConscriptionPlace: validationModel
            );

            // Act
            var result = await _service.InsertAsync(conscriptionPlace);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(conscriptionPlace.Id, result.Result.Id);
                Assert.AreEqual(conscriptionPlace.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var conscriptionPlace = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: errorList
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                validationConscriptionPlace: validationModel
            );

            // Act
            var result = await _service.InsertAsync(conscriptionPlace);

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
            var conscriptionPlace = GetConscriptionPlace();
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: null
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                validationConscriptionPlace: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(conscriptionPlace);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(conscriptionPlace.Id, result.Result.Id);
                Assert.AreEqual(conscriptionPlace.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var conscriptionPlace = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: errorList
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                validationConscriptionPlace: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(conscriptionPlace);

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
            var conscriptionPlace = GetConscriptionPlace(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: null
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                validationConscriptionPlace: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(conscriptionPlace.Id);

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
            var conscriptionPlace = GetConscriptionPlace();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<ConscriptionPlace>(
                errors: errorList
            );
            MockData(
                conscriptionPlace: conscriptionPlace,
                validationConscriptionPlace: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(conscriptionPlace.Id);

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
            ConscriptionPlace conscriptionPlace = default,
            IEnumerable<ConscriptionPlace> conscriptionPlaceList = default,
            ValidationModel<ConscriptionPlace> validationConscriptionPlace = default
            )
        {
            _dataConscriptionPlace.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(conscriptionPlace);
            _dataConscriptionPlace.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(conscriptionPlaceList);
            _dataConscriptionPlace.Setup(x => x.InsertAsync(It.IsAny<ConscriptionPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(conscriptionPlace);
            _dataConscriptionPlace.Setup(x => x.UpdateAsync(It.IsAny<ConscriptionPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(conscriptionPlace);
            _dataConscriptionPlace.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _conscriptionPlaceValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationConscriptionPlace);
            _conscriptionPlaceValidation.Setup(x => x.InsertValidationAsync(It.IsAny<ConscriptionPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationConscriptionPlace);
            _conscriptionPlaceValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<ConscriptionPlace>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationConscriptionPlace);
            _conscriptionPlaceValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationConscriptionPlace);

            _service = new ConscriptionPlaceService(_dataConscriptionPlace.Object, _conscriptionPlaceValidation.Object);
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
