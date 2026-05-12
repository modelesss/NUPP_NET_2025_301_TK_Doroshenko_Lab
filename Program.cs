using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using University.Common.Models;
using University.Common.Services;
using University.Common.Extensions;

namespace University.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота №2: Асинхронний CRUD + Parallel ===\n");

            // Створюємо асинхронний сервіс
            var studentService = new CrudServiceAsync<Student>("students.json");

            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine("Паралельне створення 1000 студентів...\n");

            // Паралельне створення 1000 студентів
            await Parallel.ForEachAsync(
                Enumerable.Range(1, 1000),
                new ParallelOptions { MaxDegreeOfParallelism = 20 }, // обмежуємо кількість потоків
                async (i, token) =>
                {
                    var student = Student.CreateNew();
                    await studentService.CreateAsync(student);
                });

            stopwatch.Stop();

            // Результати
            var allStudents = (await studentService.ReadAllAsync()).ToList();

            Console.WriteLine($"Час виконання: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine($"Успішно створено студентів: {allStudents.Count}");
            Console.WriteLine($"Мінімальний GPA: {allStudents.Min(s => s.GPA)}");
            Console.WriteLine($"Максимальний GPA: {allStudents.Max(s => s.GPA)}");
            Console.WriteLine($"Середній GPA: {allStudents.Average(s => s.GPA):F2}");

            // Збереження у файл
            await studentService.SaveAsync();

            Console.WriteLine("\n=== Лабораторна робота №2 виконана успішно! ===");
            Console.ReadKey();
        }
    }
}
