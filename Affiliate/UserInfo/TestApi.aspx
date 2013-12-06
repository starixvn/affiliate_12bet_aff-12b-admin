<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestApi.aspx.cs" Inherits="UserInfo_TestApi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Name: <input type="text" name="username" /></br>
        Date: <input type="date" name="date" /></br>
        <input type="submit" />
    </div>
    </form>
    <div runat="server" id="output">
    </div>
</body>
</html>
