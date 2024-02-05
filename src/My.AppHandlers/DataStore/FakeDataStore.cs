using My.Domain.Models.Domain;

namespace My.AppHandlers.DataStore;

public class FakeDataStore
{
    //private static List<Account> _accounts;
    private List<DomainAccount> _accounts;

    public FakeDataStore()
    {
        _accounts = new List<DomainAccount>
        {
            new DomainAccount { Id = 1, Name = "Test Account 1" },
            new DomainAccount { Id = 2, Name = "Test Account 2" },
            new DomainAccount { Id = 3, Name = "Test Account 3" }
        };
    }

    public async Task AddAccount(DomainAccount account)
    {
        _accounts.Add(account);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<DomainAccount>> GetAllAccounts() => await Task.FromResult(_accounts);

    public async Task<DomainAccount> GetAccountById(int id) =>
        await Task.FromResult(_accounts.Single(p => p.Id == id));

    public async Task EventOccured(DomainAccount account, string evt)
    {
        _accounts.Single(p => p.Id == account.Id).Name = $"{account.Name} evt: {evt}";
        await Task.CompletedTask;
    }
}
