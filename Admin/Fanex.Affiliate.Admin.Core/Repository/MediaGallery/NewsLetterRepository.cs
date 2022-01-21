using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Data;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public class NewsLetterRepository : BaseRepository, INewsLetterRepository
    {
        public ResultInfo DeleteNewsLetter(DeleteNewsLetterParam param)
        {
            const string spKey = "Aff_Admin_Newsletter_Del";
            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }

        public NewsLetter GetNewsLetterById(int id)
        {
            const string spKey = "Aff_Admin_Newsletter_Sel";
            using (var objectDb = GetObjectDb(spKey))
            {
                return objectDb.QueryEntity<NewsLetter>(new { NewsletterId = id });
            }
        }

        public EntityResult<NewsLetter> GetNewsLetters(PagingParam param)
        {
            const string spKey = "Aff_Admin_Newsletter_Sel";
            using (var objectDb = GetObjectDb(spKey))
            {
                objectDb.SetParameterValue("NRows", DefaultValue.Int);
                var items = objectDb.Query<NewsLetter>(param);

                return new EntityResult<NewsLetter>
                {
                    MainData = items,
                    TotalItem = GetTotalRow(objectDb)
                };
            }
        }

        public ResultInfo InsertNewsLetter(InsertHtmlFileParam param)
        {

            const string spKey = "Aff_Admin_Newsletter_Ins";
            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateNewsLetter(UpdateNewsLetterParam param)
        {
            const string spKey = "Aff_Admin_Newsletter_Upd";
            using (var objectDb = GetObjectDb(spKey))
            {
                AssignResultOutputParam(objectDb);
                objectDb.ExecuteNonQuery(param);
                return GetResultInfo(objectDb);
            }
        }
    }
}
