using PipeLight.Mocks;
using PipeLight.Tests.Helpers;

namespace PipeLight.Tests.Pipeline;

public class Pushing
{
    [Fact]
    public async Task Push_PipelineDoNothing_PayloadValuesEqualResultValues()
    {
        var payload = TestHelper.GetMockPayload();
        var expected = payload.Clone();
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddTransform(StepsFactory.MockWithResult())
            .Build();

        var actual = await pipeline.Push(payload);

        Assert.Equal(expected.Value, actual.Value);
        Assert.Equal(expected.RefValue, actual.RefValue);
    }

    [Fact]
    public async Task Push_PipelineDoNothingConcurrent_PayloadValuesEqualResultValues()
    {
        const int concurrentCount = 10;
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.Timer(100))
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddTransform(StepsFactory.MockWithResult())
            .Build();
        var payloadsWithResults = Enumerable.Range(0, concurrentCount)
            .Select(i => TestHelper.GetMockPayload(i))
            .ToDictionary(payload => payload, payload => pipeline.Push(payload));

        await Task.WhenAll(payloadsWithResults.Values);

        foreach (var (payload, task) in payloadsWithResults)
        {
            Assert.Equal(payload.Value, task.Result.Value);
            Assert.Equal(payload.RefValue, task.Result.RefValue);
        }
    }

    [Fact]
    public async Task Push_PipelineWorksToLong_OperationCanceledExceptionThrown()
    {
        var cancellationToken = new CancellationTokenSource(50).Token;
        var payload = TestHelper.GetMockPayload();
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.Timer(100))
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStepWithException())
            .Build();

        await Assert.ThrowsAsync<OperationCanceledException>(async () =>
        {
            await pipeline.Push(payload, cancellationToken);
        });
    }

    [Fact]
    public async Task Push_ExceptionInPipeline_MockStepWithExceptionThrown()
    {
        var payload = TestHelper.GetMockPayload();
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStepWithException())
            .Build();

        await Assert.ThrowsAsync<MockException>(async () =>
        {
            await pipeline.Push(payload);
        });
    }

    [Fact]
    public async Task Push_IncrementStringToInt_PayloadIncrementsTwoTimes()
    {
        const int expected = 2;
        const string payload = "0";
        var pipeline = TestHelper.ActivatorBuilder
            .AddTransform(StepsFactory.StringToInt())
            .AddAction(StepsFactory.LoggingPipeStep<int>(1))
            .AddAction(StepsFactory.IncrementPipeStep())
            .AddAction(StepsFactory.LoggingPipeStep<int>(2))
            .AddTransform(StepsFactory.IntToString())
            .AddAction(StepsFactory.LoggingPipeStep<string>(3))
            .AddTransform(StepsFactory.StringToInt())
            .AddAction(StepsFactory.LoggingPipeStep<int>(4))
            .AddAction(StepsFactory.IncrementPipeStep())
            .AddAction(StepsFactory.LoggingPipeStep<int>(5))
            .Build();

        var actual = await pipeline.Push(payload);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Push_IncrementIntToString_PayloadIncrementsTwoTimes()
    {
        const string expected = "2";
        const int payload = 0;
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.IncrementPipeStep())
            .AddTransform(StepsFactory.IntToString())
            .AddAction(StepsFactory.LoggingPipeStep<string>())
            .AddTransform(StepsFactory.StringToInt())
            .AddAction(StepsFactory.IncrementPipeStep())
            .AddTransform(StepsFactory.IntToString())
            .Build();

        var actual = await pipeline.Push(payload);
        
        Assert.Equal(expected, actual);
    }

    // [Fact]
    // public async Task Push_Increment_PayloadIncrementsTwoTimes()
    // {
    //     const int expected = 2;
    //     const int payload = 0;
    //     var pipeline = TestHelper.ActivatorBuilder
    //         .AddStep(StepsFactory.IncrementPipeStep())
    //         .AddStep(StepsFactory.IncrementPipeStep())
    //         .Build();
    //
    //     var actual = await pipeline.Push(payload);
    //
    //     Assert.Equal(expected, actual);
    // }
}
