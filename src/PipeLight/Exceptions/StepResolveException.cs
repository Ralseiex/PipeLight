namespace PipeLight.Exceptions;

public sealed class StepResolveException : Exception
{
    public StepResolveException()
    {
    }

    public StepResolveException(string message) : base(message)
    {
    }

    public StepResolveException(string message, Exception inner) : base(message, inner)
    {
    }
}