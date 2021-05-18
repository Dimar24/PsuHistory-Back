using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Tests.Services
{
    [TestFixture]
    class UserServiceTest
    {
        private PsuHistoryDbContext _dbContext;
        private IUserService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PsuHistoryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            _dbContext = new PsuHistoryDbContext(options.Options);
            _service = new UserService(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task TestInsertAndGetAsync()
        {
            // Arrange
            var random = new Random(0);
            var number = random.Next(5);
            var entity = GetList()[number];
            await _service.InsertAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Entry(entity.Role).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;
            _dbContext.Entry(result.Role).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.Mail == result.Mail);
                Assert.IsTrue(entity.Password == result.Password);
                Assert.IsTrue(entity.RoleId == result.RoleId);
                Assert.IsTrue(entity.CreatedAt == result.CreatedAt);
                Assert.IsTrue(entity.UpdatedAt == result.UpdatedAt);
            });
        }

        [Test]
        public async Task TestGetAllAsync()
        {
            // Arrange
            var random = new Random(0);
            var count = random.Next(5);
            var entityList = GetList().Take(count);
            foreach (var entity in entityList)
            {
                await _service.InsertAsync(entity);
            }

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.AreEqual(entityList.Count(), result.Count());
        }

        [Test]
        public async Task TestUpdateAsync()
        {
            // Arrange
            var random = new Random(0);
            var number = random.Next(5);
            var entity = GetList()[number];
            await _service.InsertAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Entry(entity.Role).State = EntityState.Detached;

            entity = new User()
            {
                Id = entity.Id,
                Mail = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                RoleId = Guid.NewGuid(),
                Role = GetRole(),
                CreatedAt = DateTime.Now.AddMinutes(60),
                UpdatedAt = DateTime.Now.AddMinutes(60)
            };
            await _service.UpdateAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Entry(entity.Role).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;
            _dbContext.Entry(result.Role).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.Mail == result.Mail);
                Assert.IsTrue(entity.Password == result.Password);
                Assert.IsTrue(entity.RoleId == result.RoleId);
                Assert.IsTrue(entity.CreatedAt == result.CreatedAt);
                Assert.IsTrue(entity.UpdatedAt == result.UpdatedAt);
            });
        }

        [Test]
        public async Task TestDeleteAsync()
        {
            // Arrange
            var random = new Random(0);
            var number = random.Next(5);
            var entity = GetList()[number];
            entity = await _service.InsertAsync(entity);
            var id = entity.Id;
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Entry(entity.Role).State = EntityState.Detached;

            // Act
            var entityExsist = await _service.GetAsync(id);
            _dbContext.Entry(entityExsist).State = EntityState.Detached;
            _dbContext.Entry(entityExsist.Role).State = EntityState.Detached;
            await _service.DeleteAsync(id);
            var entityNotExsist = await _service.GetAsync(id) ?? null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNull(entityNotExsist);
                Assert.IsNotNull(entityExsist);
            });
        }


        private List<User> GetList()
        {
            return new List<User>()
            {
                new User()
                {
                    RoleId = Guid.NewGuid(),
                    Role = GetRole(),
                    Mail = Guid.NewGuid().ToString(),
                    Password = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new User()
                {
                    RoleId = Guid.NewGuid(),
                    Role = GetRole(),
                    Mail = Guid.NewGuid().ToString(),
                    Password = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new User()
                {
                    RoleId = Guid.NewGuid(),
                    Role = GetRole(),
                    Mail = Guid.NewGuid().ToString(),
                    Password = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now.AddDays(5),
                    UpdatedAt = DateTime.Now.AddDays(5)
                },
                new User()
                {
                    RoleId = Guid.NewGuid(),
                    Role = GetRole(),
                    Mail = Guid.NewGuid().ToString(),
                    Password = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now.AddDays(-2),
                    UpdatedAt = DateTime.Now.AddDays(-3)
                },
                new User()
                {
                    RoleId = Guid.NewGuid(),
                    Role = GetRole(),
                    Mail = Guid.NewGuid().ToString(),
                    Password = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }

        private Role GetRole()
        {
            return new Role()
            {
                Name = "adminka",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
