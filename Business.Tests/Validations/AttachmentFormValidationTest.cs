using Microsoft.AspNetCore.Http;
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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Validations
{
    class AttachmentFormValidationTest
    {
        private Mock<IBaseService<Guid, AttachmentForm>> _serviceAttachmentForm;
        private Mock<IBaseService<Guid, Form>> _serviceForm;
        private IBaseValidation<Guid, AttachmentForm> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceAttachmentForm = new Mock<IBaseService<Guid, AttachmentForm>>();
            _serviceForm = new Mock<IBaseService<Guid, Form>>();
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
                attachmentForm: new AttachmentForm()
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
                { nameof(AttachmentForm), BaseValidation.ObjectNotExistById }
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
            MockData(
                attachmentForm: new AttachmentForm(),
                form: new Form()
                );
            var entity = new AttachmentForm()
            {
                FormId = Guid.NewGuid(),
                File = GetFormFile()
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

        [Test]
        public async Task InsertValidationAsync_FileIsNull_UnSucces()
        {
            // Arrange
            MockData(
                form: new Form()
                );
            var entity = new AttachmentForm()
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
        public async Task InsertValidationAsync_GetBurialIsNull_UnSucces()
        {
            // Arrange
            MockData(
                );
            var entity = new AttachmentForm()
            {
                FormId = Guid.NewGuid(),
                File = GetFormFile()
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(AttachmentForm.FormId), BaseValidation.ObjectNotExistById }
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
                attachmentForm: new AttachmentForm(),
                form: new Form()
                );
            var entity = new AttachmentForm()
            {
                FormId = Guid.NewGuid(),
                File = GetFormFile()
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

        [Test]
        public async Task UpdateValidationAsync_FileIsNull_UnSucces()
        {
            // Arrange
            MockData(
                form: new Form(),
                attachmentForm: new AttachmentForm()
                );
            var entity = new AttachmentForm()
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
        public async Task UpdateValidationAsync_GetBurialIsNull_UnSucces()
        {
            // Arrange
            MockData(
                attachmentForm: new AttachmentForm()
                );
            var entity = new AttachmentForm()
            {
                FormId = Guid.NewGuid(),
                File = GetFormFile()
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(AttachmentForm.FormId), BaseValidation.ObjectNotExistById }
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
        public async Task UpdateValidationAsync_GetAttachmentBurialIsNull_UnSucces()
        {
            // Arrange
            MockData(
                form: new Form()
                );
            var entity = new AttachmentForm()
            {
                FormId = Guid.NewGuid(),
                File = GetFormFile()
            };
            var listError = new Dictionary<string, string>()
            {
                { nameof(AttachmentForm), BaseValidation.ObjectNotExistById }
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
                attachmentForm: new AttachmentForm()
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
                { nameof(AttachmentForm), BaseValidation.ObjectNotExistById }
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
            AttachmentForm attachmentForm = null,
            Form form = null
            )
        {
            _serviceAttachmentForm.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);

            _serviceForm.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(form);

            _validation = new AttachmentFormValidation(_serviceAttachmentForm.Object, _serviceForm.Object);
        }

        private IFormFile GetFormFile()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            return fileMock.Object;
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
