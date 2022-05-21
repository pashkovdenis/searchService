namespace SearchServiceAPI.Modules.Index.Request;

public sealed class ReinforceRequest
{
    public Guid DirectoryId { get; set; }
    public Guid LayerId { get; set; }
    public Guid IndexId { get;  set;}
    public Guid SelectedPayloadId { get; init; }
    
    public ICollection<ReinforceRecord> Records { get; set;}
    
    public bool Clear { get; set; }
    
    public sealed class ReinforceRecord
    {
        public Guid IndexId { get; init; }

        public Guid PayloadId { get; init; }

        public string PayloadData { get; init; }

        public List<string> Tokens { get; init; }

        public List<ContextEntry> Context { get; init; }
        
        public string IndernalId { get; set; }
        
        public string Type { get; set; }
        
        public readonly struct ContextEntry
        {
            public string Token { get; init; }
            public double Weight { get; init; }
        }
    }
}