using PipeLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject1.Mocks.Steps;
using TestProject1.Mocks;

namespace TestProject1;

public class GenericPipelineTests
{
    [Fact]
    public async Task ExceptionInAsyncPipelineTest()
    {
        var payload = TestHelper.GetMockPayload();
        var pipeline = new Pipeline<MockPayload>()
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepAsync())
            .AddStep(new MockStepWithExceptionAsync());

        await Assert.ThrowsAsync<MockException>(async () =>
        {
            var result = await pipeline.PushAsync(payload);
        });
    }
}
