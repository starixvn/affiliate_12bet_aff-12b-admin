<%@ Page Language="C#" %>
<%@ Import Namespace="System.Web"%>
<%@ Import Namespace="System.Web.UI"%>
<%@ Import Namespace="System.Net"%>
<%@ Import Namespace="System.Net.Mail"%>
<% %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string SMTP_HOST = HttpContext.Current.Request["smtp_host"];
    int SMTP_PORT = int.Parse(!string.IsNullOrEmpty(HttpContext.Current.Request["smtp_port"])?HttpContext.Current.Request["smtp_port"]:"25");
    string SMTP_FROM = HttpContext.Current.Request["smtp_from"];
    string SMTP_USERNAME = HttpContext.Current.Request["smtp_username"];
    string SMTP_PWD = HttpContext.Current.Request["smtp_password"];
    string MAIL_SUBJECT = HttpContext.Current.Request["mail_subject"];
    string MAIL_BODY = HttpContext.Current.Request["mail_body"];
    string MAIL_TO = HttpContext.Current.Request["smtp_to"];
    bool sslChecked=(HttpContext.Current.Request["smtp_ssl"]=="1");
    public void Send()
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient(SMTP_HOST);
        mail.From = new MailAddress(SMTP_FROM);
        
        mail.To.Add(MAIL_TO);
        
        mail.Subject = MAIL_SUBJECT;
        mail.Body = MAIL_BODY;

      

        SmtpServer.Port = SMTP_PORT;
        if(!string.IsNullOrEmpty(SMTP_USERNAME) && !string.IsNullOrEmpty(SMTP_PWD))
            SmtpServer.Credentials = new System.Net.NetworkCredential(SMTP_USERNAME, SMTP_PWD);
        SmtpServer.EnableSsl = sslChecked;

        SmtpServer.Send(mail);

    }
   
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background: url();">
    <%
        string allowIPs = "116.193.67.114;";
        string userIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        if (true)//allowIPs.Contains(userIP)
        {
         %>
    <form method="post" action=''>
    smtp_host: <input type="text" name="smtp_host" value="<%=SMTP_HOST %>" /><br />
    smtp_ssl: <input type="checkbox" name="smtp_ssl" value="1" /><br />
    smtp_port: <input type="text" name="smtp_port" value="<%=SMTP_PORT %>"/><br />
    smtp_username: <input type="text" name="smtp_username" value="<%=SMTP_USERNAME %>" /><br />
    smtp_password: <input type="text" name="smtp_password" value="<%=SMTP_PWD %>" /><br />
    mail_from: <input type="text" name="smtp_from" value="<%=SMTP_FROM %>" /><br />
    mail_to: <input type="text" name="smtp_to" value="<%=MAIL_TO %>" /><br />
    mail_subject: <input type="text" name="mail_subject" value="<%=MAIL_SUBJECT %>" /><br />
    mail_body: <input type="text" name="mail_body" value="<%=MAIL_BODY %>" />
    <br /><input type="submit" value="submit" />
    </form>
    <%
            if (!string.IsNullOrEmpty(SMTP_HOST))
            {
                Send();
            %>
            <h3>Email sent</h3>
            <%
            }
        }
        else
        {   
        %>
        <h3>Access denied!</h3>
        <%} %>
</body>
</html>
