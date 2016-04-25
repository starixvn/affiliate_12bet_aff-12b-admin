using System;
using ONEbook.UM.Services.Impl;

public partial class UnderMaintenance : System.Web.UI.Page
{
    protected string UMTime_From;
    protected string UMTime_To;

    protected override void OnLoad(EventArgs e)
    {
        var underMaitenanceTime = Common.GetUnderMaintenanceTime();

        if (!underMaitenanceTime.IsUnderMaintenanceTime)
        {
            Response.Redirect("~/SignIn.aspx");
            Response.End();
            return;
        }

        UMTime_From = underMaitenanceTime.From;
        UMTime_To = underMaitenanceTime.To;

        base.OnLoad(e);
    }
}
