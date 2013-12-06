<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentRequest.aspx.cs" Inherits="PaymentRequest" %>
<!--js-->
    <script type="text/javascript" src="../App_Js/prototype.js"></script>
    <script type="text/javascript" src="../App_Js/common.js"></script>
    <script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
    <script type="text/javascript" src="../App_Js/CheckDate.js"></script>
    <script src="../App_Js/Calendar2/calendar-normal.js" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/date.js" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/Lang/calendar-e.js" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/calendar-setup.js" type="text/javascript"></script>
    <script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
    <link rel="stylesheet" type="text/css" href="../App_Js/Calendar2/Skin/calendar-blue.css" media="all" title="win2k-cold-1" />
    <script type="text/javascript">
        var arrItem = new Array();
        var arrError = new Array();
        window.onload = function() {        
            if($("hdStatus").value == "")
                $("slStatus").options[0].selected = true;
            else
                $("slStatus").value = $("hdStatus").value;
            $("slSearch").value = $("hdSearch").value;
            GetAllClick();
            SearchBy();
            //Timer manipulation
            if($("slPageSize"))
                $("slPageSize").value = $("pagesize").value;
            $("slTime").value = $("timer").value;
            if ($("slTime").value != "0") {
                time = parseInt($("slTime").value);
                setInterval("CountDown();", 1000);
            }
        };
        function TransDetail(url, title)
        {
            ibcWnd.open(url, title, 100, 10, 500, 515, 'class1', 'class2');
        }
        function ValidateForm()
        {
            arrError.clear();
            arrItem.clear();            
            if($('chkGetAll').checked == false && ($("fdate").value == "" || $("tdate").value == ""))
            {               
               arrError.push(top.fMenu.Msg.ErrDate);                           
               ErrAlter(arrError);
               return false;
            }    
            NavigatePage(1);
            return true;
        }
        function CheckValidDate(id)
        {
            if(isDate($(id).value)== false)
            {
                $(id).focus();
                return false;
            }
            return true;
        }
        function GetAllClick()
        {
            $('fdateTrigger').disabled=$('chkGetAll').checked;
            $('fdate').disabled=$('chkGetAll').checked;
            $('tdateTrigger').disabled=$('chkGetAll').checked;
            $('tdate').disabled=$('chkGetAll').checked;
            $('slStatus').disabled=$('chkGetAll').checked;
        }
        function NavigatePage(mypage){
            $("page").value = mypage
            $("frmSearch").submit();
        }
        function SearchBy()
        {
            if($("slSearch").value == "0")
            {
                $("tdUsername").show();
                $("tdOthers").hide();
            }
            else
            {
                $("tdUsername").hide();
                $("tdOthers").show();
            }
        }
    </script>
<!--/js-->