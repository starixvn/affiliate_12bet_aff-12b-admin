using System;

public partial class Controls_LeftPanel : RootControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] strArr = AppSettings.Session.UserName.Split(' ');
        ltrUserName.Text = strArr[0];
    }
}
