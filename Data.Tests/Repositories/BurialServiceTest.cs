using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.EF.SQL;
using PsuHistory.Data.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Tests.Repositories
{
    [TestFixture]
    class BurialRepositoryTest
    {
        private PsuHistoryDbContext _dbContext;
        private IBurialRepository _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PsuHistoryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            _dbContext = new PsuHistoryDbContext(options.Options);
            _service = new BurialRepository(_dbContext);
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
            _dbContext.Entry(entity.TypeBurial).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;
            _dbContext.Entry(result.TypeBurial).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.NumberBurial == result.NumberBurial);
                Assert.IsTrue(entity.Location == result.Location);
                Assert.IsTrue(entity.KnownNumber == result.KnownNumber);
                Assert.IsTrue(entity.UnknownNumber == result.UnknownNumber);
                Assert.IsTrue(entity.Year == result.Year);
                Assert.IsTrue(entity.Latitude == result.Latitude);
                Assert.IsTrue(entity.Longitude == result.Longitude);
                Assert.IsTrue(entity.Description == result.Description);
                Assert.IsTrue(entity.TypeBurialId == result.TypeBurialId);
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
            _dbContext.Entry(entity.TypeBurial).State = EntityState.Detached;

            entity = new Burial()
            {
                Id = entity.Id,
                NumberBurial = 10,
                Location = "ул. Ленина 42, Глубокое",
                KnownNumber = 1,
                UnknownNumber = 1,
                Year = 2000,
                Latitude = 28.01228,
                Longitude = 32.01228,
                Description = "Описание 221",
                TypeBurialId = Guid.NewGuid(),
                TypeBurial = GetTypeBurial(),
                CreatedAt = DateTime.Now.AddMinutes(60),
                UpdatedAt = DateTime.Now.AddMinutes(60)
            };
            await _service.UpdateAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Entry(entity.TypeBurial).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;
            _dbContext.Entry(result.TypeBurial).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.NumberBurial == result.NumberBurial);
                Assert.IsTrue(entity.Location == result.Location);
                Assert.IsTrue(entity.KnownNumber == result.KnownNumber);
                Assert.IsTrue(entity.UnknownNumber == result.UnknownNumber);
                Assert.IsTrue(entity.Year == result.Year);
                Assert.IsTrue(entity.Latitude == result.Latitude);
                Assert.IsTrue(entity.Longitude == result.Longitude);
                Assert.IsTrue(entity.Description == result.Description);
                Assert.IsTrue(entity.TypeBurialId == result.TypeBurialId);
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
            _dbContext.Entry(entity.TypeBurial).State = EntityState.Detached;

            // Act
            var entityExsist = await _service.GetAsync(id);
            _dbContext.Entry(entityExsist).State = EntityState.Detached;
            _dbContext.Entry(entityExsist.TypeBurial).State = EntityState.Detached;
            await _service.DeleteAsync(id);
            var entityNotExsist = await _service.GetAsync(id) ?? null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNull(entityNotExsist);
                Assert.IsNotNull(entityExsist);
            });
        }


        private List<Burial> GetList()
        {
            return new List<Burial>()
            {
                new Burial()
                {
                    NumberBurial = 1,
                    Location = "ул. Блохина 29, Новополоцк 211440",
                    KnownNumber = 10,
                    UnknownNumber = 10,
                    Year = 2001,
                    Latitude = 28.01,
                    Longitude = 32.01,
                    Description = "Описание 1",
                    TypeBurialId = Guid.NewGuid(),
                    TypeBurial = GetTypeBurial(),
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new Burial()
                {
                    NumberBurial = 2,
                    Location = "Molodezhnaya 69, Новополоцк",
                    KnownNumber = 20,
                    UnknownNumber = 10,
                    Year = 2002,
                    Latitude = 28.02,
                    Longitude = 32.02,
                    Description = "Описание 2",
                    TypeBurialId = Guid.NewGuid(),
                    TypeBurial = GetTypeBurial(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Burial()
                {
                    NumberBurial = 3,
                    Location = "Замковый пр., Полоцк",
                    KnownNumber = 30,
                    UnknownNumber = 10,
                    Year = 2003,
                    Latitude = 28.03,
                    Longitude = 32.03,
                    Description = "Описание 3",
                    TypeBurialId = Guid.NewGuid(),
                    TypeBurial = GetTypeBurial(),
                    CreatedAt = DateTime.Now.AddDays(5),
                    UpdatedAt = DateTime.Now.AddDays(5)
                },
                new Burial()
                {
                    NumberBurial = 4,
                    Location = "Замковая ул. 1, Полоцк",
                    KnownNumber = 40,
                    UnknownNumber = 10,
                    Year = 2004,
                    Latitude = 28.04,
                    Longitude = 32.04,
                    Description = "Описание 4",
                    TypeBurialId = Guid.NewGuid(),
                    TypeBurial = GetTypeBurial(),
                    CreatedAt = DateTime.Now.AddDays(-2),
                    UpdatedAt = DateTime.Now.AddDays(-3)
                },
                new Burial()
                {
                    NumberBurial = 5,
                    Location = "Вильнюсское ш. 1, Полоцк",
                    KnownNumber = 50,
                    UnknownNumber = 10,
                    Year = 2005,
                    Latitude = 28.05,
                    Longitude = 32.05,
                    Description = "Описание 5",
                    TypeBurialId = Guid.NewGuid(),
                    TypeBurial = GetTypeBurial(),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }

        private TypeBurial GetTypeBurial()
        {
            return new TypeBurial()
            {
                Name = "Тип",
                CreatedAt = DateTime.Now.AddDays(-5),
                UpdatedAt = DateTime.Now.AddDays(-5)
            };
        }
    }
}
