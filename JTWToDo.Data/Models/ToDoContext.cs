using Microsoft.EntityFrameworkCore;

namespace JTWToDo.Data.Models
{
    public class ToDoContext : DbContext
    {
        public virtual DbSet<ToDo> ToDo { get; set; }

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