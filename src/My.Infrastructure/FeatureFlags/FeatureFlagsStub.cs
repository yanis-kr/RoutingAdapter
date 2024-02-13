using Microsoft.Extensions.Logging;
using My.Domain.Contracts;
using My.Domain.Enums;

namespace My.Infrastructure.FeatureFlags;
public class FeatureFlagsStub : IFeatureFlag
{
    private readonly ILogger<FeatureFlagsStub> _logger;
    private bool _featureDefaultSystemLegacy = true;
    public FeatureFlagsStub(ILogger<FeatureFlagsStub> logger)
    {
        _logger = logger;
    }
    public bool IsFeatureEnabled(FeatureFlag featureName)
    {
        switch (featureName)
        {
            case FeatureFlag.FeatureDefaultSystemLegacy:
                _logger.LogInformation($"Current _featureDefaultSystemLegacy = {_featureDefaultSystemLegacy}");
                return _featureDefaultSystemLegacy;
            default:
                return false;
        }
    }

    public void ToggleFeatureFlag(FeatureFlag featureName)
    {
        switch (featureName)
        {
            case FeatureFlag.FeatureDefaultSystemLegacy:
                _featureDefaultSystemLegacy = !_featureDefaultSystemLegacy;
                _logger.LogWarning($"Switching _featureDefaultSystemLegacy = {_featureDefaultSystemLegacy}");
                break;
        }
    }
}
