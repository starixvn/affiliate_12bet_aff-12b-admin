using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class BaseRepository
    {
        protected internal virtual IObjectDb GetObjectDb(string spName, int timeout = 120)
        {
            return new ObjectDb(spName, timeout);
        }
    }
}