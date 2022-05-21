namespace SearchService.Domain.Exceptions;

public class SearchServiceException : Exception
{
    public SearchServiceException()
    {
    }

    public SearchServiceException(string message) : base(message)
    {
    }

    public SearchServiceException(string message, Exception exception) : base(message, exception)
    {
    }
}