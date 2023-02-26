using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class AccountRepository : RepositoryBase<Account>, IAccountRepository
{
    public AccountRepository(RepositoryContext repositoryContext) 
        : base(repositoryContext) { }

    public void DeleteAccount(Account account)
        => Delete(account);

    public async Task<Account> GetAccountAsync(Guid accountId, bool trackChanges) =>
        await FindByCondition(a => a.Id.Equals(accountId), trackChanges)
            .SingleOrDefaultAsync();
}
