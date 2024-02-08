using Microsoft.Extensions.Logging;
using My.Domain.Contracts;
using My.Domain.Enums;

namespace My.Infrastructure.FeatureFlags;
public class FeatureFlagsStub : IFeatureFlag
{
    private readonly ILogger<FeatureFlagsStub> _logger;
    private bool _featureDefaultSystemSys1 = true;
    public FeatureFlagsStub(ILogger<FeatureFlagsStub> logger)
    {
        _logger = logger;
    }
    public bool IsFeatureEnabled(FeatureFlag featureName)
    {
        switch (featureName)
        {
            case FeatureFlag.FeatureDefaultSystemSys1:
                _logger.LogInformation($"Current _featureDefaultSystemSys1 = {_featureDefaultSystemSys1}");
                return _featureDefaultSystemSys1;
            default:
                return false;
        }
    }

    public void ToggleFeatureFlag(FeatureFlag featureName)
    {
        switch (featureName)
        {
            case FeatureFlag.FeatureDefaultSystemSys1:
                _featureDefaultSystemSys1 = !_featureDefaultSystemSys1;
                _logger.LogWarning($"Switching _featureDefaultSystemSys1 = {_featureDefaultSystemSys1}");
                break;
        }
    }
}
