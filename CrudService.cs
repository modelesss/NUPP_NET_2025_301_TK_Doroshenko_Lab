using System;
using System.Collections.Generic;
using System.Linq;
using University.Common.Models;

namespace University.Common.Services
{
    public class CrudService<T> : ICrudService<T>
    {
        private readonly List<T> _items = new List<T>();

        public void Create(T element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            _items.Add(element);
            Console.WriteLine($"[+] Об'єкт {typeof(T).Name} успішно створено");
        }

        public T Read(Guid id)
        {
            // Для простоти будемо шукати по Id (працює для Person та Course)
            if (typeof(T).GetProperty("Id")?.GetValue(_items.FirstOrDefault()) is Guid)
            {
                var result = _items.FirstOrDefault(item =>
                    (Guid)typeof(T).GetProperty("Id")!.GetValue(item)! == id);

                if (result == null)
                    Console.WriteLine($"[-] Об'єкт з Id {id} не знайдено");

                return result;
            }
            return default!;
        }

        public IEnumerable<T> ReadAll()
        {
            return _items;
        }

        public void Update(T element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null) return;

            var id = (Guid)idProperty.GetValue(element)!;
            var index = _items.FindIndex(item => (Guid)idProperty.GetValue(item)! == id);

            if (index != -1)
            {
                _items[index] = element;
                Console.WriteLine($"[~] Об'єкт {typeof(T).Name} оновлено");
            }
        }

        public void Remove(T element)
        {
            if (_items.Remove(element))
                Console.WriteLine($"[-] Об'єкт {typeof(T).Name} видалено");
        }

        public void RemoveById(Guid id)
        {
            var item = Read(id);
            if (item != null)
                Remove(item);
        }

        public int Count() => _items.Count;
    }
}
