using FastEndpoints;
using Mapster;
using SearchService.App.UseCases.Search;
using SearchServiceAPI.Modules.Index.Presenter;
using SearchServiceAPI.Modules.Index.Request;

namespace SearchServiceAPI.Modules.Index;

public sealed class SearchEndpoint : Endpoint<SearchRequest, SearchPresenter>
{
    public ISearchHandler SearchHandler { get; init; }
    public ISearchOutput SearchOutput { get; init; }
    
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("search");
        AllowAnonymous();
    }

    public override async Task<SearchPresenter> ExecuteAsync(SearchRequest req, CancellationToken ct)
    {
        var request = req.Adapt<SearchInput>();

        await SearchHandler.Execute(request);

        return (SearchPresenter)SearchOutput;
    }
}