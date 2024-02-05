using MediatR;
using My.AppHandlers.DataStore;
using My.AppHandlers.Queries;
using My.Domain.Models.Domain;

namespace My.AppHandlers.Handlers;

public class GetAccountByIdHandler : IRequestHandler<GetAccountByIdQuery, DomainAccount>
{
    private readonly FakeDataStore _fakeDataStore;

    public GetAccountByIdHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;

    public async Task<DomainAccount> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken) =>
        await _fakeDataStore.GetAccountById(request.Id);

}
