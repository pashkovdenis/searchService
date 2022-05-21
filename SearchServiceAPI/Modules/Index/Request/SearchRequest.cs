namespace SearchServiceAPI.Modules.Index.Request;

public sealed class SearchRequest
{
    public Guid DirectoryId { get; set; }
    
    public IEnumerable<string> Context { get; set;}
    
    public IEnumerable<string> Tokens { get; set;} 
}