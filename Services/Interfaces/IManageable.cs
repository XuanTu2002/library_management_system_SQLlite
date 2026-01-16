using System.Collections.Generic;

namespace LibraryManagement.Services.Interfaces
{
    public interface IManageable<T>
    {
        void Add(T item);
        void Update(string id, T item);
        void Delete(string id);
        T? GetById(string id);
        List<T> GetAll();
    }
}