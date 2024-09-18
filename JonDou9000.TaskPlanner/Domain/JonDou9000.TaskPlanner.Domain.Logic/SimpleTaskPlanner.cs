using JonDou9000.TaskPlanner.DataAccess.JonDou9000.TaskPlanner.DataAccess.Abstractions;
using JonDou9000.TaskPlanner.Domain.JonDou9000.TaskPlanner.Domain.Models;

public class SimpleTaskPlanner
{
    private readonly IWorkItemsRepository _repository;

    // Конструктор, що приймає репозиторій
    public SimpleTaskPlanner(IWorkItemsRepository repository)
    {
        _repository = repository;
    }

    // Створення плану задач
    public WorkItem[] CreatePlan()
    {
        var items = _repository.GetAll(); // Отримуємо всі задачі з репозиторія

        if (items.Length == 0)
        {
            return Array.Empty<WorkItem>();
        }

        var itemsAsList = items.ToList();
        itemsAsList.Sort(CompareWorkItems);
        return itemsAsList.ToArray();
    }

    // Порівняння задач
    private static int CompareWorkItems(WorkItem firstItem, WorkItem secondItem)
    {
        int priorityComparison = secondItem.Priority.CompareTo(firstItem.Priority);
        if (priorityComparison != 0) return priorityComparison;

        int dueDateComparison = firstItem.DueDate.CompareTo(secondItem.DueDate);
        if (dueDateComparison != 0) return dueDateComparison;

        return string.Compare(firstItem.Title, secondItem.Title, StringComparison.OrdinalIgnoreCase);
    }
}
