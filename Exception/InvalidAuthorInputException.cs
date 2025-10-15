namespace Library.Exception;

public class InvalidAuthorInputException : System.Exception
{
    public InvalidAuthorInputException() { }

    public InvalidAuthorInputException(string message)
        : base(message) { }

    public InvalidAuthorInputException(string message, System.Exception inner)
        : base(message, inner) { }
}