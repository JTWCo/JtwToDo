using System;
using Microsoft.EntityFrameworkCore;

namespace JTWToDo.Data
{
    public interface ITodoDataContext : IDisposable
    {
        DbSet<T> dbSet<T>() where T : class;
        DbSet<ToDo> ToDo { get; set; }
    }
}
