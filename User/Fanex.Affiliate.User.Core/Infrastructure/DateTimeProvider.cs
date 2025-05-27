namespace Fanex.Affiliate.User.Core.Infrastructure
{
    using System;
    using Utilities;

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTimeHelper.UtcNow;

        public DateTime Gmt8Now => DateTimeHelper.Gmt8Now;

        public DateTime ServerTime => DateTimeHelper.ServerTime;
    }
}
