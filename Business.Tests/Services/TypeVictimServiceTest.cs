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
    class TypeVictimServiceTest
    {
        private IBaseService<Guid, TypeVictim> _service;
        private Mock<IBaseRepository<Guid, TypeVictim>> _dataTypeVictim;
        private Mock<IBaseValidation<Guid, TypeVictim>> _typeVictimValidation;

        [SetUp]
        public void Setup()
        {
            _dataTypeVictim = new Mock<IBaseRepository<Guid, TypeVictim>>();
            _typeVictimValidation = new Mock<IBaseValidation<Guid, TypeVictim>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var typeVictim = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "test data"
            );
            var validationModel = GetValidationModel<TypeVictim>(
                errors: null
            );
            MockData(
                typeVictim: typeVictim,
                validationTypeVictim: validationModel
            );

            // Act
            var result = await _service.GetAsync(typeVictim.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(typeVictim.Id, result.Result.Id);
                Assert.AreEqual(typeVictim.Name, result.Result.Name);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var typeVictim = GetTypeVictim();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<TypeVictim>(
                errors: errorList
            );
            MockData(
                typeVictim: typeVictim,
                validationTypeVictim: validationModel
            );

            // Act
            var result = await _service.GetAsync(typeVictim.Id);

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
            var typeVictim = GetTypeVictim();
            var typeVictimList = new List<TypeVictim>() {
                GetTypeVictim(id: Guid.NewGuid(), name: "test data" )
            };
            var validationModel = GetValidationModel<TypeVictim>(
                errors: null
            );
            MockData(
                typeVictim: typeVictim,
                typeVictimList: typeVictimList,
                validationTypeVictim: validationModel
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
            var typeVictim = GetTypeVictim();
            var validationModel = GetValidationModel<TypeVictim>(
                errors: null
            );
            MockData(
                typeVictim: typeVictim,
                validationTypeVictim: validationModel
            );

            // Act
            var result = await _service.InsertAsync(typeVictim);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(typeVictim.Id, result.Result.Id);
                Assert.AreEqual(typeVictim.Name, result.Result.Name);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var typeVictim = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<TypeVictim>(
                errors: errorList
            );
            MockData(
                typeVictim: typeVictim,
                validationTypeVictim: validationModel
            );

            // Act
            var result = await _service.InsertAsync(typeVictim);

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
            var typeVictim = GetTypeVictim();
            var validationModel = GetValidationModel<TypeVictim>(
                errors: null
            );
            MockData(
                typeVictim: typeVictim,
                validationTypeVictim: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(typeVictim);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(typeVictim.Id, result.Result.Id);
                Assert.AreEqual(typeVictim.Name, result.Result.Name);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var typeVictim = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<TypeVictim>(
                errors: errorList
            );
            MockData(
                typeVictim: typeVictim,
                validationTypeVictim: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(typeVictim);

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
            var typeVictim = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "test data"
            );
            var validationModel = GetValidationModel<TypeVictim>(
                errors: null
            );
            MockData(
                typeVictim: typeVictim,
                validationTypeVictim: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(typeVictim.Id);

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
            var typeVictim = GetTypeVictim();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<TypeVictim>(
                errors: errorList
            );
            MockData(
                typeVictim: typeVictim,
                validationTypeVictim: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(typeVictim.Id);

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
            TypeVictim typeVictim = default,
            IEnumerable<TypeVictim> typeVictimList = default,
            ValidationModel<TypeVictim> validationTypeVictim = default
            )
        {
            _dataTypeVictim.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(typeVictim);
            _dataTypeVictim.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(typeVictimList);
            _dataTypeVictim.Setup(x => x.InsertAsync(It.IsAny<TypeVictim>(), It.IsAny<CancellationToken>())).ReturnsAsync(typeVictim);
            _dataTypeVictim.Setup(x => x.UpdateAsync(It.IsAny<TypeVictim>(), It.IsAny<CancellationToken>())).ReturnsAsync(typeVictim);
            _dataTypeVictim.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _typeVictimValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationTypeVictim);
            _typeVictimValidation.Setup(x => x.InsertValidationAsync(It.IsAny<TypeVictim>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationTypeVictim);
            _typeVictimValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<TypeVictim>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationTypeVictim);
            _typeVictimValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationTypeVictim);

            _service = new TypeVictimService(_dataTypeVictim.Object, _typeVictimValidation.Object);
        }

        private TypeVictim GetTypeVictim(
            Guid id = default,
            string name = default
            )
        {
            return new TypeVictim()
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
