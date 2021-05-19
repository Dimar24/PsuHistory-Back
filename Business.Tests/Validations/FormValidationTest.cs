using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Service.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Validations
{
    class FormValidationTest
    {
        private Mock<IBaseService<Guid, Form>> _service;
        private IBaseValidation<Guid, Form> _validation;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IBaseService<Guid, Form>>();
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
                form: new Form()
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
            MockData();
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), BaseValidation.ObjectNotExistById }
            };

            // Act
            var result = await _validation.GetValidationAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
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
            MockData();
            var entity = new Form()
            {
                LastName = "Иванов",
                FirstName = "Иван",
                MiddleName = "Иванович"
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(result.Errors);
                Assert.IsTrue(result.IsValid);
            });
        }

        [TestCase(2, "FieldInvalidLength")]
        [TestCase(555, "FieldInvalidLength")]
        public async Task InsertValidationAsync_InvalidPlace_UnSucces(int length, string nameError)
        {
            // Arrange
            MockData();
            var entity = new Form()
            {
                LastName = GetString(length),
                FirstName = GetString(length),
                MiddleName = GetString(length)
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources(nameError), error.Value);
                }
            });
        }

        [Test]
        public async Task InsertValidationAsync_LastNameIsNull_UnSucces()
        {
            // Arrange
            MockData();
            var entity = new Form()
            {
                LastName = null
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources("FieldNotCanBeNull"), error.Value);
                }
            });
        }

        [Test]
        public async Task InsertValidationAsync_ExistAsync_UnSucces()
        {
            // Arrange
            MockData(
                isExist: true
                );
            var entity = new Form()
            {
                LastName = "Иванов",
                FirstName = "Иван",
                MiddleName = "Иванович"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), BaseValidation.ObjectExistWithThisData }
            };

            // Act
            var result = await _validation.InsertValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
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
            MockData();

            // Act
            var result = await _validation.InsertValidationAsync(null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources("ObjectNotCanBeNull"), error.Value);
                }
            });
        }

        [Test]
        public async Task UpdateValidationAsync_Succes()
        {
            // Arrange
            MockData(
                form: new Form()
                );
            var entity = new Form()
            {
                LastName = "Иванов",
                FirstName = "Иван",
                MiddleName = "Иванович"
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsEmpty(result.Errors);
                Assert.IsTrue(result.IsValid);
            });
        }

        [TestCase(2, "FieldInvalidLength")]
        [TestCase(555, "FieldInvalidLength")]
        public async Task UpdateValidationAsync_InvalidPlace_UnSucces(int length, string nameError)
        {
            // Arrange
            MockData(
                form: new Form()
                );
            var entity = new Form()
            {
                LastName = GetString(length),
                FirstName = GetString(length),
                MiddleName = GetString(length)
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources(nameError), error.Value);
                }
            });
        }

        [Test]
        public async Task UpdateValidationAsync_LastNameIsNull_UnSucces()
        {
            // Arrange
            MockData(
                form: new Form()
                );
            var entity = new Form()
            {
                LastName = null
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotEmpty(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources("FieldNotCanBeNull"), error.Value);
                }
            });
        }

        [Test]
        public async Task UpdateValidationAsync_ExistAsync_UnSucces()
        {
            // Arrange
            MockData(
                form: new Form(),
                isExist: true
                );
            var entity = new Form()
            {
                LastName = "Иванов",
                FirstName = "Иван",
                MiddleName = "Иванович"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), BaseValidation.ObjectExistWithThisData }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
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
        public async Task UpdateValidationAsync_GetAsync_UnSucces()
        {
            // Arrange
            MockData(
                );
            var entity = new Form()
            {
                LastName = "Иванов",
                FirstName = "Иван",
                MiddleName = "Иванович"
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), BaseValidation.ObjectNotExistById }
            };

            // Act
            var result = await _validation.UpdateValidationAsync(entity);

            // Assert
            Assert.Multiple(() =>
            {
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
            MockData();

            // Act
            var result = await _validation.UpdateValidationAsync(null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.AreEqual(GetBaseValidationResources("ObjectNotCanBeNull"), error.Value);
                }
            });
        }

        [Test]
        public async Task DeleteValidationAsync_Succes()
        {
            // Arrange
            MockData(
                form: new Form()
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
            MockData();
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(Form), BaseValidation.ObjectNotExistById }
            };

            // Act
            var result = await _validation.DeleteValidationAsync(id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.NotNull(result.Errors);
                Assert.IsFalse(result.IsValid);
                foreach (var error in result.Errors)
                {
                    Assert.IsTrue(listError.ContainsKey(error.Key));
                    Assert.AreEqual(listError[error.Key], error.Value);
                }
            });
        }

        private void MockData(
            Form form = null,
            bool isExist = false
            )
        {
            _service.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(form);
            _service.Setup(x => x.ExistAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExist);

            _validation = new FormValidation(_service.Object);
        }

        private static string GetString(int length) => new Randomizer().GetString(length);

        private string GetBaseValidationResources(string name)
        {
            switch (name)
            {
                case "ObjectExistWithThisData": return BaseValidation.ObjectExistWithThisData;
                case "FieldNotCanBeNull": return BaseValidation.FieldNotCanBeNull;
                case "FieldInvalidLength": return BaseValidation.FieldInvalidLength;
                case "ObjectNotExistById": return BaseValidation.ObjectNotExistById;
                case "ObjectNotCanBeNull": return BaseValidation.ObjectNotCanBeNull;
            }
            return null;
        }
    }
}
