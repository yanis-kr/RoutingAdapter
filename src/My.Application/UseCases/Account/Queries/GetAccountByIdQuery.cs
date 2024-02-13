using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.Account.Queries;

public record GetAccountByIdQuery(int Id) : IRequest<DomainAccount>;
