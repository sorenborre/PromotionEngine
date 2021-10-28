using System;
using System.Collections.Generic;

namespace PromotionEngine.Persistance
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public List<T> GetAll(Func<T, bool> action);

    }
}
