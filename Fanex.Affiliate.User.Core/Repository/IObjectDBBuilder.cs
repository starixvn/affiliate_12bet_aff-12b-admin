using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public interface IObjectDBBuilder
    {
        IObjectDb DB { get; }
    }
}