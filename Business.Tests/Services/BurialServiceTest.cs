using Moq;
using NUnit.Framework;
using PsuHistory.Business.Service.Helpers;
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
    class BurialServiceTest
    {
        private IBaseService<Guid, Burial> _service;
        private Mock<FileHelper> _fileHelper;
        private Mock<IBaseRepository<Guid, Burial>> _dataBurial;
        private Mock<IBaseRepository<Guid, AttachmentBurial>> _dataAttachmentBurial;
        private Mock<IBaseValidation<Guid, Burial>> _burialValidation;

        [SetUp]
        public void Setup()
        {
            _fileHelper = new Mock<FileHelper>(null);
            _dataBurial = new Mock<IBaseRepository<Guid, Burial>>();
            _dataAttachmentBurial = new Mock<IBaseRepository<Guid, AttachmentBurial>>();
            _burialValidation = new Mock<IBaseValidation<Guid, Burial>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var burial = GetBurial(
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
            var validationModel = GetValidationModel<Burial>(
                errors: null
            );
            MockData(
                burial: burial,
                validationBurial: validationModel
            );

            // Act
            var result = await _service.GetAsync(burial.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(burial.Id, result.Result.Id);
                Assert.AreEqual(burial.NumberBurial, result.Result.NumberBurial);
                Assert.AreEqual(burial.Location, result.Result.Location);
                Assert.AreEqual(burial.KnownNumber, result.Result.KnownNumber);
                Assert.AreEqual(burial.UnknownNumber, result.Result.UnknownNumber);
                Assert.AreEqual(burial.Year, result.Result.Year);
                Assert.AreEqual(burial.Latitude, result.Result.Latitude);
                Assert.AreEqual(burial.Longitude, result.Result.Longitude);
                Assert.AreEqual(burial.Description, result.Result.Description);
                Assert.AreEqual(burial.TypeBurialId, result.Result.TypeBurialId);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var burial = GetBurial();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<Burial>(
                errors: errorList
            );
            MockData(
                burial: burial,
                validationBurial: validationModel
            );

            // Act
            var result = await _service.GetAsync(burial.Id);

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
            var burial = GetBurial();
            var burialList = new List<Burial>() {
                GetBurial(id: Guid.NewGuid(),
                numberBurial: 1234,
                location: "Test location",
                knownNumber: 10,
                unknownNumber: 10,
                year: 1950,
                latitude: 20.20,
                longitude: 20.20,
                description: "Test description",
                typeBurialId: Guid.NewGuid())
                };

            var validationModel = GetValidationModel<Burial>(
                errors: null
            );
            MockData(
                burial: burial,
                burialList: burialList,
                validationBurial: validationModel
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
            var burial = GetBurial();
            var validationModel = GetValidationModel<Burial>(
                errors: null
            );
            MockData(
                burial: burial,
                validationBurial: validationModel
            );

            // Act
            var result = await _service.InsertAsync(burial);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(burial.Id, result.Result.Id);
                Assert.AreEqual(burial.NumberBurial, result.Result.NumberBurial);
                Assert.AreEqual(burial.Location, result.Result.Location);
                Assert.AreEqual(burial.KnownNumber, result.Result.KnownNumber);
                Assert.AreEqual(burial.UnknownNumber, result.Result.UnknownNumber);
                Assert.AreEqual(burial.Year, result.Result.Year);
                Assert.AreEqual(burial.Latitude, result.Result.Latitude);
                Assert.AreEqual(burial.Longitude, result.Result.Longitude);
                Assert.AreEqual(burial.Description, result.Result.Description);
                Assert.AreEqual(burial.TypeBurialId, result.Result.TypeBurialId);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var burial = GetBurial(
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
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<Burial>(
                errors: errorList
            );
            MockData(
                burial: burial,
                validationBurial: validationModel
            );

            // Act
            var result = await _service.InsertAsync(burial);

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
            var burial = GetBurial();
            var validationModel = GetValidationModel<Burial>(
                errors: null
            );
            MockData(
                burial: burial,
                validationBurial: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(burial);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(burial.Id, result.Result.Id);
                Assert.AreEqual(burial.NumberBurial, result.Result.NumberBurial);
                Assert.AreEqual(burial.Location, result.Result.Location);
                Assert.AreEqual(burial.KnownNumber, result.Result.KnownNumber);
                Assert.AreEqual(burial.UnknownNumber, result.Result.UnknownNumber);
                Assert.AreEqual(burial.Year, result.Result.Year);
                Assert.AreEqual(burial.Latitude, result.Result.Latitude);
                Assert.AreEqual(burial.Longitude, result.Result.Longitude);
                Assert.AreEqual(burial.Description, result.Result.Description);
                Assert.AreEqual(burial.TypeBurialId, result.Result.TypeBurialId);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var burial = GetBurial(
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
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<Burial>(
                errors: errorList
            );
            MockData(
                burial: burial,
                validationBurial: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(burial);

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
            var burial = GetBurial(
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
            var validationModel = GetValidationModel<Burial>(
                errors: null
            );
            MockData(
                burial: burial,
                validationBurial: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(burial.Id);

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
            var burial = GetBurial();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<Burial>(
                errors: errorList
            );
            MockData(
                burial: burial,
                validationBurial: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(burial.Id);

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
            Burial burial = default,
            AttachmentBurial attachmentBurial = default,
            IEnumerable<Burial> burialList = default,
            IEnumerable<AttachmentBurial> attachmentBurialList = default,
            ValidationModel<Burial> validationBurial = default
            )
        {
            _dataBurial.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(burial);
            _dataBurial.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(burialList);
            _dataBurial.Setup(x => x.InsertAsync(It.IsAny<Burial>(), It.IsAny<CancellationToken>())).ReturnsAsync(burial);
            _dataBurial.Setup(x => x.UpdateAsync(It.IsAny<Burial>(), It.IsAny<CancellationToken>())).ReturnsAsync(burial);
            _dataBurial.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _dataAttachmentBurial.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurial);
            _dataAttachmentBurial.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurialList);
            _dataAttachmentBurial.Setup(x => x.InsertAsync(It.IsAny<AttachmentBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurial);
            _dataAttachmentBurial.Setup(x => x.UpdateAsync(It.IsAny<AttachmentBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurial);
            _dataAttachmentBurial.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _burialValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationBurial);
            _burialValidation.Setup(x => x.InsertValidationAsync(It.IsAny<Burial>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationBurial);
            _burialValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<Burial>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationBurial);
            _burialValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationBurial);

            _service = new BurialService(_fileHelper.Object, _dataBurial.Object, _dataAttachmentBurial.Object, _burialValidation.Object);
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
