using Fanex.Affiliate.User.Core.DTO;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.Repository
{
    public class ObjectDBBuilder : IObjectDBBuilder
    {
        public IObjectDb Build(string spKey, string dbMode = DBMode.Main)
            => new ObjectDb(spKey + dbMode);
    }
}