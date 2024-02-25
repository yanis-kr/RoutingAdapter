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
        MyEntity entity = default;
        // Condition for rule 1
        When()
        //.Match<MyEntity>(entity => entity.SomeProperty == "SomeValue");
            .Match<MyEntity>(() => entity); // This matches every MyEntity instance


        Then()
            .Do(ctx => RuleHandler.HandleRule1(ctx))
            .Do(ctx => LogMyEntity(entity));
    }

    private void LogMyEntity(MyEntity? entity)
    {
        // Perform logging here
        Console.WriteLine($"MyEntity encountered: {entity!.SomeProperty}");
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
