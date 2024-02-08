using My.Domain.Models.MySys2;

namespace My.Domain.Contracts;
public interface IRepositoryMySys2
{
    Task AddAccount(MySys2Account account);
    Task<MySys2Account> GetAccountById(int id);
    Task<IEnumerable<MySys2Account>> GetAllAccounts();
}
