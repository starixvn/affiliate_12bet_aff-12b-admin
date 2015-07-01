<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Affiliate Admin</title>
    <link runat="server" id="IconICO" rel="shortcut icon"/>
    <![if ! IE]>
    <link runat="server" id="IconPNG" rel="shortcut icon"/>
    <![endif]>
</head>
<frameset rows="60, *, 25" frameborder="no" framespacing="0">
    <frame id="fMenu" name="fMenu" frameborder="0" noresize="noresize" scrolling="no" src="Header.aspx" height="60px" />
    <frame id="fMain" name="fMain" noresize="noresize" frameborder="0" scrolling="auto" src="Home/Home.aspx" />        
    <frame id="fFooter" name="fFooter" frameborder="0" noresize="noresize" scrolling="no" src="Footer.htm" height="20px" />
    <noframes>
        <body><h3>Sorry, Your browser does not handle frames!</h3></body>
    </noframes>
</frameset>
    <!-- Piwik -->
    <script type="text/javascript">
        var _paq = _paq || [];
        _paq.push(['trackPageView']);
        _paq.push(['enableLinkTracking']);
        (function () {
            var u = "https://pw.detecas.com/";
            _paq.push(['setTrackerUrl', u + 'piwik.php']);
            _paq.push(['setSiteId', 27]);
            var d = document, g = d.createElement('script'), s = d.getElementsByTagName('script')[0];
            g.type = 'text/javascript'; g.async = true; g.defer = true; g.src = u + 'piwik.js'; s.parentNode.insertBefore(g, s);
        })();

    </script>
    <noscript><p><img src="//pw.detecas.com/piwik.php?idsite=27" style="border:0;" alt="" /></p></noscript>
    <!-- End Piwik Code -->
</html>
