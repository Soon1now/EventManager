using EventManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Data;

public class EventManagerDbContext : DbContext
{
    public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Event> Events => Set<Event>();
    public DbSet<EventType> EventType => Set<EventType>();
    public DbSet<EventStatus> EventStatus => Set<EventStatus>();

    public DbSet<User> Users => Set<User>();
    public DbSet<Branch> Branches => Set<Branch>();

    public DbSet<EventBranch> EventBranches => Set<EventBranch>();
    public DbSet<Registration> Registrations => Set<Registration>();
    public DbSet<RegistrationStatus> RegistrationStatuses => Set<RegistrationStatus>();

    public DbSet<Question> Questions => Set<Question>();
    public DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();

    public DbSet<ContestWork> ContestWorks => Set<ContestWork>();
    public DbSet<ContestChoice> ContestChoices => Set<ContestChoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventType>()
            .ToTable("EventType");

        modelBuilder.Entity<EventStatus>()
            .ToTable("EventStatus");

        modelBuilder.Entity<QuestionType>()
            .ToTable("QuestionType");

        modelBuilder.Entity<RegistrationStatus>()
            .ToTable("RegistrationStatus");


        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branches");

            entity.HasKey(b => b.Id);

            entity.Property(b => b.Name)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(b => b.City)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(b => b.IsActive)
                .HasDefaultValue(true)
                .IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(u => u.Id);

            entity.Property(u => u.FullName)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(u => u.Email)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .IsRequired();

            entity.Property(u => u.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .IsRequired();

            entity.HasOne(u => u.Branch)
                .WithMany()
                .HasForeignKey(u => u.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Events");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(e => e.Description);

            entity.HasOne(e => e.EventType)
                .WithMany()
                .HasForeignKey(e => e.EventTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .IsRequired();
        });

        modelBuilder.Entity<EventBranch>(entity =>
        {
            entity.ToTable("EventBranchs");

            entity.HasKey(eb => new { eb.EventId, eb.BranchId });

            entity.HasOne(eb => eb.Event)
                .WithMany()
                .HasForeignKey(eb => eb.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(eb => eb.Branch)
                .WithMany()
                .HasForeignKey(eb => eb.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.ToTable("Registrations");

            entity.HasKey(r => r.Id);

            entity.HasIndex(r => new { r.UserId, r.EventId })
                .IsUnique();

            entity.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Event)
                .WithMany()
                .HasForeignKey(r => r.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.Status)
                .WithMany()
                .HasForeignKey(r => r.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(r => r.RegistratedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .IsRequired();
        });

        modelBuilder.Entity<ContestWork>(entity =>
        {
            entity.ToTable("ContestWorks");

            entity.HasKey(w => w.Id);

            entity.Property(w => w.Title)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(w => w.ContentURL)
                .HasMaxLength(2048)
                .IsRequired();

            entity.Property(w => w.Description);

            entity.HasOne(w => w.Registration)
                .WithMany()
                .HasForeignKey(w => w.RegistrationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(w => w.CreatedAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .IsRequired();
        });

        modelBuilder.Entity<ContestChoice>(entity =>
        {
            entity.ToTable("ContestChoices");

            entity.HasKey(c => c.Id);

            entity.HasOne(c => c.Work)
                .WithMany()
                .HasForeignKey(c => c.WorkId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(c => c.ChoiceAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .IsRequired();
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Questions");

            entity.HasKey(q => q.Id);

            entity.Property(q => q.Text)
                .IsRequired();

            entity.HasOne(q => q.Event)
                .WithMany()
                .HasForeignKey(q => q.EventId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(q => q.Type)
                .WithMany()
                .HasForeignKey(q => q.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answers");

            entity.HasKey(a => a.Id);

            entity.Property(a => a.Text)
                .IsRequired();

            entity.HasOne(a => a.Question)
                .WithMany()
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserAnswer>(entity =>
        {
            entity.ToTable("UserAnswers");

            entity.HasKey(ua => ua.Id);

            entity.HasIndex(ua => new { ua.RegistrationId, ua.QuestionId })
                .IsUnique();

            entity.HasOne(ua => ua.Registration)
                .WithMany()
                .HasForeignKey(ua => ua.RegistrationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ua => ua.Answer)
                .WithMany()
                .HasForeignKey(ua => ua.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(ua => ua.Question)
                .WithMany()
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(ua => ua.AnsweredAt)
                .HasDefaultValueSql("SYSUTCDATETIME()")
                .IsRequired();
        });
    }
}