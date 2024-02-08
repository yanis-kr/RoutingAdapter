using My.Domain.Contracts;
using My.Domain.Models.Domain;
using My.Domain.Models.MySys1;

namespace My.Infrastructure.MySys1;

public class RepositoryMySys1Stub : IRepositoryMySys1
{
    //private static List<Account> _accounts;
    private readonly List<MySys1Account> _accounts;

    public RepositoryMySys1Stub()
    {
        _accounts = new List<MySys1Account>
        {
            new MySys1Account { Id = 1, Name = "Test Account 1", MySys1Field = "A1"},
            new MySys1Account { Id = 3, Name = "Test Account 3", MySys1Field = "A3"}
        };
    }

    public async Task AddAccount(MySys1Account account)
    {
        _accounts.Add(account);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    public async Task<IEnumerable<MySys1Account>> GetAllAccounts() => await Task.FromResult(_accounts).ConfigureAwait(false);

    public async Task<MySys1Account> GetAccountById(int id) =>
        await Task.FromResult(_accounts.Single(p => p.Id == id)).ConfigureAwait(false);

    public async Task EventOccured(DomainAccount account, string evt)
    {
        _accounts.Single(p => p.Id == account.Id).Name = $"{account.Name} evt: {evt}";
        await Task.CompletedTask.ConfigureAwait(false);
    }

}
