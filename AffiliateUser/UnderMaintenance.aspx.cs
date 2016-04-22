using System;

public partial class UnderMaintenance : System.Web.UI.Page
{
    protected string UMTime_From;
    protected string UMTime_To;

    protected override void OnLoad(EventArgs e)
    {
        var underMaintenanceTime = Common.GetUnderMaintenanceTime();

        if (!underMaintenanceTime.IsUnderMaintenanceTime)
        {
            Response.Redirect("~/Root.aspx");
            Response.End();
            return;
        }

        UMTime_From = underMaintenanceTime.From;
        UMTime_To = underMaintenanceTime.To;
        
        base.OnLoad(e);
    }
}
