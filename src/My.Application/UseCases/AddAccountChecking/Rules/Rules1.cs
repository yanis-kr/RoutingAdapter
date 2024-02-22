using My.Application.UseCases.AddAccountChecking.RuleHandlers;
using NRules.Fluent.Dsl;

namespace My.Application.UseCases.AddAccountChecking.Rules;
public class Rules1
{
}

public class Rule1 : Rule
{
    public override void Define()
    {
        // Condition for rule 1
        When()
            .Match<MyEntity>(entity => entity.SomeProperty == "SomeValue");

        Then()
            .Do(ctx => RuleHandler.HandleRule1(ctx));
    }
}

public class Rule2 : Rule
{
    public override void Define()
    {
        // Condition for rule 2
        When()
            .Match<MyEntity>(entity => entity.SomeOtherProperty == "SomeOtherValue");

        Then()
            .Do(ctx => RuleHandler.HandleRule2(ctx));
    }
}
