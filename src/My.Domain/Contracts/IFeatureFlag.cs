using My.Domain.Enums;

namespace My.Domain.Contracts;
public interface IFeatureFlag
{
    bool IsFeatureEnabled(FeatureFlag featureName);
    void ToggleFeatureFlag(FeatureFlag featureName);
}
