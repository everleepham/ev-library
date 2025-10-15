namespace Library.Exception;

public class ResourceNotFoundException : System.Exception
{    
    public ResourceNotFoundException() { }

    public ResourceNotFoundException(string message)
        : base(message) { }
    
    public ResourceNotFoundException(string message, System.Exception inner)
        : base(message, inner) { }
}