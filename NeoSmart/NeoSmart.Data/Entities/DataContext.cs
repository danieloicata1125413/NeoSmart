using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NeoSmart.ClassLibraries.Entities;
using NeoSmart.ClassLibraries.Interfaces;

namespace NeoSmart.Data.Entities
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Training>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Topic>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(s => new { s.CountryId, s.Name }).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.StateId, c.Name }).IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(c => c.Name).IsUnique();
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is ISoftDetete && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

            foreach (var entity in entities)
            {
                DateTime DateNow = DateTime.Now;
                if (entity.State == EntityState.Added)
                {
                    ((ISoftDetete)entity.Entity).Created = DateNow;
                }
                ((ISoftDetete)entity.Entity).Updated = DateNow;
                if (entity.State == EntityState.Deleted)
                {
                    ((ISoftDetete)entity.Entity).Deleted = DateNow;
                    entity.State = EntityState.Modified;
                }
            }
        }
    }
}
