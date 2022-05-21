using FastEndpoints;
using Mapster;
using SearchService.App.UseCases.Reinforce;
using SearchServiceAPI.Modules.Index.Request;

namespace SearchServiceAPI.Modules.Index;

public sealed class RebuildEndpoint : Endpoint<ReinforceRequest>
{ 
    public IReinforceHandler ReinforceHandler { get; init; } 
 
    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("index");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ReinforceRequest req, CancellationToken ct)
    {
        var request = req.Adapt<ReinforceInput>();

        await ReinforceHandler.Execute(request);
    }
}