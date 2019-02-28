<%@ Page Language="C#" AutoEventWireup="true" Inherits="AffEditRate" Codebehind="AffEditRate.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" type="text/javascript"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();
    var isUpdate = false;
    function updateRate(exid)
    {
        $("errMsg").className = "";
        arrItem.clear();
        arrError.clear();
        if (Common.isEmpty($("txtRate").value))
        {
            arrError.push(MsgResource.requireRate);
              arrItem.push("errMsg");
              $('errMsg').show();
              Common.showError(arrError, arrItem, "error_item");
              return false;
        }
        var Rate = parseFloat($("txtRate").value);
        if(Rate <= 0)
        {
            arrError.push(MsgResource.errorRate);
            arrItem.push("errMsg");
            $('errMsg').show();
            Common.showError(arrError, arrItem, "error_item");
            return false;
	    }
        Common.delayReloadPage1();

        
        var col = $("txtRate").getAttribute("data-col");

	    var myajax = new Ajax.Request('../Handler/EditRate.ashx', {
	        method: 'post',
	        parameters: { id: exid, rate: $("txtRate").value, column: col },
	        onComplete: function(res) {
	            if (res.responseText == "1") {
	                $("errMsg").className = "done";
	                arrError.push("Update Successfully!");
	                arrItem.push("errMsg");
	                isUpdate = true;	                
	                $("errMsg").show();
	                Common.showError(arrError, arrItem, "error_item");
	                setTimeout("Common.delayReloadPage1();parent.ibcWnd.close();", 2000);
	            }
	            else {
	                arrError.push(res.responseText);
	                arrItem.push("txtRate");
	                $('errMsg').show();
	                Common.showError(arrError, arrItem, "error_item");
	                Common.hideLoading();
	            }
	        }
	    });
        return false;
    }
    
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if ((charCode > 31 && (charCode < 48 || charCode > 57)) && (charCode != 46)) {
            alert("Please input digit only.");
            return false;
        }

        return true;
    }
    
    window.onunload=function(){if(isUpdate) parent.location.reload();}
</script>
<!--/js-->