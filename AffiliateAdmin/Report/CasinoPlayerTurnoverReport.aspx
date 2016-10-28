<%@ Page Language="C#" AutoEventWireup="true" Inherits="CasinoPlayerTurnoverReport" Codebehind="CasinoPlayerTurnoverReport.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<link rel="stylesheet" type="text/css" href="../App_Js/Calendar2/Skin/calendar-blue.css" media="all" title="win2k-cold-1" />
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script src="../App_Js/Calendar2/calendar-normal.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/date.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/Lang/calendar-e.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/calendar-setup.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    window.onload = Init;
    
    function Init() {
        Common.enableCalendar($("chkDate").className == "ic-uncheck" ? true : false);
        
        if ($("hdLanguage").value != "")
            $("slPreferLang").value = $("hdLanguage").value;
    }

    function chkOnclick() {
        if ($("chkDate").className == "ic-uncheck") {
            $("chkDate").className = "ic-check";
            $("isGetAll").value = "ic-check";
            Common.enableCalendar(false);
        }
        else {
            $("chkDate").className = "ic-uncheck";
            $("isGetAll").value = "ic-uncheck";
            Common.enableCalendar(true);
        }
    }

    function Search() {
        $("page").value = "1";
        $("frmSearch").submit();
        Common.delayReloadPage1();
    }
</script>
<!--/js-->
