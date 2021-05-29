using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL.Context;
using PsuHistory.Data.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Tests.Repositories
{
    [TestFixture]
    class ConscriptionPlaceRepositoryTest
    {
        private DbContextBase _dbContext;
        private IConscriptionPlaceRepository _service;

        [SetUp]
        public void Setup()
        {
            _dbContext = FakeDbContext.GetInstance();
            _service = new ConscriptionPlaceRepository(_dbContext);
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

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.Place == result.Place);
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

            entity = new ConscriptionPlace()
            {
                Id = entity.Id,
                Place = "ул. Ленина 42, Глубокое",
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
                Assert.IsTrue(entity.Place == result.Place);
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


        private List<ConscriptionPlace> GetList()
        {
            return new List<ConscriptionPlace>()
            {
                new ConscriptionPlace()
                {
                    Place = "ул. Блохина 29, Новополоцк 211440",
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new ConscriptionPlace()
                {
                    Place = "Molodezhnaya 69, Новополоцк",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new ConscriptionPlace()
                {
                    Place = "Замковый пр., Полоцк",
                    CreatedAt = DateTime.Now.AddDays(5),
                    UpdatedAt = DateTime.Now.AddDays(5)
                },
                new ConscriptionPlace()
                {
                    Place = "Замковая ул. 1, Полоцк",
                    CreatedAt = DateTime.Now.AddDays(-2),
                    UpdatedAt = DateTime.Now.AddDays(-3)
                },
                new ConscriptionPlace()
                {
                    Place = "Вильнюсское ш. 1, Полоцк",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }
    }
}
