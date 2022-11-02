using PipeLigth.Tests.Mocks;

namespace PipeLigth.Tests;

internal class TestHelper
{
    public static MockPayloadInt GetMockPayload(int value = 5, object refValue = null)
    {
        if (refValue is null)
            refValue = new();
        return new()
        {
            Value = value,
            RefValue = refValue,
        };
    }
}
