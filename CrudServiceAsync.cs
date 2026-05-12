using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using University.Common.Models;

namespace University.Common.Services
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T>
    {
        private readonly List<T> _items = new();
        private readonly object _lock = new object(); // для thread-safety
        private readonly string _filePath;

        public CrudServiceAsync(string filePath = null)
        {
            _filePath = filePath ?? $"data_{typeof(T).Name}.json";
        }

        public async Task<bool> CreateAsync(T element)
        {
            if (element == null) return false;

            await Task.Run(() =>
            {
                lock (_lock)
                {
                    _items.Add(element);
                }
            });

            Console.WriteLine($"[+] Асинхронно створено {typeof(T).Name}");
            return true;
        }

        public async Task<T?> ReadAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    var idProperty = typeof(T).GetProperty("Id");
                    return _items.FirstOrDefault(item =>
                    {
                        var itemId = (Guid?)idProperty?.GetValue(item);
                        return itemId == id;
                    });
                }
            });
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    return _items.ToList();
                }
            });
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    return _items.Skip(page * amount).Take(amount).ToList();
                }
            });
        }

        public async Task<bool> UpdateAsync(T element)
        {
            if (element == null) return false;

            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    var idProperty = typeof(T).GetProperty("Id");
                    if (idProperty == null) return false;

                    var id = (Guid)idProperty.GetValue(element)!;
                    var index = _items.FindIndex(item => (Guid)idProperty.GetValue(item)! == id);

                    if (index != -1)
                    {
                        _items[index] = element;
                        return true;
                    }
                    return false;
                }
            });
        }

        public async Task<bool> RemoveAsync(T element)
        {
            if (element == null) return false;

            return await Task.Run(() =>
            {
                lock (_lock)
                {
                    return _items.Remove(element);
                }
            });
        }

        public async Task<bool> SaveAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    lock (_lock)
                    {
                        string json = JsonSerializer.Serialize(_items, new JsonSerializerOptions
                        {
                            WriteIndented = true
                        });
                        File.WriteAllText(_filePath, json);
                        Console.WriteLine($"[💾] Дані збережено у файл: {_filePath}");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка збереження: {ex.Message}");
                    return false;
                }
            });
        }

        // Реалізація IEnumerable<T>
        public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
