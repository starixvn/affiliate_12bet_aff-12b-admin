using System;

public partial class UnderMaintenance : System.Web.UI.Page
{
    protected string UMTime_From;
    protected string UMTime_To;

    protected override void OnLoad(EventArgs e)
    {
        if (!Common.IsUnderMaintenance())
        {
            Response.Redirect("~/Root.aspx");
            Response.End();
            return;
        }

        var underMaintenanceTime = Common.GetUnderMaintenanceTime();
        UMTime_From = underMaintenanceTime.From;
        UMTime_To = underMaintenanceTime.To;
        
        base.OnLoad(e);
    }
}
