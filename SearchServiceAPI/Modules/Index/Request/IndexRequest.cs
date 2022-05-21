namespace SearchServiceAPI.Modules.Index.Request;

public sealed class IndexRequest
{
    public Guid DirectoryId { get; set; }
    
    public List<Record> Records { get; set; }
    
    public sealed class Record
    {
        public Guid LayerId { get; init; }

        public string Document { get; init; }

        public List<string> Tokens { get; init; }

        public List<ContextEntry> Context { get; init; }

        public string InternalId { get; init; }
        
        public string Type { get; init; }

        public readonly struct ContextEntry
        {
            public string Token { get; init; }
            public double Weight { get; init; }
        }
    }
}