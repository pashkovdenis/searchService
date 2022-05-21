using SearchService.App.UseCases.DropIndex;

namespace SearchServiceAPI.Modules.Index.Presenter;

public sealed class DropIndexPresenter : IDropIndexOutput
{
    public string ErrorMessage { get; set; }
    public void Error(string message) => ErrorMessage = message;
}