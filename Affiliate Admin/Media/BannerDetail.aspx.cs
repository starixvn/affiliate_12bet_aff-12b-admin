using System;
using System.Data;
using DNAide.Web;
using DNAide.Data;
using DNAide.Core;

public partial class Media_BannerDetail : IBCPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string content = TemplateHandler.LoadTemplate("Media/BannerDetail.htm");

        //get parameters from request
        string imagevalue = CastAide.AsString(Request["imagevalue"], string.Empty);
        string lastupdate = CastAide.AsString(Request["lastupdate"], string.Empty);
        int width = CastAide.AsInt32(Request["width"], 100);
        int height = CastAide.AsInt32(Request["height"], 50);
        string bannertype = CastAide.AsString(Request["bannertype"], "image");

        if(bannertype=="image")
            TemplateHandler.RemoveBlock(ref content, "limit-fls");
        else
            TemplateHandler.RemoveBlock(ref content, "limit-img");

        TemplateHandler.Assign(ref content, "imagevalue", imagevalue);
        TemplateHandler.Assign(ref content, "lastupdate", lastupdate);
        TemplateHandler.Assign(ref content, "width", width);
        TemplateHandler.Assign(ref content, "height", height);

        this.HtmlContent = content;


    }
}
