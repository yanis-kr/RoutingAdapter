using MediatR;

namespace My.WebApi.Controllers;

public class TestErrorQuery : IRequest<string>
{
    public int Id { get; }

    public TestErrorQuery(int id)
    {
        Id = id;
    }
}
