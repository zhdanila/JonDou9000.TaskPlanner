using System;
using System.Collections.Generic;

internal static class Program
{
    public static void Main(string[] args)
    {
        var workItems = new List<WorkItem>();

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

            workItems.Add(new WorkItem
            {
                Title = title,
                Description = description,
                CreationDate = creationDate,
                DueDate = dueDate,
                Priority = priority,
                Complexity = complexity,
                IsCompleted = false
            });
        }

        var planner = new SimpleTaskPlanner();
        var sortedItems = planner.CreatePlan(workItems.ToArray());

        Console.WriteLine("\nSorted Work Items:");
        foreach (var item in sortedItems)
        {
            Console.WriteLine(item);
        }
    }
}
