<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Header.aspx.cs" Inherits="Header" %>
<!--js-->
<script src="App_Js/prototype.js" type="text/javascript"></script>
<script src="App_Js/Header.js" type="text/javascript"></script>
<link href='App_Themes/DefaultV2/header.css' type='text/css' rel='stylesheet' />
<!--/js-->
 <% if (AppSettings.Environment.Equals("UAT", StringComparison.OrdinalIgnoreCase))
   { %>
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
    <noscript>
        <p>
            <img src="https://pw.detecas.com/piwik.php?idsite=27" style="border: 0;" alt="" /></p>
    </noscript>
    <!-- End Piwik Code -->
    <% } %>

