using System.Diagnostics;
using PipeLight.Abstractions.Steps;

namespace PipeLight.Tests.Mocks.Steps;

internal class LoggingPipeStep : IPipelineStep<string>, IPipelineStep<int>
{
    private readonly string? _message;

    public LoggingPipeStep(int message) : this(message.ToString())
    {
        
    }
    public LoggingPipeStep(string? message = null)
    {
        _message = message;
    }
    public Task<string> Execute(string payload)
    {
        Log(payload);
        return Task.FromResult(payload);
    }

    public Task<int> Execute(int payload)
    {
        Log(payload);
        return Task.FromResult(payload);
    }

    private void Log(object payload)
    {
        var log = _message ?? string.Empty;
        log += $" Type: {payload.GetType().Name}, Value: {payload}";
        Console.WriteLine(log);
        Debug.WriteLine(log);
    }
}