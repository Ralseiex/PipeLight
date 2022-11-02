using PipeLight.Interfaces;
using PipeLight.Nodes;
using PipeLight.Nodes.Steps.Interfaces;
using PipeLight.Pipes.Interfaces;
using System.Collections.Immutable;

namespace PipeLight.Pipes;


public class Pipe : IPipe
{
    public IPipe<T> AddStep<T>(IPipeStep<T> firstStep)
    {
        var pipe = new Pipe<T>();
        pipe.AddStep(firstStep);
        return pipe;
    }
    public ISealedPipe<T> Seal<T>(ISealedStep<T> firstAndlastStep)
    {
        return new SealedPipe<T>(firstAndlastStep);
    }
    public IPipeline<T, TNewOut> AddTransform<T, TNewOut>(IPipeTransform<T, TNewOut> pipeTransform)
    {
        var firstNode = new TransformNode<T, TNewOut>(pipeTransform);
        var pipeline = new Pipeline<T, TNewOut>(firstNode);
        return pipeline;
    }
    public IPipeline<T, T> AddPipe<T>(IPipe<T> pipe)
    {
        return new Pipeline<T>(pipe);
    }
}

public class Pipe<T> : IPipe<T>
{
    private readonly List<IPipeStep<T>> _steps = new();
    public IEnumerable<IPipeStep<T>> Steps => _steps.ToImmutableArray();


    public Pipe()
    {

    }
    public Pipe(IPipeStep<T> firstStep)
    {
        AddStep(firstStep);
    }


    public IPipe<T> AddStep(IPipeStep<T> pipeStep)
    {
        _steps.Add(pipeStep);
        return this;
    }
    public ISealedPipe<T> Seal(ISealedStep<T> lastStep)
    {
        return new SealedPipe<T>(this, lastStep);
    }
    public IPipeline<T, TNewOut> AddTransform<TNewOut>(IPipeTransform<T, TNewOut> transform)
    {
        var pipeline = new Pipeline<T>(this)
            .AddTransform(transform);
        return pipeline;
    }

    public async Task<T> PushAsync(T payload)
    {
        var result = payload;
        foreach (var step in _steps)
        {
            result = await step.ExecuteStepAsync(result);
        }
        return result;
    }
}
