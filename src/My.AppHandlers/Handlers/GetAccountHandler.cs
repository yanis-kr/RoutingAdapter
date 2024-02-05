using MediatR;
using My.AppHandlers.DataStore;
using My.AppHandlers.Queries;
using My.Domain.Models.Domain;

namespace My.AppHandlers.Handlers;

public class GetAccountsHandler : IRequestHandler<GetAccountsQuery, IEnumerable<DomainAccount>>
{
    private readonly FakeDataStore _fakeDataStore;

    public GetAccountsHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

    public async Task<IEnumerable<DomainAccount>> Handle(GetAccountsQuery request,
        CancellationToken cancellationToken) => await _fakeDataStore.GetAllAccounts();
}
