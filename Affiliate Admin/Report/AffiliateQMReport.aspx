<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AffiliateQMReport.aspx.cs" Inherits="Report_AffiliateQMReport" %>
<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" language="javascript">
    window.onload = Init;
    
    function Init()
    {
        $("slmonth").value = $("month").value;
        $("slyear").value = $("year").value;
        if ($("hdLanguage").value != "")
            $("slPreferLang").value = $("hdLanguage").value;
    }
    function Search() {
        $("page").value = "1";
        $("frmSearch").submit();
        DelayReloadPage1();
    }
    
    function Focus(){
        if($("affname").value == "[All]")
        {
            $("affname").value = "";
        }
        else
        {
            
        }
    }
    
    function Blur()
    {
        if($("affname").value == "")
        {
            $("affname").value = $("all").value;
        }
    }
</script>
<!--/js-->
