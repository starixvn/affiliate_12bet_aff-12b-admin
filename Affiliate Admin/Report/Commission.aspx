<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Commission.aspx.cs" Inherits="Report_Commission" %>
<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" language="javascript">
    window.onload = Init;

    function Init() {
        $("slmonth").value = $("month").value;
        $("slyear").value = $("year").value;

        if ($("hdLanguage").value != "") {
            $("slPreferLang").value = $("hdLanguage").value;
        }

        if ($("hdSortBy").value != "") {
            $("slSortBy").value = $("hdSortBy").value;
        }

        $("slSport").value = $("hdSport").value;
    }

    function chkOnclick() {
        if ($("chkDesc").className == "ic-uncheck") {
            $("chkDesc").className = "ic-check";
            $("isDesc").value = "ic-check";
        }
        else {
            $("chkDesc").className = "ic-uncheck";
            $("isDesc").value = "ic-uncheck";
        }
    }

    function Search() {
        $("frmSearch").submit();
        DelayReloadPage1();
    }

    function Focus() {
        if ($("affname").value == "[All]") {
            $("affname").value = "";
        }
        else {

        }
    }

    function Blur() {
        if ($("affname").value == "") {
            $("affname").value = $("all").value;
        }
    }
</script>
<!--/js-->