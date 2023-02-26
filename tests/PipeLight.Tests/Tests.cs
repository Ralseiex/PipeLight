﻿using System.Diagnostics;
using PipeLight.Builders;
using PipeLight.Extensions;
using PipeLight.Tests.Mocks;
using PipeLight.Tests.Mocks.Steps;

namespace PipeLight.Tests;

public class Tests
{
    #region Creation Tests

    [Fact]
    public void AddAsyncStep()
    {
        var pipeline = new PipelineBuilder()
            .AddStep(new MockStep())
            .AddStep(new MockStep())
            .AddStep(new MockStep())
            .Seal(new MockSeal());
        Assert.NotNull(pipeline);
    }
    [Fact]
    public void AddAsyncSealStepTest()
    {
        var pipeline = new PipelineBuilder()
            .Seal(new MockSeal());
        Assert.NotNull(pipeline);
    }
    [Fact]
    public void AddAsyncSealTest()
    {
        var pipeline = new PipelineBuilder()
            .Seal(new MockSeal());
        Assert.NotNull(pipeline);
    }

    #endregion

    #region Push Tests

    [Fact]
    public async Task PushAsyncTest()
    {
        var payload = TestHelper.GetMockPayload();
        var builder = new PipelineBuilder();
        var pipeline = builder
            .AddStep(new MockStep())
            .AddStep(new MockStep())
            .AddStep(new MockStep())
            .AddTransform(new MockStepWithResult())
            .Build();

        var result = await pipeline.Push(payload);

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
        var pipeline = new PipelineBuilder()
            .AddTransform(new MockStepWithResult())
            .Build();

        var result1 = await pipeline.Push(payload1);
        var result2 = await pipeline.Push(payload2);

        Assert.True(
            result1.Value != result2.Value &&
            result1.RefValue != result2.RefValue);
    }

    [Fact]
    public async Task ExceptionInAsyncPipelineTest()
    {
        var payload = TestHelper.GetMockPayload();
        var pipeline = new PipelineBuilder()
            .AddStep(new MockStep())
            .AddStep(new MockStep())
            .AddStep(new MockStepWithException())
            .Build();

        await Assert.ThrowsAsync<MockException>(async () =>
        {
            var result = await pipeline.Push(payload);
        });
    }
    [Fact]
    public async Task IncrementWithTransformTest()
    {
        var pipeline1 = new PipelineBuilder()
            .AddTransform(new StringToIntTransform())
            .AddStep(new LoggingPipeStep(1))
            .AddStep(new IncrementPipeStep())
            .AddStep(new LoggingPipeStep(2))
            .AddTransform(new IntToStringTransform())
            .AddStep(new LoggingPipeStep(3))
            .AddTransform(new StringToIntTransform())
            .AddStep(new LoggingPipeStep(4))
            .AddStep(new IncrementPipeStep())
            .AddStep(new LoggingPipeStep(5))
            .Build();

        var pipeline2 =new PipelineBuilder()
            .AddStep(new IncrementPipeStep())
            .AddTransform(new IntToStringTransform())
            .AddStep(new LoggingPipeStep())
            .AddStep(new LoggingPipeStep())
            .AddTransform(new StringToIntTransform())
            .AddStep(new IncrementPipeStep())
            .Build();

        var result1 = await pipeline1.Push("2");
        var result2 = await pipeline2.Push(2);

        
        Assert.Equal(4, result1);
        Assert.Equal(4, result2);
    }
    [Fact]
    public async Task IncrementTest()
    {
        var pipeline = new PipelineBuilder()
            .AddStep(new IncrementPipeStep())
            .AddStep(new IncrementPipeStep())
            .Build();

        var result = await pipeline.Push(2);

        Assert.Equal(4, result);
    }
    [Fact]
    public async Task DoublePushTest()
    {
        var pipeline = new PipelineBuilder()
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
            .AddStep(new IncrementPipeStep())
            .Build();
    
        var task1 = pipeline.Push(1);
        var task2 = pipeline.Push(3);
    
        await Task.WhenAll(task1, task2);
    
        Assert.Equal(4, task1.Result);
        Assert.Equal(6, task2.Result);
    }
    [Fact]
    public async Task DoublePushTest2()
    {
        var pipeline = new PipelineBuilder()
                .AddStep((int x) => ++x)
                .AddStep(x => ++x)
                .AddStep(x => ++x)
            .AddTransform(new IntToStringTransform())
                .AddStep(new LoggingPipeStep())
            .AddTransform(new StringToIntTransform())
                .AddStep(x => ++x)
                .AddStep(x => ++x)
                .AddStep(x => ++x)
            .Build()
            ;
    
        var task1 = pipeline.Push(1);
        var task2 = pipeline.Push(3);
    
        await Task.WhenAll(task1, task2);
    
        Assert.Equal(7, task1.Result);
        Assert.Equal(9, task2.Result);
    }

    #endregion

}