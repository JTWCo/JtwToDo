using JTWToDo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using JTWToDo.Business;

namespace JTWToDo.Api.Controllers
{
    [Produces("application/json")]
    [Route("jtwtodoapi/ToDo")]
    public class ToDoController : BaseController
    {
        public ToDoController(ITodoDataContext context) : base(context) { }

        [Route("GetAll")]
        public IEnumerable<ToDo> GetAll(int clientId)
        {
            IEnumerable<ToDo> todos = BusinessFactory.ToDoFactory.CreateService<IToDoService>(DataContext).GetAll() as IEnumerable<ToDo>;

            return todos.OrderByDescending(x => x.DueDate);
        }

        [Route("Get/{id}")]
        public IEnumerable<ToDo> Get(int id)
        {
            return BusinessFactory.ToDoFactory.CreateService<IToDoService>(DataContext).Get(id) as IEnumerable<ToDo>;
        }

        // POST: api/ToDo
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/ToDo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]ToDo entity)
        {
            BusinessFactory.ToDoFactory.CreateService<IToDoService>(DataContext).Update(entity);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
