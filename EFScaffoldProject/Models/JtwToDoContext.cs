using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JTWToDo.Data.Models
{
    public partial class JtwToDoContext : DbContext
    {
        public virtual DbSet<ToDo> ToDo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=tcp:jtwconsultingco.database.windows.net,1433;Initial Catalog=JtwToDo;Persist Security Info=False;User ID=jtwadmin;Password=fom@chf48UuCc3js;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>(entity =>
            {
                entity.Property(e => e.ConcurrencyVersion)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            });
        }
    }
}
