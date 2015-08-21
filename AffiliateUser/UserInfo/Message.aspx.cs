using System;
using System.Globalization;
using DNAide.Web;
using DNAide.Core;
using AffiliateUser;
/// <summary>
/// Class to display Hot news content to customer
/// written by: Tong Hoang Dung on 19/03/2009
/// </summary>
public partial class Message : RootPageLogIn
{
    public string hotNewsContent;
    protected void Page_Load(object sender, EventArgs e)
    {
        hotNewsContent = TemplateHandler.LoadTemplate("UserInfo/HotNews.htm");
        TemplateHandler.Assign(ref hotNewsContent, "MessageHistory", Resources.Lang.MessageHistory);
        TemplateHandler.Assign(ref hotNewsContent, "Search", Resources.Lang.Search);
        TemplateHandler.Assign(ref hotNewsContent, "All", Resources.Lang.All);
        TemplateHandler.Assign(ref hotNewsContent, "Normal", Resources.Lang.Normal);
        TemplateHandler.Assign(ref hotNewsContent, "Important", Resources.Lang.Important);
        TemplateHandler.Assign(ref hotNewsContent, "From", Resources.Lang.From);
        TemplateHandler.Assign(ref hotNewsContent, "To", Resources.Lang.To);
        TemplateHandler.Assign(ref hotNewsContent, "Submit", Resources.Lang.Submit);
        TemplateHandler.Assign(ref hotNewsContent, "ID", Resources.Lang.ID);
        TemplateHandler.Assign(ref hotNewsContent, "PostDate", Resources.Lang.PostDate);
        TemplateHandler.Assign(ref hotNewsContent, "Message", Resources.Lang.Message);
        TemplateHandler.Assign(ref hotNewsContent, "CurrentYear", DateTime.Now.Year);
        int intTotalPage = 0;
        int intPageIndex = CastAide.AsInt32(Request["page"], 1);
        int intPageSize = CastAide.AsInt32(Request["slPageSize"], 10);

        int intImportant = CastAide.AsInt32(Request["slSearchOption"], -1);
        CultureInfo cult = new CultureInfo("en-US");
        object dtmFrom = Request["fdate"];
        object dtmTo = Request["tdate"];
        if (dtmFrom != null || dtmTo != null)
        {
            TemplateHandler.Assign(ref hotNewsContent, "fdate", Common.ConvertDateTime(dtmFrom, "/", true, false));
            TemplateHandler.Assign(ref hotNewsContent, "ServerDateTime", Common.ConvertDateTime(dtmFrom, "/", true, false));
            TemplateHandler.Assign(ref hotNewsContent, "tdate", Common.ConvertDateTime(dtmTo, "/", true, false));
        }
        else
        {
            TemplateHandler.Assign(ref hotNewsContent, "fdate", Common.ConvertDateTime(DateTime.Now, "/", true, false));
            TemplateHandler.Assign(ref hotNewsContent, "ServerDateTime", Common.ConvertDateTime(DateTime.Now, "/", true, false));
            TemplateHandler.Assign(ref hotNewsContent, "tdate", Common.ConvertDateTime(DateTime.Now, "/", true, false));
        }

        TemplateHandler.ReplaceBlock(ref hotNewsContent, "Content", GenerateHTML.HotNews.GetHotNewsContentList(intImportant, dtmFrom, dtmTo, intPageIndex, intPageSize, ref intTotalPage));
        TemplateHandler.Assign(ref hotNewsContent, "page", intPageIndex);
        if (intTotalPage > 0)
        {
            TemplateHandler.Assign(ref hotNewsContent, "paging-top", Common.GenPaging(intTotalPage, intPageIndex, intPageSize, 0, "aPageTop", "txtPageTop", null, null, 0, false, true, false, false));
            TemplateHandler.Assign(ref hotNewsContent, "paging-bottom", Common.GenPaging(intTotalPage, intPageIndex, intPageSize, 0, "aPageBottom", "txtPageBottom", null, null, 0, false, false, false, false));
        }
        else
        {
            TemplateHandler.Assign(ref hotNewsContent, "paging-top", string.Empty);
            TemplateHandler.Assign(ref hotNewsContent, "paging-bottom", string.Empty);
        }
    }
}
