<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftPanel.ascx.cs" Inherits="Controls_LeftPanel" %>
<div class="leftcontainer">
    <div id="containerMainLeft">
        <div id="subnavleft">
            <div id="subnav-head">
                &nbsp;<asp:Literal ID="ltrUserName" runat="server"></asp:Literal>
            </div>
            <span class="navleft"><a href="Account_Overview.aspx" id="lnkAccOverview">
                <%=Resources.Lang.AccOverview%>
            </a></span><span class="navleft"><a href="StatementHistory.aspx" id="lnkStaHis">
                <%=Resources.Lang.StatementHistory%>
            </a></span><span class="navleft"><a href="MyAccount.aspx" id="lnkMyAcc">
                <%=Resources.Lang.MyAcc %>
            </a></span><span class="navleft"><a href="PaymentRequest.aspx" id="lnkPayment">
                <%=Resources.Lang.PaymentRequest %>
            </a></span><span class="navleft"><a href="MediaGallery.aspx" id="lnkMediaGallery">
                <%=Resources.Lang.Media %>
            </a>              
            </span>
            <span class="navleft"><a href="MediaStatistics.aspx" id="lnkMediaStatistics">
                <%=Resources.Lang.TrackingStatistics%>
            </a></span>
            <div id="subnav-foot">
                &nbsp;</div>
        </div>
        
        <div class="banner_<%=AppSettings.Session.Flag %>" onclick="document.location.href='../ContactUS.aspx'">
            &nbsp;
        </div>
    </div>
</div>
