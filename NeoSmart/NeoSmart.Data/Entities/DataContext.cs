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
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<FormationOccupation> FormationOccupations { get; set; }
        public DbSet<FormationTopic> FormationTopics { get; set; }

        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<ResourceType> ResourceTypes { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicExam> TopicExams { get; set; }
        public DbSet<TopicExamQuestion> TopicExamQuestions { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingImage> TrainingImages { get; set; }
        public DbSet<TrainingResource> TrainingResources { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }
        public DbSet<TrainingSessionAttend> TrainingSessionAttends { get; set; }
        public DbSet<TrainingSessionInscription> TrainingSessionInscriptions { get; set; }
        public DbSet<TrainingSessionInscriptionTemporal> TrainingSessionInscriptionTemporals { get; set; }
        public DbSet<TrainingTopic> TrainingTopics { get; set; }
        public DbSet<TrainingTopicExam> TrainingTopicExams { get; set; }
        public DbSet<UserTopicExam> UserTopicExams { get; set; }
        public DbSet<UserTopicExamAnswer> UserTopicExamAnswers { get; set; }

        
        
        
        
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Process>().HasIndex(c => c.Cod).IsUnique();
            modelBuilder.Entity<Occupation>().HasIndex(s => new { s.ProcessId, s.Cod }).IsUnique();
            modelBuilder.Entity<Formation>().HasIndex(s => new { s.Cod }).IsUnique();
            modelBuilder.Entity<Training>().HasIndex(s => new { s.Cod }).IsUnique();
            modelBuilder.Entity<Topic>().HasIndex(c => c.Description).IsUnique();
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
