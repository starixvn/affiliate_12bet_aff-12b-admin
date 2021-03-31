using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.MetaModels.NewsViewLog;
using Fanex.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public class NewViewLogRepository : BaseRepository, INewViewLogRepository
    {
        public EntityResult<NewViewLogInfo> GetNewViewLogList(NewViewLogParam param)
        {
            const string SpKey = "Aff_Admin_Affiliate_Sel";
            var result = new EntityResult<NewViewLogInfo>();
            var dataList = new List<NewViewLogInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("UserId", DbType.Int32, param.UserId);
                objectDb.AddInParameter("UserName", DbType.String, param.AffName);
                objectDb.AddInParameter("PreferredLanguages", DbType.String, "en-GB,en-US,hi-IN,id-ID,ko-KR,th-TH,vi-VN,zh-CN,zh-TW");
                AssignParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new NewViewLogInfo {
                            AffId = Convert.ToInt32(reader[Field.AffId]),
                            AffName = Convert.ToString(reader["Username"]),
                            ActionNumber = reader["NoOfMember"] == DBNull.Value ? 0 : Convert.ToInt32(reader["NoOfMember"]),
                            CreatedDate = Convert.ToDateTime(reader["RegisteredDate"]),
                            Action = Convert.ToString(reader["Status"])
                        });
                    }
                }
                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }
            return result;
        }

        public EntityResult<NewViewLogDetailInfo> GetNewViewLogDetailList(NewViewLogParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateLog_Sel";
            var result = new EntityResult<NewViewLogDetailInfo>();
            var dataList = new List<NewViewLogDetailInfo>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("LogActionIds", DbType.String, param.LogActionIds);
                objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                AssignParam(objectDb, param);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new NewViewLogDetailInfo
                        {
                            Id = Convert.ToInt32(reader["LogId"]),
                            CreatedDate = Convert.ToDateTime(reader["Date"]),
                            OldData = ParseXmlToJson(Convert.ToString(reader["OldData"])),
                            NewData = ParseXmlToJson(Convert.ToString(reader["NewData"])),
                            UpdatedBy = Convert.ToString(reader["UpdatedName"]),
                            Action = Convert.ToString(reader["Action"]),
                            Ip = Convert.ToString(reader["IP"]),
                        });
                    }
                }

                result.MainData = dataList;
                var totalRow = objectDb.GetParameterValue("NRows");
                result.TotalItem = totalRow == DBNull.Value ? 0 : Convert.ToInt32(totalRow);
            }

            return result;
        }

        private static void AssignParam(IObjectDb objectDb, NewViewLogParam param)
        {
            //objectDb.AddInParameter("UserName", DbType.String, param.AffName);
            objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
            objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);
            objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);
        }

        private static string ParseXmlToJson(string xmlString)
        {
            if (!string.IsNullOrEmpty(xmlString) && xmlString.TrimStart().StartsWith("<"))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlString);

                    var rootNode = doc.SelectSingleNode("root");
                    var childNodes = rootNode.ChildNodes;

                    if (childNodes.Count != 1)
                    {
                        return string.Empty;
                    }

                    return JsonConvert.SerializeXmlNode(doc);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
