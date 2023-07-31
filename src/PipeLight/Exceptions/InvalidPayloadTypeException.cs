namespace PipeLight.Exceptions;

public class InvalidPayloadTypeException : Exception
{
    public InvalidPayloadTypeException()
    {
    }

    public InvalidPayloadTypeException(string message) : base(message)
    {
    }

    public InvalidPayloadTypeException(string message, Exception inner) : base(message, inner)
    {
    }
}
