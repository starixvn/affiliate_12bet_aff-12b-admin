using System;
using System.Text;
public partial class Master2 : System.Web.UI.MasterPage
{
    protected string msgkickout = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        string _nl = Environment.NewLine;
        StringBuilder _s = new StringBuilder();
        _s.Append("var gMsg = {" + _nl); // global message        
        _s.Append("     Errmsgkickout: \"" + Resources.Lang.Msgkickout.Replace("\"", "'") + "\"" + _nl);
        _s.Append("};");
        msgkickout = _nl + _s.ToString() + _nl;
        
    }
}
