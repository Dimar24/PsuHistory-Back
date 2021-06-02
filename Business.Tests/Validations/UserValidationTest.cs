using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using PsuHistory.Business.Service.Interfaces;
using PsuHistory.Business.Service.Validations;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.Repository.Interfaces;
using PsuHistory.Resource.Recources.Validation;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Validations
{
    class UserValidationTest
    {
        private Mock<IBaseRepository<Guid, User>> _serviceUser;
        private Mock<IBaseRepository<Guid, Role>> _serviceRole;
        private IBaseValidation<Guid, User> _validation;

        [SetUp]
        public void Setup()
        {
            _serviceUser = new Mock<IBaseRepository<Guid, User>>();
            _serviceRole = new Mock<IBaseRepository<Guid, Role>>();
        }

        [TearDown]
        public void Teardown()
        { }

        [Test]
        public async Task GetValidationAsync_Succes()
        {
            // Arrange
            MockData(
                isExistUser: true,
                isExistUserById: true,
                isExistRole: true,
                isExistRoleById: true
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
                isExistUser: true,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(User), string.Format(BaseValidation.ObjectNotExistById, nameof(User), id) }
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
                isExistUser: true,
                isExistUserById: true,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: "test",
                password: "test",
                roleId: Guid.NewGuid()
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
        public async Task InsertValidationAsync_Exist_UnSucces()
        {
            // Arrange
            MockData(
                isExistUser: true,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: "test",
                password: "test",
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Role),string.Format(BaseValidation.ObjectNotExistById, nameof(User.Role), entity.RoleId) }
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
                isExistUser: true,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            User entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(User), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(User)) }
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

        [TestCase(2)]
        [TestCase(135)]
        public async Task InsertValidationAsync_InvalidMail_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: GetString(length),
                password: "test",
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Mail), string.Format(BaseValidation.FieldInvalidLength, nameof(User.Mail), 3, 256) }
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
        public async Task InsertValidationAsync_MailIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: null,
                password: "test",
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Mail), string.Format(BaseValidation.FieldNotCanBeNull, nameof(User.Mail)) }
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

        [TestCase(2)]
        [TestCase(135)]
        public async Task InsertValidationAsync_InvalidPassword_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: "test",
                password: GetString(length),
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Mail), string.Format(BaseValidation.FieldInvalidLength, nameof(User.Mail), 6, 64) }
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
        public async Task InsertValidationAsync_PasswordIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: "test",
                password: null,
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Mail), string.Format(BaseValidation.FieldNotCanBeNull, nameof(User.Mail)) }
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
                isExistUser: true,
                isExistUserById: true,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: "test",
                password: "test",
                roleId: Guid.NewGuid()
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
        public async Task UpdateValidationAsync_Exist_UnSucces()
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: true,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: "test",
                password: "test",
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Role),string.Format(BaseValidation.ObjectNotExistById, nameof(User.Role), entity.RoleId) }
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
                isExistUser: true,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            User entity = null;
            var listError = new Dictionary<string, string>()
            {
                { nameof(User), string.Format(BaseValidation.ObjectNotCanBeNull, nameof(User)) }
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

        [TestCase(2)]
        [TestCase(135)]
        public async Task UpdateValidationAsync_InvalidMail_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: GetString(length),
                password: "test",
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Mail), string.Format(BaseValidation.FieldInvalidLength, nameof(User.Mail), 3, 256) }
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
        public async Task UpdateValidationAsync_MailIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: null,
                password: "test",
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Mail), string.Format(BaseValidation.FieldNotCanBeNull, nameof(User.Mail)) }
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

        [TestCase(2)]
        [TestCase(135)]
        public async Task UpdateValidationAsync_InvalidPassword_UnSucces(int length)
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: "test",
                password: GetString(length),
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Mail), string.Format(BaseValidation.FieldInvalidLength, nameof(User.Mail), 6, 64) }
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
        public async Task UpdateValidationAsync_PasswordIsNull_UnSucces()
        {
            // Arrange
            MockData(
                isExistUser: false,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var entity = GetUser(
                id: Guid.NewGuid(),
                mail: "test",
                password: null,
                roleId: Guid.NewGuid()
            );
            var listError = new Dictionary<string, string>()
            {
                { nameof(User.Mail), string.Format(BaseValidation.FieldNotCanBeNull, nameof(User.Mail)) }
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
                isExistUser: true,
                isExistUserById: true,
                isExistRole: true,
                isExistRoleById: true
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
                isExistUser: true,
                isExistUserById: false,
                isExistRole: true,
                isExistRoleById: true
            );
            var id = Guid.NewGuid();
            var listError = new Dictionary<string, string>()
            {
                { nameof(User), string.Format(BaseValidation.ObjectNotExistById, nameof(User), id) }
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
            bool isExistUser = false,
            bool isExistUserById = false,
            bool isExistRole = false,
            bool isExistRoleById = false
            )
        {
            _serviceUser.Setup(x => x.ExistAsync(It.IsAny<User>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistUser);
            _serviceUser.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistUserById);

            _serviceRole.Setup(x => x.ExistAsync(It.IsAny<Role>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistRole);
            _serviceRole.Setup(x => x.ExistByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())).ReturnsAsync(isExistRoleById);

            _validation = new UserValidation(_serviceUser.Object, _serviceRole.Object);
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

        private static string GetString(int length) => new Randomizer().GetString(length);
    }
}
