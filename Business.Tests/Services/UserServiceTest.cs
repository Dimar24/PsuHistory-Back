using Moq;
using NUnit.Framework;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Services;
using PsuHistory.Common.Models;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Services
{
    class UserServiceTest
    {
        private IBaseService<Guid, User> _service;
        private Mock<IBaseRepository<Guid, User>> _dataUser;
        private Mock<IBaseValidation<Guid, User>> _userValidation;

        [SetUp]
        public void Setup()
        {
            _dataUser = new Mock<IBaseRepository<Guid, User>>();
            _userValidation = new Mock<IBaseValidation<Guid, User>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetAsync_Succes()
        {
            // Arrange
            var user = GetUser(
                id: Guid.NewGuid(),
                mail: "test data",
                password: "test data",
                roleId: Guid.NewGuid()
            );
            var validationModel = GetValidationModel<User>(
                errors: null
            );
            MockData(
                user: user,
                validationUser: validationModel
            );

            // Act
            var result = await _service.GetAsync(user.Id);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(user.Id, result.Result.Id);
                Assert.AreEqual(user.Mail, result.Result.Mail);
                Assert.AreEqual(user.Password, result.Result.Password);
                Assert.AreEqual(user.RoleId, result.Result.RoleId);
            });
        }

        [Test]
        public async Task GetAsync_UnSucces()
        {
            // Arrange
            var user = GetUser();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<User>(
                errors: errorList
            );
            MockData(
                user: user,
                validationUser: validationModel
            );

            // Act
            var result = await _service.GetAsync(user.Id);

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
            var user = GetUser();
            var userList = new List<User>() {
                GetUser(id: Guid.NewGuid(), 
                mail: "test data",
                password: "test data",
                roleId: Guid.NewGuid() )
            };
            var validationModel = GetValidationModel<User>(
                errors: null
            );
            MockData(
                user: user,
                userList: userList,
                validationUser: validationModel
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
            var user = GetUser();
            var validationModel = GetValidationModel<User>(
                errors: null
            );
            MockData(
                user: user,
                validationUser: validationModel
            );

            // Act
            var result = await _service.InsertAsync(user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.AreEqual(user.Id, result.Result.Id);
                Assert.AreEqual(user.Mail, result.Result.Mail);
                Assert.AreEqual(user.Password, result.Result.Password);
                Assert.AreEqual(user.RoleId, result.Result.RoleId);
            });
        }

        [Test]
        public async Task InsertAsync_UnSucces()
        {
            // Arrange
            var user = GetUser(
                id: Guid.NewGuid(),
                mail: "test data",
                password: "test data",
                roleId: Guid.NewGuid()
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<User>(
                errors: errorList
            );
            MockData(
                user: user,
                validationUser: validationModel
            );

            // Act
            var result = await _service.InsertAsync(user);

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
            var user = GetUser();
            var validationModel = GetValidationModel<User>(
                errors: null
            );
            MockData(
                user: user,
                validationUser: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(result.IsValid);
                Assert.NotNull(result.Result);
                Assert.IsNull(result.Errors);
                Assert.AreEqual(user.Id, result.Result.Id);
                Assert.AreEqual(user.Mail, result.Result.Mail);
                Assert.AreEqual(user.Password, result.Result.Password);
                Assert.AreEqual(user.RoleId, result.Result.RoleId);
            });
        }

        [Test]
        public async Task UpdateAsync_UnSucces()
        {
            // Arrange
            var user = GetUser(
                id: Guid.NewGuid(),
                mail: "test data",
                password: "test data",
                roleId: Guid.NewGuid()
            );
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<User>(
                errors: errorList
            );
            MockData(
                user: user,
                validationUser: validationModel
            );

            // Act
            var result = await _service.UpdateAsync(user);

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
            var user = GetUser(
                mail: "test data",
                password: "test data",
                roleId: Guid.NewGuid()
            );
            var validationModel = GetValidationModel<User>(
                errors: null
            );
            MockData(
                user: user,
                validationUser: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(user.Id);

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
            var user = GetUser();
            var errorList = new Dictionary<string, string>() {
                { "TestKey", "TestValue" }
            };
            var validationModel = GetValidationModel<User>(
                errors: errorList
            );
            MockData(
                user: user,
                validationUser: validationModel
            );

            // Act
            var result = await _service.DeleteAsync(user.Id);

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
            User user = default,
            IEnumerable<User> userList = default,
            ValidationModel<User> validationUser = default
            )
        {
            _dataUser.Setup(x => x.GetAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _dataUser.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(userList);
            _dataUser.Setup(x => x.InsertAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _dataUser.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);
            _dataUser.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));

            _userValidation.Setup(x => x.GetValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationUser);
            _userValidation.Setup(x => x.InsertValidationAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationUser);
            _userValidation.Setup(x => x.UpdateValidationAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationUser);
            _userValidation.Setup(x => x.DeleteValidationAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationUser);

            _service = new UserService(_dataUser.Object, _userValidation.Object);
        }

        private User GetUser(
            Guid id = default,
            string mail = default,
            string password = default,
            Guid roleId = default
            )
        {
            return new User()
            {
                Id = id,
                Mail = mail,
                Password = password,
                RoleId = roleId,
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
