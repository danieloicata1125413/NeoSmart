﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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


        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<RequestUser> RequestUsers { get; set; }

        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingImage> TrainingImages { get; set; }
        public DbSet<TrainingResource> TrainingResources { get; set; }
        public DbSet<TrainingExam> TrainingExams { get; set; }
        public DbSet<TrainingExamQuestion> TrainingExamQuestions { get; set; }
        public DbSet<TrainingExamQuestionOption> TrainingExamQuestionOptions { get; set; }
        public DbSet<TrainingTopic> TrainingTopics { get; set; }


        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionExam> SessionExams { get; set; }
        public DbSet<SessionInscriptionExam> SessionInscriptionExams { get; set; }
        public DbSet<SessionInscriptionExamAnswer> SessionInscriptionExamAnswers { get; set; }
        public DbSet<SessionInscriptionStatus> SessionInscriptionStatus { get; set; }
        public DbSet<SessionInscriptionAttend> SessionInscriptionAttends { get; set; }
        public DbSet<SessionInscriptionTemporal> SessionInscriptionTemporals { get; set; }
        public DbSet<SessionInscription> SessionInscriptions { get; set; }
        public DbSet<SessionStatus> SessionStatus { get; set; }

        public DbSet<UserFirebaseToken> AspNetUserFirebaseTokens { get; set; }
        public DbSet<UserNotification> AspNetUserNotifications { get; set; }
        public DbSet<UserTokenReset> AspNetUserTokenResets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>().HasIndex(c => c.Nit).IsUnique();
            modelBuilder.Entity<Process>().HasIndex(p => new { p.CompanyId, p.Description }).IsUnique();
            modelBuilder.Entity<Occupation>().HasIndex(o => new { o.ProcessId, o.Description }).IsUnique();
            modelBuilder.Entity<Formation>().HasIndex(f => new { f.CompanyId, f.Description }).IsUnique();
            modelBuilder.Entity<Topic>().HasIndex(t => new { t.CompanyId, t.Description }).IsUnique();
            modelBuilder.Entity<Training>().HasIndex(t => new { t.ProcessId, t.Description }).IsUnique();
            modelBuilder.Entity<SessionStatus>().HasIndex(c => c.Name).IsUnique();

            modelBuilder.Entity<SessionInscription>().HasIndex(t => new { t.SessionId, t.UserId }).IsUnique();
            modelBuilder.Entity<SessionInscriptionAttend>().HasIndex(t => t.SessionInscriptionId).IsUnique();


            modelBuilder.Entity<RequestStatus>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<RequestUser>().HasIndex(t => new { t.RequestId, t.UserId }).IsUnique();

            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(s => new { s.CountryId, s.Name }).IsUnique();
            modelBuilder.Entity<City>().HasIndex(c => new { c.StateId, c.Name }).IsUnique();
            modelBuilder.Entity<DocumentType>().HasIndex(d => d.Name).IsUnique();
            DisableCascadingDelete(modelBuilder);
        }

        private void DisableCascadingDelete(ModelBuilder modelBuilder)
        {
            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys());
            foreach (var relationship in relationships)
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
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
