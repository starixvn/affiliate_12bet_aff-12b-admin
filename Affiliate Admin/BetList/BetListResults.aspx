<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BetListResults.aspx.cs" Inherits="BetList_BetListResults" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" href="../Content/Fanex.Result/fanex.result.css" />
    <link rel="stylesheet" type="text/css" href="../Content/Fanex.Result/12betadmin.result.css" />
    <link rel="stylesheet" type="text/css" href="../Content/tipsy/tipsy.css" />
    <link href="../Content/tipsy/custom-tipsy.css" rel="stylesheet" type="text/css" />
    <script src="../App_Js/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../App_Js/tipsy/jquery.tipsy.js" type="text/javascript"></script>
    <title></title>
        </head>
<body style="background-color: #fff; background-image: none;  font-size: 13px">
    <div>
        <%= betlistResultsHtmlString%>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.penalty-result').tipsy({ opacity: 1.0 });
        });
    </script>
</body>
</html>
