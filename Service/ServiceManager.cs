using AutoMapper;
using Contracts;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceContracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAccountService> _accountService;
        private readonly Lazy<IToDoTaskService> _toDoService;
        private readonly Lazy<IAuthenticationService> _authenticationService;

        public ServiceManager(ILoggerManager logger, IRepositoryManager repository, 
            IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _accountService = new(() => new AccountService(repository, mapper, logger));
            _toDoService = new( () => new ToDoTaskService(repository, logger, mapper));
            _authenticationService = new( () => new AuthenticationService(logger, mapper, userManager, configuration));
        }

        public IAccountService AccountService => _accountService.Value;
        public IToDoTaskService ToDoTaskService => _toDoService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
