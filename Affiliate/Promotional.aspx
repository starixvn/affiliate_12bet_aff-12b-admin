<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Promotional.aspx.cs" Inherits="UserInfo_Promotional" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=Resources.Lang.Promotional%></title>
    <script type="text/javascript">
        function OnLogToBanner() {
            if (window.opener == null) {
                var win = window.open(document.getElementById("hdUrl").value);
                win.focus();
                win.document.getElementById("txtusername").focus();
            }
            else {
                window.opener = window.top;
                window.opener.document.getElementById("txtusername").focus();
                window.close();
            }
        }
    </script>
</head>

<body>
    <asp:Literal runat="server" ID="lblContent"></asp:Literal>
    <input type="hidden" value="<%=Common.RootURL()%>" id="hdUrl" />
</body>
</html>