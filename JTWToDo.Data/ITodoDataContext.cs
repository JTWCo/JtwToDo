using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JTWToDo.Data
{
    public interface ITodoDataContext : IDisposable
    {
        DbSet<T> dbSet<T>() where T : class;
       DbSet<ToDo> ToDo { get; set; }

        int SaveChanges();
        Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;
    }
}
