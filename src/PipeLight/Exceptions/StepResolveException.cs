namespace PipeLight.Exceptions;

public class StepResolveException : Exception
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
