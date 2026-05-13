using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using University.Common.Services;
using University.Infrastructure.Data;
using University.Infrastructure.Repositories;

namespace University.Infrastructure.Services
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        private readonly IRepository<T> _repository;
        private readonly string _filePath; // для резервного збереження

        public CrudServiceAsync(IRepository<T> repository, string filePath = null)
        {
            _repository = repository;
            _filePath = filePath ?? $"backup_{typeof(T).Name}.json";
        }

        public async Task<bool> CreateAsync(T element)
        {
            if (element == null) return false;
            await _repository.AddAsync(element);
            await _repository.SaveChangesAsync();
            Console.WriteLine($"[+] Асинхронно створено {typeof(T).Name} в БД");
            return true;
        }

        public async Task<T?> ReadAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            var all = await ReadAllAsync();
            return all.Skip(page * amount).Take(amount);
        }

        public async Task<bool> UpdateAsync(T element)
        {
            if (element == null) return false;
            await _repository.UpdateAsync(element);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            if (element == null) return false;
            await _repository.DeleteAsync(element);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveAsync()
        {
            // Додаткове збереження у JSON як backup
            try
            {
                var all = await ReadAllAsync();
                string json = JsonSerializer.Serialize(all, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(_filePath, json);
                Console.WriteLine($"[💾] Backup збережено у {_filePath}");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerator<T> GetEnumerator() => ReadAllAsync().Result.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
