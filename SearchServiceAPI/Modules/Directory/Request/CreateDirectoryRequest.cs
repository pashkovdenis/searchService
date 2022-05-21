using SearchService.Domain.Enumerations;

namespace SearchServiceAPI.Modules.Directory.Request;

public class CreateDirectoryRequest
{ 
    public string Token { get; init; }
    public string Name { get; init; }
    
    public SearchMode Mode { get; init;}
    public IEnumerable<LayerTemplate> LayerTemplates { get; init; }
 
    public sealed class LayerTemplate
    {
        public double DefaultDominance { get; init; }
        public double Reinforce { get; init; }
        public double ResultTresshold { get; init; }
        public int DefaultContextSize { get; init; }
 
        public Dictionary<string, string> Comparers { get; init; }
    }
}