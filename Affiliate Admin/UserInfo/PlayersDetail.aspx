<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlayersDetail.aspx.cs" Inherits="PlayersDetail" %>

<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    function NavigatePage(mypage){
        $("page").value = mypage
        $("frSearch").submit();
    }
</script>
<!--/js-->