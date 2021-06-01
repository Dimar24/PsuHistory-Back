using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using PsuHistory.Business.Service.Helpers;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Services;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Services
{
    class AttachmentBurialServiceTest
    {
        private IBaseService<Guid, AttachmentBurial> _service;
        private Mock<FileHelper> _fileHelper;
        private Mock<IBaseRepository<Guid, AttachmentBurial>> _dataAttachmentBurial;
        private Mock<IBaseValidation<Guid, AttachmentBurial>> _attachmentBurialValidation;

        [SetUp]
        public void Setup()
        {
            _fileHelper = new Mock<FileHelper>(null);
            _dataAttachmentBurial = new Mock<IBaseRepository<Guid, AttachmentBurial>>();
            _attachmentBurialValidation = new Mock<IBaseValidation<Guid, AttachmentBurial>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial(
                id: Guid.NewGuid(),
                fileName: "test data",
                filePath: "test data",
                fileType: "test data",
                burialId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: null
            );
            MockData(
                attachmentBurial: attachmentBurial,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.GetAsync(attachmentBurial.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(attachmentBurial.Id, result.Result.Id);
                Assert.AreEqual(attachmentBurial.BurialId, result.Result.BurialId);
                Assert.AreEqual(attachmentBurial.File, result.Result.File);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: errorList
            );
            MockData(
                attachmentBurial: attachmentBurial,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.GetAsync(attachmentBurial.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
            });
        }

        [Test]
        public async Task GetAllAsync_Succes()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial();
            var attachmentBurialList = new List<AttachmentBurial>() {
                GetAttachmentBurial(
                    id: Guid.NewGuid(),
                    fileName: "test data",
                    filePath: "test data",
                    fileType: "test data",
                    burialId: Guid.NewGuid(),
                    file: GetFormFile() )
            };
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: null
            );
            MockData(
                attachmentBurial: attachmentBurial,
                attachmentBurialList: attachmentBurialList,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.IsNotNull(result.Result);
                Assert.IsNotEmpty(result.Result);
            });
        }

        [Test]
        public async Task InsertAsync_Succes()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial();
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: null
            );
            MockData(
                attachmentBurial: attachmentBurial,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.InsertAsync(attachmentBurial);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(attachmentBurial.Id, result.Result.Id);
                Assert.AreEqual(attachmentBurial.BurialId, result.Result.BurialId);
                Assert.AreEqual(attachmentBurial.File, result.Result.File);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial(
                id: Guid.NewGuid(),
                fileName: "test data",
                filePath: "test data",
                fileType: "test data",
                burialId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: errorList
            );
            MockData(
                attachmentBurial: attachmentBurial,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.InsertAsync(attachmentBurial);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
            });
        }

        [Test]
        public async Task UpdateAsync_Succes()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial();
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: null
            );
            MockData(
                attachmentBurial: attachmentBurial,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(attachmentBurial);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(attachmentBurial.Id, result.Result.Id);
                Assert.AreEqual(attachmentBurial.BurialId, result.Result.BurialId);
                Assert.AreEqual(attachmentBurial.File, result.Result.File);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial(
                id: Guid.NewGuid(),
                fileName: "test data",
                filePath: "test data",
                fileType: "test data",
                burialId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: errorList
            );
            MockData(
                attachmentBurial: attachmentBurial,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(attachmentBurial);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
            });
        }

        [Test]
        public async Task DeleteAsync_Succes()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial(
                id: Guid.NewGuid(),
                fileName: "test data",
                filePath: "test data",
                fileType: "test data",
                burialId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: null
            );
            MockData(
                attachmentBurial: attachmentBurial,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(attachmentBurial.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNull(result.Errors);
            });
        }

        [Test]
        public async Task DeleteAsync_UnSucces()
        {
            // Arrange
            var attachmentBurial = GetAttachmentBurial();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<AttachmentBurial>(
                errors: errorList
            );
            MockData(
                attachmentBurial: attachmentBurial,
                validationAttachmentBurial: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(attachmentBurial.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsFalse(result.IsValid);
                Assert.IsNull(result.Result);
                Assert.IsNotNull(result.Errors);
                Assert.IsNotEmpty(result.Errors);
            });
        }

        private void MockData(
            AttachmentBurial attachmentBurial = default,
            IEnumerable<AttachmentBurial> attachmentBurialList = default,
            ValidationModel<AttachmentBurial> validationAttachmentBurial = default
            )
        {
            _fileHelper.Setup(x => x.SaveFile(It.IsAny<IFormFile>()))
                .ReturnsAsync(GetFileModelByAttachmentBurial(attachmentBurial));
            _fileHelper.Setup(x => x.SaveFileRange(It.IsAny<IList<IFormFile>>()))
                .ReturnsAsync(GetFileModelListByAttachmentBurial(attachmentBurialList));
            _fileHelper.Setup(x => x.DeleteFile(It.IsAny<string>()));

            _dataAttachmentBurial.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurial);
            _dataAttachmentBurial.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurialList);
            _dataAttachmentBurial.Setup(x => x.InsertAsync(It.IsAny<AttachmentBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurial);
            _dataAttachmentBurial.Setup(x => x.UpdateAsync(It.IsAny<AttachmentBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentBurial);
            _dataAttachmentBurial.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _attachmentBurialValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationAttachmentBurial);
            _attachmentBurialValidation.Setup(x => x.InsertValidationAsync(It.IsAny<AttachmentBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationAttachmentBurial);
            _attachmentBurialValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<AttachmentBurial>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationAttachmentBurial);
            _attachmentBurialValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationAttachmentBurial);

            _service = new AttachmentBurialService(_fileHelper.Object, _dataAttachmentBurial.Object, _attachmentBurialValidation.Object);
        }

        private AttachmentBurial GetAttachmentBurial(
            Guid id = default,
            string fileName = default,
            string filePath = default,
            string fileType = default,
            Guid burialId = default,
            IFormFile file = default
            )
        {
            return new AttachmentBurial()
            {
                Id = id,
                FileName = fileName,
                FilePath = filePath,
                FileType = fileType,
                BurialId = burialId,
                File = file
            };
        }

        private ValidationModel<TResult> GetValidationModel<TResult>(
            Dictionary<string, string> errors = default
            )
        {
            return new ValidationModel<TResult>()
            {
                Errors = errors
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

        private FileModel GetFileModelByAttachmentBurial(AttachmentBurial attachmentBurial = default)
        {
            return new FileModel()
            {
                FileName = attachmentBurial.FileName,
                FilePath = attachmentBurial.FilePath,
                FileType = attachmentBurial.FileType
            };
        }

        private List<FileModel> GetFileModelListByAttachmentBurial(IEnumerable<AttachmentBurial> attachmentBurialList = default)
        {
            var fileModelList = new List<FileModel>();

            if(attachmentBurialList is not null)
            {
                foreach (var fileModel in attachmentBurialList)
                {
                    fileModelList.Add(GetFileModelByAttachmentBurial(fileModel));
                }
            }

            return fileModelList;
        }
    }
}
