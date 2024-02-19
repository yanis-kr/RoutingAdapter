using MediatR;

namespace My.Application.UseCases.TestError;

public class TestErrorQuery : IRequest<string>
{
    public int Id { get; }

    public TestErrorQuery(int id)
    {
        Id = id;
    }
}
