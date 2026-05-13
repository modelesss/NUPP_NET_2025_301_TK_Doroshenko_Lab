using System;
using System.Collections.Generic;

namespace University.Common.Services
{
    public interface ICrudService<T>
    {
        void Create(T element);
        T Read(Guid id);
        IEnumerable<T> ReadAll();
        void Update(T element);
        void Remove(T element);
        void RemoveById(Guid id);
    }
}
