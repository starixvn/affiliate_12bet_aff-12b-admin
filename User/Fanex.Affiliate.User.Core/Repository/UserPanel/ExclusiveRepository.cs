using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class ExclusiveRepository : IExclusiveRepository
    {
        private readonly IObjectDBBuilder _builder;

        public ExclusiveRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public Exclusive GetExclusiveById(int exclusiveId)
        {
            const string spKey = "Aff_Admin_Exclusive_Sel";
            using (var objectDb = _builder.Build(spKey))
            {
                return objectDb.QueryEntity<Exclusive>(new { exclusiveId });
            }
        }
    }
}
