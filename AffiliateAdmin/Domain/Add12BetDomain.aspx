<%@ Page Language="C#" AutoEventWireup="true" Inherits="Add12BetDomain" Codebehind="Add12BetDomain.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    window.onload = InitEvent;

    function InitEvent() {
        $('txt12betdomain').blur();
        $('txt12betdomain').focus();
        CheckBox_AddEvent();
    }
    
   function checkinput()
   {
       if ($('txt12betdomain').value.trim() == '')
        {
            Common.alertError(top.fMenu.Msg._12BetDomain);
            $('txt12betdomain').focus();
            return false;
        }
        return true;
   }
   function Add12BetDomain() {
       var arrError = new Array();
       var arrItem = new Array();
        if(checkinput())
        {
            var ajax = new Ajax.Request('../Handler/Update12BetDomain.ashx', {
                method: 'post',
                parameters: {
                    _12BetDomainName: $('txt12betdomain').value.trim(),
                    ssl: $("chkSSL").hasClassName("ic-check"),
                    sta: 'Add',
                    "X-CSRF-Token": $('X-CSRF-Token').value
                },
                onComplete: function(strdata) {

                    if (strdata.responseText == 'Ok') {                        
                        $("errMsg").className = "done";
                        arrError.push("12Bet Domain has been added successfully!");
                        arrItem.push("errMsg");
                        $("errMsg").show();
                        Common.showError(arrError, arrItem, "error_item");
                        Common.delayReloadPage1();                       
                        setTimeout("ClosePopup();", 3000);
                    }
                    if (strdata.responseText != 'Ok') {
                        Common.alertError(strdata.responseText);
                    }

                }
            });  
        }
   }
    function PreExec1(e){        
        e =(e) ?e :window.event;
        var key =(e.keyCode) ?e.keyCode :e.which;
        if (key == 13) {
            Add12BetDomain();
        }
        else if (key == 39 || key == 34) {
            alert("Affiliate Domain name can not contain this character");
            return false;
        }
    }
    function PreExec(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            Add12BetDomain();
        }
    }
     function Reset()
     {
         $('txt12betdomain').value = "";        
    }
</script>
<!--/js-->