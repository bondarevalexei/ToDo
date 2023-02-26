using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using LoggerService;
using ServiceContracts;
using Shared.DTO;

namespace Service
{
    public class AccountService : IAccountService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public AccountService(IRepositoryManager repository, IMapper mapper, ILoggerManager logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task DeleteAccountAsync(Guid accountId, bool trackChanges)
        {
            var account = 
                await GetAccountAndCheckIfItExists(accountId, trackChanges);

            _repository.Account.DeleteAccount(account);
            await _repository.SaveAsync();
        }

        public async Task<AccountDto> GetAccountAsync(Guid accountId, bool trackChanges)
        {
            var account =
                await GetAccountAndCheckIfItExists(accountId, trackChanges);

            var accountDto = _mapper.Map<AccountDto>(account);
            return accountDto;
        }

        public async Task UpdateAccountAsync(Guid accountId, 
            AccountForUpdateDto accountForUpdate, bool trackChanges)
        {
            var accountEntity =
            await GetAccountAndCheckIfItExists(accountId, trackChanges);
        }

        private async Task<Account> GetAccountAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var account =
                await _repository.Account.GetAccountAsync(id, trackChanges);

            if (account is null)
                throw new AccountNotFoundException(id);

            return account;
        }
    }
}
