using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class ViewLogService : BaseRepository, IViewLogService
    {
        public EntityResult<ViewLog> GetViewLogList(ViewLogParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateLog_Sel";
            var result = new EntityResult<ViewLog>();
            var dataList = new List<ViewLog>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("LogActionIds", DbType.String, param.LogActionIds);
                objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    if (param.IsNewViewLog)
                    {
                        while (reader.Read())
                        {
                            var item = BuildViewLogItem(reader);
                            item.OldData = ParseXmlToJson(Convert.ToString(reader["OldData"]));
                            item.NewData = ParseXmlToJson(Convert.ToString(reader["NewData"]));

                            dataList.Add(item);
                        }
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            var item = BuildViewLogItem(reader);
                            item.Description = Convert.ToString(reader["Description"]);

                            dataList.Add(item);
                        }
                    }
                }

                result.MainData = dataList;
                result.TotalItem = GetTotalRow(objectDb);
            }

            return result;
        }

        public IEnumerable<LogAction> GetLogActionList()
        {
            const string SpKey = "Aff_LogAction_Sel";
            var dataList = new List<LogAction>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new LogAction
                        {
                            ActionIds = Convert.ToString(reader["ActionIds"]),
                            ActionName = Convert.ToString(reader["ActionName"]),
                        });
                    }
                }
            }

            return dataList;
        }

        private static ViewLog BuildViewLogItem(IDataReader reader)
        {
            return new ViewLog
            {
                Id = Convert.ToInt32(reader["LogId"]),
                Date = Convert.ToDateTime(reader["Date"]),
                UpdatedBy = Convert.ToString(reader["UpdatedName"]),
                Action = Convert.ToString(reader["Action"]),
                Ip = Convert.ToString(reader["IP"]),
            };
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
