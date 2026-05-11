using System;
using System.Collections.Generic;

namespace University.Common.Models
{
    public class Teacher : Person
    {
        public string Department { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public List<string> Subjects { get; set; } = new();

        public Teacher() : base() { }

        public override string GetFullName()
        {
            return $"Викладач: {base.GetFullName()} ({Department})";
        }

        public void AddSubject(string subject)
        {
            Subjects.Add(subject);
            Console.WriteLine($"Предмет '{subject}' додано викладачу {GetFullName()}");
        }

        public void ShowSubjects()
        {
            Console.WriteLine($"Викладач {GetFullName()} викладає такі предмети:");
            if (Subjects.Count == 0)
            {
                Console.WriteLine("   (немає предметів)");
            }
            else
            {
                foreach (var subject in Subjects)
                {
                    Console.WriteLine($"   - {subject}");
                }
            }
            Console.WriteLine();
        }

        // === Статичний метод CreateNew() для лабораторної №2 ===
        public static Teacher CreateNew()
        {
            var random = new Random();
            string[] firstNames = { "Іван", "Олена", "Сергій", "Наталія", "Андрій", "Тетяна", "Максим" };
            string[] lastNames = { "Шевченко", "Коваленко", "Мельник", "Петренко", "Григоренко", "Бондар" };
            string[] departments = { "Комп'ютерних наук", "Математики", "Фізики", "Біології", "Історії", "Економіки" };

            var teacher = new Teacher
            {
                FirstName = firstNames[random.Next(firstNames.Length)],
                LastName = lastNames[random.Next(lastNames.Length)],
                Department = departments[random.Next(departments.Length)],
                Salary = 18000 + random.Next(25000)  // від 18000 до 42999
            };

            // Додаємо 1-3 предмети
            int subjectCount = random.Next(1, 4);
            for (int i = 0; i < subjectCount; i++)
            {
                teacher.AddSubject($"Предмет {random.Next(101, 999)}");
            }

            return teacher;
        }
    }
}
