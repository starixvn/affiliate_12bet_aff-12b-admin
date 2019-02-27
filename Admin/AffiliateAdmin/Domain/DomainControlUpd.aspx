<%@ Page Language="C#" AutoEventWireup="true" Inherits="Domain_DomainControlUpd" Codebehind="DomainControlUpd.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();

    window.onload = InitEvent;
    function InitEvent() {
        CheckBox_AddEvent();
    }

    function UpdDomainControl() {
        $("errMsg").className = "";
        arrItem.clear();
        arrError.clear();

        if ($("sl12BetDomain") === null) {
            alert("Please choose a redirect domain.");
            return false;
        }

        Common.delayReloadPage1();

        var myajax = new Ajax.Request('../Handler/DomainControl.ashx', {
            method: 'post',
            parameters: {
                id: $("id").value,
                market: $("slMarket").value,
                domainAff: $("slAffDomain").value,
                domain12bet: $("sl12BetDomain").value,
                status: $("chkStatus").hasClassName("ic-check"),
                action: "upd",
                "X-CSRF-Token": $('X-CSRF-Token').value
            },
            onComplete: function (res) {
                if (res.responseText == "1") {
                    $("errMsg").className = "done";
                    arrError.push("Update Successfully!");
                    arrItem.push("errMsg");
                    $("errMsg").show();
                    Common.showError(arrError, arrItem, "error_item");
                    setTimeout("Common.delayReloadPage1();parent.ibcWnd.close();parent.location.href=parent.location.href;", 3000);
                }
                else {
                    arrError.push(res.responseText);
                    arrItem.push("slMarket");
                    $('errMsg').show();
                    Common.showError(arrError, arrItem, "error_item");
                    Common.hideLoading();
                }
            }
        });
        return false;
    }

</script>
<!--/js-->