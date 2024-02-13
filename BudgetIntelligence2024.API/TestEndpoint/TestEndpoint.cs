using FastEndpoints;

namespace BudgetIntelligence2024.API.TestEndpoint;

public class TestEndpoint : Endpoint<TestRequest, TestResponse>
{
    public override void Configure()
    {
        Post("test");
        
        AllowAnonymous();   //TODO: implement auth
    }

    public override Task HandleAsync(TestRequest req, CancellationToken ct)
    {
        // This sends the response
        Response = new TestResponse
        {
            Resp = "the response"
        };

        return Task.CompletedTask;
    }
}

public class TestRequest
{
    public string Test {  get; set; }
}

public class TestResponse
{
    public string Resp { get; set; }
}
