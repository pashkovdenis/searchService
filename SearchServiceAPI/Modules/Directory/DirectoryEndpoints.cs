using FastEndpoints;
using Mapster;
using SearchService.App.UseCases.GetDirectory;
using SearchServiceAPI.Modules.Directory.Presenter;
using SearchServiceAPI.Modules.Directory.Request;

namespace SearchServiceAPI.Modules.Directory;

public sealed class DirectoryEndpoints : Endpoint<GetDirectoryRequest, GetDirectoryPresenter>
{
    public IGetDirectoryUseCase GetDirectoryUsecase { get; init; }
    public IGetDirectoryOutput Output { get; init; }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("directory");
        AllowAnonymous();
    }
 
    public override async Task<GetDirectoryPresenter> ExecuteAsync(GetDirectoryRequest req, CancellationToken ct)
    {
        var request = req.Adapt<GetDirectoryInput>();
        await GetDirectoryUsecase.Execute(request);
        return (GetDirectoryPresenter)Output;
    }
}