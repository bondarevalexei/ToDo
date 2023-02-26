using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository
{
    public class ToDoTaskRepository : RepositoryBase<ToDoTask>, IToDoTaskRepository
    {
        public ToDoTaskRepository(RepositoryContext repositoryContext)
            : base(repositoryContext) { }

        public void CreateToDoTaskForAccount(Guid accountId, ToDoTask toDoTask)
        {
            toDoTask.AccountId = accountId;
            Create(toDoTask);
        }

        public void DeleteToDoTask(ToDoTask toDoTask) =>
            Delete(toDoTask);

        public async Task<IEnumerable<ToDoTask>> GetAllTasksAsync(Guid accountId,
            ToDoTaskParameters parameters, bool trackChanges) =>
            await FindByCondition(t => t.AccountId.Equals(accountId), trackChanges)
                .Sort(parameters.OrderBy)
                .ToListAsync();


        public async Task<ToDoTask> GetToDoTaskAsync(Guid accountId, Guid id, bool trackChanges) =>
            await FindByCondition(t => t.AccountId.Equals(accountId) && t.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
    }
}
