﻿using System;

namespace JonDou9000.TaskPlanner.Domain.JonDou9000.TaskPlanner.Domain.Models
{
    public class WorkItem
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Автоматично створюємо новий Guid
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Complexity Complexity { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"{Title}: due {DueDate:dd.MM.yyyy}, {Priority.ToString().ToLower()} priority";
        }

        // Метод для копіювання об'єкта
        public WorkItem Clone()
        {
            return (WorkItem)MemberwiseClone();
        }
    }
}
