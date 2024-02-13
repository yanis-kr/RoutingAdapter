using My.Domain.Models.Legacy;

namespace My.Domain.Contracts;
public interface IRepositoryLegacy
{
    Task AddAccount(LegacyAccount account);
    Task<LegacyAccount> GetAccountById(int id);
    Task<IEnumerable<LegacyAccount>> GetAllAccounts();
}
