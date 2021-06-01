using Moq;
using NUnit.Framework;
using PsuHistory.Business.Service.Helpers;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Services;
using PsuHistory.Common.Models;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Tests.Services
{
    class FormServiceTest
    {
        private IBaseService<Guid, Form> _service;
        private Mock<FileHelper> _fileHelper;
        private Mock<IBaseRepository<Guid, Form>> _dataForm;
        private Mock<IBaseRepository<Guid, AttachmentForm>> _dataAttachmentForm;
        private Mock<IBaseValidation<Guid, Form>> _formValidation;

        [SetUp]
        public void Setup()
        {
            _fileHelper = new Mock<FileHelper>(null);
            _dataForm = new Mock<IBaseRepository<Guid, Form>>();
            _dataAttachmentForm = new Mock<IBaseRepository<Guid, AttachmentForm>>();
            _formValidation = new Mock<IBaseValidation<Guid, Form>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var form = GetForm(
                lastName: "test data",
                firstName: "test data",
                middleName: "test data"
            );
            var validationModel = GetValidationModel<Form>(
                errors: null
            );
            MockData(
                form: form,
                validationForm: validationModel
            );

            // Act
            var result = await _service.GetAsync(form.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(form.Id, result.Result.Id);
                Assert.AreEqual(form.LastName, result.Result.LastName);
                Assert.AreEqual(form.FirstName, result.Result.FirstName);
                Assert.AreEqual(form.MiddleName, result.Result.MiddleName);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var form = GetForm();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<Form>(
                errors: errorList
            );
            MockData(
                form: form,
                validationForm: validationModel
            );

            // Act
            var result = await _service.GetAsync(form.Id);

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
            var form = GetForm();
            var formList = new List<Form>() {
                GetForm(id: Guid.NewGuid(),
                lastName: "test data",
                firstName: "test data",
                middleName: "test data"
                )
            };
            var validationModel = GetValidationModel<Form>(
                errors: null
            );
            MockData(
                form: form,
                formList: formList,
                validationForm: validationModel
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
            var form = GetForm();
            var validationModel = GetValidationModel<Form>(
                errors: null
            );
            MockData(
                form: form,
                validationForm: validationModel
            );

            // Act
            var result = await _service.InsertAsync(form);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(form.Id, result.Result.Id);
                Assert.AreEqual(form.LastName, result.Result.LastName);
                Assert.AreEqual(form.FirstName, result.Result.FirstName);
                Assert.AreEqual(form.MiddleName, result.Result.MiddleName);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var form = GetForm(
                id: Guid.NewGuid(),
                lastName: "test data",
                firstName: "test data",
                middleName: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<Form>(
                errors: errorList
            );
            MockData(
                form: form,
                validationForm: validationModel
            );

            // Act
            var result = await _service.InsertAsync(form);

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
            var form = GetForm();
            var validationModel = GetValidationModel<Form>(
                errors: null
            );
            MockData(
                form: form,
                validationForm: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(form);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(form.Id, result.Result.Id);
                Assert.AreEqual(form.LastName, result.Result.LastName);
                Assert.AreEqual(form.FirstName, result.Result.FirstName);
                Assert.AreEqual(form.MiddleName, result.Result.MiddleName);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var form = GetForm(
                id: Guid.NewGuid(),
                lastName: "test data",
                firstName: "test data",
                middleName: "test data"
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<Form>(
                errors: errorList
            );
            MockData(
                form: form,
                validationForm: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(form);

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
            var form = GetForm(
                id: Guid.NewGuid(),
                lastName: "test data",
                firstName: "test data",
                middleName: "test data"
            );
            var validationModel = GetValidationModel<Form>(
                errors: null
            );
            MockData(
                form: form,
                validationForm: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(form.Id);

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
            var form = GetForm();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<Form>(
                errors: errorList
            );
            MockData(
                form: form,
                validationForm: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(form.Id);

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
            Form form = default,
            AttachmentForm attachmentForm = default,
            IEnumerable<Form> formList = default,
            IEnumerable<AttachmentForm> attachmentFormList = default,
            ValidationModel<Form> validationForm = default
            )
        {
            _dataForm.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(form);
            _dataForm.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(formList);
            _dataForm.Setup(x => x.InsertAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(form);
            _dataForm.Setup(x => x.UpdateAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(form);
            _dataForm.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _dataAttachmentForm.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
            _dataAttachmentForm.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(attachmentFormList);
            _dataAttachmentForm.Setup(x => x.InsertAsync(It.IsAny<AttachmentForm>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
            _dataAttachmentForm.Setup(x => x.UpdateAsync(It.IsAny<AttachmentForm>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
            _dataAttachmentForm.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _formValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationForm);
            _formValidation.Setup(x => x.InsertValidationAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationForm);
            _formValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationForm);
            _formValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationForm);

            _service = new FormService(_fileHelper.Object, _dataForm.Object, _dataAttachmentForm.Object, _formValidation.Object);
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
                MiddleName = middleName
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
    }
}
