using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Xml;
using JonDou9000.TaskPlanner.DataAccess.JonDou9000.TaskPlanner.DataAccess.Abstractions;
using JonDou9000.TaskPlanner.Domain.JonDou9000.TaskPlanner.Domain.Models;

namespace JonDou9000.TaskPlanner.DataAccess.JonDou9000.TaskPlanner.DataAccess
{
    public class FileWorkItemsRepository : IWorkItemsRepository
    {
        private const string FileName = "work-items.json";   // Назва файлу
        private readonly Dictionary<Guid, WorkItem> _workItems;  // Словник для зберігання завдань у пам'яті

        public FileWorkItemsRepository()
        {
            // Ініціалізація словника і завантаження даних з файлу
            if (File.Exists(FileName))
            {
                var fileContent = File.ReadAllText(FileName);
                if (!string.IsNullOrWhiteSpace(fileContent))
                {
                    var items = JsonConvert.DeserializeObject<WorkItem[]>(fileContent);
                    _workItems = new Dictionary<Guid, WorkItem>();

                    foreach (var item in items)
                    {
                        _workItems[item.Id] = item;
                    }
                }
                else
                {
                    _workItems = new Dictionary<Guid, WorkItem>();
                }
            }
            else
            {
                _workItems = new Dictionary<Guid, WorkItem>();
            }
        }

        // Додавання нового елемента у сховище
        public Guid Add(WorkItem workItem)
        {
            var newItem = workItem.Clone();  // Створюємо копію
            _workItems[newItem.Id] = newItem;  // Додаємо копію в словник
            return newItem.Id;  // Повертаємо новий Guid
        }

        // Отримання елемента за ID
        public WorkItem Get(Guid id)
        {
            return _workItems.ContainsKey(id) ? _workItems[id] : null;
        }

        // Отримання всіх елементів
        public WorkItem[] GetAll()
        {
            return new List<WorkItem>(_workItems.Values).ToArray();
        }

        // Оновлення існуючого елемента
        public bool Update(WorkItem workItem)
        {
            if (_workItems.ContainsKey(workItem.Id))
            {
                _workItems[workItem.Id] = workItem;
                return true;
            }
            return false;
        }

        // Видалення елемента за ID
        public bool Remove(Guid id)
        {
            return _workItems.Remove(id);
        }

        // Збереження змін у файл
        public void SaveChanges()
        {
            var items = new List<WorkItem>(_workItems.Values).ToArray();
            var json = JsonConvert.SerializeObject(items, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FileName, json);  // Записуємо JSON у файл
        }
    }
}
