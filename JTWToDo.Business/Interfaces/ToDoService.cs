using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using JTWToDo.Data;
using Microsoft.EntityFrameworkCore;

namespace JTWToDo.Business.Interfaces
{
    public class ToDoService : BaseService
    {
        public ToDoService(ITodoDataContext dbContext) : base(dbContext) { }

        public override IEnumerable<BaseEntity> GetAll()
        {
            IEnumerable<ToDo> todos;
            using (DbContext)
            {
                IQueryable<ToDo> query = DbContext.ToDo;
                todos = query.ToList();
            }

            return todos;
        }

        public override IEnumerable<BaseEntity> Get(int id)
        {
            IEnumerable<ToDo> todos;
            using (DbContext)
            {
                IQueryable<ToDo> query = DbContext.ToDo.Where(todo => todo.Id == id);
                todos = query.ToList();
            }

            return todos;
        }

        public override void Update(BaseEntity entity)
        {
            using (DbContext)
            {

                //all of this should be extracted out to the base class and made more generic, but in the interest of time, doing this by brute force here
                if (entity.Id == 0)
                {
                    DbContext.Add<ToDo>((ToDo) entity);
                }

                ToDo existingToDo = Get(entity.Id).FirstOrDefault() as ToDo;

                ToDo updatedToDo = entity as ToDo;
                if (existingToDo != null)
                {
                    if (existingToDo.ConcurrencyVersion == updatedToDo.ConcurrencyVersion)
                    {
                        existingToDo.DueDate = updatedToDo.DueDate;
                        existingToDo.Completed = updatedToDo.Completed;
                        existingToDo.DateCreated = updatedToDo.DateCreated;
                        existingToDo.Description = updatedToDo.Description;
                        existingToDo.Notes = updatedToDo.Notes;
                        existingToDo.LastUpdated = DateTime.Now;
                    }
                    else
                    {  throw new DBConcurrencyException("Concurrency Exception");}
                }
                
                DbContext.SaveChanges();
            }
        }
    }
}
