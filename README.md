[![](https://img.shields.io/nuget/v/soenneker.rebrickable.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.rebrickable.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.rebrickable.clientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.rebrickable.clientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.rebrickable.clientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.rebrickable.clientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.rebrickable.clientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.rebrickable.clientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Rebrickable.ClientUtil

Provides a lazily initialized Rebrickable client for LEGO sets, parts, colors, minifigures, elements, themes, and user collections.

## Installation

```bash
dotnet add package Soenneker.Rebrickable.ClientUtil
```

## Configuration

```json
{
  "Rebrickable": {
    "ApiKey": "your-api-key"
  }
}
```

## Usage

```csharp
using Soenneker.Rebrickable.ClientUtil.Abstract;
using Soenneker.Rebrickable.ClientUtil.Registrars;

services.AddRebrickableClientUtilAsSingleton();

public sealed class RebrickableColorService
{
    private readonly IRebrickableClientUtil _rebrickable;

    public RebrickableColorService(IRebrickableClientUtil rebrickable)
    {
        _rebrickable = rebrickable;
    }

    public async Task GetColors(CancellationToken cancellationToken)
    {
        var client = await _rebrickable.Get(cancellationToken);
        await using Stream colors = await client.Api.V3.Lego.Colors.GetAsync(
            cancellationToken: cancellationToken);
    }
}
```

The underlying provider sends `Authorization: key <api-key>` on every request. Rebrickable list endpoints are paginated; use their `next` response value or the `page` and `page_size` query parameters rather than assuming the first response is complete.

Use `AddRebrickableClientUtilAsScoped()` when each scope should have its own lazily initialized API client. Both registrations reuse the singleton authenticated HTTP client provider.
