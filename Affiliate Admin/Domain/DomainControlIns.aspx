<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DomainControlIns.aspx.cs" Inherits="Domain_DomainControlIns" %>

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

    function AddDomainControl() {
        $("errMsg").className = "";
        arrItem.clear();
        arrError.clear();

        DelayReloadPage1();
        var myajax = new Ajax.Request('../Handler/DomainControl.ashx', {
            method: 'post',
            parameters: { 
                        market: $("slMarket").value,
                        domainAff: $("slAffDomain").value,
                        domain12bet: $("sl12BetDomain").value,
                        status: $("chkStatus").hasClassName("ic-check"),
                        action: "ins" 
                        },
            onComplete: function (res) {
                if (res.responseText == "1") {
                    $("errMsg").className = "done";
                    arrError.push("Insert Successfully!");
                    arrItem.push("errMsg");
                    $("errMsg").show();
                    ShowError(arrError, arrItem, "error_item");
                    setTimeout("DelayReloadPage1();parent.ibcWnd.close();parent.location.href=parent.location.href;", 3000);
                }
                else {
                    arrError.push(res.responseText);
                    arrItem.push("slMarket");
                    $('errMsg').show();
                    ShowError(arrError, arrItem, "error_item");
                    HideDelayReloadPage();
                }
            }
        });
        return false;
    }
</script>
<!--/js-->