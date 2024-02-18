using MediatR;
using My.Domain.Models.Domain;

namespace My.Application.UseCases.GetAccountById;

public record GetAccountByIdQuery(int Id) : IRequest<DomainAccount>;
