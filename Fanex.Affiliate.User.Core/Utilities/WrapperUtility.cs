using System;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public class WrapperUtility : IWrapperUtility
    {
        public DateTime GetNowDateTime()
        {
            return DateTime.Now;
        }

        public Random GetRandomGenerator()
        {
            return new Random();
        }
    }
}
