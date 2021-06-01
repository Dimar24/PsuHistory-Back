using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Domain.Models.Users;
using PsuHistory.Data.EF.SQL.Configuration;
using System;

namespace PsuHistory.Data.EF.SQL.Context
{
    public abstract class DbContextBase : DbContext
    {
        /// <summary>
        /// Метод используется для тестов
        /// </summary>
        /// <param name="options"></param>
        protected DbContextBase(DbContextOptions options) : base(options)
        { }

        public DbSet<Burial> Burials { get; set; }
        public DbSet<TypeBurial> TypeBurials { get; set; }
        public DbSet<AttachmentBurial> AttachmentBurials { get; set; }
        public DbSet<Victim> Victims { get; set; }
        public DbSet<TypeVictim> TypeVictims { get; set; }
        public DbSet<DutyStation> DutyStations { get; set; }
        public DbSet<BirthPlace> BirthPlaces { get; set; }
        public DbSet<ConscriptionPlace> ConscriptionPlaces { get; set; }

        public DbSet<Form> Forms { get; set; }
        public DbSet<AttachmentForm> AttachmentForms { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TODO Вынести потом, Возможно
            modelBuilder.ApplyConfiguration(new AttachmentBurialConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentFormConfiguration());
            modelBuilder.ApplyConfiguration(new BirthPlaceConfiguration());
            modelBuilder.ApplyConfiguration(new BurialConfiguration());
            modelBuilder.ApplyConfiguration(new ConscriptionPlaceConfiguration());
            modelBuilder.ApplyConfiguration(new DutyStationConfiguration());
            modelBuilder.ApplyConfiguration(new FormConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new TypeBurialConfiguration());
            modelBuilder.ApplyConfiguration(new TypeVictimConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VictimConfiguration());

            //TODO Вынести потом, Возможно
            var date = DateTime.UtcNow;

            var roleOwner = new Role() { Id = Guid.NewGuid(), Name = "Owner", CreatedAt = date, UpdatedAt = date };
            var roleAdmin = new Role() { Id = Guid.NewGuid(), Name = "Admin", CreatedAt = date, UpdatedAt = date };
            var roleModerator = new Role() { Id = Guid.NewGuid(), Name = "Moderator", CreatedAt = date, UpdatedAt = date };

            modelBuilder.Entity<Role>().HasData(new Role[] { roleOwner, roleAdmin, roleModerator });

            var passwordHash = "AQAAAAEAACcQAAAAENAnCVyWq0lo9yySX3Ka7WMkN6jmIjUBKz1CohwrKt5ngJpr5Pq4fY4sLSXWs3ul/A=="; // Password = "secret"

            var userOwner = new User() { Id = Guid.NewGuid(), Mail = "Owner", Password = passwordHash, RoleId = roleOwner.Id, /*Role = roleOwner,*/ CreatedAt = date, UpdatedAt = date };
            var userAdmin = new User() { Id = Guid.NewGuid(), Mail = "Admin", Password = passwordHash, RoleId = roleAdmin.Id, /*Role = roleAdmin,*/ CreatedAt = date, UpdatedAt = date };
            var userModerator = new User() { Id = Guid.NewGuid(), Mail = "Moderator", Password = passwordHash, RoleId = roleModerator.Id, /*Role = roleModerator,*/ CreatedAt = date, UpdatedAt = date };

            modelBuilder.Entity<User>().HasData(new User[] { userOwner, userAdmin, userModerator });
        }


    }
}
