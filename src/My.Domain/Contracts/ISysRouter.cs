using My.Domain.Enums;

namespace My.Domain.Contracts;
public interface ISysRouter
{
    public TargetSystem GetRoute(int accountId);
    public Task AddRoute(int accountId, TargetSystem target);
}
