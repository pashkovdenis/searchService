using FastEndpoints;
using Mapster;
using SearchService.App.UseCases.DropDirectory;
using SearchServiceAPI.Modules.Directory.Request;

namespace SearchServiceAPI.Modules.Directory;

public class DropDirectoryEndpoint : Endpoint<DropDirectoryRequest>
{
    public IDropDirectoryHandler DropDirectoryHandler { get; init; }
    
    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("directory");
        AllowAnonymous();
    }

    public override Task HandleAsync(DropDirectoryRequest req, CancellationToken ct)
    {
        var request = req.Adapt<DropDirectoryInput>();
        return DropDirectoryHandler.Execute(request);
    }
}