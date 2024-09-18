using JonDou9000.TaskPlanner.Domain.Models;
using JonDou9000.TaskPlanner.DataAccess; // Підключаємо репозиторій
using System;

internal static class Program
{
    public static void Main(string[] args)
    {
        // Створюємо екземпляр репозиторію, який буде працювати з файлом work-items.json
        var repository = new FileWorkItemsRepository();

        while (true)
        {
            Console.Write("Enter title (or type 'exit' to finish): ");
            string title = Console.ReadLine();
            if (title.ToLower() == "exit") break;

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            Console.Write("Enter creation date (dd.MM.yyyy): ");
            DateTime creationDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter due date (dd.MM.yyyy): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter priority (None, Low, Medium, High, Urgent): ");
            Priority priority = Enum.Parse<Priority>(Console.ReadLine(), true);

            Console.Write("Enter complexity (None, Minutes, Hours, Days, Weeks): ");
            Complexity complexity = Enum.Parse<Complexity>(Console.ReadLine(), true);

            // Створюємо новий WorkItem і додаємо його в репозиторій
            var workItem = new WorkItem
            {
                Title = title,
                Description = description,
                CreationDate = creationDate,
                DueDate = dueDate,
                Priority = priority,
                Complexity = complexity,
                IsCompleted = false
            };

            repository.Add(workItem); // Додаємо завдання в репозиторій
        }

        // Зберігаємо всі введені завдання у файл
        repository.SaveChanges();

        // Отримуємо всі збережені завдання з репозиторію
        var allItems = repository.GetAll();

        Console.WriteLine("\nAll Work Items:");
        foreach (var item in allItems)
        {
            Console.WriteLine(item);
        }
    }
}
