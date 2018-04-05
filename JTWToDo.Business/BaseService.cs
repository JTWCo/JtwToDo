using JTWToDo.Data;
using System.Collections.Generic;

namespace JTWToDo.Business
{
    public abstract class BaseService : IBaseService
    {
        public ITodoDataContext DbContext;

        protected BaseService(ITodoDataContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract IEnumerable<BaseEntity> GetAll();
        public abstract IEnumerable<BaseEntity> Get(int id);
        public abstract void Update(BaseEntity entity);
        public abstract void Delete(int id);
    }
}