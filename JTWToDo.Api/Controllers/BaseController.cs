using JTWToDo.Data;
using Microsoft.AspNetCore.Mvc;

namespace JTWToDo.Api.Controllers
{
    public class BaseController : Controller
    {
        public ITodoDataContext DataContext;

        public BaseController(ITodoDataContext context)
        {
            DataContext = context;
        }
    }
}