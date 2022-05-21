namespace SearchServiceAPI.Modules.Directory.Request;

public class DropDirectoryRequest
{
    public string Token { get; init; }

    public Guid DirectoryId { get; init; }
}