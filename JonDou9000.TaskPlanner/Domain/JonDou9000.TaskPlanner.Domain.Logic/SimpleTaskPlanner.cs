using JonDou9000.TaskPlanner.Domain.JonDou9000.TaskPlanner.Domain.Models;
using System.Collections.Generic;
using System.Linq;

public class SimpleTaskPlanner
{
    public WorkItem[] CreatePlan(WorkItem[] items)
    {
        var itemsAsList = items.ToList();
        itemsAsList.Sort(CompareWorkItems);
        return itemsAsList.ToArray();
    }

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
