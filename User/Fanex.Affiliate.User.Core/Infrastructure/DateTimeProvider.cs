namespace Fanex.Affiliate.User.Core.Infrastructure
{
    using System;
    using Utilities;

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTimeHelper.Now;
    }
}
