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
    class DutyStationServiceTest
    {
        private IBaseService<Guid, DutyStation> _service;
        private Mock<IBaseRepository<Guid, DutyStation>> _dataDutyStation;
        private Mock<IBaseValidation<Guid, DutyStation>> _dutyStationValidation;

        [SetUp]
        public void Setup()
        {
            _dataDutyStation = new Mock<IBaseRepository<Guid, DutyStation>>();
            _dutyStationValidation = new Mock<IBaseValidation<Guid, DutyStation>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var dutyStation = GetDutyStation(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var validationModel = GetValidationModel<DutyStation>(
                errors: null
            );
            MockData(
                dutyStation: dutyStation,
                validationDutyStation: validationModel
            );

            // Act
            var result = await _service.GetAsync(dutyStation.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(dutyStation.Id, result.Result.Id);
                Assert.AreEqual(dutyStation.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var dutyStation = GetDutyStation();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<DutyStation>(
                errors: errorList
            );
            MockData(
                dutyStation: dutyStation,
                validationDutyStation: validationModel
            );

            // Act
            var result = await _service.GetAsync(dutyStation.Id);

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
            var dutyStation = GetDutyStation();
            var dutyStationList = new List<DutyStation>() {
                GetDutyStation(id: Guid.NewGuid(), 
                place: "test data" )
            };
            var validationModel = GetValidationModel<DutyStation>(
                errors: null
            );
            MockData(
                dutyStation: dutyStation,
                dutyStationList: dutyStationList,
                validationDutyStation: validationModel
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
            var dutyStation = GetDutyStation();
            var validationModel = GetValidationModel<DutyStation>(
                errors: null
            );
            MockData(
                dutyStation: dutyStation,
                validationDutyStation: validationModel
            );

            // Act
            var result = await _service.InsertAsync(dutyStation);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(dutyStation.Id, result.Result.Id);
                Assert.AreEqual(dutyStation.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var dutyStation = GetDutyStation(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<DutyStation>(
                errors: errorList
            );
            MockData(
                dutyStation: dutyStation,
                validationDutyStation: validationModel
            );

            // Act
            var result = await _service.InsertAsync(dutyStation);

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
            var dutyStation = GetDutyStation();
            var validationModel = GetValidationModel<DutyStation>(
                errors: null
            );
            MockData(
                dutyStation: dutyStation,
                validationDutyStation: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(dutyStation);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(dutyStation.Id, result.Result.Id);
                Assert.AreEqual(dutyStation.Place, result.Result.Place);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var dutyStation = GetDutyStation(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<DutyStation>(
                errors: errorList
            );
            MockData(
                dutyStation: dutyStation,
                validationDutyStation: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(dutyStation);

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
            var dutyStation = GetDutyStation(
                id: Guid.NewGuid(),
                place: "test data"
            );
            var validationModel = GetValidationModel<DutyStation>(
                errors: null
            );
            MockData(
                dutyStation: dutyStation,
                validationDutyStation: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(dutyStation.Id);

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
            var dutyStation = GetDutyStation();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<DutyStation>(
                errors: errorList
            );
            MockData(
                dutyStation: dutyStation,
                validationDutyStation: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(dutyStation.Id);

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
            DutyStation dutyStation = default,
            IEnumerable<DutyStation> dutyStationList = default,
            ValidationModel<DutyStation> validationDutyStation = default
            )
        {
            _dataDutyStation.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(dutyStation);
            _dataDutyStation.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dutyStationList);
            _dataDutyStation.Setup(x => x.InsertAsync(It.IsAny<DutyStation>(), It.IsAny<CancellationToken>())).ReturnsAsync(dutyStation);
            _dataDutyStation.Setup(x => x.UpdateAsync(It.IsAny<DutyStation>(), It.IsAny<CancellationToken>())).ReturnsAsync(dutyStation);
            _dataDutyStation.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _dutyStationValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationDutyStation);
            _dutyStationValidation.Setup(x => x.InsertValidationAsync(It.IsAny<DutyStation>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationDutyStation);
            _dutyStationValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<DutyStation>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationDutyStation);
            _dutyStationValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationDutyStation);

            _service = new DutyStationService(_dataDutyStation.Object, _dutyStationValidation.Object);
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
