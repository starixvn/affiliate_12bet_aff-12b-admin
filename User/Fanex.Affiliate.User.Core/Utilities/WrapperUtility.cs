using System;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public class WrapperUtility : IWrapperUtility
    {
        public DateTime GetNowDateTime()
        {
            return DateTimeHelper.Gmt8Now;
        }

        public Random GetRandomGenerator()
        {
            return new Random();
        }
    }
}
