using PipeLight.Tests.Mocks;

namespace PipeLight.Tests;

internal static class TestHelper
{
    public static MockPayloadInt GetMockPayload(int value = 5, object? refValue = null)
    {
        refValue ??= new();
        return new()
        {
            Value = value,
            RefValue = refValue,
        };
    }
}
