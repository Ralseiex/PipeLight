using PipeLight.Abstractions.Builders;
using PipeLight.Factories;
using PipeLight.Mocks;

namespace PipeLight.Tests.Helpers;

internal static class TestHelper
{
    public static MockPayloadInt GetMockPayload(int value = 0, object? refValue = null) =>
        new()
        {
            Value = value,
            RefValue = refValue ?? new(),
        };
    public static IPipelineBuilder ActivatorBuilder => new ActivatorPipelineBuilderFactory().CreateBuilder();
}
