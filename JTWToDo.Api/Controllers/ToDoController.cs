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
        [HttpGet]
        public IEnumerable<ToDo> GetAll(int clientId)
        {
            IEnumerable<ToDo> todos = BusinessFactory.ToDoFactory.CreateService<IToDoService>(DataContext).GetAll() as IEnumerable<ToDo>;

            return todos;
        }

        [Route("Get/{id}")]
        [HttpGet]
        public IEnumerable<ToDo> Get(int id)
        {
            return BusinessFactory.ToDoFactory.CreateService<IToDoService>(DataContext).Get(id) as IEnumerable<ToDo>;
        }

        [Route("Post")]
        [HttpPost]
        //TODO: This should really return some sort of response object which includes information about errors which occur
        public void Post([FromBody]ToDo entity)
        {
            BusinessFactory.ToDoFactory.CreateService<IToDoService>(DataContext).Update((ToDo)entity);
        }

        [Route("Put/{id}")]
        [HttpPut]
        //TODO: This should really return some sort of response object which includes information about errors which occur
        public void Put(int id, [FromBody]ToDo entity)
        {
            BusinessFactory.ToDoFactory.CreateService<IToDoService>(DataContext).Update((ToDo)entity);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        //TODO: This should really return some sort of response object which includes information about errors which occur
        public void Delete(int id)
        {
            BusinessFactory.ToDoFactory.CreateService<IToDoService>(DataContext).Delete(id);
        }
    }
}
