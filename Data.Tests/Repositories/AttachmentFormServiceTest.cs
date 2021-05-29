using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.EF.SQL.Context;
using PsuHistory.Data.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Tests.Repositories
{
    [TestFixture]
    class AttachmentFormRepositoryTest
    {
        private DbContextBase _dbContext;
        private IAttachmentFormRepository _service;

        [SetUp]
        public void Setup()
        {
            _dbContext = FakeDbContext.GetInstance();
            _service = new AttachmentFormRepository(_dbContext);
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
            _dbContext.Entry(entity.Form).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;
            _dbContext.Entry(result.Form).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.FileName == result.FileName);
                Assert.IsTrue(entity.FilePath == result.FilePath);
                Assert.IsTrue(entity.FileType == result.FileType);
                Assert.IsTrue(entity.FormId == result.FormId);
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
            _dbContext.Entry(entity.Form).State = EntityState.Detached;

            entity = new AttachmentForm()
            {
                Id = entity.Id,
                FormId = Guid.NewGuid(),
                Form = GetForm(),
                FileName = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now.AddMinutes(60),
                UpdatedAt = DateTime.Now.AddMinutes(60)
            };
            await _service.UpdateAsync(entity);
            _dbContext.Entry(entity).State = EntityState.Detached;
            _dbContext.Entry(entity.Form).State = EntityState.Detached;

            // Act
            var result = await _service.GetAsync(entity.Id);
            _dbContext.Entry(result).State = EntityState.Detached;
            _dbContext.Entry(result.Form).State = EntityState.Detached;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsTrue(entity.FileName == result.FileName);
                Assert.IsTrue(entity.FilePath == result.FilePath);
                Assert.IsTrue(entity.FileType == result.FileType);
                Assert.IsTrue(entity.FormId == result.FormId);
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
            _dbContext.Entry(entity.Form).State = EntityState.Detached;

            // Act
            var entityExsist = await _service.GetAsync(id);
            _dbContext.Entry(entityExsist).State = EntityState.Detached;
            _dbContext.Entry(entityExsist.Form).State = EntityState.Detached;
            await _service.DeleteAsync(id);
            var entityNotExsist = await _service.GetAsync(id) ?? null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNull(entityNotExsist);
                Assert.IsNotNull(entityExsist);
            });
        }


        private List<AttachmentForm> GetList()
        {
            return new List<AttachmentForm>()
            {
                new AttachmentForm()
                {
                    FormId = Guid.NewGuid(),
                    Form = GetForm(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".png",
                    CreatedAt = DateTime.Now.AddDays(-5),
                    UpdatedAt = DateTime.Now.AddDays(-5)
                },
                new AttachmentForm()
                {
                    FormId = Guid.NewGuid(),
                                        Form = GetForm(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".png",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new AttachmentForm()
                {
                    FormId = Guid.NewGuid(),
                    Form = GetForm(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".jpg",
                    CreatedAt = DateTime.Now.AddDays(5),
                    UpdatedAt = DateTime.Now.AddDays(5)
                },
                new AttachmentForm()
                {
                    FormId = Guid.NewGuid(),
                    Form = GetForm(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".png",
                    CreatedAt = DateTime.Now.AddDays(-2),
                    UpdatedAt = DateTime.Now.AddDays(-3)
                },
                new AttachmentForm()
                {
                    FormId = Guid.NewGuid(),
                    Form = GetForm(),
                    FileName = Guid.NewGuid().ToString(),
                    FilePath = "/files/images",
                    FileType = ".svg",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            };
        }

        private Form GetForm()
        {
            return new Form()
            {
                FirstName = "Имя",
                LastName = "Фамилия",
                MiddleName = "Отчество",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }
    }
}
