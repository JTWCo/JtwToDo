using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JTWToDo.Data
{
    public class ToDoDataContext : DbContext, ITodoDataContext
    {
        public DbSet<ToDo> ToDo { get; set; }

        public ToDoDataContext(DbContextOptions<ToDoDataContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");  //TODO: move this out of a "magic string" into config

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

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> dbSet<T>() where T : class
        {
            return Set<T>();
        }

        public override int SaveChanges()
        {
            Audit();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            Audit();
            return await base.SaveChangesAsync();
        }

        private void Audit()
        {
            var entries = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).LastUpdated = DateTime.UtcNow;
            }
        }
    }
}