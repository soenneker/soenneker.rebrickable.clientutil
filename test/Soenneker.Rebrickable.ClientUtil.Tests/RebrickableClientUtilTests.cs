using Soenneker.Rebrickable.ClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Rebrickable.ClientUtil.Tests;

[Collection("Collection")]
public sealed class RebrickableClientUtilTests : FixturedUnitTest
{
    private readonly IRebrickableClientUtil _kiotaclient;

    public RebrickableClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _kiotaclient = Resolve<IRebrickableClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
