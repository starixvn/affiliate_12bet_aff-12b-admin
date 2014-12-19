<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BetList.aspx.cs"
    Inherits="BetList_BetList" %>
    
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
<script type="text/javascript" language="javascript" src="../App_Js/jquery/jquery-1.4.2.min.js"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/IBCWnd/IBCWnd.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" language="javascript" src="../App_Js/BetList.js"></script>

<div id="contents">
    <div id="box_insite">
        <div class="header_in">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td class="in_left">&nbsp;</td>
                    <td width="170" class="in_top">Betlist: <%= betlistInfo%></td>
                    <td class="in_right">&nbsp;</td>
                </tr>                
            </table>
        </div>
        <br/>
        <div style="width: 633px; height: 700px; overflow: auto">
            <%= betlistHtmlString%>
        </div>
    </div>
</div>