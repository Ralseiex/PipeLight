using Benchmarks;
using PipeLight;
using PipeLight.Extensions;
using System.Diagnostics;
using PipeLigth.Tests.Mocks;
using PipeLigth.Tests.Mocks.Steps;

namespace PipeLigth.Tests;

public class Tests
{
    #region Creation Tests

    [Fact]
    public void CreationTest()
    {
        var pipeline = new PipelineFactory();
        Assert.NotNull(pipeline);
    }
    [Fact]
    public void AddAsyncStepTest()
    {
        var pipeline = new PipelineFactory().CreatePipeline()
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .Seal(new MockSealAsync());
        Assert.NotNull(pipeline);
    }
    [Fact]
    public void AddAsyncSealStepTest()
    {
        var pipeline = new PipelineFactory().CreatePipeline()
            .Seal(new MockSealAsync());
        Assert.NotNull(pipeline);
    }
    [Fact]
    public void AddAsyncSealTest()
    {
        var pipeline = new PipelineFactory().CreatePipeline()
            .Seal(new MockSealAsync());
        Assert.NotNull(pipeline);
    }

    #endregion

    #region Push Tests

    [Fact]
    public async Task PushAsyncTest()
    {
        var payload = TestHelper.GetMockPayload();
        var pipeline = new PipelineFactory().CreatePipeline()
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .AddTransform(new MockAsyncStepWithResult());

        var result = await pipeline.PushAsync(payload);

        Assert.True(
            result is MockPipelineResult &&
            result.Value == payload.Value &&
            result.RefValue == payload.RefValue);
    }
    [Fact]
    public async Task DoublePushAsyncTest()
    {
        var payload1 = TestHelper.GetMockPayload(5);
        var payload2 = TestHelper.GetMockPayload(6);
        var pipeline = new PipelineFactory().CreatePipeline()
            .AddTransform(new MockAsyncStepWithResult());

        var result1 = await pipeline.PushAsync(payload1);
        var result2 = await pipeline.PushAsync(payload2);

        Assert.True(
            result1.Value != result2.Value &&
            result1.RefValue != result2.RefValue);
    }

    [Fact]
    public async Task ExceptionInAsyncPipelineTest()
    {
        var payload = TestHelper.GetMockPayload();
        var pipeline = new PipelineFactory().CreatePipeline()
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepWithExceptionAsync());

        await Assert.ThrowsAsync<MockException>(async () =>
        {
            var result = await pipeline.PushAsync(payload);
        });
    }
    [Fact]
    public async Task IncrementTest()
    {
        var pipeline1 =
            new PipelineFactory().CreatePipeline()
            .AddTransform(new StringToIntTransform())
            .AddStep(new IncrementPipeStep())
            .AddTransform(new IntToStringTransform())
            .AddStep(new LoggingPipeStep())
            .AddStep(new LoggingPipeStep())
            .AddTransform(new StringToIntTransform())
            .AddStep(new IncrementPipeStep());

        var pipeline2 =
            new PipelineFactory().CreatePipeline()
            .AddStep(new IncrementPipeStep())
            .AddTransform(new IntToStringTransform())
            .AddStep(new LoggingPipeStep())
            .AddStep(new LoggingPipeStep())
            .AddTransform(new StringToIntTransform())
            .AddStep(new IncrementPipeStep());

        var result1 = await pipeline1.PushAsync("2");
        var result2 = await pipeline2.PushAsync(2);

        Assert.True(result1 == 4 && result2 == 4);
    }
    [Fact]
    public async Task DoublePushTest()
    {
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
            .AddTransform(new IntToStringTransform())
            .AddStep(new LoggingPipeStep())
            .AddTransform(new StringToIntTransform())
            .AddStep(new IncrementPipeStep());

        var task1 = pipeline.PushAsync(1);
        var task2 = pipeline.PushAsync(3);

        await Task.WhenAll(task1, task2);

        Assert.True(task1.Result == 4 && task2.Result == 6);
    }
    [Fact]
    public async Task DoublePushTest2()
    {
        var factory = new PipelineFactory();
        var pipeline = factory.CreatePipeline()
            .AddPipe(factory.CreatePipeline()
                .AddStep((int x) => ++x)
                .AddStep((int x) => ++x)
                .AddStep((int x) => ++x)
            )
            .AddTransform(new IntToStringTransform())
                .AddStep(new LoggingPipeStep())
            .AddTransform(new StringToIntTransform())
            .AddPipe(factory.CreatePipeline()
                .AddStep((int x) => ++x)
                .AddStep((int x) => ++x)
                .AddStep((int x) => ++x)
            )
            ;

        var task1 = pipeline.PushAsync(1);
        var task2 = pipeline.PushAsync(3);

        await Task.WhenAll(task1, task2);

        Assert.True(task1.Result == 7 && task2.Result == 9);
    }

    #endregion

}