using SearchService.App.UseCases.CreateDirectory;

namespace SearchServiceAPI.Modules.Directory.Presenter;

public sealed class CreateDirectoryPresenter : ICreateDirectoryOutput
{ 
    public string ErrorMessage { get; set; }
    
    public void Ok(CreateDirectoryOutput output)
    {
        Id = output.Id;
    }

    public void Error(string message) => ErrorMessage = message;

    public Guid Id { get; private set; }
}