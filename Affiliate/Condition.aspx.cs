using System;
using System.Web;

public partial class Condition : RootPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpResponse.RemoveOutputCacheItem("/Affiliate/Condition.aspx");
    }
}
