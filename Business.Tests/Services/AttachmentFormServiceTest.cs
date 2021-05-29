using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using PsuHistory.Business.Service.Helpers;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Services;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Services
{
    class AttachmentFormServiceTest
    {
        private IBaseService<Guid, AttachmentForm> _service;
        private Mock<FileHelper> _fileHelper;
        private Mock<IBaseRepository<Guid, AttachmentForm>> _dataAttachmentForm;
        private Mock<IBaseValidation<Guid, AttachmentForm>> _attachmentFormValidation;

        [SetUp]
        public void Setup()
        {
            _fileHelper = new Mock<FileHelper>(null);
            _dataAttachmentForm = new Mock<IBaseRepository<Guid, AttachmentForm>>();
            _attachmentFormValidation = new Mock<IBaseValidation<Guid, AttachmentForm>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var attachmentForm = GetAttachmentForm(
                id: Guid.NewGuid(),
                fileName: "test data",
                filePath: "test data",
                fileType: "test data",
                formId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: null
            );
            MockData(
                attachmentForm: attachmentForm,
                validationAttachmentForm: validationModel
            );

            // Act
            var result = await _service.GetAsync(attachmentForm.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(attachmentForm.Id, result.Result.Id);
                Assert.AreEqual(attachmentForm.FileName, result.Result.FileName);
                Assert.AreEqual(attachmentForm.FilePath, result.Result.FilePath);
                Assert.AreEqual(attachmentForm.FileType, result.Result.FileType);
                Assert.AreEqual(attachmentForm.FormId, result.Result.FormId);
                Assert.AreEqual(attachmentForm.File, result.Result.File);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var attachmentForm = GetAttachmentForm();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: errorList
            );
            MockData(
                attachmentForm: attachmentForm,
                validationAttachmentForm: validationModel
            );

            // Act
            var result = await _service.GetAsync(attachmentForm.Id);

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
            var attachmentForm = GetAttachmentForm();
            var attachmentFormList = new List<AttachmentForm>() {
                GetAttachmentForm(
                    id: Guid.NewGuid(),
                    fileName: "test data",
                    filePath: "test data",
                    fileType: "test data",
                    formId: Guid.NewGuid(),
                    file: GetFormFile() )
            };
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: null
            );
            MockData(
                attachmentForm: attachmentForm,
                attachmentFormList: attachmentFormList,
                validationAttachmentForm: validationModel
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
            var attachmentForm = GetAttachmentForm();
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: null
            );
            MockData(
                attachmentForm: attachmentForm,
                validationAttachmentForm: validationModel
            );

            // Act
            var result = await _service.InsertAsync(attachmentForm);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(attachmentForm.Id, result.Result.Id);
                Assert.AreEqual(attachmentForm.FileName, result.Result.FileName);
                Assert.AreEqual(attachmentForm.FilePath, result.Result.FilePath);
                Assert.AreEqual(attachmentForm.FileType, result.Result.FileType);
                Assert.AreEqual(attachmentForm.FormId, result.Result.FormId);
                Assert.AreEqual(attachmentForm.File, result.Result.File);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var attachmentForm = GetAttachmentForm(
                id: Guid.NewGuid(),
                fileName: "test data",
                filePath: "test data",
                fileType: "test data",
                formId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: errorList
            );
            MockData(
                attachmentForm: attachmentForm,
                validationAttachmentForm: validationModel
            );

            // Act
            var result = await _service.InsertAsync(attachmentForm);

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
            var attachmentForm = GetAttachmentForm();
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: null
            );
            MockData(
                attachmentForm: attachmentForm,
                validationAttachmentForm: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(attachmentForm);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(attachmentForm.Id, result.Result.Id);
                Assert.AreEqual(attachmentForm.FileName, result.Result.FileName);
                Assert.AreEqual(attachmentForm.FilePath, result.Result.FilePath);
                Assert.AreEqual(attachmentForm.FileType, result.Result.FileType);
                Assert.AreEqual(attachmentForm.FormId, result.Result.FormId);
                Assert.AreEqual(attachmentForm.File, result.Result.File);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var attachmentForm = GetAttachmentForm(
                id: Guid.NewGuid(),
                fileName: "test data",
                filePath: "test data",
                fileType: "test data",
                formId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: errorList
            );
            MockData(
                attachmentForm: attachmentForm,
                validationAttachmentForm: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(attachmentForm);

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
            var attachmentForm = GetAttachmentForm(
                id: Guid.NewGuid(),
                fileName: "test data",
                filePath: "test data",
                fileType: "test data",
                formId: Guid.NewGuid(),
                file: GetFormFile()
            );
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: null
            );
            MockData(
                attachmentForm: attachmentForm,
                validationAttachmentForm: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(attachmentForm.Id);

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
            var attachmentForm = GetAttachmentForm();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<AttachmentForm>(
                errors: errorList
            );
            MockData(
                attachmentForm: attachmentForm,
                validationAttachmentForm: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(attachmentForm.Id);

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
            AttachmentForm attachmentForm = default,
            IEnumerable<AttachmentForm> attachmentFormList = default,
            ValidationModel<AttachmentForm> validationAttachmentForm = default
            )
        {
            _fileHelper.Setup(x => x.SaveFile(It.IsAny<IFormFile>()))
                .ReturnsAsync(GetFileModelByAttachmentForm(attachmentForm));
            _fileHelper.Setup(x => x.SaveFileRange(It.IsAny<IList<IFormFile>>()))
                .ReturnsAsync(GetFileModelListByAttachmentForm(attachmentFormList));
            _fileHelper.Setup(x => x.DeleteFile(It.IsAny<string>()));

            _dataAttachmentForm.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
            _dataAttachmentForm.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(attachmentFormList);
            _dataAttachmentForm.Setup(x => x.InsertAsync(It.IsAny<AttachmentForm>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
            _dataAttachmentForm.Setup(x => x.UpdateAsync(It.IsAny<AttachmentForm>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
            _dataAttachmentForm.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _attachmentFormValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationAttachmentForm);
            _attachmentFormValidation.Setup(x => x.InsertValidationAsync(It.IsAny<AttachmentForm>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationAttachmentForm);
            _attachmentFormValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<AttachmentForm>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationAttachmentForm);
            _attachmentFormValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationAttachmentForm);

            _service = new AttachmentFormService(_fileHelper.Object, _dataAttachmentForm.Object, _attachmentFormValidation.Object);
        }

        private AttachmentForm GetAttachmentForm(
            Guid id = default,
            string fileName = default,
            string filePath = default,
            string fileType = default,
            Guid formId = default,
            IFormFile file = default
            )
        {
            return new AttachmentForm()
            {
                Id = id,
                FileName = fileName,
                FilePath = filePath,
                FileType = fileType,
                FormId = formId,
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

        private FileModel GetFileModelByAttachmentForm(AttachmentForm attachmentForm = default)
        {
            return new FileModel()
            {
                FileName = attachmentForm.FileName,
                FilePath = attachmentForm.FilePath,
                FileType = attachmentForm.FileType
            };
        }

        private List<FileModel> GetFileModelListByAttachmentForm(IEnumerable<AttachmentForm> attachmentFormList = default)
        {
            var fileModelList = new List<FileModel>();

            if (attachmentFormList is not null)
            {
                foreach (var fileModel in attachmentFormList)
                {
                    fileModelList.Add(GetFileModelByAttachmentForm(fileModel));
                }
            }

            return fileModelList;
        }
    }
}
