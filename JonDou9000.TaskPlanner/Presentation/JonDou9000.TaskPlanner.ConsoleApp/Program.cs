using JonDou9000.TaskPlanner.Domain.Models;
using System;
using JonDou9000.TaskPlanner.DataAccess.JonDou9000.TaskPlanner.DataAccess;

internal static class Program
{
    public static void Main(string[] args)
    {
        var repository = new FileWorkItemsRepository();

        while (true)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("[A]dd work item");
            Console.WriteLine("[B]uild a plan");
            Console.WriteLine("[M]ark work item as completed");
            Console.WriteLine("[R]emove a work item");
            Console.WriteLine("[Q]uit the app");

            var input = Console.ReadLine().ToLower();

            switch (input)
            {
                case "a":
                    AddWorkItem(repository);
                    break;
                case "b":
                    BuildPlan(repository);
                    break;
                case "m":
                    MarkWorkItemAsCompleted(repository);
                    break;
                case "r":
                    RemoveWorkItem(repository);
                    break;
                case "q":
                    repository.SaveChanges();
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private static void AddWorkItem(FileWorkItemsRepository repository)
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine();

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

        repository.Add(workItem);
        Console.WriteLine("Work item added.");
    }

    private static void BuildPlan(FileWorkItemsRepository repository)
    {
        var planner = new SimpleTaskPlanner();
        var workItems = repository.GetAll();

        if (workItems.Length == 0)
        {
            Console.WriteLine("No work items to plan.");
            return;
        }

        var sortedItems = planner.CreatePlan(workItems);
        Console.WriteLine("\nSorted Work Items:");
        foreach (var item in sortedItems)
        {
            Console.WriteLine(item);
        }
    }

    private static void MarkWorkItemAsCompleted(FileWorkItemsRepository repository)
    {
        Console.Write("Enter the ID of the work item to mark as completed: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid workItemId))
        {
            var item = repository.Get(workItemId);
            if (item != null)
            {
                item.IsCompleted = true;
                repository.Update(item);
                Console.WriteLine("Work item marked as completed.");
            }
            else
            {
                Console.WriteLine("Work item not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }
    }

    private static void RemoveWorkItem(FileWorkItemsRepository repository)
    {
        Console.Write("Enter the ID of the work item to remove: ");
        if (Guid.TryParse(Console.ReadLine(), out Guid workItemId))
        {
            if (repository.Remove(workItemId))
            {
                Console.WriteLine("Work item removed.");
            }
            else
            {
                Console.WriteLine("Work item not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }
    }
}
