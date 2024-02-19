using My.Domain.Enums;

namespace My.Domain.Contracts;
public interface ISysRouter
{
    Task<TargetSystem> GetRoute(int accountId);
    Task AddRoute(int accountId, TargetSystem target);
}
