namespace Lab4.Exceptions;

[Serializable]
public class TooOldException : AppException
{
    public TooOldException()
    {}

    public TooOldException(string message) : base(message)
    {}

    public TooOldException(string message, Exception innerException) : base (message, innerException)
    {}    
}