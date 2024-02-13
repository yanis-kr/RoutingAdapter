using MediatR;
using My.Domain.Contracts;
using My.Domain.Models.Domain;
using AutoMapper;
using My.Domain.Enums;
using My.Domain.Models.Legacy;
using My.Domain.Models.Modern;
using My.Application.UseCases.Account.Commands;
using My.Application.UseCases.Account.Validators;

namespace My.Application.UseCases.Account.Handlers;

public class AddAccountHandler : IRequestHandler<AddAccountCommand, DomainAccountResponse>
{
    private readonly IRepositoryLegacy _legacyRepo;
    private readonly IRepositoryModern _modernRepo;
    private readonly ISysRouter _router;
    private readonly IFeatureFlag _featureFlag;
    private readonly IMapper _mapper;

    public AddAccountHandler(
        IRepositoryLegacy legacyRepo,
        IRepositoryModern modernRepo,
        ISysRouter router,
        IFeatureFlag featureFlag,
        IMapper mapper)
    {
        _legacyRepo = legacyRepo;
        _modernRepo = modernRepo;
        _router = router;
        _featureFlag = featureFlag;
        _mapper = mapper;
    }

    public async Task<DomainAccountResponse> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateAccountCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken).ConfigureAwait(true);

        if (validationResult.Errors.Count > 0)
        {
            throw new Exceptions.ValidationException(validationResult);
        }

        var isLegacy = _featureFlag.IsFeatureEnabled(FeatureFlag.FeatureDefaultSystemLegacy);
        if (isLegacy)
        {
            var account = _mapper.Map<DomainAccount, LegacyAccount>(request.Account);
            await _legacyRepo.AddAccount(account).ConfigureAwait(false);
            await _router.AddRoute(request.Account.Id, TargetSystem.Legacy).ConfigureAwait(true);
        }
        else
        {
            var account = _mapper.Map<DomainAccount, ModernAccount>(request.Account);
            await _modernRepo.AddAccount(account).ConfigureAwait(false);
            await _router.AddRoute(request.Account.Id, TargetSystem.Modern).ConfigureAwait(true);
        }

        var createAccountCommandResponse = new DomainAccountResponse();
        if (validationResult.Errors.Count > 0)
        {
            createAccountCommandResponse.Success = false;
            createAccountCommandResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                createAccountCommandResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }
        if (createAccountCommandResponse.Success)
        {
            //may create some mappings here, perhaps returning more granular DTO
            createAccountCommandResponse.Account = request.Account;
        }

        return createAccountCommandResponse;

    }
}
