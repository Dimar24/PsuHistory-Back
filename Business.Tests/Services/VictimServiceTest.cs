//using Moq;
//using NUnit.Framework;
//using PsuHistory.Business.Service.Interfaces;
//using PsuHistory.Business.Service.Services;
//using PsuHistory.Data.Domain.Models.Monuments;
//using PsuHistory.Data.Repository.Interfaces;
//using PsuHistory.Models;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Business.Tests.Services
//{
//    class VictimServiceTest
//    {
//        private IBaseService<Guid, Victim> _service;
//        private Mock<IBaseRepository<Guid, Victim>> _dataVictim;
//        private Mock<IBaseValidation<Guid, Victim>> _victimValidation;

//        [SetUp]
//        public void Setup()
//        {
//            _dataVictim = new Mock<IBaseRepository<Guid, Victim>>();
//            _victimValidation = new Mock<IBaseValidation<Guid, Victim>>();
//        }

//        [TearDown]
//        public void Teardown()
//        { }

//        [Test]
//        public async Task GetAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetVictim(
//                id: Guid.NewGuid(),
//                lastName: "test data",
//                firstName: "test data",
//                middleName: "test data",
//                isHeroSoviet: true,
//                isPartisan: true,
//                dateOfBirth: "test data",
//                dateOfDeath: "test data",
//                typeVictimId: Guid.NewGuid(),
//                dutyStationId: Guid.NewGuid(),
//                birthPlaceId: Guid.NewGuid(),
//                conscriptionPlaceId: Guid.NewGuid(),
//                burialId: Guid.NewGuid()
//            );
//            var validationModel = GetValidationModel<Victim>(
//                errors: null
//            );
//            MockData(
//                victim: victim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.GetAsync(victim.Id);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.NotNull(result.Result);
//                Assert.AreEqual(victim.Id, result.Result.Id);
//                Assert.AreEqual(victim.LastName, result.Result.LastName);
//                Assert.AreEqual(victim.FirstName, result.Result.FirstName);
//                Assert.AreEqual(victim.MiddleName, result.Result.MiddleName);
//                Assert.AreEqual(victim.IsHeroSoviet, result.Result.IsHeroSoviet);
//                Assert.AreEqual(victim.IsPartisan, result.Result.IsPartisan);
//                Assert.AreEqual(victim.DateOfBirth, result.Result.DateOfBirth);
//                Assert.AreEqual(victim.DateOfDeath, result.Result.DateOfDeath);
//                Assert.AreEqual(victim.TypeVictimId, result.Result.TypeVictimId);
//                Assert.AreEqual(victim.DutyStationId, result.Result.DutyStationId);
//                Assert.AreEqual(victim.BirthPlaceId, result.Result.BirthPlaceId);
//                Assert.AreEqual(victim.ConscriptionPlaceId, result.Result.ConscriptionPlaceId);
//                Assert.AreEqual(victim.BurialId, result.Result.BurialId);
//            });
//        }

//        [Test]
//        public async Task GetAsync_UnSucces()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var errorList = new Dictionary<string, string>() {
//                { "TestKey", "TestValue" }
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: errorList
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.GetAsync(typeVictim.Id);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsFalse(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNotNull(result.Errors);
//                Assert.IsNotEmpty(result.Errors);
//            });
//        }

//        [Test]
//        public async Task GetAllAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var typeVictimList = new List<TypeVictim>() {
//                GetTypeVictim(id: Guid.NewGuid(), name: "test data" )
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                typeVictimList: typeVictimList,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.GetAllAsync();

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.IsNotNull(result.Result);
//                Assert.IsNotEmpty(result.Result);
//            });
//        }

//        [Test]
//        public async Task InsertAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.InsertAsync(typeVictim);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.NotNull(result.Result);
//                Assert.AreEqual(typeVictim.Id, result.Result.Id);
//                Assert.AreEqual(typeVictim.Name, result.Result.Name);
//            });
//        }

//        [Test]
//        public async Task InsertAsync_UnSucces()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim(
//                id: Guid.NewGuid(),
//                name: "test data"
//            );
//            var errorList = new Dictionary<string, string>() {
//                { "TestKey", "TestValue" }
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: errorList
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.InsertAsync(typeVictim);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsFalse(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNotNull(result.Errors);
//                Assert.IsNotEmpty(result.Errors);
//            });
//        }

//        [Test]
//        public async Task UpdateAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.UpdateAsync(typeVictim);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.NotNull(result.Result);
//                Assert.IsNull(result.Errors);
//                Assert.AreEqual(typeVictim.Id, result.Result.Id);
//                Assert.AreEqual(typeVictim.Name, result.Result.Name);
//            });
//        }

//        [Test]
//        public async Task UpdateAsync_UnSucces()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim(
//                id: Guid.NewGuid(),
//                name: "test data"
//            );
//            var errorList = new Dictionary<string, string>() {
//                { "TestKey", "TestValue" }
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: errorList
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.UpdateAsync(typeVictim);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsFalse(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNotNull(result.Errors);
//                Assert.IsNotEmpty(result.Errors);
//            });
//        }

//        [Test]
//        public async Task DeleteAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim(
//                id: Guid.NewGuid(),
//                name: "test data"
//            );
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.DeleteAsync(typeVictim.Id);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNull(result.Errors);
//            });
//        }

//        [Test]
//        public async Task DeleteAsync_UnSucces()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var errorList = new Dictionary<string, string>() {
//                { "TestKey", "TestValue" }
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: errorList
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.DeleteAsync(typeVictim.Id);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsFalse(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNotNull(result.Errors);
//                Assert.IsNotEmpty(result.Errors);
//            });
//        }

//        private void MockData(
//            Victim victim = default,
//            IEnumerable<Victim> victimList = default,
//            ValidationModel<Victim> validationVictim = default
//            )
//        {
//            if (victim is null)
//            {
//                throw new ArgumentNullException(nameof(victim));
//            }

//            _dataVictim.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(victim);
//            _dataVictim.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(victimList);
//            _dataVictim.Setup(x => x.InsertAsync(It.IsAny<Victim>(), It.IsAny<CancellationToken>())).ReturnsAsync(victim);
//            _dataVictim.Setup(x => x.UpdateAsync(It.IsAny<Victim>(), It.IsAny<CancellationToken>())).ReturnsAsync(victim);
//            _dataVictim.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

//            _victimValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationVictim);
//            _victimValidation.Setup(x => x.InsertValidationAsync(It.IsAny<Victim>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationVictim);
//            _victimValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<Victim>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationVictim);
//            _victimValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationVictim);

//            _service = new VictimService(_dataVictim.Object, _victimValidation.Object);
//        }

//        private Victim GetVictim(
//            Guid id = default,
//            string lastName = default,
//            string firstName = default,
//            string middleName = default,
//            bool isHeroSoviet = default,
//            bool isPartisan = default,
//            string dateOfBirth = default,
//            string dateOfDeath = default,
//            Guid typeVictimId = default,
//            Guid dutyStationId = default,
//            Guid birthPlaceId = default,
//            Guid conscriptionPlaceId = default,
//            Guid burialId = default
//            )
//        {
//            return new Victim()
//            {
//                Id = id,
//                LastName = lastName,
//                FirstName = firstName,
//                MiddleName = middleName,
//                IsHeroSoviet = isHeroSoviet,
//                IsPartisan = isPartisan,
//                DateOfBirth = dateOfBirth,
//                DateOfDeath = dateOfDeath,
//                TypeVictimId = typeVictimId,
//                DutyStationId = dutyStationId,
//                BirthPlaceId = birthPlaceId,
//                ConscriptionPlaceId = conscriptionPlaceId,
//                BurialId = burialId
//            };
//        }

//        private ValidationModel<TResult> GetValidationModel<TResult>(
//            Dictionary<string, string> errors = default
//            )
//        {
//            return new ValidationModel<TResult>()
//            {
//                Errors = errors
//            };
//        }
//    }
//}
