using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Tests.Services
{
    [TestFixture]
    class TypeVictimServiceTest
    {
        private PsuHistoryDbContext _dbContext;
        private ITypeVictimService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PsuHistoryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            _dbContext = new PsuHistoryDbContext(options.Options);
            _service = new TypeVictimService(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task TestInsertAndGetAsync_Succes()
        {
            // Arrange
            var random = new Random(0);
            var number = random.Next(5);
            var entity = GetList()[number];
            await _service.InsertAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.Name == result.Name);
                Assert.IsTrue(entity.CreatedAt == result.CreatedAt);
                Assert.IsTrue(entity.UpdatedAt == result.UpdatedAt);
            });
        }

        [Test]
        public async Task TestGetAllAsync_Succes()
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
        public async Task TestExistAsync_Succes()
        {
            // Arrange
            var entity = GetTypeVictim("Тип № Пять");
            await _service.InsertAsync(entity);

            // Act
            var result = await _service.ExistAsync(entity);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestExistByIdAsync_Succes()
        {
            // Arrange
            var entity = GetTypeVictim("Тип № Пять");
            await _service.InsertAsync(entity);

            // Act
            var result = await _service.ExistByIdAsync(entity.Id);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task TestUpdateAsync_Succes()
        {
            // Arrange
            var random = new Random(0);
            var number = random.Next(5);
            var entity = GetList()[number];
            await _service.InsertAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;

            entity = new TypeVictim()
            {
                Id = entity.Id,
                Name = "Тип № Тест",
                CreatedAt = DateTime.Now.AddMinutes(60),
                UpdatedAt = DateTime.Now.AddMinutes(60)
            };
            await _service.UpdateAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.Name == result.Name);
                Assert.IsTrue(entity.CreatedAt == result.CreatedAt);
                Assert.IsTrue(entity.UpdatedAt == result.UpdatedAt);
            });
        }

        [Test]
        public async Task TestDeleteAsync_Succes()
        {
            // Arrange
            var random = new Random(0);
            var number = random.Next(5);
            var entity = GetList()[number];
            entity = await _service.InsertAsync(entity);
            var id = entity.Id;
            _dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            var entityExsist = await _service.GetAsync(id);
            _dbContext.Entry(entityExsist).State = EntityState.Detached;
            await _service.DeleteAsync(id);
            var entityNotExsist = await _service.GetAsync(id) ?? null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNull(entityNotExsist);
                Assert.IsNotNull(entityExsist);
            });
        }

        private TypeVictim GetTypeVictim(
            string name = null
            )
        {
            return new TypeVictim()
            {
                Name = name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        private List<TypeVictim> GetList()
        {
            return new List<TypeVictim>()
            {
                new TypeVictim()
                {
                    Name = "Тип № Один",
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new TypeVictim()
                {
                    Name = "Тип № Два",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new TypeVictim()
                {
                    Name = "Тип № Три",
                    CreatedAt = DateTime.Now.AddDays(5),
                    UpdatedAt = DateTime.Now.AddDays(5)
                },
                new TypeVictim()
                {
                    Name = "Тип № Четыри",
                    CreatedAt = DateTime.Now.AddDays(-2),
                    UpdatedAt = DateTime.Now.AddDays(-3)
                },
                new TypeVictim()
                {
                    Name = "Тип № Пять",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }
    }
}
