using System;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public interface IWrapperUtility
    {
        DateTime GetGmt8NowDateTime();

        Random GetRandomGenerator();
    }
}
