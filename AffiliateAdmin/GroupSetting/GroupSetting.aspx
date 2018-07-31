<%@ Page Language="C#" MasterPageFile="~/Portal/Layout.Master" AutoEventWireup="true" Inherits="GroupSetting" Codebehind="GroupSetting.aspx.cs" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../App_Js/GroupSetting.css" media="all"/>
</asp:Content>

<asp:Content ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript" src="../App_Js/GroupSetting.js"></script>
</asp:Content>

<asp:Content ContentPlaceHolderID="masterContent" runat="server" ID="mainContent">
    <%=_htmlContent %>
</asp:Content>
