using BusinessLogic.Interfaces.Base;
using Shared.Entities;
using Shared.Models;
using Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services.Base
{
    public class BaseCrudService<T> : IBaseCrudService<T> where T : PersistentObject, new()
    {
        protected readonly IRepository<T> Repository;

        public BaseCrudService(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual IQueryable<T> GetAll()
        {
            return typeof(IDeletableObject).IsAssignableFrom(typeof(T))
                ? Repository.GetAll().Where(x => ((IDeletableObject)x).Deleted == false)
                : Repository.GetAll();
        }

        public virtual T Get(long id)
        {
            return typeof(IDeletableObject).IsAssignableFrom(typeof(T))
                ? Repository.GetAll().FirstOrDefault(x => x.Id == id && ((IDeletableObject)x).Deleted == false)
                : Repository.GetAll().FirstOrDefault(x => x.Id == id);
        }

        public virtual T GetWithDeleted(long id)
        {
            return Repository.GetAll().FirstOrDefault(x => x.Id == id);
        }

        public virtual void Create(T item)
        {
            if (item == null)
                throw new ArgumentException("Object cannot be null, while creating object");

            if (item.Id != 0)
                throw new ArgumentException("You cannot create object with ID greater than 0");

            Repository.Add(item);
        }

        public virtual void Deleted(long id)
        {
            if (id == 0)
                throw new ArgumentException("Не возможно удалить объект с ID равным 0");

            Deleted(Get(id));
        }

        public virtual void Deleted(T item)
        {
            if (item == null)
                throw new ArgumentException("Не возможно удалить Null объект");

            if (typeof(IDeletableObject).IsAssignableFrom(typeof(T)) == false)
            {
                Repository.Remove(item);
                return;
            }

            var deletableItem = (IDeletableObject)item;

            deletableItem.Deleted = true;
            item = (T)deletableItem;
            Update(item);
        }

        public virtual void Update(T item)
        {
            if ((item?.Id ?? 0) == 0)
            {
                throw new Exception("Для обновление объекта требуется идентификатор");
            }

            Repository.Update(item);
        }

        public void Update(T item, long id)
        {
            if (id == 0)
            {
                throw new Exception("Для обновления объекта требуется идентификатор");
            }

            Repository.Update(item, id);
        }
    }
}
