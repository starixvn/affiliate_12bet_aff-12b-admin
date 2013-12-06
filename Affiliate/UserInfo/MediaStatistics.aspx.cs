using System;
using System.Collections;
using DNAide.Web;
using DNAide.Data;
using System.Text;

public partial class UserInfo_MediaStatistics :RootPageLogIn
{    
    protected string statisticsContent = string.Empty;    
    protected void Page_Load(object sender, EventArgs e)
    {
        statisticsContent = TemplateHandler.LoadTemplate("UserInfo/TrackingStatistics.htm");
        TemplateHandler.RemoveBlock(ref statisticsContent, "StatisticsDetailContent");
        TemplateHandler.Assign(ref statisticsContent, "{TrackingStatistics}", Resources.Lang.TrackingStatistics);
        TemplateHandler.Assign(ref statisticsContent, "{TrackingCode}", Resources.Lang.TrackingCode);
        TemplateHandler.Assign(ref statisticsContent, "{slTrackingCode}", GenerateHTML.TrackingSystem.GetTrackingCodeList(true, string.Empty));
        TemplateHandler.Assign(ref statisticsContent, "{Submit}", Resources.Lang.Submit);
        TemplateHandler.Assign(ref statisticsContent, "{Month}", Resources.Lang.Month);
        TemplateHandler.Assign(ref statisticsContent, "{TotalClick}", Resources.Lang.TotalClick);
        TemplateHandler.Assign(ref statisticsContent, "{UniqueClick}", Resources.Lang.UniqueClick);
        TemplateHandler.Assign(ref statisticsContent, "{Date}", Resources.Lang.Date);
        TemplateHandler.ReplaceBlock(ref statisticsContent, "Content", GenerateHTML.TrackingSystem.GetStatisticsAllMonthsContentList(null));
        
    }
  
   
}
