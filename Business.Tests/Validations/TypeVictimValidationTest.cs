using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
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
    class TypeVictimValidationTest
    {
        private Mock<IBaseRepository<Guid, TypeVictim>> _serviceTypeVictim;
        private IBaseValidation<Guid, TypeVictim> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceTypeVictim = new Mock<IBaseRepository<Guid, TypeVictim>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetValidationAsync_Succes()
        {
            // Arrange
            MockData(
                isExistTypeVictim: true,
                isExistTypeVictimById: true
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
                isExistTypeVictim: true,
                isExistTypeVictimById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), string.Format(BaseValidation.ObjectNotExistById, nameof(TypeVictim), id) }
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
                isExistTypeVictim: false,
                isExistTypeVictimById: false
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "Test Name"
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
        public async Task InsertValidationAsync_Exist_UnSucces()
        {
            // Arrange
            MockData(
                isExistTypeVictim: true,
                isExistTypeVictimById: false
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "Test Name"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), string.Format(BaseValidation.ObjectExistWithThisData, nameof(TypeVictim)) }
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
                isExistTypeVictim: true,
                isExistTypeVictimById: false
            );
            TypeVictim entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(TypeVictim)) }
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
        [TestCase(135)]
        public async Task InsertValidationAsync_InvalidName_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistTypeVictim: false,
                isExistTypeVictimById: false
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim.Name), string.Format(BaseValidation.FieldInvalidLength, nameof(TypeVictim.Name), 3, 128) }
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
        public async Task InsertValidationAsync_NameIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistTypeVictim: false,
                isExistTypeVictimById: false
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim.Name), string.Format(BaseValidation.FieldNotCanBeNull, nameof(TypeVictim.Name)) }
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
                isExistTypeVictim: false,
                isExistTypeVictimById: true
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "Test Name"
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
        public async Task UpdateValidationAsync_ExistById_UnSucces()
        {
            // Arrange
            MockData(
                isExistTypeVictim: false,
                isExistTypeVictimById: false
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "Test Name"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), string.Format(BaseValidation.ObjectNotExistById, nameof(TypeVictim), entity.Id) }
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
        public async Task UpdateValidationAsync_Exist_UnSucces()
        {
            // Arrange
            MockData(
                isExistTypeVictim: true,
                isExistTypeVictimById: true
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: "Test Name"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), string.Format(BaseValidation.ObjectExistWithThisData, nameof(TypeVictim)) }
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
                isExistTypeVictim: true,
                isExistTypeVictimById: true
            );
            TypeVictim entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(TypeVictim)) }
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
        [TestCase(135)]
        public async Task UpdateValidationAsync_InvalidName_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistTypeVictim: false,
                isExistTypeVictimById: true
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim.Name), string.Format(BaseValidation.FieldInvalidLength, nameof(TypeVictim.Name), 3, 128) }
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
        public async Task UpdateValidationAsync_NameIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistTypeVictim: false,
                isExistTypeVictimById: true
            );
            var entity = GetTypeVictim(
                id: Guid.NewGuid(),
                name: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim.Name), string.Format(BaseValidation.FieldNotCanBeNull, nameof(TypeVictim.Name)) }
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
                isExistTypeVictim: true,
                isExistTypeVictimById: true
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
                isExistTypeVictim: true,
                isExistTypeVictimById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeVictim), string.Format(BaseValidation.ObjectNotExistById, nameof(TypeVictim), id) }
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
            bool isExistTypeVictim = false,
            bool isExistTypeVictimById = false
            )
        {
            _serviceTypeVictim.Setup(x => x.ExistAsync(It.IsAny<TypeVictim>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistTypeVictim);
            _serviceTypeVictim.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistTypeVictimById);

            _validation = new TypeVictimValidation(_serviceTypeVictim.Object);
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

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
