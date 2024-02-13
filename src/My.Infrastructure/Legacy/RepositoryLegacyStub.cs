using My.Domain.Contracts;
using My.Domain.Models.Domain;
using My.Domain.Models.Legacy;

namespace My.Infrastructure.Legacy;

public class RepositoryLegacyStub : IRepositoryLegacy
{
    //private static List<Account> _accounts;
    private readonly List<LegacyAccount> _accounts;

    public RepositoryLegacyStub()
    {
        _accounts = new List<LegacyAccount>
        {
            new LegacyAccount { Id = 1, Name = "Test Account 1", LegacyField = "A1"},
            new LegacyAccount { Id = 3, Name = "Test Account 3", LegacyField = "A3"}
        };
    }

    public async Task AddAccount(LegacyAccount account)
    {
        _accounts.Add(account);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    public async Task<IEnumerable<LegacyAccount>> GetAllAccounts() => await Task.FromResult(_accounts).ConfigureAwait(false);

    public async Task<LegacyAccount> GetAccountById(int id) =>
        await Task.FromResult(_accounts.Single(p => p.Id == id)).ConfigureAwait(false);

    public async Task EventOccured(DomainAccount account, string evt)
    {
        _accounts.Single(p => p.Id == account.Id).Name = $"{account.Name} evt: {evt}";
        await Task.CompletedTask.ConfigureAwait(false);
    }

}
