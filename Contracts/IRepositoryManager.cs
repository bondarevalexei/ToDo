namespace Contracts;

public interface IRepositoryManager
{
    IAccountRepository Account { get; }
    IToDoTaskRepository ToDoTask { get; }
    Task SaveAsync();
}
