using JonDou9000.TaskPlanner.Domain.JonDou9000.TaskPlanner.Domain.Models;
using System;

namespace JonDou9000.TaskPlanner.DataAccess.JonDou9000.TaskPlanner.DataAccess.Abstractions
{
    public interface IWorkItemsRepository
    {
        Guid Add(WorkItem workItem);
        WorkItem Get(Guid id);
        WorkItem[] GetAll();
        bool Update(WorkItem workItem);
        bool Remove(Guid id);
        void SaveChanges();
    }
}
