using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using JTWToDo.Data;

namespace JTWToDo.Business
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

        //ToDo:  This should really return some sort of result object that includes information about any errors that occur and/or the updated entity
        public override void Update(BaseEntity entity)
        {
            using (DbContext)
            {
                var updatedToDo = entity as ToDo;

                //TODO:all of this should be abstracted out to the base class and made more generic 
                //(likely using reflection to iterate through the entity properties)
                //, but in the interest of time, doing this by brute force here
                if (entity.Id == 0)
                {
                    if (updatedToDo != null)
                    {
                        updatedToDo.DateCreated = DateTime.Now;
                        DbContext.Add(updatedToDo);
                    }
                }

                var existingToDo = DbContext.ToDo.FirstOrDefault(todo => todo.Id == entity.Id);

                if (existingToDo != null)
                {
                    if (updatedToDo != null && existingToDo.ConcurrencyVersion.SequenceEqual(updatedToDo.ConcurrencyVersion))
                    {
                        existingToDo.DueDate = updatedToDo.DueDate;
                        existingToDo.Completed = updatedToDo.Completed;
                        existingToDo.Description = updatedToDo.Description;
                        existingToDo.Notes = updatedToDo.Notes;
                        existingToDo.LastUpdated = DateTime.Now;
                    }
                    else
                    { throw new DBConcurrencyException("Concurrency Exception"); }
                }
                DbContext.SaveChanges();
            }
        }

        //ToDo:  This should really return some sort of result object that includes information about any errors that occur
        public override void Delete(int id)
        {
            using (DbContext)
            {

                //TODO: extract out to the base class and make more generic
                ToDo existingToDo = DbContext.ToDo.FirstOrDefault(todo => todo.Id == id);

                if (existingToDo != null)
                {
                    DbContext.ToDo.Remove(existingToDo);
                    DbContext.SaveChanges();
                }
            }
        }
    }
}