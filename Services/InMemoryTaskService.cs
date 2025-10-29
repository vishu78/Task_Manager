using System.Collections.Concurrent;
using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public class InMemoryTaskService : ITaskService
    {
        private readonly ConcurrentDictionary<Guid, TaskItem> _tasks = new();

        public IEnumerable<TaskItem> GetAll() => _tasks.Values;

        public TaskItem? Get(Guid id) =>
            _tasks.TryGetValue(id, out var task) ? task : null;

        public TaskItem Add(TaskItem task)
        {
            task.Id = Guid.NewGuid();
            task.IsCompleted = false;
            _tasks[task.Id] = task;
            return task;
        }

        public bool Update(Guid id, TaskItem updated)
        {
            if (!_tasks.ContainsKey(id)) return false;
            _tasks[id].Description = updated.Description;
            _tasks[id].IsCompleted = updated.IsCompleted;
            return true;
        }

        public bool Delete(Guid id) =>
            _tasks.TryRemove(id, out _);
    }
}
