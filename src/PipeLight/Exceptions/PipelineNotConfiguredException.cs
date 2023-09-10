namespace PipeLight.Exceptions;

internal sealed class PipelineNotConfiguredException : Exception
{
    public PipelineNotConfiguredException()
    {
    }

    public PipelineNotConfiguredException(string message) : base(message)
    {
    }

    public PipelineNotConfiguredException(string message, Exception inner) : base(message, inner)
    {
    }
}