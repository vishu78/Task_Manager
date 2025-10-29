using TaskManagerApi.Models;

namespace TaskManagerApi.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetAll();
        TaskItem? Get(Guid id);
        TaskItem Add(TaskItem task);
        bool Update(Guid id, TaskItem updated);
        bool Delete(Guid id);
    }
}
