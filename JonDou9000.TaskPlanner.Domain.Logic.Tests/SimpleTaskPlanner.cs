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

    // Метод CreatePlan без параметрів, що використовує дані з репозиторію
    public WorkItem[] CreatePlan()
    {
        // Отримуємо всі задачі з репозиторію
        var allItems = _repository.GetAll();

        // Фільтруємо виконані завдання (ті, у яких IsCompleted == true)
        var pendingItems = allItems.Where(item => !item.IsCompleted).ToList();

        // Сортуємо відфільтровані завдання
        pendingItems.Sort(CompareWorkItems);

        return pendingItems.ToArray();
    }

    // Метод для порівняння завдань за пріоритетом і строком виконання
    private static int CompareWorkItems(WorkItem firstItem, WorkItem secondItem)
    {
        // Порівняння за Priority (спаданням)
        int priorityComparison = secondItem.Priority.CompareTo(firstItem.Priority);
        if (priorityComparison != 0) return priorityComparison;

        // Порівняння за DueDate (зростанням)
        int dueDateComparison = firstItem.DueDate.CompareTo(secondItem.DueDate);
        if (dueDateComparison != 0) return dueDateComparison;

        // Порівняння за Title (алфавітний порядок)
        return string.Compare(firstItem.Title, secondItem.Title, StringComparison.OrdinalIgnoreCase);
    }
}

