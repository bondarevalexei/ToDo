using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IToDoTaskRepository
{
    Task<IEnumerable<ToDoTask>> GetAllTasksAsync(Guid accountId,
            ToDoTaskParameters parameters, bool trackChanges);

    Task<ToDoTask> GetToDoTaskAsync(Guid accountId, Guid id, bool trackChanges);

    void CreateToDoTaskForAccount(Guid accountId, ToDoTask toDoTask);

    void DeleteToDoTask(ToDoTask toDoTask);
}
