using PipeLight.Abstractions.Builders;
using PipeLight.Builders;
using PipeLight.Mocks;
using PipeLight.Steps;

namespace PipeLight.Tests.Helpers;

internal static class TestHelper
{
    public static MockPayloadInt GetMockPayload(int value = 0, object? refValue = null) =>
        new()
        {
            Value = value,
            RefValue = refValue ?? new(),
        };
    public static IPipelineBuilder ActivatorBuilder => new PipelineBuilder(new ActivatorStepResolver());
}
