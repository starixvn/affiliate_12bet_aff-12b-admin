using DNAide.Web;
using DNAide.Core;
using System.Data;
using DNAide.Data;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Text
/// </summary>
namespace GenerateHTML
{
    public class Text
    {
        public Text()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string GetEditedText(int textID, int noRow, string deviceType)
        {
            DbResult result = DataHandler.Text.GetTextDetail(textID);
            if (result.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListText.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "ViewMode1");
                DataRow dr = result.ResultSet.Tables[0].Rows[0];

                DataView dv = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;
                TemplateHandler.Assign(ref content, "order-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "No-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "rowstyle", "active_listbanner");
                TemplateHandler.Assign(ref content, "contentvalue", Common.FormatStringJavascript(dr["message"].ToString()));             
                TemplateHandler.Assign(ref content, "catevalue", Common.GetDataOptionBanner(dv, "slCategory", "Category", dr["category"].ToString(), false));
                TemplateHandler.Assign(ref content, "langvalue", Common.GetDataOptionBanner(dv, "slLanguage", "Language", dr["language"].ToString(), false));
                TemplateHandler.Assign(ref content, "textid", dr["bannerid"].ToString());
                TemplateHandler.Assign(ref content, "currencyvalue", Common.GetDataOptionBanner(dv, "slCurrency", "Currency", dr["currency"].ToString(), false));
                TemplateHandler.Assign(ref content, "devicetypevalue", AffAdmin.GenerateHTML.Domain.GenDeviceList("slDevice", deviceType));
                TemplateHandler.Assign(ref content, "deviceType", deviceType);
                
                return content;
            }

            return string.Empty;
        }
        public static string GetListText(string category, string language, string currency, int pageIndex, ref int minNo, ref int maxNo, ref int totalText, ref int totalPage, int pageSize, string deviceType)
        {
            DbResult result = null;
            int nRows = 0;
            result = DataHandler.Text.GetListText(category, language, currency, pageIndex, pageSize, deviceType);
            nRows = CastAide.AsInt32(result.Outputs["nrows"]);
            int pagesize = pageSize;
            int cols = 10;
            minNo = 0;
            maxNo = 0;
            if (nRows > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListText.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");
                int totalpage = CastAide.AsInt32(result.Outputs["totalpages"]);
                DataTable dtbResult = result.ResultSet.Tables[0];
                StringBuilder contentList = new StringBuilder();
                int counter = 0;
                minNo = (pageIndex - 1) * pagesize + 1;
                foreach (DataRow dr in dtbResult.Rows)
                {
                    counter++;
                    string rowText = content;
                    if (counter % 2 == 0)
                    {
                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordFirst");
                    }
                    else
                    {
                        TemplateHandler.Assign(ref rowText, "rowstyle", "RecordSecond");
                    }
                    TemplateHandler.Assign(ref rowText, "No-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "order-val", (pageIndex - 1) * pagesize + counter);
                    TemplateHandler.Assign(ref rowText, "lastupdate", dr["Lastupdate"].ToString());
                    TemplateHandler.Assign(ref rowText, "contentvalue", Common.FormatStringHtml(dr["message"].ToString()));
                    TemplateHandler.Assign(ref rowText, "catevalue", dr["category"].ToString());
                    TemplateHandler.Assign(ref rowText, "langvalue", dr["language"].ToString());
                    TemplateHandler.Assign(ref rowText, "textid", dr["bannerid"].ToString());
                    TemplateHandler.Assign(ref rowText, "currencyvalue", dr["currency"].ToString());
                    TemplateHandler.Assign(ref rowText, "devicetypevalue", dr["devicetype"].ToString());
                    TemplateHandler.Assign(ref rowText, "deviceType", deviceType);
                    contentList.Append(rowText);
                }
                maxNo = (pageIndex - 1) * pagesize + counter;
                totalText = nRows;
                if (totalpage > 0)
                {
                    totalPage = totalpage;
                }
                return contentList.ToString();
            }
            else
            {
                return Common.NoInfo(cols);
            }

        }
        public static string GetViewedText(int textID, int noRow, string deviceType)
        {
            DbResult result = DataHandler.Text.GetTextDetail(textID);
            if (result.ResultSet.Tables[0].Rows.Count > 0)
            {
                string temp = TemplateHandler.LoadTemplate("Media/MainListText.htm");
                string content = TemplateHandler.CopyBlock(temp, "Content");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode1");
                TemplateHandler.RemoveBlock(ref content, "UpdateMode2");

                DataRow dr = result.ResultSet.Tables[0].Rows[0];

                DataView dv = DbHelper.Execute("Aff_BannerSelType_1", DBType.AFFILIATE_12BET).ResultSet.Tables[0].DefaultView;
                TemplateHandler.Assign(ref content, "No-val", noRow.ToString());
                TemplateHandler.Assign(ref content, "order-val", noRow.ToString());
                if (noRow % 2 == 0)
                {
                    TemplateHandler.Assign(ref content, "rowstyle", "RecordFirst");
                }
                else
                {
                    TemplateHandler.Assign(ref content, "rowstyle", "RecordSecond");
                }
                TemplateHandler.Assign(ref content, "contentvalue", Common.FormatStringHtml((dr["message"].ToString())));
                TemplateHandler.Assign(ref content, "lastupdate", dr["Lastupdate"].ToString());
                TemplateHandler.Assign(ref content, "catevalue", dr["category"].ToString());
                TemplateHandler.Assign(ref content, "langvalue", dr["language"].ToString());
                TemplateHandler.Assign(ref content, "textid", dr["bannerid"].ToString());
                TemplateHandler.Assign(ref content, "currencyvalue", dr["currency"].ToString());
                TemplateHandler.Assign(ref content, "devicetypevalue", dr["devicetype"]);
                TemplateHandler.Assign(ref content, "deviceType", deviceType);
                return content;

            }

            return string.Empty;
        }
    }
}
