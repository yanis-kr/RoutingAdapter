using NRules.RuleModel;

namespace My.Application.UseCases.AddAccountChecking.RuleHandlers;
public static class RuleHandler
{
    public static void HandleRule1(IContext context)
    {
        var result = new RuleResult { Message = "r1 ran" };
        context.Insert(result);
    }

    public static void HandleRule2(IContext context)
    {
        var result = new RuleResult { Message = "r2 ran" };
        context.Insert(result);
    }
}
