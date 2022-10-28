namespace PipeLight.Exceptions;

[Serializable]
public class StepHasNoHandlerException : Exception
{
    public StepHasNoHandlerException() { }
    public StepHasNoHandlerException(string message) : base(message) { }
    public StepHasNoHandlerException(string message, Exception inner) : base(message, inner) { }
    protected StepHasNoHandlerException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
