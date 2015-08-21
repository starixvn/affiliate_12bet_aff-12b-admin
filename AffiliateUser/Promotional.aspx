<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_Promotional" Codebehind="Promotional.aspx.cs" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>12BET Affiliate Program | Promotional Descriptions</title>
    <meta name="description" content="12BET - Sports Betting and Online Casino Affiliate Program: Promotional Descriptions" />
    <meta name="keywords" content="12BET, 12BET Affiliate Program, Sports Betting Affiliate, Casino Affiliate, betting affiliate programs, promotional descriptions, top affiliate programs, asian casino affiliate programs, Asian betting affiliate programs, high payout affiliate programs" />
    <meta name="robots" content="index,follow,archive" />

    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-5160706-44']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'stats.g.doubleclick.net/dc.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

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