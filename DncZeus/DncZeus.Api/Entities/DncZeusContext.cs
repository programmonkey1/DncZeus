using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DncZeus.Api.Entities
{
    public partial class DncZeusContext : DbContext
    {
        public DncZeusContext()
        {
        }

        public DncZeusContext(DbContextOptions<DncZeusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DncWorkTask> DncWorkTask { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DncZeus;User ID=sa;Password=147258");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DncWorkTask>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompletionTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InformationNote)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MattersNeedingAttention)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProgressDeviation).HasColumnType("datetime2(2)");

                entity.Property(e => e.ProjectManager)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Publisher)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaskContent)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaskPerson)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaskPlan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaskTheme)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaskTime).HasColumnType("datetime2(2)");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdPartyCooperation)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
