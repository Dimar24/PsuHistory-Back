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
                isExsitAttachmentFormById: true,
                isExsitFormById: false
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
                isExsitAttachmentFormById: false,
                isExsitFormById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentForm), string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentForm), id) }
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
                isExsitAttachmentFormById: false,
                isExsitFormById: false
            );
            var entity = GetAttachmentForm(
                id: Guid.NewGuid(),
                formId: Guid.NewGuid(),
                file: GetFormFile()
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
        public async Task InsertValidationAsync_FileIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExsitAttachmentFormById: false,
                isExsitFormById: false
            );
            var entity = GetAttachmentForm(
                id: Guid.NewGuid(),
                formId: Guid.NewGuid(),
                file: null
            );
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentForm.File), string.Format(BaseValidation.FileNotCanBeNull) }
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
                isExsitAttachmentFormById: false,
                isExsitFormById: false
            );
            AttachmentForm entity = null;
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentForm), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(AttachmentForm)) }
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
                isExsitAttachmentFormById: true,
                isExsitFormById: true
            );
            var entity = GetAttachmentForm(
                id: Guid.NewGuid(),
                formId: Guid.NewGuid(),
                file: GetFormFile()
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
        public async Task UpdateValidationAsync_FileIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExsitAttachmentFormById: true,
                isExsitFormById: true
            );
            var entity = GetAttachmentForm(
                id: Guid.NewGuid(),
                formId: Guid.NewGuid(),
                file: null
            );
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentForm.File), string.Format(BaseValidation.FileNotCanBeNull) }
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
        public async Task UpdateValidationAsync_ExistFormIdInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExsitAttachmentFormById: true,
                isExsitFormById: false
            );
            var entity = GetAttachmentForm(
                id: Guid.NewGuid(),
                formId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentForm.Form), string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentForm.Form), entity.FormId) }
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
        public async Task UpdateValidationAsync_ExistAttachmentFormIdInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExsitAttachmentFormById: false,
                isExsitFormById: true
            );
            var entity = GetAttachmentForm(
                id: Guid.NewGuid(),
                formId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentForm), string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentForm), entity.Id) }
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
                isExsitAttachmentFormById: false,
                isExsitFormById: false
            );
            AttachmentForm entity = null;
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentForm), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(AttachmentForm)) }
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
                isExsitAttachmentFormById: true,
                isExsitFormById: false
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
                isExsitAttachmentFormById: false,
                isExsitFormById: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentForm), string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentForm), id) }
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
            bool isExsitAttachmentFormById = true,
            bool isExsitFormById = true
            )
        {
            _serviceAttachmentForm.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExsitAttachmentFormById);

            _serviceForm.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExsitFormById);

            _validation = new AttachmentFormValidation(_serviceAttachmentForm.Object, _serviceForm.Object);
        }

        private AttachmentForm GetAttachmentForm(
            Guid id,
            Guid formId,
            IFormFile file = null
            )
        {
            return new AttachmentForm()
            {
                Id = id,
                FormId = formId,
                File = file
            };
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
    }
}
