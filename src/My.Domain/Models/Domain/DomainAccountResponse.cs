using My.Domain.Models.Responses;

namespace My.Domain.Models.Domain;
public class DomainAccountResponse : BaseResponse
{
    public DomainAccountResponse() : base()
    {

    }

    public DomainAccount Account { get; set; } = default!;
}
