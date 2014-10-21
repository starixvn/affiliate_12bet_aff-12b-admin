<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Announcement.aspx.cs" Inherits="UserInfo_Announcement" %>
<%@ Import Namespace="Resources" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .aaa {
            padding: 0 15px;
        }
    </style>
    <script src="../App_Js/prototype.js" type="text/javascript"></script>
    <script src="../App_Js/Cookie.js" language="javascript" type="text/javascript"></script>
</head>
<body style="background-color: #fff; background-image: none;  font-size: 13px">
    <div class="aaa">
        <h2 style="color: #b50000; font-weight: normal; text-transform: uppercase">
            <%= Lang.AnnouncementHeader %>
        </h2>
        <%= Lang.AnnouncementBody %>
    </div>
    <hr/>
    <div class="aaa">
        <input type="checkbox" id="chkDontShowMsgAgain"/> <label for="chkDontShowMsgAgain"><%= Lang.DontShowMessageAgain %></label>       
    </div>
    <script type="text/javascript">
        function Init() {
            Cookie.init({ name: 'Announcement', expires: 30 });
            
            document.getElementById('chkDontShowMsgAgain').onclick = function () {
                Cookie.setData('DontShowMessage', this.checked);
            };
        }

        window.onload = Init;
    </script>
</body>
</html>


