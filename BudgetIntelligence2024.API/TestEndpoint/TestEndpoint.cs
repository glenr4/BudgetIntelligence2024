using FastEndpoints;
using System.Text.Json.Serialization;

namespace BudgetIntelligence2024.API.TestEndpoint;

public class TestEndpoint : Endpoint<TestRequest>
{
    public override void Configure()
    {
        Post("test");

        AllowAnonymous();   //TODO: implement auth
    }

    public override Task HandleAsync(TestRequest req, CancellationToken ct)
    {
        return Task.CompletedTask;
    }
}

public class TestRequest
{
    public string Test {  get; set; }
}