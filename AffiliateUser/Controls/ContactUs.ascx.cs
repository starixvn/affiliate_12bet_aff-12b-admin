using System;

public partial class Controls_ContactUs : System.Web.UI.UserControl
{
    public string AffID = "", Name = "", Email = "", Action = "", script = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Action += "Handler/ContactUS.ashx";
        string email = AppSettings.MailContact;
        ltrContactTitle.Text = Resources.Lang.ContactTitle.Replace("{email}", "<a href=\"mailto:"+ email +"\">"+ email +"</a>,");
        ltrName.Text = Resources.Lang.Name;
        ltrAffiliateID.Text = Resources.Lang.Affiliate;
        ltrSubject.Text = Resources.Lang.Subject;
        ltrEmail.Text = Resources.Lang.Email1;
        ltrComment.Text = Resources.Lang.Comment;
        AffID = AppSettings.Session.UserName;
        Name = AppSettings.Session.FirstName + " " + AppSettings.Session.LastName;
        if (Name.Length == 1) Name = "";
        Email = AppSettings.Session.Email;
        ltrSuccess.Text = Resources.Lang.MessageSentReview.Replace("{br}", "<br>");

        if (!string.IsNullOrEmpty(Request["f"]) && Request["f"].Equals("h"))
        {
            script = "<script type ='text/javascript'>";
            script += "SendMailSuccess();";
            script += "</script>";
        }
    }
    static string width = "";
    public static string WidthTable
    {
        get { return width; }
        set { width = value; }
    }


    static string title1 = "";
    public static string DisplayTitle1
    {
        get { return title1; }
        set { title1 = value; }
    }
    static string title2 = "";
    public static string DisplayTitle2
    {
        get { return title2; }
        set { title2 = value; }
    }
    static string size = "";
    public static string Size
    {
        get { return size; }
        set { size = value; }
    }

    static string widthtextarea = "";
    public static string WidthTextArea
    {
        get { return widthtextarea; }
        set { widthtextarea = value; }
    }

    
}
