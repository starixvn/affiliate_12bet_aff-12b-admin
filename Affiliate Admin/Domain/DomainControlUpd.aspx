<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DomainControlUpd.aspx.cs" Inherits="Domain_DomainControlUpd" %>

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

    function ChangeDevice() {
        var myajax = new Ajax.Request('../Handler/Get12BetDomainList.ashx', {
            method: 'post',
            parameters: {
                deviceType: $("slDevice").value,
                redirectDomain: $('redirectDomain').value
            },
            onComplete: function (res) {
                $('12betdomain-region').innerHTML = res.responseText;
            }
        });
    }

    function UpdDomainControl() {
        $("errMsg").className = "";
        arrItem.clear();
        arrError.clear();

        if ($("sl12BetDomain") === null) {
            alert("Please choose a redirect domain.");
            return false;
        }

        DelayReloadPage1();

        var myajax = new Ajax.Request('../Handler/DomainControl.ashx', {
            method: 'post',
            parameters: {
                id: $("id").value,
                market: $("slMarket").value,
                domainAff: $("slAffDomain").value,
                domain12bet: $("sl12BetDomain").value,
                status: $("chkStatus").hasClassName("ic-check"),
                action: "upd"
            },
            onComplete: function (res) {
                if (res.responseText == "1") {
                    $("errMsg").className = "done";
                    arrError.push("Update Successfully!");
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

    document.observe("dom:loaded", function () {
        $('slDevice').observe('change', ChangeDevice);
    });

</script>
<!--/js-->