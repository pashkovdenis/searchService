using SearchService.App.UseCases.DropDirectory;

namespace SearchServiceAPI.Modules.Directory.Presenter;

public sealed class DropDirectoryPresenter : IDropDirectoryOutput
{
    public string ErrorMessage { get; set; }
    public void Error(string message) => ErrorMessage = message;
}