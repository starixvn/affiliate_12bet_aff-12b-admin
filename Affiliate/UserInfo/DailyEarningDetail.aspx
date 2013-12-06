<%@ Page Title="" Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="DailyEarningDetail.aspx.cs" Inherits="UserInfo_DailyEarningDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script src="../App_Js/Calendar2/calendar-normal.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/date.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/Lang/calendar_<%=AppSettings.Session.Language%>.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/calendar-setup.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script type="text/javascript" src="../App_Js/Common.js"></script>
    <script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>

    <link href="../App_Js/Calendar2/Skin/calendar-blue.css?vers=<%Response.Write(AppSettings.JsVersion);%>" media="all" rel="stylesheet" type="text/css" title="Blue" />

    <%=strTemplate%>
    <script type="text/javascript">
        Calendar.setup({
            inputField: "monthdate",
            ifFormat: "%m/%d/%Y",
            showsTime: false,
            button: "fdateTrigger",
            singleClick: true,
            showOthers: true,
            step: 1
        });
  </script> 
</asp:Content>

