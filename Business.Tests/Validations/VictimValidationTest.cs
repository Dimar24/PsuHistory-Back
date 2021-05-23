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
    class VictimValidationTest
    {
        private Mock<IBaseRepository<Guid, Victim>> _serviceVictim;
        private Mock<IBaseRepository<Guid, TypeVictim>> _serviceTypeVictim;
        private Mock<IBaseRepository<Guid, DutyStation>> _serviceDutyStation;
        private Mock<IBaseRepository<Guid, BirthPlace>> _serviceBirthPlace;
        private Mock<IBaseRepository<Guid, ConscriptionPlace>> _serviceConscriptionPlace;
        private Mock<IBaseRepository<Guid, Burial>> _serviceBurial;
        private IBaseValidation<Guid, Victim> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceVictim = new Mock<IBaseRepository<Guid, Victim>>();
            _serviceTypeVictim = new Mock<IBaseRepository<Guid, TypeVictim>>();
            _serviceDutyStation = new Mock<IBaseRepository<Guid, DutyStation>>();
            _serviceBirthPlace = new Mock<IBaseRepository<Guid, BirthPlace>>();
            _serviceConscriptionPlace = new Mock<IBaseRepository<Guid, ConscriptionPlace>>();
            _serviceBurial = new Mock<IBaseRepository<Guid, Burial>>();
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
                isExistVictim: true,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
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
                isExistVictim: true,
                isExistVictimById: false,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim), id) }
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
                isExistVictim: true,
                isExistVictimById: false,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            Victim entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Victim)) }
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
                isExistVictim: true,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            Victim entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Victim)) }
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
                isExistVictim: true,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
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
                isExistVictim: true,
                isExistVictimById: false,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim), id) }
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
            bool isExistVictim = default,
            bool isExistVictimById = default,
            bool isExistTypeVictimById = default,
            bool isExistDutyStationById = default,
            bool isExistBirthPlaceById = default,
            bool isExistConscriptionPlaceById = default,
            bool isExistBurialById = default
            )
        {
            _serviceVictim.Setup(x => x.ExistAsync(It.IsAny<Victim>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistVictim);
            _serviceVictim.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistVictimById);

            _serviceTypeVictim.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistTypeVictimById);
            _serviceDutyStation.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistDutyStationById);
            _serviceBirthPlace.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistBirthPlaceById);
            _serviceConscriptionPlace.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistConscriptionPlaceById);
            _serviceBurial.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistBurialById);

            _validation = new VictimValidation(
                _serviceVictim.Object, 
                _serviceTypeVictim.Object, 
                _serviceDutyStation.Object, 
                _serviceBirthPlace.Object, 
                _serviceConscriptionPlace.Object, 
                _serviceBurial.Object);
        }

        private Victim GetVictim(
            Guid id,
            string lastName = default,
            string firstName = default,
            string middleName = default
            )
        {
            return new Victim()
            {
                Id = id,
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
            };
        }

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
