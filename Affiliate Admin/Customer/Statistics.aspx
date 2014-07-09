<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Customer_Statistics" %>

<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript">
    function ChangeDate(spId, selId, isSelDate)
    {
        if(isSelDate)
        {
            $(spId).hide();
            $(selId).show();
            $(selId).focus();
        }
        else
        {
            $(spId).show();
            $(selId).hide();
        }
    }
    function Search()
    {
        DelayReloadPage1();
        $('frmSearch').submit();
    }
    function ShowPage(isShow)
    {
        if(!isShow)
        {
            $("tblContent").hide();
            $("divExpend").show();
            parent.document.getElementById("fRightContent").rows="35,*";
        }
        else
        {
            $("tblContent").show();
            $("divExpend").hide();
            parent.document.getElementById("fRightContent").rows="210,*";
        }
    }
</script>
<!--/js-->