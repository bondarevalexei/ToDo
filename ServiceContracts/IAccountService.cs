using Shared.DTO;

namespace ServiceContracts
{
    public interface IAccountService
    {
        Task UpdateAccountAsync(Guid accountId, AccountForUpdateDto accountForUpdate,
            bool trackChanges);

        Task DeleteAccountAsync(Guid accountId, bool trackChanges);

        Task<AccountDto> GetAccountAsync(Guid accountId, bool trackChanges);
    }
}