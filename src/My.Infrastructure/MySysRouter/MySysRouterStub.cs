using My.Domain.Contracts;
using My.Domain.Enums;

namespace My.Infrastructure.MySysRouter;
public class MySysRouterStub : ISysRouter
{
    private readonly List<int> _accountsSys1 = new List<int> { 1, 3 };
    private readonly List<int> _accountsSys2 = new List<int> { 2, 3 };
    private readonly IFeatureFlag _featureFlag;

    public MySysRouterStub(IFeatureFlag featureFlag)
    {
        _featureFlag = featureFlag;
    }

    public TargetSystem GetRoute(int accountId)
    {
        if (_accountsSys1.Contains(accountId) && _accountsSys2.Contains(accountId))
        {
            if (_featureFlag.IsFeatureEnabled(FeatureFlag.FeatureDefaultSystemSys1))
            {
                return TargetSystem.Legacy;
            }
            else
            {
                return TargetSystem.Modern;
            }
        }
        else if (_accountsSys1.Contains(accountId))
        {
            return TargetSystem.Legacy;
        }
        else if (_accountsSys2.Contains(accountId))
        {
            return TargetSystem.Modern;
        }
        else
        {
            throw new ArgumentException("Account not found");
        }
    }

    public Task AddRoute(int accountId, TargetSystem target)
    {
        if (target == TargetSystem.Legacy)
        {
            _accountsSys1.Add(accountId);
        }
        else if (target == TargetSystem.Modern)
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
