using Microsoft.EntityFrameworkCore;
using PsuHistory.Data.Domain.Models.Histories;
using PsuHistory.Data.Domain.Models.Monuments;

namespace PsuHistory.Data.EF.SQL
{
    public class PsuHistoryDbContextPostgreSQL : DbContext
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

        public PsuHistoryDbContextPostgreSQL(DbContextOptions<PsuHistoryDbContextPostgreSQL> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
