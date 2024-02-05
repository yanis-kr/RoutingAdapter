using MediatR;
using My.AppHandlers.Commands;
using My.AppHandlers.DataStore;
using My.Domain.Models.Domain;

namespace My.AppHandlers.Handlers;

public class AddAccountHandler : IRequestHandler<AddAccountCommand, DomainAccount>
{
    private readonly FakeDataStore _fakeDataStore;

    public AddAccountHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

    public async Task<DomainAccount> Handle(AddAccountCommand request, CancellationToken cancellationToken)
    {
        await _fakeDataStore.AddAccount(request.Account);

        return request.Account;
    }
}
