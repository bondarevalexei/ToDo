using Entities.Models;

namespace Contracts;

public interface IAccountRepository
{
    Task<Account> GetAccountAsync(Guid accountId, bool trackChanges);

    void DeleteAccount(Account account);
}
