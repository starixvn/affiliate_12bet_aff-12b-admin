<%@ Page Language="C#" AutoEventWireup="true" Inherits="AddAffiliateDomain" CodeBehind="AddAffiliateDomain.aspx.cs" %>

<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    window.onload = InitEvent;

    function InitEvent() {
        Focus();
        Common.initDivCheckBox();
    }

    function Focus() {
        $('txtaffdomain').blur();
        $('txtaffdomain').focus();
    }

    function checkinput() {
        if ($('txtaffdomain').value.trim() == '') {
            Common.alertError(top.fMenu.Msg.AffDomain);
            $('txtaffdomain').focus();
            return false;
        }
        return true;
    }
    function AddAffDomain() {
        var arrError = new Array();
        var arrItem = new Array();
        if (checkinput()) {
            Common.delayReloadPage1();
            var ajax = new Ajax.Request('../Handler/UpdateAffDomain.ashx', {
                method: 'post',
                parameters: {
                    affDomainName: $('txtaffdomain').value.trim(), sta: 'Add',
                    "X-CSRF-Token": $('X-CSRF-Token').value,
                    ssl: $("chkSSL").hasClassName("ic-check")
                },
                onComplete: function (strdata) {
                    Common.hideLoading();
                    if (strdata.responseText == 'Ok') {
                        $("errMsg").className = "done";
                        arrError.push("Affiliate Domain has been added successfully!");
                        arrItem.push("errMsg");
                        $("errMsg").show();
                        Common.showError(arrError, arrItem, "error_item");
                        setTimeout("ClosePopup();", 1500);
                    }
                    if (strdata.responseText != 'Ok') {
                        Common.alertError(strdata.responseText);
                    }

                }
            });
        }
    }
    function PreExec1(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            AddAffDomain();
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
            AddAffDomain();
        }
    }
    function Reset() {
        $('txtaffdomain').value = "";
    }
</script>
<!--/js-->