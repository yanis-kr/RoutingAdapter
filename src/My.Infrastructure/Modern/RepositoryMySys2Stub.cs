using My.Domain.Contracts;
using My.Domain.Models.Modern;

namespace My.Infrastructure.Modern;

public class RepositoryModernStub : IRepositoryModern
{
    private readonly List<ModernAccount> _accounts;

    public RepositoryModernStub()
    {
        _accounts = new List<ModernAccount>
        {
            new ModernAccount { Id = 2, Name = "Test Account 2", ModernField = "B2" },
            new ModernAccount { Id = 3, Name = "Test Account 3", ModernField = "B3"  }
        };
    }

    public async Task AddAccount(ModernAccount account)
    {
        _accounts.Add(account);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    public async Task<IEnumerable<ModernAccount>> GetAllAccounts() => await Task.FromResult(_accounts).ConfigureAwait(false);

    public async Task<ModernAccount> GetAccountById(int id) =>
        await Task.FromResult(_accounts.Single(p => p.Id == id)).ConfigureAwait(false);

}
