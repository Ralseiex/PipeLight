# PipeLight
PipeLight is a lightweight library that helps you to build sequential chains of code (pipelines) with clear syntax.

## Examples
Creating "Steps":
```C#
public class IncrementPipeStep : IPipeStep<int>
{
    public async Task<int> ExecuteStepAsync(int payload)
    {
        return await Task.FromResult(payload + 1);
    }
}
public class LoggingPipeStep : IPipeStep<string>
{
    public async Task<string> ExecuteStepAsync(string payload)
    {
        Console.WriteLine(payload);
        Debug.WriteLine(payload);
        return await Task.FromResult(payload);
    }
}
public class IntToStringFitting : IPipeTransform<int, string>
{
    public async Task<string> TransformAsync(int payload)
    {
        return await Task.FromResult(payload.ToString());
    }
}
public class StringToIntFitting : IPipeTransform<string, int>
{
    public async Task<int> TransformAsync(string payload)
    {
        return await Task.FromResult(int.Parse(payload));
    }
}
```
Creating pipeline:
```C#
var factory = new PipelineFactory();
var pipeline = factory.CreatePipeline()
    .AddStep(new IncrementPipeStep())
    .AddStep(new IncrementPipeStep())
    .AddTransform(new IntToStringTransform())
    .AddStep(new LoggingPipeStep())
    .AddTransform(new StringToIntTransform())
    .AddStep(new IncrementPipeStep());
```
We can also use lambda:
```C#
var factory = new PipelineFactory();
var pipeline = factory.CreatePipeline()
    .AddStep((int p) => ++p)
    .AddStep(p => ++p)
    .AddTransform(p => p.ToString())
    .AddStep(p =>
    {
        Console.WriteLine(p);
        Debug.WriteLine(p);
        return p;
    })
    .AddTransform(p => int.Parse(p))
    .AddStep(new IncrementPipeStep());
```
Executing pipeline:
```C#
var result1 = await pipeline.PushAsync(1); // result1 = 4
var result2 = await pipeline.PushAsync(3); // result2 = 6
```
Parallel execution:
```C#
var task1 = pipeline.PushAsync(1);
var task2 = pipeline.PushAsync(3);
var task3 = pipeline.PushAsync(5);

await Task.WhenAll(task1, task2, task3);
```

## Steps
Use following interfaces to define steps:
- `IPipeStep<T>` - takes a value of type T and passes a value of type T further down the pipeline.
- `IPipeTransform<TIn, TOut>` - takes a value of type TIn and passes a value of type TOut further down the pipeline. 
- `ISealedStep<T>` - takes a value of type T, but doesn't pass anything further. At this step, the pipeline completes its work.

Please note that all steps are asynchronous.

## SealedPipeline
If you add a `seal` step to the pipeline using the `Seal(ISealedPipe<T> lastStep)` method, then you will get a `SealedPipeline` and you won't be able to continue building the pipeline. `ISealedPipe<T>` returns nothing.