namespace SearchService.Domain.Enumerations;

/// <summary>
///     Search mode used during the search
/// </summary>
public enum SearchMode
{
    // Search layers one by one and add result to the results set. 
    Sequental,

    // Add output of the previous layer to the input of the next layer.
    Multiply,

    // Use output of the previous layer as an input for the next layer.
    Cross
}