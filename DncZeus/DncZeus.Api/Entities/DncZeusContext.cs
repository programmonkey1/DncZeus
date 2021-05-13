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
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddMattersNeedingAttention).IsUnicode(false);

                entity.Property(e => e.AddThirdPartyCooperation).IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CompletionEndTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompletionFirstTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompletionTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InformationNote)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MattersNeedingAttention).IsUnicode(false);

                entity.Property(e => e.No1).HasColumnName("NO1");

                entity.Property(e => e.No10).HasColumnName("NO10");

                entity.Property(e => e.No11).HasColumnName("NO11");

                entity.Property(e => e.No12).HasColumnName("NO12");

                entity.Property(e => e.No13).HasColumnName("NO13");

                entity.Property(e => e.No14).HasColumnName("NO14");

                entity.Property(e => e.No15).HasColumnName("NO15");

                entity.Property(e => e.No16).HasColumnName("NO16");

                entity.Property(e => e.No17).HasColumnName("NO17");

                entity.Property(e => e.No18).HasColumnName("NO18");

                entity.Property(e => e.No19).HasColumnName("NO19");

                entity.Property(e => e.No2).HasColumnName("NO2");

                entity.Property(e => e.No20).HasColumnName("NO20");

                entity.Property(e => e.No21).HasColumnName("NO21");

                entity.Property(e => e.No22).HasColumnName("NO22");

                entity.Property(e => e.No23).HasColumnName("NO23");

                entity.Property(e => e.No24).HasColumnName("NO24");

                entity.Property(e => e.No25).HasColumnName("NO25");

                entity.Property(e => e.No26).HasColumnName("NO26");

                entity.Property(e => e.No27).HasColumnName("NO27");

                entity.Property(e => e.No28).HasColumnName("NO28");

                entity.Property(e => e.No29).HasColumnName("NO29");

                entity.Property(e => e.No3).HasColumnName("NO3");

                entity.Property(e => e.No30).HasColumnName("NO30");

                entity.Property(e => e.No31).HasColumnName("NO31");

                entity.Property(e => e.No4).HasColumnName("NO4");

                entity.Property(e => e.No5).HasColumnName("NO5");

                entity.Property(e => e.No6).HasColumnName("NO6");

                entity.Property(e => e.No7).HasColumnName("NO7");

                entity.Property(e => e.No8).HasColumnName("NO8");

                entity.Property(e => e.No9).HasColumnName("NO9");

                entity.Property(e => e.PlanList)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProgressDeviation)
                    .HasMaxLength(50)
                    .IsUnicode(false);

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

                entity.Property(e => e.TaskTime)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdPartyCooperation).IsUnicode(false);
            });
        }
    }
}
