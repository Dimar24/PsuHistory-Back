using Microsoft.AspNetCore.Http;
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
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Validations
{
    class AttachmentBurialValidationTest
    {
        private Mock<IBaseRepository<Guid, AttachmentBurial>> _serviceAttachmentBurial;
        private Mock<IBaseRepository<Guid, Burial>> _serviceBurial;
        private IBaseValidation<Guid, AttachmentBurial> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceAttachmentBurial = new Mock<IBaseRepository<Guid, AttachmentBurial>>();
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
                isExsitAttachmentBurial: true,
                isExsitBurial: false
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
                isExsitAttachmentBurial: false,
                isExsitBurial: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentBurial), string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentBurial), id) }
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
                isExsitAttachmentBurial: false,
                isExsitBurial: false
            );
            var entity = GetAttachmentBurial(
                id: Guid.NewGuid(),
                burialId: Guid.NewGuid(),
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
                isExsitAttachmentBurial: false,
                isExsitBurial: false
            );
            var entity = GetAttachmentBurial(
                id: Guid.NewGuid(),
                burialId: Guid.NewGuid(),
                file: null
            );
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentBurial.File), string.Format(BaseValidation.FileNotCanBeNull) }
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
                isExsitAttachmentBurial: false,
                isExsitBurial: false
            );
            AttachmentBurial entity = null;
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentBurial), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(AttachmentBurial)) }
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
                isExsitAttachmentBurial: true,
                isExsitBurial: true
            );
            var entity = GetAttachmentBurial(
                id: Guid.NewGuid(),
                burialId: Guid.NewGuid(),
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
                isExsitAttachmentBurial: true,
                isExsitBurial: true
            );
            var entity = GetAttachmentBurial(
                id: Guid.NewGuid(),
                burialId: Guid.NewGuid(),
                file: null
            );
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentBurial.File), string.Format(BaseValidation.FileNotCanBeNull) }
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
        public async Task UpdateValidationAsync_ExistBurialIdInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExsitAttachmentBurial: true,
                isExsitBurial: false
            );
            var entity = GetAttachmentBurial(
                 id: Guid.NewGuid(),
                burialId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentBurial.Burial), string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentBurial.Burial), entity.BurialId) }
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
        public async Task UpdateValidationAsync_ExistAttachmentBurialIdInvalid_UnSucces()
        {
            // Arrange
            MockData(
                isExsitAttachmentBurial: false,
                isExsitBurial: true
            );
            var entity = GetAttachmentBurial(
                id: Guid.NewGuid(),
                burialId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentBurial), string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentBurial), entity.Id) }
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
                isExsitAttachmentBurial: false,
                isExsitBurial: false
            );
            AttachmentBurial entity = null;
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentBurial), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(AttachmentBurial)) }
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
                isExsitAttachmentBurial: true,
                isExsitBurial: false
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
                isExsitAttachmentBurial: false,
                isExsitBurial: false
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>() {
                { nameof(AttachmentBurial), string.Format(BaseValidation.ObjectNotExistById, nameof(AttachmentBurial), id) }
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
            bool isExsitAttachmentBurial = default,
            bool isExsitBurial = default
            )
        {
            _serviceAttachmentBurial.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExsitAttachmentBurial);

            _serviceBurial.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExsitBurial);

            _validation = new AttachmentBurialValidation(_serviceAttachmentBurial.Object, _serviceBurial.Object);
        }

        private AttachmentBurial GetAttachmentBurial(
            Guid id = default,
            Guid burialId = default,
            IFormFile file = default
            )
        {
            return new AttachmentBurial()
            {
                Id = id,
                BurialId = burialId,
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
