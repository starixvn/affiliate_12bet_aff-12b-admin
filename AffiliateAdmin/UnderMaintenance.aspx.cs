using System;
using ONEbook.UM.Services.Impl;

public partial class UnderMaintenance : System.Web.UI.Page
{
    protected string UMTime_From;
    protected string UMTime_To;

    protected override void OnLoad(EventArgs e)
    {
        if (!Common.IsUnderMaintenance())
        {
            Response.Redirect("~/SignIn.aspx");
            Response.End();
            return;
        }

        var underMaitenanceTime = Common.GetUnderMaintenanceTime();
        UMTime_From = underMaitenanceTime.From;
        UMTime_To = underMaitenanceTime.To;

        base.OnLoad(e);
    }
}
