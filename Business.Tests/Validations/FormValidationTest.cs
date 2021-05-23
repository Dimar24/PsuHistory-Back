using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Validations
{
    [TestFixture]
    class FormValidationTest
    {
        private Mock<IBaseRepository<Guid, Form>> _serviceForm;
        private IBaseValidation<Guid, Form> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceForm = new Mock<IBaseRepository<Guid, Form>>();
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
                isExistForm: true,
                isExistFormById: true
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
                isExistForm: true,
                isExistFormById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), string.Format(BaseValidation.ObjectNotExistById, nameof(Form), id) }
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
                isExistForm: false,
                isExistFormById: false
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName"
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
                isExistForm: true,
                isExistFormById: false
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), string.Format(BaseValidation.ObjectExistWithThisData, nameof(Form)) }
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
                isExistForm: true,
                isExistFormById: false
            );
            Form entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Form)) }
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
        public async Task InsertValidationAsync_InvalidLastName_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistForm: false,
                isExistFormById: false
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form.LastName), string.Format(BaseValidation.FieldInvalidLength, nameof(Form.LastName), 3, 128) }
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
                isExistForm: false,
                isExistFormById: false
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form.LastName), string.Format(BaseValidation.FieldNotCanBeNull, nameof(Form.LastName)) }
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

        [TestCase(135)]
        public async Task InsertValidationAsync_InvalidFirstName_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistForm: false,
                isExistFormById: false
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName",
                firstName: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form.FirstName), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Form.FirstName), 128) }
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

        [TestCase(135)]
        public async Task InsertValidationAsync_InvalidMiddleName_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistForm: false,
                isExistFormById: false
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName",
                middleName: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form.MiddleName), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Form.MiddleName), 128) }
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
                isExistForm: false,
                isExistFormById: true
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName"
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
                isExistForm: false,
                isExistFormById: false
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), string.Format(BaseValidation.ObjectNotExistById, nameof(Form), entity.Id) }
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
                isExistForm: true,
                isExistFormById: true
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName"
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), string.Format(BaseValidation.ObjectExistWithThisData, nameof(Form)) }
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
                isExistForm: true,
                isExistFormById: true
            );
            Form entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(Form)) }
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
        public async Task UpdateValidationAsync_InvalidLastName_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistForm: false,
                isExistFormById: true
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form.LastName), string.Format(BaseValidation.FieldInvalidLength, nameof(Form.LastName), 3, 128) }
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
                isExistForm: false,
                isExistFormById: true
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: null
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form.LastName), string.Format(BaseValidation.FieldNotCanBeNull, nameof(Form.LastName)) }
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

        [TestCase(135)]
        public async Task UpdateValidationAsync_InvalidFirstName_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistForm: false,
                isExistFormById: true
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName",
                firstName: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form.FirstName), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Form.FirstName), 128) }
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

        [TestCase(135)]
        public async Task UpdateValidationAsync_InvalidMiddleName_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistForm: false,
                isExistFormById: true
            );
            var entity = GetForm(
                id: Guid.NewGuid(),
                lastName: "Test LastName",
                middleName: GetString(length)
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form.MiddleName), string.Format(BaseValidation.FieldInvalidMaxLength, nameof(Form.MiddleName), 128) }
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
                isExistForm: true,
                isExistFormById: true
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
                isExistForm: true,
                isExistFormById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), string.Format(BaseValidation.ObjectNotExistById, nameof(Form), id) }
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
            bool isExistForm = default,
            bool isExistFormById = default
            )
        {
            _serviceForm.Setup(x => x.ExistAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistForm);
            _serviceForm.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistFormById);

            _validation = new FormValidation(_serviceForm.Object);
        }

        private Form GetForm(
            Guid id = default,
            string lastName = default,
            string firstName = default,
            string middleName = default
            )
        {
            return new Form()
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
