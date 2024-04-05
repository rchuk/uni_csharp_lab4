namespace Lab4.Exceptions;

[Serializable]
public class EmptyNameException : AppException
{
    public EmptyNameException()
    {}

    public EmptyNameException(string message) : base(message)
    {}

    public EmptyNameException(string message, Exception innerException) : base (message, innerException)
    {}   
}
