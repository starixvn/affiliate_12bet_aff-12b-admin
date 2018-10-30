<%@ Page Language="C#" AutoEventWireup="true" Inherits="Customer_Default" CodeBehind="AffDefault.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Affiliate Admin</title>
    <script type="text/javascript" src="../App_Js/Common.js"></script>
    <script type="text/javascript" src="../App_Js/prototype.js"></script>
    <script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>

    <style type="text/css">
        html, body {
            height: calc(100% - 8px);
        }

        .left-panel-frame, .right-panel-frame, .customer-page-container {
            height: 100%;
        }

        .left-panel-frame {
            float: left;
            width: 260px;
        }

        .collapse .left-panel-frame {
            width: 25px;
        }

        .right-panel-frame {
            float: left;
            width: calc(100% - 260px);
        }

        .collapse .right-panel-frame {
            width: calc(100% - 25px);
        }

        .statistics-frame {
            height: 210px;
        }

        .user-panel-frame {
            height: calc(100% - 210px);
        }

        .not-approved .statistics-frame {
            display: none;
        }

        .not-approved .user-panel-frame {
            height: 100%;
        }
    </style>
</head>

<body>
    <div class="customer-page-container" id="customer-page-container">
        <div class="left-panel-frame" id="left-panel-frame">
            <iframe width="100%" height="100%" id="fInfo" name="fInfo" frameborder="0" scrolling="auto" src="AffiliateInfo.aspx?username=<%=strUsername%>&pageid=<%=Pageid %>"></iframe>
        </div>
        <div class="right-panel-frame" id="right-panel-frame">
            <div class="statistics-frame" id="statistics-frame">
                <iframe width="100%" height="100%" id="fStatistics" name="fStatistics" frameborder="0" scrolling="no" src=""></iframe>
            </div>
            <div class="user-panel-frame">
                <iframe width="100%" height="100%" id="fContent" name="fContent" frameborder="0" scrolling="auto" src=""></iframe>
            </div>
        </div>
    </div>
</body>
</html>