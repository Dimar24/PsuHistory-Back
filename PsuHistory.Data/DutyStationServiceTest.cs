using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsuHistory.Data
{
    [TestFixture]
    class DutyStationServiceTest
    {
        private PsuHistoryDbContext _dbContext;
        private IDutyStationService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PsuHistoryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            _dbContext = new PsuHistoryDbContext(options.Options);
            _service = new DutyStationService(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            _dbContext.Dispose();
        }

        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        [TestCase(0)]
        public async Task TestInsertAndGetAsync(int number)
        {
            // Arrange
            var entity = GetList()[number];
            await _service.InsertAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;

            // Assert
            Assert.IsTrue(
                entity.Place == result.Place &&
                entity.CreatedAt == result.CreatedAt &&
                entity.UpdatedAt == result.UpdatedAt
                );
        }

        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        [TestCase(0)]
        public async Task TestGetAllAsync(int count)
        {
            // Arrange
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

        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        [TestCase(0)]
        public async Task TestUpdateAsync(int number)
        {
            // Arrange
            var entity = GetList()[number];
            await _service.InsertAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;

            entity = new DutyStation()
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
            Assert.IsTrue(
                entity.Place == result.Place &&
                entity.CreatedAt == result.CreatedAt &&
                entity.UpdatedAt == result.UpdatedAt
                );
        }

        [TestCase(4)]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        [TestCase(0)]
        public async Task TestDeleteAsync(int number)
        {
            // Arrange
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
            Assert.IsTrue(
                entityExsist is not null &&
                entityNotExsist is null
                );
        }


        private List<DutyStation> GetList()
        {
            return new List<DutyStation>()
            {
                new DutyStation()
                {
                    Place = "ул. Блохина 29, Новополоцк 211440",
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new DutyStation()
                {
                    Place = "Molodezhnaya 69, Новополоцк",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new DutyStation()
                {
                    Place = "Замковый пр., Полоцк",
                    CreatedAt = DateTime.Now.AddDays(5),
                    UpdatedAt = DateTime.Now.AddDays(5)
                },
                new DutyStation()
                {
                    Place = "Замковая ул. 1, Полоцк",
                    CreatedAt = DateTime.Now.AddDays(-2),
                    UpdatedAt = DateTime.Now.AddDays(-3)
                },
                new DutyStation()
                {
                    Place = "Вильнюсское ш. 1, Полоцк",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }
    }
}
