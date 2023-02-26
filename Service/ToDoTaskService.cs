using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using LoggerService;
using ServiceContracts;
using Shared.DTO;
using Shared.RequestFeatures;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Runtime.InteropServices;

namespace Service
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ToDoTaskService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ToDoTaskDto> CreateToDoTaskForAccountAsync(Guid accountId, 
            ToDoTaskForCreationDto toDoTaskForCreation, bool trackChanges)
        {
            await CheckIfAccountExists(accountId, trackChanges);

            var accountEntity = _mapper.Map<ToDoTask>(toDoTaskForCreation);

            _repository.ToDoTask.CreateToDoTaskForAccount(accountId, accountEntity);
            await _repository.SaveAsync();

            var toDoToReturn = _mapper.Map<ToDoTaskDto>(accountEntity);
            return toDoToReturn;
        }

        public async Task DeleteToDoTaskForAccountAsync(Guid accountId, Guid id, bool trackChanges)
        {
            await CheckIfAccountExists(accountId, trackChanges);
            
            var ToDoForAccount =
                await GetToDoForAccountAndCheckIfItExists(accountId, id, trackChanges);

            _repository.ToDoTask.DeleteToDoTask(ToDoForAccount);
            await _repository.SaveAsync();
        }

        public async Task<ToDoTaskDto> GetToDoAsync(Guid accountId, Guid id, bool trackChanges)
        {
            await CheckIfAccountExists(accountId, trackChanges);

            var toDoDb = 
                await _repository.ToDoTask.GetToDoTaskAsync(accountId, id, trackChanges);

            var toDo = _mapper.Map<ToDoTaskDto>(toDoDb);
            return toDo;
        }

        public async Task<IEnumerable<ToDoTaskDto>> GetToDosAsync(Guid accountId, 
            ToDoTaskParameters parameters, bool trackChanges)
        {
            await CheckIfAccountExists(accountId, trackChanges);

            var sortedToDos = 
                await _repository.ToDoTask.GetAllTasksAsync(accountId, parameters, trackChanges);

            var toDosDto = _mapper.Map<IEnumerable<ToDoTaskDto>>(sortedToDos);

            return toDosDto;
        }

        public async Task<(ToDoTaskForUpdateDto toDoTaskForUpdate, ToDoTask toDoEntity)> 
            GetToDoTaskForPatchAsync(Guid accountId, Guid id, bool accTrackChanges, bool toDoTaskTrackChanges)
        {
            await CheckIfAccountExists(accountId, accTrackChanges);

            var toDoDb =
                await GetToDoForAccountAndCheckIfItExists(accountId, id, toDoTaskTrackChanges);

            var toDoToPatch = _mapper.Map<ToDoTaskForUpdateDto>(toDoDb);

            return (toDoToPatch, toDoDb);
        }

        public async Task SaveChangesForPatchAsync(ToDoTaskForUpdateDto toDoTaskToPatch, ToDoTask toDoEntity)
        {
            _mapper.Map(toDoTaskToPatch, toDoEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateToDoTaskForAccountAsync(Guid acoountId, Guid id, 
            ToDoTaskForUpdateDto toDoTaskForUpdate, bool accTrackChanges, bool toDoTaskTrackChanges)
        {
            await CheckIfAccountExists(acoountId, accTrackChanges);

            var toDoDb =
                await _repository.ToDoTask.GetToDoTaskAsync(acoountId, id, toDoTaskTrackChanges);

            _mapper.Map(toDoTaskForUpdate, toDoDb);
            await _repository.SaveAsync();
        }

        private async Task CheckIfAccountExists(Guid accountId, bool trackChanges)
        {
            var account = 
                await _repository.Account.GetAccountAsync(accountId, trackChanges);

            if (account is null)
                throw new AccountNotFoundException(accountId);
        }

        private async Task<ToDoTask> GetToDoForAccountAndCheckIfItExists(
            Guid accountId, Guid id, bool trackChanges)
        {
            var toDoDb =
                await _repository.ToDoTask.GetToDoTaskAsync(accountId, id, trackChanges);

            if(toDoDb is null)
                throw new ToDoTaskNotFoundException(id);

            return toDoDb;
        }
    }
}
