using System;
using JTWToDo.Business.Interfaces;
using JTWToDo.Data;

namespace JTWToDo.Business
{
    public class BusinessFactory
    {
        public static class ToDoFactory
        {
            public static BaseService CreateService<T>(ITodoDataContext context)
            {
                if (typeof(T).IsAssignableFrom(typeof(IToDoService)))
                {
                    return new ToDoService(context);
                }

                throw new ArgumentException(string.Format("Invalid Service Type: {0}", typeof(T).GetType().Name), string.Empty);
            }


        }
    }
}