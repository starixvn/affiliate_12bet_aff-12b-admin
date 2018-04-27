<%@ Page Language="C#" AutoEventWireup="true" Inherits="EditAffiliateDomain" Codebehind="EditAffiliateDomain.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/prototype-full.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script language="javascript" type="text/javascript">
    var isUpdate = false;

    window.onload = InitEvent;
    function InitEvent() {
        Common.initDivCheckBox();
        $('txtaffdomain').blur();
        $('txtaffdomain').focus();
    }

   function EditAffDomain(affDomainId)
   {
       if ($("txtaffdomain").value.trim() == "")
        {
            Common.alertError(top.fMenu.Msg.ErrAffDomain);
            return;
       }
       Common.delayReloadPage1();
        var ajax = new Ajax.Request('../Handler/UpdateAffDomain.ashx', {
            method: 'post',
            parameters: {
                affDomainId: affDomainId, affDomainName: $('txtaffdomain').value, sta: 'Edit',
                "X-CSRF-Token": $('X-CSRF-Token').value,
                ssl: $("chkSSL").hasClassName("ic-check")
            },
            onComplete: function (strdata) {
                Common.hideLoading();
                if (strdata.responseText == 'Ok') {
                    isUpdate = true;
                    $("errMsg").className = "done";
                    console.log(top.fMenu);
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
	            EditAffDomain(domainid);
	        else if (key == 39 || key == 34) {
	            alert("Affiliate Domain name can not contain this character");
	            return false;
	        }
        }
    window.onunload = function(){if(isUpdate) parent.location.reload();};
</script>
<!--/js-->