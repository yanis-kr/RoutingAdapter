using My.Domain.Contracts;
using My.Domain.Models.MySys2;

namespace My.Infrastructure.MySys2;

public class RepositoryMySys2Stub : IRepositoryMySys2
{
    private readonly List<MySys2Account> _accounts;

    public RepositoryMySys2Stub()
    {
        _accounts = new List<MySys2Account>
        {
            new MySys2Account { Id = 2, Name = "Test Account 2", MySys2Field = "B2" },
            new MySys2Account { Id = 3, Name = "Test Account 3", MySys2Field = "B3"  }
        };
    }

    public async Task AddAccount(MySys2Account account)
    {
        _accounts.Add(account);
        await Task.CompletedTask.ConfigureAwait(false);
    }

    public async Task<IEnumerable<MySys2Account>> GetAllAccounts() => await Task.FromResult(_accounts).ConfigureAwait(false);

    public async Task<MySys2Account> GetAccountById(int id) =>
        await Task.FromResult(_accounts.Single(p => p.Id == id)).ConfigureAwait(false);

}
