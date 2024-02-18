using FluentValidation;

namespace My.Application.UseCases.AddAccount;

public class AddAccountCommandValidator : AbstractValidator<AddAccountCommand>
{
    public AddAccountCommandValidator()
    {
        RuleFor(n => n.Account.Name)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(i => i.Account.Id)
            .GreaterThan(0);

        RuleFor(e => e)
            .MustAsync(AccountIdIsUnique)
            .WithMessage("An event with the same name and date already exists.");
    }

    private async Task<bool> AccountIdIsUnique(AddAccountCommand a, CancellationToken token)
    {
        //todo : add logic to check if the account id is unique in repository
        return await Task.FromResult(true).ConfigureAwait(false);
    }
}
