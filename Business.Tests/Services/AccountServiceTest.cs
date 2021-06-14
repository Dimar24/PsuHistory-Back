using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PsuHistory.Business.DTO.Models.AccountDataModels;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Services;
using PsuHistory.Common.Models;
using PsuHistory.Common.Options;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Services
{
    class AccountServiceTest
    {
        private IBaseAccoutService<Login> _service;
        private Mock<IOptions<AuthOptions>> _dataAuthOptions;
        private Mock<IBaseRepository<Guid, User>> _dataUser;
        private Mock<IBaseAccoutValidation<Login>> _loginValidation;

        [SetUp]
        public void Setup()
        {
            _dataAuthOptions = new Mock<IOptions<AuthOptions>>();
            _dataUser = new Mock<IBaseRepository<Guid, User>>();
            _loginValidation = new Mock<IBaseAccoutValidation<Login>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task LoginAsync_Succes()
        {
            // Arrange
            var login = GetLogin(
                mail: "test data",
                password: "test data",
                token: "test data"
            );
            var validationModel = GetValidationModel<Login>(
                errors: null
            );
            MockData(
                //login: login,
                validatinLogin: validationModel
            );

            // Act
            //var result = await _service.LoginAsync(login.token);

            // Assert
            Assert.Multiple(() =>
            {
                //Assert.IsTrue(result.IsValid);
                //Assert.NotNull(result.Result);
                //Assert.AreEqual(user.Id, result.Result.Id);
                //Assert.AreEqual(user.Mail, result.Result.Mail);
                //Assert.AreEqual(user.Password, result.Result.Password);
                //Assert.AreEqual(user.RoleId, result.Result.RoleId);
            });
        }


        private void MockData(
            //User user = default,
            //Login login = default,
            //IEnumerable<Login> loginList = default,
            IEnumerable<User> userList = default,
            ValidationModel<Login> validatinLogin = default
            )
        {
            var authOptions = new AuthOptions()
            {
                Issuer = "MyAuthServer",
                Audience = "MyAuthClient",
                Secret = "mysupersecret_secretkey!123",
                TokenLifetime = 3600
            };

            _dataAuthOptions.Setup(x => x.Value).Returns(authOptions);

            _dataUser.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(userList);

            //_dataAuthOptions.

            _loginValidation.Setup(x => x.LoginAsync(It.IsAny<Login>(), It.IsAny<CancellationToken>())).ReturnsAsync(validatinLogin);

            _service = new AccountService(_dataAuthOptions.Object, _dataUser.Object, _loginValidation.Object);
        }

        private Login GetLogin(
            //Guid id = default,
            string mail = default,
            string password = default,
            string token = default
            )
        {
            return new Login()
            {
                //Id = id,
                Mail = mail,
                Password = password,
                Token = token,
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
