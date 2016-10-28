<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_Adjustment" CodeBehind="Adjustment.aspx.cs" %>

<!--js-->

<script type="text/javascript" src="../App_Js/prototype.js"></script>

<script type="text/javascript" src="../App_Js/Common.js"></script>

<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>

<script type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();
    var isEdit = false;
    function Adjust() {
        if (isEdit)
            return false;
        isEdit = true;
        arrItem.clear();
        arrError.clear();
        if (Common.isEmpty($("txtAdjust").value.trim())) {
            Common.alertError("The amount is required");
            isEdit = false;
            return false;
        }
        var txtAdj = 0;
        var adjVal = 0;
        try {
            txtAdj = Common.toNumber($("txtAdjust").value)
            adjVal = parseFloat(txtAdj);
            if (!(txtAdj.isNumber()) || adjVal == 0) {
                Common.alertError("The amount is invalid");
                isEdit = false;
                return false;
            }
        }
        catch (o) { Common.alertError("The amount is invalid"); return false; }

        Common.delayReloadPage1();
        var ajax = new Ajax.Request('../Handler/Adjustment.ashx', {
            method: 'post',
            parameters: { custid: $('custid').value, adj: adjVal, des: $('txtDescript').value },
            onComplete: function (strdata) {
                Common.hideLoading();
                var result = strdata.responseText.split("@");
                if (result[0] == '0') {
                    //reload this page and lef panel
                    $("errMsg").className = "done";
                    Common.alertSuccess(result[1]);                   
                    parent.fInfo.$("isReload").value = "4";
                    setTimeout("parent.fInfo.$('frmSearch').submit();", 1500);
                }
                else {
                    Common.alertError(result[1]);
                    isEdit = false;
                }
            }
        });
        return false;
    }
</script>

<!--/js-->