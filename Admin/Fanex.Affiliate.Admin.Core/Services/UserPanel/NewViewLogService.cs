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
    public class NewViewLogService : BaseRepository, INewViewLogService
    {
        public EntityResult<NewViewLog> GetViewLogList(ViewLogParam param)
        {
            const string SpKey = "Aff_Admin_AffiliateLog_Sel";
            var result = new EntityResult<NewViewLog>();
            var dataList = new List<NewViewLog>();

            using (var objectDb = GetObjectDb(SpKey))
            {
                objectDb.AddInParameter("AffID", DbType.Int32, param.AffId);
                objectDb.AddInParameter("LogType", DbType.Int32, param.LogType);
                objectDb.AddInParameter("FromDate", DbType.DateTime, param.FromDate);
                objectDb.AddInParameter("ToDate", DbType.DateTime, param.ToDate);
                objectDb.AddInParameter("PageIndex", DbType.Int32, param.PageIndex);
                objectDb.AddInParameter("PageSize", DbType.Int32, param.PageSize);

                objectDb.AddOutParameter("NRows", DbType.Int32, Size.SizeOfInt);

                using (var reader = objectDb.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataList.Add(new NewViewLog
                        {
                            Id = Convert.ToInt32(reader["LogId"]),
                            Date = Convert.ToDateTime(reader["Date"]),
                            UpdatedBy = Convert.ToString(reader["UpdatedName"]),
                            Action = Convert.ToString(reader["Action"]),
                            OldData = ParseXmlToJson(Convert.ToString(reader["OldData"])),
                            NewData = ParseXmlToJson(Convert.ToString(reader["NewData"])),
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

        private static string ParseXmlToJson(string xmlString)
        {
            if (!string.IsNullOrEmpty(xmlString) && xmlString.TrimStart().StartsWith("<"))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlString);

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
