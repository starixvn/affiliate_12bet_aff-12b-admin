<%@ Page Language="C#" AutoEventWireup="true" Inherits="BetList_BetListParlayDetail" Codebehind="BetListParlayDetail.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../Content/betlist/systemparlay.detail.css" />
    <title></title>
</head>
<body style="background-color: #fff; background-image: none;  font-size: 13px">
    <div class="content-data">
        <%= betlistParlayDetailHtmlString%>
    </div>

    <div class="note-block">
        <p class="note-content"><span class="asterisk">* </span><span class="note-title"><%=Resources.Lang.Note2%></span></p>
        <p class="text-content"><%=Resources.Lang.SystemParlayDetailNote%></p>
    </div>
</body>
</html>
