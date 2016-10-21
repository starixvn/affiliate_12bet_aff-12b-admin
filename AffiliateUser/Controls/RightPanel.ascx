<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_RightPanel" Codebehind="RightPanel.ascx.cs" %>
<!--login-->

<div runat="server" id="LoginPanel" class="box_right">
    <div class="box_right_header">
        <%=strTitleLogin %>
    </div>
    <div class="box_right_cons01">
            <input type="hidden" id="X-CSRF-Token" name="X-CSRF-Token" value="<%=_XsrfTokenFormValue %>" />
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td width="9%">
                    <div class="icuser">
                    </div>
                </td>
                <td height="23" colspan="3">
                    <%=Resources.Lang.Username %>:                    
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td height="23" colspan="3">
                    <input name="txtusername" id="txtusername" type="text" style="width: 160px;" />
                </td>
            </tr>
            <tr>
                <td>
                    <div class="icpass">
                    </div>
                </td>
                <td height="23" colspan="3">
                    <%=strPwd %>:
                </td>
            </tr>
            <tr>
                <td />
                <td height="23" colspan="3">
                    <input name="txtpassword" id="txtpassword" autocomplete="off" type="password" style="width: 160px;" />
                </td>
            </tr>
            <tr>
                <td />
                <td height="23" width="32%">
                    <input name="txtvalidate" id="txtvalidate" type="text" class="login" value="<%=Resources.Lang.Validation %>"
                        style="width: 70px;" align="absmiddle" onfocus="setvaluefocus(this,1,'val');"
                        onblur="setvaluefocus(this,0,'val');" />
                </td>
                <td height="23" width="19%">
                    <img id="validCode" height="20px" width="40px" alt="Valiation Code" src="Authorization/LoginCaptcha.ashx"
                        align="absmiddle" onclick="ChangeCapchaCode(this);" class="hand" />
                </td>
                <td height="23" width="40%">
                    <input name="btnlogin" id="btnlogin" class="button" type="button" onclick="ExcLogin();" value="<%=strLogin %>" />
                </td>
            </tr>
            <tr>
                <td />
                <td height="23" colspan="3">
                    <a id="A1" class="link_black" runat="server" href="~/SignUp.aspx">
                        <%=strSignup %></a> | <a id="A2" runat="server" class="link_black" href="~/ForgetPass.aspx">
                            <%=strForgetPwd %></a>
                </td>
            </tr>
        </table>
        <input id="val" name="val" type="hidden" value="<%=Resources.Lang.Validation %>"/>
    </div>
</div>
<!--/login-->
<!--After login-->
<div class="box_right" id="divAfterLogin" runat="server">        
    <div class="box_right_header">
        <div class="icon_top"><asp:Literal ID="ltrUserName" runat="server"></asp:Literal></div>
    </div>
	<div id="rightmenu">
			<span><a runat="server" href="~/UserInfo/Account_Overview.aspx" id="lnkAccOverview"><%=Resources.Lang.AccOverview%></a></span>
            <% if (AppSettings.Session.CommGroupName == "Korean Agent Comm")
                { %>
               
                <span><a runat="server" href="~/UserInfo/Customers.aspx" id="A15"><%= Resources.Lang.YourCustomers %></a></span>
            <%} %>
          
			<span><a runat="server" href="~/UserInfo/StatementHistory.aspx" id="lnkStaHis"><%=Resources.Lang.StatementHistory%></a></span>
			<span><a runat="server" href="~/UserInfo/MyAccount.aspx" id="lnkMyAcc"><%=Resources.Lang.MyAcc %></a></span>
			<span><a runat="server" href="~/UserInfo/PaymentRequest.aspx" id="lnkPayment"><%=Resources.Lang.PaymentRequest %></a></span>
			
            <% if (AppSettings.Session.CommGroupName != "Media")
                { %>
                
                <span><a runat="server" href="~/UserInfo/MediaGalleryNew.aspx" id="lnkMediaGalleryNew"><%= Resources.Lang.Media%></a></span>
            <%} %>
            <%else
                { %>
                <span><a runat="server" href="~/UserInfo/MediaBanner.aspx" id="lnkMediaBanner"><%=Resources.Lang.Media %></a></span>
            <%} %>
			<span><a runat="server" href="~/UserInfo/MediaText.aspx" id="lnkMediaText"><%=Resources.Lang.Text %></a></span>
			<span><a runat="server" href="~/UserInfo/MediaStatistics.aspx" id="lnkMediaStatistics"><%=Resources.Lang.TrackingStatistics%></a></span>
            <span><a runat="server" href="~/UserInfo/MediaStatistics_New.aspx" id="A14"><%=Resources.Lang.Referrals%>&nbsp;<span class="news">&nbsp;</span></a></span>
            <span><a runat="server" href="~/UserInfo/MediaStatistics_LandingPage.aspx" id="A13"><%=Resources.Lang.LandingPages%>&nbsp;<span class="news">&nbsp;</span></a></span>
	</div>
</div>
<!--/After login-->

<div class="box_right">
    <div class="box_right_header">
        <div style="float:left"><%=Resources.Lang.Faqs %></div>
        <div style="float:right;width:30px;">
            <a href="#" style="width:22px;height:22px;" onclick="OnOutline();">
                <img src="<%=Common.RootURL()%>/<%=AppSettings.ThemeDir %>/Images/expand.png" class="outline-img" id="imgOutline" /> 
            </a> 
        </div>
        </div>
    <div class="box_right_cons02" id="dvOutline" style="display:none;">
        <a id="A3" class="link_white" runat="server" href="~/FAQs.aspx#f1">1. <%=Resources.Lang.WhatIs12BETAffiliates%></a> 
        <a id="A4" class="link_white" runat="server" href="~/FAQs.aspx#f2">2. <%=Resources.Lang.HowDoISignUp%></a> 
        <a id="A5" class="link_white" runat="server" href="~/FAQs.aspx#f3">3. <%=Resources.Lang.WhatIfIDontHave%></a> 
        <a id="A6" class="link_white" runat="server" href="~/FAQs.aspx#f4">4. <%=Resources.Lang.WhereDoIGetBanners%></a> 
        <a id="A7" class="link_white" runat="server" href="~/FAQs.aspx#f5">5. <%=Resources.Lang.HowDoesTheSystem%></a> 
        <a id="A8" class="link_white" runat="server" href="~/FAQs.aspx#f6">6. <%=Resources.Lang.HowCanICheck%></a> 
        <a id="A9" class="link_white" runat="server" href="~/FAQs.aspx#f7">7. <%=Resources.Lang.WhatAreTheWithdrawal%></a> 
        <a id="A10" class="link_white" runat="server" href="~/FAQs.aspx#f8">8. <%=Resources.Lang.WhatIsTheMinimumWithdrawal%></a>
    </div>
</div>
<div class="box_right">
    <div class="box_right_header">
        <a href="#" onclick="javascript:window.open('<%=Common.RootURL()%>/Promotional.aspx','_blank','scrollbars=yes,resizable=yes,width=810,height=610',false);" class="outline-a full-a"><%=Resources.Lang.Promotional %></a>
    </div>
</div>
<div class="box_right">
    <div class="box_right_header">
        <a href="#" onclick="javascript:window.open('<%=Common.RootURL()%>/Customized.aspx','_blank','scrollbars=yes,resizable=yes,width=1000,height=410',false);" class="outline-a full-a"><%=Resources.Lang.Customized %></a>
    </div>
</div>

<div id="aSignup" style="margin-bottom: 5px;" runat="server">
    <div id="flashcontent">
        <img src="<%=strURL %>" width="268px" height="144px" />
    </div>
    <script type="text/javascript">
        function OnOutline() {
            if (document.getElementById("dvOutline")) {
                if (document.getElementById("dvOutline").style.display == 'none') {
                    document.getElementById("dvOutline").style.display = 'block';
                    document.getElementById("imgOutline").src = '<%=Common.RootURL()%>/<%=AppSettings.ThemeDir %>/Images/clollapse.png';
                }
                else {
                    document.getElementById("dvOutline").style.display = 'none';
                    document.getElementById("imgOutline").src = '<%=Common.RootURL()%>/<%=AppSettings.ThemeDir %>/Images/expand.png';
                }
            }
        }

        function OnExpandView() {
            if (document.getElementById("right_pane")) {
                if (document.getElementById("right_pane").style.display == 'none') {
                    document.getElementById("right_pane").style.display = 'block';
                    document.getElementById("imgOutline").src = '<%=Common.RootURL()%>/<%=AppSettings.ThemeDir %>/Images/clollapse.png';
                }
                else {
                    document.getElementById("right_pane").style.display = 'none';
                    document.getElementById("imgOutline").src = '<%=Common.RootURL()%>/<%=AppSettings.ThemeDir %>/Images/expand.png';
                }
            }
        }
    </script>
</div>
<a id="A12" href="~/ContactUs.aspx" runat="server">
    <div class="login_ads4_<%=AppSettings.Session.Flag %>">
    </div>
</a>
<a id="A11" href="~/ContactUs.aspx" runat="server">
    <div class="login_ads2_<%=AppSettings.Session.Flag %>">
    </div>
</a>
<!--/login-->
<script language="javascript" type="text/javascript">
<%=errmsg %>
<%=script %>
</script>
