using My.Domain.Contracts;
using My.Domain.Enums;

namespace My.Infrastructure.MySysRouter;
public class MySysRouterStub : ISysRouter
{
    private readonly List<int> _accountsLegacy = new List<int> { 1, 3 };
    private readonly List<int> _accountsModern = new List<int> { 2, 3 };
    private readonly IFeatureFlag _featureFlag;

    public MySysRouterStub(IFeatureFlag featureFlag)
    {
        _featureFlag = featureFlag;
    }

    public Task<TargetSystem> GetRoute(int accountId)
    {
        if (_accountsLegacy.Contains(accountId) && _accountsModern.Contains(accountId))
        {
            if (_featureFlag.IsFeatureEnabled(FeatureFlag.FeatureDefaultSystemLegacy))
            {
                return Task.FromResult(TargetSystem.Legacy);
            }
            else
            {
                return Task.FromResult(TargetSystem.Modern);
            }
        }
        else if (_accountsLegacy.Contains(accountId))
        {
            return Task.FromResult(TargetSystem.Legacy);
        }
        else if (_accountsModern.Contains(accountId))
        {
            return Task.FromResult(TargetSystem.Modern);
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
            _accountsLegacy.Add(accountId);
        }
        else if (target == TargetSystem.Modern)
        {
            _accountsModern.Add(accountId);
        }
        else
        {
            throw new ArgumentException("Target system not found");
        }
        return Task.CompletedTask;
    }
}
