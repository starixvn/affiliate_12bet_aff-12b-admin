using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class NoteService : BaseRepository, INoteService
    {
        public EntityResult<NoteDetail> GetNoteList(AffiliatePagingParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateNote_Sel";
            var result = new EntityResult<NoteDetail>();
            var dataList = new List<NoteDetail>();
            NoteDetail dataItem = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataItem = BuildNoteItem(reader);
                        dataItem.CreatedBy = Convert.ToString(reader["UpdatedName"]);
                        dataItem.LastModify = reader["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedDate"]);

                        dataList.Add(dataItem);
                    }
                }
                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        public Note GetNoteContentById(int affId, int noteId)
        {
            const string SpKey = "Aff_Admin_AffiliateNote_Detail_Sel";
            Note result = null;

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, affId);
                objectDb.AddInParameter("NoteId", DbType.Int32, noteId);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = BuildNoteItem(reader);
                    }
                }
            }

            return result;
        }

        public ResultInfo InsertNote(InsertUpdateNoteParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateNote_Ins";
            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        public ResultInfo UpdateNote(InsertUpdateNoteParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateNote_Upd";
            using (var objectDb = GetObjectDb(SpKey))
            {
                AssignCommonParam(objectDb, param);
                objectDb.AddInParameter("Noteid", DbType.Int32, param.NoteId);
                AssignResultOutputParam(objectDb);

                objectDb.ExecuteNonQuery();

                return GetResultInfo(objectDb);
            }
        }

        private static void AssignCommonParam(IObjectDb objectDb, InsertUpdateNoteParam param)
        {
            objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
            objectDb.AddInParameter("UserID", DbType.Int32, param.UserId);
            objectDb.AddInParameter("Note", DbType.String, param.NoteContent);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);
        }

        private static NoteDetail BuildNoteItem(IDataReader reader)
        {
            return new NoteDetail
            {
                NoteId = Convert.ToInt32(reader["NoteId"]),
                NoteContent = Convert.ToString(reader["Note"])
            };
        }
    }
}
