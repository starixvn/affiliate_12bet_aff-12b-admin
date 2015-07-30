using System;
using DNAide.Web;
public partial class ForgetPass : RootPage 
{
    public string submit,reset,emailrequire,emailinvalid;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string tplSuc = TemplateHandler.LoadTemplate("LoginDetailSucc.htm");
        TemplateHandler.Assign(ref tplSuc, "Yourlogindetail", Resources.Lang.Yourlogindetail);
        TemplateHandler.Assign(ref tplSuc, "Pleasecheckyour", Resources.Lang.Pleasecheckyour);
        TemplateHandler.Assign(ref tplSuc, "Foranyreasonyou", Resources.Lang.Foranyreasonyou);
        ltrmsgsucc.Text = tplSuc;

        ltryouremail.Text = Resources.Lang.YourEmail;
        ltrtitleforget.Text = Resources.Lang.TitelForgetPass;
        submit = Resources.Lang.Submit;
        reset = Resources.Lang.Reset;
        
        ltrtitelrecoveruspw.Text = Resources.Lang.ForgetPassword;
        emailrequire = Resources.Lang.ErrEmailRequire;
        emailinvalid = Resources.Lang.ErrEmailInvalid;
    }
}
