namespace PipeLight.Exceptions;

public class PipeNotFoundException : Exception
{
    public PipeNotFoundException()
    {
    }

    public PipeNotFoundException(string message) : base(message)
    {
    }

    public PipeNotFoundException(string message, Exception inner) : base(message, inner)
    {
    }
}