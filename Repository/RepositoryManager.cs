using Contracts;
using Entities.Models;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<IToDoTaskRepository> _toDoTaskRepository;
    private readonly Lazy<IAccountRepository> _accountRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _toDoTaskRepository = new(() => new ToDoTaskRepository(repositoryContext));
        _accountRepository = new(() => new AccountRepository(repositoryContext));
    }

    public IToDoTaskRepository ToDoTask => 
        _toDoTaskRepository.Value;

    public IAccountRepository Account => 
        _accountRepository.Value;

    public async Task SaveAsync() =>
        await _repositoryContext.SaveChangesAsync();
}
