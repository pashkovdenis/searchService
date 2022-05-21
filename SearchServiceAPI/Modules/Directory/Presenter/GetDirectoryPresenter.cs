using SearchService.App.UseCases.GetDirectory;

namespace SearchServiceAPI.Modules.Directory.Presenter;

public sealed class GetDirectoryPresenter : IGetDirectoryOutput
{
    public string ErrorMessage { get; set; }
    public IEnumerable<DirectoryOutput> Results { get; set; }
    public void Ok(GetDirectoryOutput output) => Results = output.Directories;
    public void Error(string message) => ErrorMessage = message;
}