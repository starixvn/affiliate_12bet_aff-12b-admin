using System;

public partial class ChangePass : RootPageLogIn
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //ltrChangePass.Text = Resources.Lang.ChangePass + " " + AppSettings.Session.UserName;
        string strUserName = Request["username"];
        ltrTextRemind.Text = Resources.Lang.ChangePassText;
        //ltrTextRemind.Text = Resources.Lang.PassCheck;
        ltrCurrPass.Text = Resources.Lang.OldPass;
        ltrNewPass.Text = Resources.Lang.NewPass;
        ltrComfimPass.Text = Resources.Lang.RePass;        
    }
}
