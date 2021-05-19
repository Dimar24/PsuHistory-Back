using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Monuments;
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
    class AttachmentBurialValidationTest
    {
        private Mock<IBaseService<Guid, AttachmentBurial>> _serviceAttachmentBurial;
        private Mock<IBaseService<Guid, Burial>> _serviceBurial;
        private IBaseValidation<Guid, AttachmentBurial> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceAttachmentBurial = new Mock<IBaseService<Guid, AttachmentBurial>>();
            _serviceBurial = new Mock<IBaseService<Guid, Burial>>();
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
            await MockData(
                attachmentBurial: new AttachmentBurial()
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
            await MockData();
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(AttachmentBurial), BaseValidation.ObjectNotExistById }
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
            await MockData();
            var entity = new AttachmentBurial()
            {
                BurialId = Guid.NewGuid()
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
            await MockData();
            var entity = new AttachmentBurial()
            {
                BurialId = Guid.NewGuid()
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
        public async Task InsertValidationAsync_PlaceIsNull_UnSucces()
        {
            // Arrange
            await MockData();
            var entity = new AttachmentBurial()
            {
                File = null
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
            await MockData(
                isExist: true
                );
            var entity = new AttachmentBurial()
            {
                BurialId = Guid.NewGuid()
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(AttachmentBurial), BaseValidation.ObjectExistWithThisData }
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
            await MockData();

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
            await MockData(
                attachmentBurial: new AttachmentBurial()
                );
            var entity = new AttachmentBurial()
            {
                BurialId = Guid.NewGuid()
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
            await MockData(
                attachmentBurial: new AttachmentBurial()
                );
            var entity = new AttachmentBurial()
            {
                BurialId = Guid.NewGuid()
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
        public async Task UpdateValidationAsync_PlaceIsNull_UnSucces()
        {
            // Arrange
            await MockData(
                attachmentBurial: new AttachmentBurial()
                );
            var entity = new AttachmentBurial()
            {
                File = null
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
            await MockData(
                attachmentBurial: new AttachmentBurial(),
                isExist: true
                );
            var entity = new AttachmentBurial()
            {
                BurialId = Guid.NewGuid()
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(AttachmentBurial), BaseValidation.ObjectExistWithThisData }
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
            await MockData(
                );
            var entity = new AttachmentBurial()
            {
                BurialId = Guid.NewGuid()
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(AttachmentBurial), BaseValidation.ObjectNotExistById }
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
            await MockData();

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
            await MockData(
                attachmentBurial: new AttachmentBurial()
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
            await MockData();
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(AttachmentBurial), BaseValidation.ObjectNotExistById }
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

        private async Task MockData(
            AttachmentBurial attachmentBurial = null,
            Burial burial = null,
            bool isExistBurial = false,
            bool isExist = false
            )
        {
            _serviceAttachmentBurial.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurial);
            _serviceAttachmentBurial.Setup(x => x.ExistAsync(It.IsAny<AttachmentBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExist);

            _serviceBurial.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(burial);
            _serviceBurial.Setup(x => x.ExistAsync(It.IsAny<Burial>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistBurial);

            _validation = new AttachmentBurialValidation(_serviceAttachmentBurial.Object, _serviceBurial.Object);
        }

        private static string GetString(int length) => new Randomizer().GetString(length);

        private string GetBaseValidationResources(string name)
        {
            switch (name)
            {
                case "ObjectExistWithThisData": return BaseValidation.ObjectExistWithThisData; break;
                case "FieldNotCanBeNull": return BaseValidation.FieldNotCanBeNull; break;
                case "FieldInvalidLength": return BaseValidation.FieldInvalidLength; break;
                case "ObjectNotExistById": return BaseValidation.ObjectNotExistById; break;
                case "ObjectNotCanBeNull": return BaseValidation.ObjectNotCanBeNull; break;
            }
            return null;
        }
    }
}
