using System;

namespace Fanex.Affiliate.User.Core.Infrastructure
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }

        DateTime Gmt8Now { get; }

        DateTime ServerTime { get; }
    }
}
