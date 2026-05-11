using System;
using University.Common.Models;
using University.Common.Services;
using University.Common.Extensions;

namespace University.ConsoleApp  // ← Змінили namespace
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== Лабораторна робота: Університет ===\n");
            Console.WriteLine("=== Лабораторна робота: Університет ===\n");

            // Створюємо сервіси
            var studentService = new CrudService<Student>();
            var teacherService = new CrudService<Teacher>();
            var courseService = new CrudService<Course>();

            // === 1. Створення об'єктів ===
            Console.WriteLine("Створюємо викладачів...");
            var teacher1 = new Teacher
            {
                FirstName = "Іван",
                LastName = "Шевченко",
                Department = "Комп'ютерних наук",
                Salary = 25000
            };
            teacher1.AddSubject("Програмування");
            teacher1.AddSubject("Бази даних");

            var teacher2 = new Teacher
            {
                FirstName = "Олена",
                LastName = "Коваленко",
                Department = "Математики",
                Salary = 22000
            };

            Console.WriteLine("Створюємо студентів...");
            var student1 = new Student
            {
                FirstName = "Петро",
                LastName = "Мельник",
                Course = 3,
                GPA = 4.7
            };

            var student2 = new Student
            {
                FirstName = "Анна",
                LastName = "Бондар",
                Course = 2,
                GPA = 4.2
            };

            Console.WriteLine("Створюємо курси...");
            var course1 = new Course
            {
                Title = "C# Програмування",
                Description = "Основи сучасної розробки на C#",
                Credits = 6,
                Teacher = teacher1
            };

            // === 2. Використання CRUD ===
            Console.WriteLine("\n=== CRUD операції ===");

            studentService.Create(student1);
            studentService.Create(student2);
            teacherService.Create(teacher1);
            courseService.Create(course1);

            Console.WriteLine($"\nКількість студентів: {studentService.Count()}");
            Console.WriteLine($"Кількість викладачів: {teacherService.Count()}");

            // Вивід студентів
            var students = studentService.ReadAll().ToList();
            students.PrintAll();

            teacher1.ShowSubjects();
            course1.ShowInfo();

            Person.ShowTotalPersons();

            Console.WriteLine("\n=== Лабораторна робота виконана успішно! ===");
            Console.ReadKey();
        }
    }
}
