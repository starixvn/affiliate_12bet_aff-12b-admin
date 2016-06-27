<%@ Page Language="C#" AutoEventWireup="true" Inherits="AddAffiliateDomain" CodeBehind="AddAffiliateDomain.aspx.cs" %>

<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script language="javascript" type="text/javascript">

    window.onload = Focus;
    function Focus() {
        $('txtaffdomain').blur();
        $('txtaffdomain').focus();
    }

    function checkinput() {
        if ($('txtaffdomain').value == '') {
            ErrAlter(top.fMenu.Msg.AffDomain);
            $('txtaffdomain').focus();
            return false;
        }
        return true;
    }
    function AddAffDomain() {
        var arrError = new Array();
        var arrItem = new Array();
        if (checkinput()) {
            DelayReloadPage1();
            var ajax = new Ajax.Request('../Handler/UpdateAffDomain.ashx', {
                method: 'post',
                parameters: { affDomainName: $('txtaffdomain').value, sta: 'Add' },
                onComplete: function (strdata) {
                    HideDelayReloadPage();
                    if (strdata.responseText == 'Ok') {
                        $("errMsg").className = "done";
                        arrError.push("Affiliate Domain has been added successfully!");
                        arrItem.push("errMsg");
                        $("errMsg").show();
                        ShowError(arrError, arrItem, "error_item");
                        setTimeout("ClosePopup();", 1500);
                    }
                    if (strdata.responseText != 'Ok') {
                        ErrAlter(strdata.responseText);
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