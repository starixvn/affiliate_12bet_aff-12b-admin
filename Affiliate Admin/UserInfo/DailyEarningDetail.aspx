<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyEarningDetail.aspx.cs" Inherits="DailyEarningDetail" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>

<script type="text/javascript">
    $(function () {
        $("#betlistpopup").dialog({
            autoOpen: false,
            show: {
                effect: "blind",
                duration: 500
            },
            hide: {
                effect: "explode",
                duration: 500
            }
        });
    });

    function showBetlist() {
        $("#betlistpopup").dialog("open");
    }
</script>
<!--/js-->