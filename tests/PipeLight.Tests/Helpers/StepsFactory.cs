using PipeLight.Mocks.Steps;

namespace PipeLight.Tests.Helpers;

internal static class StepsFactory
{
    
    public static MockAction MockStep() => new();
    
    public static MockWithResult MockWithResult() => new();
    
    public static MockSeal MockSeal() => new();
    
    public static MockWithResultSeal MockWithResultSeal() => new();
    
    public static IntToStringTransform IntToString() => new();
    
    public static StringToIntTransform StringToInt() => new();
    
    public static MockTimerAction Timer(TimeSpan delay) => new(delay);
    
    public static MockTimerAction Timer(int milliseconds) => new(TimeSpan.FromMilliseconds(milliseconds));
    
    public static MockActionWithException MockStepWithException() => new();
    
    public static ActionWithException<TException> StepWithException<TException>() where TException : Exception, new() 
        => new();

    public static LoggingPipeAction<T> LoggingPipeStep<T>(int message) => new(message);
    
    public static LoggingPipeAction<T> LoggingPipeStep<T>(string? message = null) => new(message);

    public static IncrementPipeAction IncrementPipeStep() => new();
}
