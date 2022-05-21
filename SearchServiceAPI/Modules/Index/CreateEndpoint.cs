using FastEndpoints;
using Mapster;
using SearchService.App.UseCases.Index;
using SearchServiceAPI.Modules.Index.Request;

namespace SearchServiceAPI.Modules.Index;

public sealed class CreateEndpoint : Endpoint<IndexRequest>
{
    public ICreateIndexHandler CreateIndexHandler { get; init; }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("index");
        AllowAnonymous();
    }

    public override Task HandleAsync(IndexRequest req, CancellationToken ct)
    {
        var request = req.Adapt<CreateIndexInput>();
        return CreateIndexHandler.Execute(request);
    }
}