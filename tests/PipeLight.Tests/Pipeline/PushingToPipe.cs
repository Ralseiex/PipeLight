using PipeLight.Mocks;
using PipeLight.Tests.Helpers;

namespace PipeLight.Tests.Pipeline;

public class PushingToPipe
{
    [Fact]
    public async Task PushToPipe_PipelineDoNothing_PayloadValuesEqualResultValues()
    {
        var payload = TestHelper.GetMockPayload();
        var expected = payload.Clone();
        var pipeline = TestHelper.ActivatorBuilder
            .AddStep(StepsFactory.MockStep())
            .AddStep(StepsFactory.MockStep())
            .AddStep(StepsFactory.MockStep())
            .AddTransform(StepsFactory.MockWithResult())
            .Build();
        var lastPipeId = pipeline.PipesHashes.Last();

        var actual = await pipeline.PushToPipe(payload, lastPipeId);

        Assert.Equal(expected.Value, actual.Value);
        Assert.Equal(expected.RefValue, actual.RefValue);
    }

    [Fact]
    public async Task PushToPipe_PipelineDoNothingConcurrent_PayloadValuesEqualResultValues()
    {
        const int concurrentCount = 10;
        var pipeline = TestHelper.ActivatorBuilder
            .AddStep(StepsFactory.Timer(100))
            .AddStep(StepsFactory.MockStep())
            .AddStep(StepsFactory.MockStep())
            .AddTransform(StepsFactory.MockWithResult())
            .Build();
        var lastPipeId = pipeline.PipesHashes.Last();
        var payloadsWithResults = Enumerable.Range(0, concurrentCount)
            .Select(i => TestHelper.GetMockPayload(i))
            .ToDictionary(payload => payload, payload => pipeline.PushToPipe(payload, lastPipeId));

        await Task.WhenAll(payloadsWithResults.Values);

        foreach (var (payload, task) in payloadsWithResults)
        {
            Assert.Equal(payload.Value, task.Result.Value);
            Assert.Equal(payload.RefValue, task.Result.RefValue);
        }
    }

    [Fact]
    public async Task PushToPipe_SkippingTimer_MockExceptionExceptionThrown()
    {
        var cancellationToken = new CancellationTokenSource(50).Token;
        var payload = TestHelper.GetMockPayload();
        var pipeline = TestHelper.ActivatorBuilder
            .AddStep(StepsFactory.Timer(100))
            .AddStep(StepsFactory.MockStep())
            .AddStep(StepsFactory.MockStepWithException())
            .Build();
        var lastPipeId = pipeline.PipesHashes.Last();

        await Assert.ThrowsAsync<MockException>(async () =>
        {
            var result = await pipeline.PushToPipe(payload, lastPipeId, cancellationToken);
        });
    }

    [Fact]
    public async Task PushToPipe_SkippingExceptionPipeInPipeline_ExceptionNotThrown()
    {
        var payload = TestHelper.GetMockPayload();
        var pipeline = TestHelper.ActivatorBuilder
            .AddStep(StepsFactory.MockStep())
            .AddStep(StepsFactory.MockStep())
            .AddStep(StepsFactory.MockStepWithException())
            .AddStep(StepsFactory.MockStep())
            .Build();
        var lastPipeId = pipeline.PipesHashes.Last();

        var result = await pipeline.PushToPipe(payload, lastPipeId);
    }

    [Fact]
    public async Task PushToPipe_IncrementStringToInt_PayloadIncrementsTwoTimes()
    {
        const int expected = 2;
        const string payload = "1";
        var pipeline = TestHelper.ActivatorBuilder
            .AddTransform(StepsFactory.StringToInt())
            .AddStep(StepsFactory.LoggingPipeStep<int>(1))
            .AddStep(StepsFactory.IncrementPipeStep())
            .AddStep(StepsFactory.LoggingPipeStep<int>(2))
            .AddTransform(StepsFactory.IntToString())
            .AddStep(StepsFactory.LoggingPipeStep<string>(3))
            .AddTransform(StepsFactory.StringToInt())
            .AddStep(StepsFactory.LoggingPipeStep<int>(4))
            .AddStep(StepsFactory.IncrementPipeStep())
            .AddStep(StepsFactory.LoggingPipeStep<int>(5))
            .Build();
        var midPipeId = pipeline.PipesHashes.Skip(5).First();

        var actual = await pipeline.PushToPipe(payload, midPipeId);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task PushToPipe_IncrementIntToString_PayloadIncrementsTwoTimes()
    {
        const string expected = "2";
        const string payload = "1";
        var pipeline = TestHelper.ActivatorBuilder
            .AddStep(StepsFactory.IncrementPipeStep())
            .AddTransform(StepsFactory.IntToString())
            .AddStep(StepsFactory.LoggingPipeStep<string>())
            .AddTransform(StepsFactory.StringToInt())
            .AddStep(StepsFactory.IncrementPipeStep())
            .AddTransform(StepsFactory.IntToString())
            .Build();
        var lastPipeId = pipeline.PipesHashes.Skip(3).First();

        var actual = await pipeline.PushToPipe(payload, lastPipeId);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task PushToPipe_Increment_PayloadIncrementsTwoTimes()
    {
        const int expected = 2;
        const int payload = 1;
        var pipeline = TestHelper.ActivatorBuilder
            .AddStep(StepsFactory.IncrementPipeStep())
            .AddStep(StepsFactory.IncrementPipeStep())
            .Build();
        var lastPipeId = pipeline.PipesHashes.Last();

        var actual = await pipeline.PushToPipe(payload, lastPipeId);

        Assert.Equal(expected, actual);
    }
}