﻿namespace Lab4.Exceptions;

[Serializable]
public class AppException : Exception
{
    public AppException()
    {}

    public AppException(string message) : base(message)
    {}

    public AppException(string message, Exception innerException) : base (message, innerException)
    {}   
}