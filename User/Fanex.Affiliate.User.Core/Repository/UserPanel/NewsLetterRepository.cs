using Fanex.Affiliate.User.Core.DTO.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class NewsLetterRepository : INewsLetterRepository
    {
        private IObjectDBBuilder _builder { get; }

        public NewsLetterRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public NewsLetter GetNewsLetterById(int id)
        {
            const string spKey = "Aff_Admin_Newsletter_Sel";
            using( var objectDb = _builder.Build(spKey))
            {
                return objectDb.QueryEntity<NewsLetter>(new { NewsletterId = id });
            }
        }

        public IEnumerable<NewsLetter> GetNewsLettersByDate(DateTime date)
        {
            const string spKey = "Aff_Admin_Newsletter_Sel";
            using (var objectDb = _builder.Build(spKey))
            {
                return objectDb.Query<NewsLetter>(new { Date = date });
            }
        }
    }
}
