using System.Linq;

namespace WebSocket.BusinessLogic.Interfaces.Base
{
    public interface IBaseCrudService <T> 
    {
        IQueryable<T> GetAll();
        T Get(long id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        void Delete(long id);
    }
}
