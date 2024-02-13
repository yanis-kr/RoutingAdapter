using My.Domain.Models.Modern;

namespace My.Domain.Contracts;
public interface IRepositoryModern
{
    Task AddAccount(ModernAccount account);
    Task<ModernAccount> GetAccountById(int id);
    Task<IEnumerable<ModernAccount>> GetAllAccounts();
}
