<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Performance.aspx.cs" Inherits="Report_Performance" %>
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
<script type="text/javascript">
    window.onload = function() {
        //EnableCalendar($("chkDate").className == "ic-uncheck" ? true : false);
        $("slPreferLang").value = $("hdLanguage").value;

        //set selected
        Select_Value_Set('slPreferLang', getParam('slPreferLang'));
        Select_Value_Set('slSport', getParam('slSport'));
        Select_Value_Set('sortby', getParam('sortby'));
    };
    function chkOnclick() {
        if ($("chkDate").className == "ic-uncheck") {
            $("chkDate").className = "ic-check";
            $("isGetAll").value = "ic-check";
            EnableCalendar(false);
        }
        else {
            $("chkDate").className = "ic-uncheck"
            $("isGetAll").value = "ic-uncheck";
            EnableCalendar(true);
        }
    }
    function Search() {
        $("frmSearch").submit();
        DelayReloadPage1();
    }
    function getParam(name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return results[1];
    }

    function Select_Value_Set(SelectName, Value) {
        var obj = document.getElementById(SelectName);
        for (index = 0; index < obj.options.length; index++) {
            if (obj.options[index].value == Value)
                obj.options[index].selected = true;
        }
    }



    function Focus() {
        if ($("affname").value == "[All]") {
            $("affname").value = "";
        }
    }

    function Blur() {
        if ($("affname").value == "") {
            $("affname").value = $("all").value;
        }
    }
</script>
<!--/js-->