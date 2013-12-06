<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="MediaStatistics_New.aspx.cs" Inherits="UserInfo_MediaStatistics_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script src="../App_Js/Calendar2/calendar-normal.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/date.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/Lang/calendar_<%=AppSettings.Session.Language%>.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/calendar-setup.js?vers=<%Response.Write(AppSettings.JsVersion);%>" type="text/javascript"></script>
    <link href="../App_Js/Calendar2/Skin/calendar-blue.css?vers=<%Response.Write(AppSettings.JsVersion);%>" media="all" rel="stylesheet" type="text/css" title="Blue" />
  <%=statisticsContent %>
  <script type="text/javascript">
        Calendar.setup({
            inputField : "fdate",
            ifFormat: "%m/%d/%Y",
            showsTime : false, 
            button : "fdateTrigger",
            singleClick : true,
            showOthers : true,
            step : 1
        });

        Calendar.setup({
            inputField: "tdate",
            ifFormat: "%m/%d/%Y",
            showsTime: false,
            button: "tdateTrigger",
            singleClick: true,
            showOthers: true,
            step: 1
        });
  </script>
 </asp:Content>
