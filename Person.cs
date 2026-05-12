using System;

namespace University.Common.Models
{
    // Делегат
    public delegate void PersonEventHandler(string message);

    public abstract class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public static int TotalPersonsCreated { get; private set; } = 0;

        // Подія
        public event PersonEventHandler? OnPersonCreated;

        protected Person()
        {
            Id = Guid.NewGuid();
            TotalPersonsCreated++;

            // Викликаємо подію
            OnPersonCreated?.Invoke($"Створено особу: {GetFullName()}");
        }

        public virtual string GetFullName() => $"{FirstName} {LastName}";

        // Статичний метод
        public static void ShowTotalPersons()
        {
            Console.WriteLine($"Загальна кількість створених осіб: {TotalPersonsCreated}");
        }

        static Person()
        {
            Console.WriteLine("=== Клас Person був ініціалізований ===");
        }
    }
}
