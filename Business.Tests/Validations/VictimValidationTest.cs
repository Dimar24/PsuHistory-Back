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
        public async Task InsertValidationAsync_Succes()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
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
        public async Task InsertValidationAsync_VictimExist_UnSucces()
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
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim), string.Format(BaseValidation.ObjectExistWithThisData, nameof(Victim)) }
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
        public async Task InsertValidationAsync_TypeVictimExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: false,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.TypeVictim), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.TypeVictim), entity.TypeVictimId) }
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
        public async Task InsertValidationAsync_DutyStationExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: false,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.DutyStation), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.DutyStation), entity.DutyStationId) }
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
        public async Task InsertValidationAsync_BirthPlaceExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: false,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.BirthPlace), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.BirthPlace), entity.BirthPlaceId) }
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
        public async Task InsertValidationAsync_ConscriptionPlaceExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: false,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.ConscriptionPlace),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.ConscriptionPlace), entity.ConscriptionPlaceId) }
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
        public async Task InsertValidationAsync_BurialExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: false
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.Burial), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.Burial), entity.BurialId) }
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
        public async Task InsertValidationAsync_LastNameIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: null,
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.LastName), string.Format(BaseValidation.FieldNotCanBeNull, nameof(Victim.LastName)) }
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
        public async Task InsertValidationAsync_LastNameInvalid_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: GetString(length),
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.LastName), string.Format(BaseValidation.FieldInvalidLength, nameof(Victim.LastName), 3, 128) }
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
        public async Task InsertValidationAsync_FirstNameInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: GetString(135),
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.FirstName), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.FirstName), 128) }
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
        public async Task InsertValidationAsync_MiddleNameInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: GetString(135),
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.MiddleName), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.MiddleName), 128) }
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
        public async Task InsertValidationAsync_DateOfBirthInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: GetString(135),
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.DateOfBirth), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.DateOfBirth), 64) }
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
        public async Task InsertValidationAsync_DateOfDeathInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "02.02.1944",
                dateOfDeath: GetString(135),
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.DateOfDeath), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.DateOfDeath), 64) }
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
        public async Task UpdateValidationAsync_Succes()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
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
        public async Task UpdateValidationAsync_VictimExist_UnSucces()
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
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim), string.Format(BaseValidation.ObjectExistWithThisData, nameof(Victim)) }
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
        public async Task UpdateValidationAsync_VictimExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: false,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim), entity.Id) }
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
        public async Task UpdateValidationAsync_TypeVictimExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: false,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.TypeVictim), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.TypeVictim), entity.TypeVictimId) }
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
        public async Task UpdateValidationAsync_DutyStationExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: false,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.DutyStation), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.DutyStation), entity.DutyStationId) }
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
        public async Task UpdateValidationAsync_BirthPlaceExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: false,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.BirthPlace), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.BirthPlace), entity.BirthPlaceId) }
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
        public async Task UpdateValidationAsync_ConscriptionPlaceExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: false,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.ConscriptionPlace),
                        string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.ConscriptionPlace), entity.ConscriptionPlaceId) }
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
        public async Task UpdateValidationAsync_BurialExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: false
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.Burial), string.Format(BaseValidation.ObjectNotExistById, nameof(Victim.Burial), entity.BurialId) }
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
        public async Task UpdateValidationAsync_LastNameIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: null,
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.LastName), string.Format(BaseValidation.FieldNotCanBeNull, nameof(Victim.LastName)) }
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
        public async Task UpdateValidationAsync_LastNameInvalid_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: GetString(length),
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.LastName), string.Format(BaseValidation.FieldInvalidLength, nameof(Victim.LastName), 3, 128) }
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
        public async Task UpdateValidationAsync_FirstNameInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: GetString(135),
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.FirstName), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.FirstName), 128) }
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
        public async Task UpdateValidationAsync_MiddleNameInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: GetString(135),
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "01.01.1921",
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.MiddleName), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.MiddleName), 128) }
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
        public async Task UpdateValidationAsync_DateOfBirthInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: GetString(135),
                dateOfDeath: "02.02.1944",
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.DateOfBirth), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.DateOfBirth), 64) }
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
        public async Task UpdateValidationAsync_DateOfDeathInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExistVictim: false,
                isExistVictimById: true,
                isExistTypeVictimById: true,
                isExistDutyStationById: true,
                isExistBirthPlaceById: true,
                isExistConscriptionPlaceById: true,
                isExistBurialById: true
            );
            var entity = GetVictim(
                id: Guid.NewGuid(),
                lastName: "Иванов",
                firstName: "Иван",
                middleName: "Иванович",
                isHeroSoviet: true,
                isPartisan: true,
                dateOfBirth: "02.02.1944",
                dateOfDeath: GetString(135),
                typeVictimId: Guid.NewGuid(),
                dutyStationId: Guid.NewGuid(),
                birthPlaceId: Guid.NewGuid(),
                conscriptionPlaceId: Guid.NewGuid(),
                burialId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Victim.DateOfDeath), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Victim.DateOfDeath), 64) }
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
            string middleName = default,
            bool isHeroSoviet = default,
            bool isPartisan = default,
            string dateOfBirth = default,
            string dateOfDeath = default,
            Guid typeVictimId = default,
            Guid dutyStationId = default,
            Guid birthPlaceId = default,
            Guid conscriptionPlaceId = default,
            Guid burialId = default
            )
        {
            return new Victim()
            {
                Id = id,
                LastName = lastName,
                FirstName = firstName,
                MiddleName = middleName,
                IsHeroSoviet = isHeroSoviet,
                IsPartisan = isPartisan,
                DateOfBirth = dateOfBirth,
                DateOfDeath = dateOfDeath,
                TypeVictimId = typeVictimId,
                DutyStationId = dutyStationId,
                BirthPlaceId = birthPlaceId,
                ConscriptionPlaceId = conscriptionPlaceId,
                BurialId = burialId
            };
        }

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
