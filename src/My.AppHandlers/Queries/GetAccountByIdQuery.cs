using MediatR;
using My.Domain.Models.Domain;

namespace My.AppHandlers.Queries;

public record GetAccountByIdQuery(int Id) : IRequest<DomainAccount>;
