using PipeLight;
using PipeLight.Extensions;
using TestProject1.Mocks;
using TestProject1.Mocks.Steps;

namespace TestProject1;

public class NonGenericPipelineTests
{
    #region Creation Tests

    [Fact]
    public void CreationTest()
    {
        var pipeline = new Pipeline();
        Assert.NotNull(pipeline);
    }
    [Fact]
    public void AddAsyncStepTest()
    {
        var pipeline = new Pipeline()
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .Seal(new MockSealAsync());
        Assert.NotNull(pipeline);
    }
    [Fact]
    public void AddAsyncSealStepTest()
    {
        var pipeline = new Pipeline()
            .AddStep(new MockSealAsync());
        Assert.NotNull(pipeline);
    }
    [Fact]
    public void AddAsyncSealTest()
    {
        var pipeline = new Pipeline()
            .Seal(new MockSealAsync());
        Assert.NotNull(pipeline);
    }

    #endregion

    #region Push Tests

    [Fact]
    public async Task PushAsyncTest()
    {
        var payload = TestHelper.GetMockPayload();
        var pipeline = new Pipeline()
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .AddStep(new MockAsyncStepWithResult());

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
        var pipeline = new Pipeline()
            .AddStep(new MockAsyncStepWithResult());

        var result1 = await pipeline.PushAsync(payload1);
        var result2 = await pipeline.PushAsync(payload2);

        Assert.True(
            result1.Value != result2.Value &&
            result1.RefValue != result2.RefValue);
    }

    #endregion

}