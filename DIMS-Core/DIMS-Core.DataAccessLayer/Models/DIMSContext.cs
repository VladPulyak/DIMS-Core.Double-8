using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DIMS_Core.DataAccessLayer.Models
{
    public partial class DIMSContext : DbContext
    {
        public DIMSContext()
        {
        }

        public DIMSContext(DbContextOptions<DIMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Direction> Directions { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskState> TaskStates { get; set; }
        public virtual DbSet<TaskTrack> TaskTracks { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UserTask> UserTasks { get; set; }
        public virtual DbSet<VTask> VTasks { get; set; }
        public virtual DbSet<VUserProfile> VUserProfiles { get; set; }
        public virtual DbSet<VUserProgress> VUserProgresses { get; set; }
        public virtual DbSet<VUserTask> VUserTasks { get; set; }
        public virtual DbSet<VUserTrack> VUserTracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=DIMS;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Direction>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TaskState>(entity =>
            {
                entity.HasKey(e => e.StateId)
                    .HasName("PK_StateId");

                entity.ToTable("TaskState");

                entity.HasIndex(e => e.StateName, "UniqueStateNmae")
                    .IsUnique();

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TaskTrack>(entity =>
            {
                entity.ToTable("TaskTrack");

                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.UserTask)
                    .WithMany(p => p.TaskTracks)
                    .HasForeignKey(d => d.UserTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTaskId");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_UserProfiles_UserId");

                entity.Property(e => e.Address).HasMaxLength(120);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Skype).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Direction)
                    .WithMany(p => p.UserProfiles)
                    .HasForeignKey(d => d.DirectionId);
            });

            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.ToTable("UserTask");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StateId");

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaskId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTasks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserId");
            });

            modelBuilder.Entity<VTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TaskId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VUserProfile>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserProfiles");

                entity.Property(e => e.Address).HasMaxLength(120);

                entity.Property(e => e.Direction)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(101);

                entity.Property(e => e.MobilePhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Skype).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("date");
            });

            modelBuilder.Entity<VUserProgress>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserProgress");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(101);
            });

            modelBuilder.Entity<VUserTask>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserTask");

                entity.Property(e => e.DeadlineDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VUserTrack>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vUserTrack");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TrackDate).HasColumnType("datetime");

                entity.Property(e => e.TrackNote)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
