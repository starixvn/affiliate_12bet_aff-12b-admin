<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="BetList_BetList" Codebehind="BetList.aspx.cs" %>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="Server">
    
<style type="text/css">
    .popup .container-fluid
    {
        margin: 0 0;
    }
    
    .underdog, span.stake, span.handicap
    {
        font-weight: bold;
    }
    
</style>
<link rel="stylesheet" type="text/css" href="../Content/betlist/betlist.css">
<script src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script>
    <script src="../App_Js/prototype.js" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script src="../App_Js/IBCWnd/IBCWnd.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" language="javascript" src="../App_Js/BetList.js"></script>

<div id="contents">
    <div id="box_insite">
        <div class="header_in">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td class="in_left">&nbsp;</td>
                    <td width="170" class="in_top">Member Betlist</td>
                    <td class="in_right">&nbsp;</td>
                </tr>                
            </table>
        </div>
        <div style="height: 5px;"></div>
        <div id="betlist_breadcrum" style="margin-left: 5px;">
            <a href="../UserInfo/StatementHistory.aspx"><%=Resources.Lang.StatementHistory%> (<%= fromDate.ToString("MMM yyyy")%>)</a> >> 
            <a href="../UserInfo/DailyEarning.aspx?monthdate=<%= monthdate.ToShortDateString()%>"><%= fromDate.ToString("MMMM yyyy")%></a> >>
            <a href="../UserInfo/DailyEarningDetail.aspx?monthdate=<%= fromDate.ToShortDateString()%>&custid=<%= originalCustId%>&slSportType=<%= slSportType%>"><%= fromDate.ToString("dd MMMM yyyy")%></a> >>
            <b><%= UserName%></b>
        </div>
        <div style="height: 5px;"></div>
        <div style="width: auto; height: auto; overflow: auto">
            <%= betlistHtmlString%>
        </div>
    </div>
</div>
<form>
    <input type="hidden" id="affid" name="affid" value="<%= AppSettings.Session.CustID %>"/>
</form>

<script type="text/javascript">
    var $j = jQuery.noConflict();
</script>
</asp:Content>
