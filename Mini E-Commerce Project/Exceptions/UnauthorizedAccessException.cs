namespace Mini_E_Commerce_Project.Exceptions;

public class UnAuthorizedAccessException : Exception
{
    public UnAuthorizedAccessException(string message) : base(message)
    {
        
    }
}
