//using Moq;
//using NUnit.Framework;
//using PsuHistory.Business.Service.Interfaces;
//using PsuHistory.Data.Domain.Models.Histories;
//using PsuHistory.Data.Repository.Interfaces;
//using PsuHistory.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Business.Tests.Services
//{
//    class FormServiceTest
//    {
//        private IBaseService<Guid, Form> _service;
//        private Mock<IBaseRepository<Guid, Form>> _dataForm;
//        //private Mock<IBaseRepository<Guid, AttachmentForm>> _dataAttachmentForm;
//        private Mock<IBaseValidation<Guid, Form>> _formValidation;

//        [SetUp]
//        public void Setup()
//        {
//            _dataForm = new Mock<IBaseRepository<Guid, Form>>();
//            _formValidation = new Mock<IBaseValidation<Guid, Form>>();
//            //_dataAttachmentForm = new Mock<IBaseValidation<Guid, AttachmentForm>>();
//        }

//        [TearDown]
//        public void Teardown()
//        { }

//        [Test]
//        public async Task GetAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim(
//                id: Guid.NewGuid(),
//                name: "test data"
//            );
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.GetAsync(typeVictim.Id);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.NotNull(result.Result);
//                Assert.AreEqual(typeVictim.Id, result.Result.Id);
//                Assert.AreEqual(typeVictim.Name, result.Result.Name);
//            });
//        }

//        [Test]
//        public async Task GetAsync_UnSucces()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var errorList = new Dictionary<string, string>() {
//                { "TestKey", "TestValue" }
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: errorList
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.GetAsync(typeVictim.Id);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsFalse(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNotNull(result.Errors);
//                Assert.IsNotEmpty(result.Errors);
//            });
//        }

//        [Test]
//        public async Task GetAllAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var typeVictimList = new List<TypeVictim>() {
//                GetTypeVictim(id: Guid.NewGuid(), name: "test data" )
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                typeVictimList: typeVictimList,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.GetAllAsync();

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.IsNotNull(result.Result);
//                Assert.IsNotEmpty(result.Result);
//            });
//        }

//        [Test]
//        public async Task InsertAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.InsertAsync(typeVictim);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.NotNull(result.Result);
//                Assert.AreEqual(typeVictim.Id, result.Result.Id);
//                Assert.AreEqual(typeVictim.Name, result.Result.Name);
//            });
//        }

//        [Test]
//        public async Task InsertAsync_UnSucces()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim(
//                id: Guid.NewGuid(),
//                name: "test data"
//            );
//            var errorList = new Dictionary<string, string>() {
//                { "TestKey", "TestValue" }
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: errorList
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.InsertAsync(typeVictim);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsFalse(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNotNull(result.Errors);
//                Assert.IsNotEmpty(result.Errors);
//            });
//        }

//        [Test]
//        public async Task UpdateAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.UpdateAsync(typeVictim);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.NotNull(result.Result);
//                Assert.IsNull(result.Errors);
//                Assert.AreEqual(typeVictim.Id, result.Result.Id);
//                Assert.AreEqual(typeVictim.Name, result.Result.Name);
//            });
//        }

//        [Test]
//        public async Task UpdateAsync_UnSucces()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim(
//                id: Guid.NewGuid(),
//                name: "test data"
//            );
//            var errorList = new Dictionary<string, string>() {
//                { "TestKey", "TestValue" }
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: errorList
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.UpdateAsync(typeVictim);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsFalse(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNotNull(result.Errors);
//                Assert.IsNotEmpty(result.Errors);
//            });
//        }

//        [Test]
//        public async Task DeleteAsync_Succes()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim(
//                id: Guid.NewGuid(),
//                name: "test data"
//            );
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: null
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.DeleteAsync(typeVictim.Id);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsTrue(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNull(result.Errors);
//            });
//        }

//        [Test]
//        public async Task DeleteAsync_UnSucces()
//        {
//            // Arrange
//            var typeVictim = GetTypeVictim();
//            var errorList = new Dictionary<string, string>() {
//                { "TestKey", "TestValue" }
//            };
//            var validationModel = GetValidationModel<TypeVictim>(
//                errors: errorList
//            );
//            MockData(
//                typeVictim: typeVictim,
//                validationTypeVictim: validationModel
//            );

//            // Act
//            var result = await _service.DeleteAsync(typeVictim.Id);

//            // Assert
//            Assert.Multiple(() =>
//            {
//                Assert.IsFalse(result.IsValid);
//                Assert.IsNull(result.Result);
//                Assert.IsNotNull(result.Errors);
//                Assert.IsNotEmpty(result.Errors);
//            });
//        }

//        private void MockData(
//            Form form = default,
//            //AttachmentForm attachmentForm = default,
//            IEnumerable<Form> formList = default,
//            //IEnumerable<AttachmentForm> attachmentFormList = default,
//            ValidationModel<Form> validationForm = default
//            )
//        {
//            if (form is null)
//            {
//                throw new ArgumentNullException(nameof(form));
//            }

//            _dataForm.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(form);
//            _dataForm.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(formList);
//            _dataForm.Setup(x => x.InsertAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(form);
//            _dataForm.Setup(x => x.UpdateAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(form);
//            _dataForm.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

//            //_dataAttachmentForm.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
//            //_dataAttachmentForm.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(attachmentFormList);
//            //_dataAttachmentForm.Setup(x => x.InsertAsync(It.IsAny<AttachmentForm>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
//            //_dataAttachmentForm.Setup(x => x.UpdateAsync(It.IsAny<AttachmentForm>(), It.IsAny<CancellationToken>())).ReturnsAsync(attachmentForm);
//            //_dataAttachmentForm.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

//            _formValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationForm);
//            _formValidation.Setup(x => x.InsertValidationAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationForm);
//            _formValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<Form>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationForm);
//            _formValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationForm);

//            _service = new FormService(_dataForm.Object, _formValidation.Object);
//        }

//        private Form GetForm(
//            Guid id = default,
//            string lastName = default
//            )
//        {
//            return new Form()
//            {
//                Id = id,
//                LastName = lastName
//            };
//        }

//        private ValidationModel<TResult> GetValidationModel<TResult>(
//            Dictionary<string, string> errors = default
//            )
//        {
//            return new ValidationModel<TResult>()
//            {
//                Errors = errors
//            };
//        }
//    }
//}
