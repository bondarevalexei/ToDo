using Entities.Models;
using Shared.DTO;
using Shared.RequestFeatures;

namespace ServiceContracts
{
    public interface IToDoTaskService
    {
        Task<IEnumerable<ToDoTaskDto>> GetToDosAsync(Guid accountId, 
            ToDoTaskParameters parameters, bool trackChanges);

        Task<ToDoTaskDto> GetToDoAsync(Guid accountId, Guid id, bool trackChanges);

        Task<ToDoTaskDto> CreateToDoTaskForAccountAsync(Guid accountId, 
            ToDoTaskForCreationDto toDoTaskForCreation, bool trackChanges);

        Task DeleteToDoTaskForAccountAsync(Guid accountId, Guid id, bool trackChanges);

        Task UpdateToDoTaskForAccountAsync(Guid acoountId, Guid id, ToDoTaskForUpdateDto
            toDoTaskForUpdate, bool accTrackChanges, bool toDoTaskTrackChanges);

        Task<(ToDoTaskForUpdateDto toDoTaskForUpdate, ToDoTask toDoEntity)> GetToDoTaskForPatchAsync(
            Guid accountId, Guid id, bool accTrackChanges, bool toDoTaskTrackChanges);

        Task SaveChangesForPatchAsync(ToDoTaskForUpdateDto toDoTaskToPatch, ToDoTask toDoEntity);
    }
}