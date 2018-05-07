<%@ Page Language="C#" AutoEventWireup="true" Inherits="Edit12BetDomain" Codebehind="Edit12BetDomain.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/prototype-full.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script language="javascript" type="text/javascript">
    var isUpdate = false;

    window.onload = InitEvent;
    function InitEvent() {
        CheckBox_AddEvent();
        $('txt12betdomain').blur();
        $('txt12betdomain').focus();
    }

   function Edit12BetDomain(_12BetDomainId)
   {
       if ($("txt12betdomain").value.trim() == "")
        {
           Common.alertError(top.fMenu.Msg.AffDomain);
            return;
        }
       
        var ajax = new Ajax.Request('../Handler/Update12BetDomain.ashx', {
            method: 'post',
            parameters: {
                _12BetDomainId: _12BetDomainId,
                _12BetDomainName: $('txt12betdomain').value.trim(),
                ssl: $("chkSSL").hasClassName("ic-check"),
                sta: 'Edit',
                "X-CSRF-Token": $('X-CSRF-Token').value
            },
            onComplete: function(strdata) {
                if (strdata.responseText == 'Ok') {
                    isUpdate = true;
                    $("errMsg").className = "done";
                    Common.alertSuccess(top.fMenu.Msg.UpdateSucc);
                    setTimeout("parent.ibcWnd.close();", 2000);
                }
                else {
                    Common.alertError(strdata.responseText);
                }
            }
        });  
   }
    function PreExec(e,domainid){        
            e =(e) ?e :window.event;
	        var key =(e.keyCode) ?e.keyCode :e.which;
	        if (key ==13)
	            Edit12BetDomain(domainid);
	        else if (key == 39 || key == 34) {
	            alert("12Bet Domain name can not contain this character");
	            return false;
	        }
        }
    window.onunload = function(){if(isUpdate) parent.location.reload();};
</script>
<!--/js-->