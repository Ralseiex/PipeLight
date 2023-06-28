using PipeLight.Abstractions.Steps;
using PipeLight.Mocks.Steps;

namespace PipeLight.Tests.Helpers;

internal static class StepsFactory
{
    
    public static MockStep MockStep() => new();
    
    public static MockWithResult MockWithResult() => new();
    
    public static MockSeal MockSeal() => new();
    
    public static MockWithResultSeal MockWithResultSeal() => new();
    
    public static IntToStringTransform IntToString() => new();
    
    public static StringToIntTransform StringToInt() => new();
    
    public static MockTimerStep Timer(TimeSpan delay) => new(delay);
    
    public static MockTimerStep Timer(int milliseconds) => new(TimeSpan.FromMilliseconds(milliseconds));
    
    public static MockStepWithException MockStepWithException() => new();
    
    public static StepWithException<TException> StepWithException<TException>() where TException : Exception, new() 
        => new();

    public static LoggingPipeStep<T> LoggingPipeStep<T>(int message) => new(message);
    
    public static LoggingPipeStep<T> LoggingPipeStep<T>(string? message = null) => new(message);

    public static IncrementPipeStep IncrementPipeStep() => new();
}
