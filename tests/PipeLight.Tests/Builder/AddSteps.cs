using PipeLight.Tests.Helpers;

namespace PipeLight.Tests.Builder;

public class AddSteps
{
    
    [Fact]
    public void AddStep_SingleStep_PipelineLengthEqualsStepsCount()
    {
        var builder = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep());
        Assert.Equal(1, builder.PipelineLength);
    }
    
    [Fact]
    public void AddStep_ThreeStep_PipelineLengthEqualsStepsCount()
    {
        var builder = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep());
        Assert.Equal(3, builder.PipelineLength);
    }
    
    [Fact]
    public void AddTransform_SingleStep_PipelineLengthEqualsStepsCount()
    {
        var builder = TestHelper.ActivatorBuilder
            .AddTransform(StepsFactory.IntToString());
        Assert.Equal(1, builder.PipelineLength);
    }
    
    [Fact]
    public void Seal_SingleStep_PipelineLengthEqualsStepsCount()
    {
        var builder = TestHelper.ActivatorBuilder
            .Seal(StepsFactory.MockSeal());
        Assert.Equal(1, builder.PipelineLength);
    }
    
    [Fact]
    public void AddTransform_ThreeStep_PipelineLengthEqualsStepsCount()
    {
        var builder = TestHelper.ActivatorBuilder
            .AddTransform(StepsFactory.IntToString())
            .AddTransform(StepsFactory.StringToInt())
            .AddTransform(StepsFactory.IntToString());
        Assert.Equal(3, builder.PipelineLength);
    }
    
    [Fact]
    public void AddStepSeal_ThreeStep_PipelineLengthEqualsStepsCount()
    {
        var builder = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .Seal(StepsFactory.MockSeal());
        Assert.Equal(3, builder.PipelineLength);
    }
    
    [Fact]
    public void AddStepAddTransform_ThreeStep_PipelineLengthEqualsStepsCount()
    {
        var builder = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddTransform(StepsFactory.MockWithResult());
        Assert.Equal(3, builder.PipelineLength);
    }
    
    [Fact]
    public void AddStepAddTransformSeal_ThreeStep_PipelineLengthEqualsStepsCount()
    {
        var builder = TestHelper.ActivatorBuilder
            .AddAction(StepsFactory.MockStep())
            .AddAction(StepsFactory.MockStep())
            .AddTransform(StepsFactory.MockWithResult())
            .Seal(StepsFactory.MockWithResultSeal());
        Assert.Equal(4, builder.PipelineLength);
    }
}
