﻿namespace PharmacuticalE_Commerce.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int? id);
        IEnumerable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(int? id);
    }
}
