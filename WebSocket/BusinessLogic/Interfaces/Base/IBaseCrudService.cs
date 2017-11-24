using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces.Base
{
    public interface IBaseCrudService<T>
    {
        IQueryable<T> GetAll();
        T Get(long id);
        T GetWithDeleted(long id);
        void Create(T item);
        void Update(T item);
        void Update(T item, long id);
        void Deleted(T item);
        void Deleted(long id);
    }
}
