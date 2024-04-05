namespace Lab4.Exceptions;

[Serializable]
public class InvalidEmailException : AppException
{
    public InvalidEmailException()
    {}

    public InvalidEmailException(string message) : base(message)
    {}

    public InvalidEmailException(string message, Exception innerException) : base (message, innerException)
    {}   
}