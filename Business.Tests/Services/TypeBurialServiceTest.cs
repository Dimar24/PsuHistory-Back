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
    [TestFixture]
    class TypeBurialServiceTest
    {
        private IBaseService<Guid, TypeBurial> _service;
        private Mock<IBaseRepository<Guid, TypeBurial>> _dataTypeBurial;
        private Mock<IBaseValidation<Guid, TypeBurial>> _typeBurialValidation;

        [SetUp]
        public void Setup()
        {
            _dataTypeBurial = new Mock<IBaseRepository<Guid, TypeBurial>>();
            _typeBurialValidation = new Mock<IBaseValidation<Guid, TypeBurial>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var typeBurial = GetTypeBurial(
                id: Guid.NewGuid(),
                name: "test data"
            );
            var validationModel = GetValidationModel<TypeBurial>(
                errors: null
            );
            MockData(
                typeBurial: typeBurial,
                validationTypeBurial: validationModel
            );

            // Act
            var result = await _service.GetAsync(typeBurial.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(typeBurial.Id, result.Result.Id);
                Assert.AreEqual(typeBurial.Name ,result.Result.Name);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var typeBurial = GetTypeBurial();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<TypeBurial>(
                errors: errorList
            );
            MockData(
                typeBurial: typeBurial,
                validationTypeBurial: validationModel
            );

            // Act
            var result = await _service.GetAsync(typeBurial.Id);

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
            var typeBurial = GetTypeBurial();
            var typeBurialList = new List<TypeBurial>() {
                GetTypeBurial(id: Guid.NewGuid(), name: "test data" )
            };
            var validationModel = GetValidationModel<TypeBurial>(
                errors: null
            );
            MockData(
                typeBurial: typeBurial,
                typeBurialList: typeBurialList,
                validationTypeBurial: validationModel
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
            var typeBurial = GetTypeBurial();
            var validationModel = GetValidationModel<TypeBurial>(
                errors: null
            );
            MockData(
                typeBurial: typeBurial,
                validationTypeBurial: validationModel
            );

            // Act
            var result = await _service.InsertAsync(typeBurial);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(typeBurial.Id, result.Result.Id);
                Assert.AreEqual(typeBurial.Name, result.Result.Name);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var typeBurial = GetTypeBurial(
                id: Guid.NewGuid(),
                name: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<TypeBurial>(
                errors: errorList
            );
            MockData(
                typeBurial: typeBurial,
                validationTypeBurial: validationModel
            );

            // Act
            var result = await _service.InsertAsync(typeBurial);

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
            var typeBurial = GetTypeBurial();
            var validationModel = GetValidationModel<TypeBurial>(
                errors: null
            );
            MockData(
                typeBurial: typeBurial,
                validationTypeBurial: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(typeBurial);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(typeBurial.Id, result.Result.Id);
                Assert.AreEqual(typeBurial.Name, result.Result.Name);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var typeBurial = GetTypeBurial(
                id: Guid.NewGuid(),
                name: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<TypeBurial>(
                errors: errorList
            );
            MockData(
                typeBurial: typeBurial,
                validationTypeBurial: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(typeBurial);

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
            var typeBurial = GetTypeBurial(
                id: Guid.NewGuid(),
                name: "test data"
            );
            var validationModel = GetValidationModel<TypeBurial>(
                errors: null
            );
            MockData(
                typeBurial: typeBurial,
                validationTypeBurial: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(typeBurial.Id);

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
            var typeBurial = GetTypeBurial();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<TypeBurial>(
                errors: errorList
            );
            MockData(
                typeBurial: typeBurial,
                validationTypeBurial: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(typeBurial.Id);

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
            TypeBurial typeBurial = default,
            IEnumerable<TypeBurial> typeBurialList = default,
            ValidationModel<TypeBurial> validationTypeBurial = default
            )
        {
            if (typeBurial is null)
            {
                throw new ArgumentNullException(nameof(typeBurial));
            }

            _dataTypeBurial.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(typeBurial);
            _dataTypeBurial.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(typeBurialList);
            _dataTypeBurial.Setup(x => x.InsertAsync(It.IsAny<TypeBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(typeBurial);
            _dataTypeBurial.Setup(x => x.UpdateAsync(It.IsAny<TypeBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(typeBurial);
            _dataTypeBurial.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _typeBurialValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationTypeBurial);
            _typeBurialValidation.Setup(x => x.InsertValidationAsync(It.IsAny<TypeBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationTypeBurial);
            _typeBurialValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<TypeBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationTypeBurial);
            _typeBurialValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationTypeBurial);

            _service = new TypeBurialService(_dataTypeBurial.Object, _typeBurialValidation.Object);
        }

        private TypeBurial GetTypeBurial(
            Guid id = default,
            string name = default
            )
        {
            return new TypeBurial()
            {
                Id = id,
                Name = name
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
