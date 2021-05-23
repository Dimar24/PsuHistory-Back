using PsuHistory.Data.Domain.Models.Monuments;
using PsuHistory.Data.Domain.Models.Histories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using PsuHistory.Data.EF.SQL.Configuration;
using PsuHistory.Data.Domain.Models.Users;

namespace PsuHistory.Data.EF.SQL
{
    public class PsuHistoryDbContext : DbContext
    {
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

        public PsuHistoryDbContext(DbContextOptions<PsuHistoryDbContext> options) 
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder
            //    .UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Вынести потом
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
        }
    }
}
