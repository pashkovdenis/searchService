using SearchService.App.UseCases.Index;

namespace SearchServiceAPI.Modules.Index.Presenter;

public sealed class CreateIndexPresenter : ICreateIndexOutput
{
    public string ErrorMesage { get; set; }
    public void Error(string message) => ErrorMesage = message;
}