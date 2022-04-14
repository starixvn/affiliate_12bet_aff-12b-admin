using System;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public class DateTimeHelper: IDateTimeHelper
    {
        public DateTime GetNowDateTime()
        {
            return DateTime.Now;
        }
    }
}
