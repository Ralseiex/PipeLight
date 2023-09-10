using System.Diagnostics;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Mocks.Steps;

public class LoggingPipeAction<T> : IPipelineAction<T>
{
    private readonly string? _message;

    public LoggingPipeAction(int message) : this(message.ToString())
    {
    }
    public LoggingPipeAction(string? message = null) => _message = message;

    public Task<T> Execute(T payload)
    {
        Log(payload);
        return Task.FromResult(payload);
    }
    
    private void Log(T payload)
    {
        var log = _message ?? string.Empty;
        log += $" Type: {payload?.GetType().Name}, Value: {payload}";
        Console.WriteLine(log);
        Debug.WriteLine(log);
    }
}
