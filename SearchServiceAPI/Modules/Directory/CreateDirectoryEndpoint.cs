using FastEndpoints;
using Mapster;
using SearchService.App.UseCases.CreateDirectory;
using SearchServiceAPI.Modules.Directory.Presenter;
using SearchServiceAPI.Modules.Directory.Request;

namespace SearchServiceAPI.Modules.Directory;

public class CreateDirectoryEndpoint : Endpoint<CreateDirectoryRequest, CreateDirectoryPresenter>
{
    public ICreateDirectoryHandler CreateDirectoryHandler { get; init; }
    public ICreateDirectoryOutput Output { get; init; }
    
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("directory");
        AllowAnonymous();
    }

    public override async Task<CreateDirectoryPresenter> ExecuteAsync(CreateDirectoryRequest req, CancellationToken ct)
    {
        var request = req.Adapt<CreateDirectoryInput>();
        
        await CreateDirectoryHandler.Execute(request);

        return (CreateDirectoryPresenter)Output;
    }
} 