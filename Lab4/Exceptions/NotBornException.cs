namespace Lab4.Exceptions;

[Serializable]
public class NotBornException : AppException
{
    public NotBornException()
    {}

    public NotBornException(string message) : base(message)
    {}

    public NotBornException(string message, Exception innerException) : base (message, innerException)
    {}   
}