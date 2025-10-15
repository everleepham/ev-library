namespace Library.Exception;

public class DbConnectionException: System.Exception

{
    public DbConnectionException() { }

    public DbConnectionException(string message)
        : base(message) { }
    
    public DbConnectionException(string message, System.Exception inner)
        : base(message, inner) { }

}