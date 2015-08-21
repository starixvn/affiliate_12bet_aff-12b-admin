<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_Footer" Codebehind="Footer.ascx.cs" %>
<script type="text/JavaScript">
    function MM_openBrWindow(theURL,winName,features) { //v2.0
      window.open(theURL,winName,features);
    }

    function MM_goToURL() { //v3.0
      var i, args=MM_goToURL.arguments; document.MM_returnValue = false;
      for (i=0; i<(args.length-1); i+=2) eval(args[i]+".location='"+args[i+1]+"'");
    }
</script>
<div class="browser"><%=Resources.Lang.BestView %> <a target="_blank" href="http://www.mozilla.com/en-US/products/download.html"><span class="firefox">&nbsp;</span></a> <a target="_blank" href="http://www.microsoft.com/uk/windows/internet-explorer/default.aspx"><span class="ie">&nbsp;</span></a></div>
<div class="copyright"><%=Resources.Lang.Copyright.Replace("{now}", DateTime.Now.Year.ToString()) %></div>
<div class="button_footer"><asp:HyperLink runat="server" CssClass="f_button" ID="hlkProgram" NavigateUrl="~/ProgramDetails.aspx"></asp:HyperLink> | <asp:HyperLink runat="server" CssClass="f_button" ID="hlkFAQ" NavigateUrl="~/FAQs.aspx"></asp:HyperLink> | <asp:HyperLink runat="server" CssClass="f_button" ID="hlkCondition" NavigateUrl="~/Condition.aspx"></asp:HyperLink></div>