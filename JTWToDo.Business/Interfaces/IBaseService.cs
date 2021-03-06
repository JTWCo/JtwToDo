﻿using JTWToDo.Data;
using System.Collections.Generic;

namespace JTWToDo.Business
{
    public interface IBaseService
    {
        IEnumerable<BaseEntity> GetAll();
        IEnumerable<BaseEntity> Get(int id);
        void Update(BaseEntity entity);
        void Delete(int id);
    }
}