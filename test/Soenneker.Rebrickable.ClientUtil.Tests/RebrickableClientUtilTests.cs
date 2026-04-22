using Soenneker.Rebrickable.ClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Rebrickable.ClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class RebrickableClientUtilTests : HostedUnitTest
{
    private readonly IRebrickableClientUtil _kiotaclient;

    public RebrickableClientUtilTests(Host host) : base(host)
    {
        _kiotaclient = Resolve<IRebrickableClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
