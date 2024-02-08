using My.Domain.Contracts;
using My.Domain.Enums;

namespace My.Infrastructure.Router;
public class MySysRouterStub : ISysRouter
{
    private readonly List<int> _accountsSys1 = new List<int> { 1, 3 };
    private readonly List<int> _accountsSys2 = new List<int> { 2, 3 };

    public TargetSystem GetRoute(int accountId)
    {
        if (_accountsSys1.Contains(accountId))
        {
            return TargetSystem.MySys1;
        }
        else if (_accountsSys2.Contains(accountId))
        {
            return TargetSystem.MySys2;
        }
        else
        {
            throw new ArgumentException("Account not found");
        }
    }

    public Task AddRoute(int accountId, TargetSystem target)
    {
        if (target == TargetSystem.MySys1)
        {
            _accountsSys1.Add(accountId);
        }
        else if (target == TargetSystem.MySys2)
        {
            _accountsSys2.Add(accountId);
        }
        else
        {
            throw new ArgumentException("Target system not found");
        }
        return Task.CompletedTask;
    }
}
