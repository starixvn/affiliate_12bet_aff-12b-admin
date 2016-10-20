<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="Message" Codebehind="Message.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<script src="../App_Js/Calendar2/calendar-normal.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
<script src="../App_Js/Calendar2/date.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
<script src="../App_Js/Calendar2/Lang/calendar_<%=AppSettings.Session.Language%>.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
<script src="../App_Js/Calendar2/calendar-setup.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
<link href="../App_Js/Calendar2/Skin/calendar-blue.css?vers=<%Response.Write(AppSettings.JsVersion);%>" media="all" rel="stylesheet" type="text/css" title="Blue" />
<%=hotNewsContent %>
<script type="text/javascript">
    if($("slSearchOption") && Common.getDataFromUrl("slSearchOption") != "")
        $("slSearchOption").value = Common.getDataFromUrl("slSearchOption");
    
    window.onload = function() {EnableDisableSearchDate();};
    if (Common.getDataFromUrl("chkDate") == "on")
    {
        $("chkDate").checked = true;
        EnableDisableSearchDate();
    }
</script>
</asp:Content>