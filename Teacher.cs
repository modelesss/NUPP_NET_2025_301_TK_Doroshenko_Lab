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

        // Цей метод був відсутній
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
    }
}
