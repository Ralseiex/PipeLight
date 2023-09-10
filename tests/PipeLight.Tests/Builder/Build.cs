using PipeLight.Tests.Helpers;

namespace PipeLight.Tests.Builder;

public class Build
{
    [Fact]
    public void Build_WithSingleStep_PipelineIsNotNull()
    {
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .Build();
        Assert.NotNull(pipeline);
    }
    
    [Fact]
    public void Build_WithSingleTransform_PipelineIsNotNull()
    {
        var pipeline = TestHelper.ActivatorBuilder
            .AddTransform(StepsFactory.IntToString())
            .Build();
        Assert.NotNull(pipeline);
    }
    
    [Fact]
    public void Build_WithSingleSeal_PipelineIsNotNull()
    {
        var pipeline = TestHelper.ActivatorBuilder
            .Seal(StepsFactory.MockSeal())
            .Build();
        Assert.NotNull(pipeline);
    }
    
    [Fact]
    public void Build_WithThreeSteps_PipelineIsNotNull()
    {
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .Build();
        Assert.NotNull(pipeline);
    }
    
    [Fact]
    public void Build_WithThreeTransforms_PipelineIsNotNull()
    {
        var pipeline = TestHelper.ActivatorBuilder
            .AddTransform(StepsFactory.IntToString())
            .AddTransform(StepsFactory.StringToInt())
            .AddTransform(StepsFactory.IntToString())
            .Build();
        Assert.NotNull(pipeline);
    }
    
    [Fact]
    public void Build_WithStepsAndSeal_PipelineIsNotNull()
    {
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .Seal(StepsFactory.MockSeal())
            .Build();
        Assert.NotNull(pipeline);
    }
    
    [Fact]
    public void Build_WithStepsAndTransform_PipelineIsNotNull()
    {
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddTransform(StepsFactory.MockWithResult())
            .Build();
        Assert.NotNull(pipeline);
    }
    
    [Fact]
    public void Build_WithStepsAndTransformAndSeal_PipelineIsNotNull()
    {
        var pipeline = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddTransform(StepsFactory.MockWithResult())
            .Seal(StepsFactory.MockWithResultSeal())
            .Build();
        Assert.NotNull(pipeline);
    }
}
