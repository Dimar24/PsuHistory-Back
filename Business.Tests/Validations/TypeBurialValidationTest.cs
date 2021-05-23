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
    class TypeBurialValidationTest
    {
        private Mock<IBaseRepository<Guid, TypeBurial>> _serviceTypeBurial;
        private IBaseValidation<Guid, TypeBurial> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceTypeBurial = new Mock<IBaseRepository<Guid, TypeBurial>>();
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
                isExistTypeBurial: true,
                isExistTypeBurialById: true
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
                isExistTypeBurial: true,
                isExistTypeBurialById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial), string.Format(BaseValidation.ObjectNotExistById, nameof(TypeBurial), id) }
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
                isExistTypeBurial: false,
                isExistTypeBurialById: false
            );
            var entity = GetTypeBurial(
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
                isExistTypeBurial: true,
                isExistTypeBurialById: false
            );
            var entity = GetTypeBurial(
                id: Guid.NewGuid(),
                name: "Test Name"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial), string.Format(BaseValidation.ObjectExistWithThisData, nameof(TypeBurial)) }
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
                isExistTypeBurial: true,
                isExistTypeBurialById: false
            );
            TypeBurial entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(TypeBurial)) }
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
                isExistTypeBurial: false,
                isExistTypeBurialById: false
            );
            var entity = GetTypeBurial(
                id: Guid.NewGuid(),
                name: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial.Name), string.Format(BaseValidation.FieldInvalidLength, nameof(TypeBurial.Name), 3, 128) }
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
                isExistTypeBurial: false,
                isExistTypeBurialById: false
            );
            var entity = GetTypeBurial(
                id: Guid.NewGuid(),
                name: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial.Name), string.Format(BaseValidation.FieldNotCanBeNull, nameof(TypeBurial.Name)) }
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
                isExistTypeBurial: false,
                isExistTypeBurialById: true
            );
            var entity = GetTypeBurial(
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
                isExistTypeBurial: false,
                isExistTypeBurialById: false
            );
            var entity = GetTypeBurial(
                id: Guid.NewGuid(),
                name: "Test Name"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial), string.Format(BaseValidation.ObjectNotExistById, nameof(TypeBurial), entity.Id) }
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
                isExistTypeBurial: true,
                isExistTypeBurialById: true
            );
            var entity = GetTypeBurial(
                id: Guid.NewGuid(),
                name: "Test Name"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial), string.Format(BaseValidation.ObjectExistWithThisData, nameof(TypeBurial)) }
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
                isExistTypeBurial: true,
                isExistTypeBurialById: true
            );
            TypeBurial entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(TypeBurial)) }
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
                isExistTypeBurial: false,
                isExistTypeBurialById: true
            );
            var entity = GetTypeBurial(
                id: Guid.NewGuid(),
                name: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial.Name), string.Format(BaseValidation.FieldInvalidLength, nameof(TypeBurial.Name), 3, 128) }
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
                isExistTypeBurial: false,
                isExistTypeBurialById: true
            );
            var entity = GetTypeBurial(
                id: Guid.NewGuid(),
                name: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial.Name), string.Format(BaseValidation.FieldNotCanBeNull, nameof(TypeBurial.Name)) }
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
                isExistTypeBurial: true,
                isExistTypeBurialById: true
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
                isExistTypeBurial: true,
                isExistTypeBurialById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(TypeBurial), string.Format(BaseValidation.ObjectNotExistById, nameof(TypeBurial), id) }
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
            bool isExistTypeBurial = default,
            bool isExistTypeBurialById = default
            )
        {
            _serviceTypeBurial.Setup(x => x.ExistAsync(It.IsAny<TypeBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistTypeBurial);
            _serviceTypeBurial.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistTypeBurialById);

            _validation = new TypeBurialValidation(_serviceTypeBurial.Object);
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

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
