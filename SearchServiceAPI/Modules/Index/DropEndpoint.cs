using FastEndpoints;
using Mapster;
using SearchService.App.UseCases.DropIndex;
using SearchServiceAPI.Modules.Index.Request;

namespace SearchServiceAPI.Modules.Index;

public sealed class DropEndpoint : Endpoint<DropIndexRequest>
{
    public IDropIndexHandler DropIndexHandler { get; init; }

    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("index");
        AllowAnonymous();
    }

    public override Task HandleAsync(DropIndexRequest req, CancellationToken ct)
    {
        var request = req.Adapt<DropIndexInput>();
        return DropIndexHandler.Execute(request);
    }
}