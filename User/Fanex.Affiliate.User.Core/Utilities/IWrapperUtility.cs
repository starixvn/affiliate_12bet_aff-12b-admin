using System;
using Fanex.Logging;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public interface IWrapperUtility
    {
        DateTime GetNowDateTime();

        Random GetRandomGenerator();

        ILogger GetLogger();
    }
}
