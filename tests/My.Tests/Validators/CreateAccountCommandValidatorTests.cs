using FluentValidation.TestHelper;
using My.Application.UseCases.AddAccount;
using My.Domain.Models.Domain;

namespace My.Tests.Validators;
public class CreateAccountCommandValidatorTests
{
    private readonly AddAccountCommandValidator _validator;

    public CreateAccountCommandValidatorTests()
    {
        _validator = new AddAccountCommandValidator();
    }

    [Fact]
    public async Task ShouldHaveErrorWhenNameIsEmpty()
    {
        var domainAccount = new DomainAccount { Name = string.Empty };
        AddAccountCommand model = new AddAccountCommand(domainAccount);
        var result = await _validator.TestValidateAsync(model).ConfigureAwait(true);
        result.ShouldHaveValidationErrorFor(x => x.Account.Name);
    }

    [Fact]
    public async Task ShouldHaveErrorWhenIdIsLessThanOne()
    {
        var domainAccount = new DomainAccount { Id = 0 };
        AddAccountCommand model = new AddAccountCommand(domainAccount);
        var result = await _validator.TestValidateAsync(model).ConfigureAwait(true);
        result.ShouldHaveValidationErrorFor(x => x.Account.Id);
    }

    // Add more tests for other rules and scenarios
}
