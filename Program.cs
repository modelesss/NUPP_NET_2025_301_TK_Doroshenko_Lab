using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using University.Common.Models;
using University.Common.Services;
using University.Infrastructure.Data;
using University.Infrastructure.Models;
using University.Infrastructure.Repositories;
using University.Infrastructure.Services;

namespace University.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота №3: Entity Framework Core ===\n");

            // Налаштування DI та DbContext
            var services = new ServiceCollection();

            services.AddDbContext<UniversityContext>(options =>
                options.UseSqlite("Data Source=university.db"));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(ICrudServiceAsync<>), typeof(CrudServiceAsync<>));

            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<UniversityContext>();
            await context.Database.MigrateAsync(); // застосовуємо міграції

            var studentRepo = scope.ServiceProvider.GetRequiredService<IRepository<StudentEntity>>();
            var studentService = new CrudServiceAsync<StudentEntity>(studentRepo);

            // === Тестування ===
            var stopwatch = Stopwatch.StartNew();

            Console.WriteLine("Створення 50 студентів у базі даних...");

            for (int i = 0; i < 50; i++)
            {
                var student = Student.CreateNew(); // з Common

                var studentEntity = new StudentEntity
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.DateOfBirth,
                    StudentId = student.StudentId,
                    Course = student.Course,
                    GPA = student.GPA
                };

                await studentService.CreateAsync(studentEntity);
            }

            stopwatch.Stop();

            var allStudents = (await studentService.ReadAllAsync()).ToList();

            Console.WriteLine($"Час виконання: {stopwatch.ElapsedMilliseconds} мс");
            Console.WriteLine($"Студентів у БД: {allStudents.Count}");
            Console.WriteLine($"Середній GPA: {allStudents.Average(s => s.GPA):F2}");

            Console.WriteLine("\n=== Лабораторна №3 виконана! ===");
            Console.ReadKey();
        }
    }
}
