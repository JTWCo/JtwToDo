using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JTWToDo.Data;

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
    }
}
