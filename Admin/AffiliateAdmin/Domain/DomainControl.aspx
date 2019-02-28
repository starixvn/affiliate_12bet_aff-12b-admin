<%@ Page Language="C#" AutoEventWireup="true" Inherits="Domain_DomainControl" Codebehind="DomainControl.aspx.cs" %>

<!--js-->
<script src="../App_Js/prototype.js" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript">
    function CloseDiv() {
        ibcWnd.close();
    }
    function AddDomainControl(market) {
        DoCal("DomainControlIns.aspx?market="+ market, "Add New Domain Control", 450, 250);
    }
    function DomainControlDel(id) {
        if (confirm("Are you sure you want to delete this domain settings?")) {
            Common.delayReloadPage1();
            var myajax = new Ajax.Request('../Handler/DomainControl.ashx', {
                method: 'post',
                parameters: {
                    id: id,
                    action: "del",
                    "X-CSRF-Token": $('X-CSRF-Token').value
                },
                onComplete: function (res) {
                    if (res.responseText == "1") {
                        location.href = location.href;
                    }
                    else {
                        Common.alertError(res.responseText);
                    }
                    Common.hideLoading();
                }
            });
        }
    }
</script>
<!--/js-->