namespace ServiceContracts;

public interface IServiceManager
{
    IAccountService AccountService { get; }
    IToDoTaskService ToDoTaskService { get; }
    IAuthenticationService AuthenticationService { get; }
}
