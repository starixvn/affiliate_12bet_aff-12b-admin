using System;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data;
using System.Data;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public class NoteRepository : BaseRepository, INoteRepository
    {
        public EntityResult<Note> GetNoteList(NoteParam param)
        {
            const string SpKey = "Aff_AdminNoteSel";
            var result = new EntityResult<Note>();
            var dataList = new List<Note>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, 4);
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(BuildNoteItem(reader));
                    }
                }
                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }
            return result;
        }

        public Note GetNoteById(int affId, int noteId)
        {
            const string SpKey = "Aff_AdminNoteSel";

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, affId);
                objectDb.AddInParameter("NoteId", DbType.Int32, noteId);

                using (var reader = objectDb.ExecuteReader())
                {
                    return BuildNoteItem(reader);
                }
            }
        }

        public ResultInfo InsertNote(InsertUpdateNoteParam param)
        {
            const string SpKey = "Aff_AdminNoteIns";
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
            const string SpKey = "Aff_AdminNoteIns";
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
            objectDb.AddInParameter("CustID", DbType.Int32, param.AffId);
            objectDb.AddInParameter("Updateby", DbType.Int32, param.UserId);
            objectDb.AddInParameter("Note", DbType.String, param.NoteContent);
            objectDb.AddInParameter("IP", DbType.String, param.Ip);

            objectDb.AddOutParameter("ErrCode", DbType.Int32, 4);
            objectDb.AddOutParameter("ErrMsg", DbType.String, 2000);
        }

        private static Note BuildNoteItem(IDataReader reader)
        {
            return new Note
            {
                NoteId = Convert.ToInt32(reader["NoteId"]),
                NoteContent = Convert.ToString(reader["Note"]),
                CreatedBy = Convert.ToString(reader["UpdateName"]),
                LastModify = reader["UpdatedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedDate"])
            };
        }
    }
}
