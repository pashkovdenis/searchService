using SearchService.App.UseCases.Search;

namespace SearchServiceAPI.Modules.Index.Presenter;

public sealed class SearchPresenter : ISearchOutput
{
    public string ErrorMessage { get; set; }
    
    public SearchOutput Output { get; set; }

    public void Ok(SearchOutput output) => Output = output;

    public void Error(string message) => ErrorMessage = message;
}