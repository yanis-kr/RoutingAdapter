using My.Domain.Models.MySys1;

namespace My.Domain.Contracts;
public interface IRepositoryMySys1
{
    Task AddAccount(MySys1Account account);
    Task<MySys1Account> GetAccountById(int id);
    Task<IEnumerable<MySys1Account>> GetAllAccounts();
}
