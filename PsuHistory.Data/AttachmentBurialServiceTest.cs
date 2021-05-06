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
    public class AttachmentBurialServiceTest
    {
        private PsuHistoryDbContext _dbContext;
        private IAttachmentBurialService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PsuHistoryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new PsuHistoryDbContext(options);
            _service = new AttachmentBurialService(_dbContext);
        }

        [TearDown]
        public void Teardown()
        {
            _dbContext.Dispose();
        }

        [TestCase("fc6f9c07-1f12-4be0-a666-bd874963b08d", "402ec4ba-829c-4840-83f9-6ef78b972256", "/files/images", ".png")]
        [TestCase("fc6f9c07-1f12-4be0-a666-bd874963b08d", "e17ebcd2-bdef-4203-a960-a6874bc76d0b", "/files/images", ".jpg")]
        [TestCase("fc6f9c07-1f12-4be0-a666-bd874963b08d", "f18c260d-2b57-4947-a38c-af602d2a0848", "/files/images", ".svg")]
        public async Task TestInsertAndGetAsync(Guid burialId, string fileName, string filePath, string fileType)
        {
            // Arrange
            var entity = new AttachmentBurial()
            {
                BurialId = burialId,
                FileName = fileName,
                FilePath = filePath,
                FileType = fileType,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _service.InsertAsync(entity);

            // Act
            var result = await _service.GetAsync(entity.Id);

            // Assert
            Assert.IsTrue(
                entity.FileName == result.FileName &&
                entity.FilePath == result.FilePath &&
                entity.FileType == result.FileType &&
                entity.BurialId == result.BurialId &&
                entity.CreatedAt == result.CreatedAt &&
                entity.UpdatedAt == result.UpdatedAt
                );
        }

        [TestCase(5)]
        [TestCase(2)]
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

        [TestCase("402ec4ba-829c-4840-83f9-6ef78b972256", "/files/images", ".png", "fc6f9c07-1f12-4be0-a666-bd874963b08d")]
        [TestCase("e17ebcd2-bdef-4203-a960-a6874bc76d0b", "/files/images", ".jpg", "fc6f9c07-1f12-4be0-a666-bd874963b08d")]
        [TestCase("f18c260d-2b57-4947-a38c-af602d2a0848", "/files/images", ".svg", "fc6f9c07-1f12-4be0-a666-bd874963b08d")]
        public async Task TestUpdateAsync(string fileName, string filePath, string fileType, string newFileName)
        {
            // Arrange
            var entity = new AttachmentBurial()
            {
                BurialId = Guid.NewGuid(),
                FileName = fileName,
                FilePath = filePath,
                FileType = fileType,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _service.InsertAsync(entity);

            entity = new AttachmentBurial()
            {
                Id = entity.Id,
                BurialId = Guid.NewGuid(),
                FileName = newFileName,
                FilePath = filePath,
                FileType = fileType,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _service.UpdateAsync(entity);

            // Act
            var result = await _service.GetAsync(entity.Id);

            // Assert
            Assert.IsTrue(
                entity.FileName == result.FileName &&
                entity.FilePath == result.FilePath &&
                entity.FileType == result.FileType &&
                entity.BurialId == result.BurialId &&
                entity.CreatedAt == result.CreatedAt &&
                entity.UpdatedAt == result.UpdatedAt
                );
        }

        [TestCase(3)]
        [TestCase(2)]
        [TestCase(0)]
        public async Task TestDeleteAsync(int number)
        {
            // Arrange
            var entity = GetList()[number];
            await _service.InsertAsync(entity);

            // Act
            var entityExsist = await _service.GetAsync(entity.Id) ?? null;
            await _service.DeleteAsync(entity.Id);
            var entityNotExsist = await _service.GetAsync(entity.Id) ?? null;

            // Assert
            Assert.IsTrue(
                entityExsist is not null &&
                entityNotExsist is null
                );
        }

        private List<AttachmentBurial> GetList()
        {
            return new List<AttachmentBurial>()
            {
                new AttachmentBurial()
                {
                    BurialId = Guid.NewGuid(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".png",
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new AttachmentBurial()
                {
                    BurialId = Guid.NewGuid(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".png",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new AttachmentBurial()
                {
                    BurialId = Guid.NewGuid(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".jpg",
                    CreatedAt = DateTime.Now.AddDays(5),
                    UpdatedAt = DateTime.Now.AddDays(5)
                },
                new AttachmentBurial()
                {
                    BurialId = Guid.NewGuid(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".png",
                    CreatedAt = DateTime.Now.AddDays(-2),
                    UpdatedAt = DateTime.Now.AddDays(-3)
                },
                new AttachmentBurial()
                {
                    BurialId = Guid.NewGuid(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".svg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }
    }
}