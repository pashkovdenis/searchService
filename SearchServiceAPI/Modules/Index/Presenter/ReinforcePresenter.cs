using SearchService.App.UseCases.Reinforce;

namespace SearchServiceAPI.Modules.Index.Presenter;

public sealed class ReinforcePresenter : IReinforceOutput
{
    public string ErrorMessage { get; set; }
    public void Error(string message) => ErrorMessage = message;
}