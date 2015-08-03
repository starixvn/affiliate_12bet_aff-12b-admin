<%@ Page Language="C#" AutoEventWireup="true" Inherits="ViewMessDetails" Codebehind="ViewMessDetails.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript">
    window.onload = Init;
    var arrItem = new Array();
    var arrErr = new Array();

    function Init() {
        /*assign handler for checkbox div*/
        chkList = $$("div .chk");
        if (chkList.length > 0)
            chkList[0].observe('click', chkOnClick);
        if ($("hdMode").value == "view") {
            SetViewMode();
            if ($("hdImportant").value == "on")
                $$("div .chk")[0].className = "chk ic-check";
            else
                $$("div .chk")[0].className = "chk ic-uncheck";
        }
        else {
            ShowAddNewMessage();
        }
    }

    function chkOnClick(event) {
        if ($("hdMode").value == "view")
            return;
        var element = Event.element(event);
        if (element.hasClassName("chk ic-uncheck"))
            element.className = "chk ic-check";
        else
            element.className = "chk ic-uncheck";
    }
    
    function CheckForm()
    {
        return true;
    } 
    
    var paththeme = "{paththeme}";
    function DeleteSelectedMessage() {
        if (confirm($("ConfirmDeleteMess").value)) {
            var params = "";
            for (var i = minNo; i <= maxNo; i++) {
                if ($("chk" + i).checked) {
                    params += $("td" + i).innerHTML;
                    params += ",";
                }
            }
            DeleteMessage(params);
        }
    }
    function DeleteMessage(msgList) {
        try {
            var params;
            params = "params=" + msgList;

            var ajax = new Ajax.Request("../Handler/DeleteMessageList.ashx", {
                method: 'post',
                parameters: params,
                onComplete: function(data) {
                if (data.responseText != "") {
                        alert(data.responseText);
                }
                ClosePopup();
                }
            });

        }
        catch (e) {
            alert(e);
        }
    }
   
    function DeleteCurrentMessage() {
        if (confirm($("ConfirmDeleteMess").value)) {
            DeleteMessage($("mid").value + ",");
        }
    }
     
    function SetViewMode() {
        $("txtMessageEn").readOnly = true;
        $("txtMessageCn").readOnly = true;
        $("txtMessageTw").readOnly = true;
        $("txtMessageTh").readOnly = true;
        $("txtMessageEs").readOnly = true;
        $("txtMessageVi").readOnly = true;
        $("txtMessageId").readOnly = true;
        $("txtMessageKr").readOnly = true;
        $("txtMessageJp").readOnly = true;
        $("btnAdd").style.display = "none";
        $("btnUpdate").style.display = "none";
        $("btnEdit").style.display = "";
        $("btnDeleteCur").style.display = "";
        $("hdMode").value = "view";

    }
    function SetEditMode() {
        $("txtMessageEn").readOnly = false;
        $("txtMessageCn").readOnly = false;
        $("txtMessageTw").readOnly = false;
        $("txtMessageTh").readOnly = false;
        $("txtMessageEs").readOnly = false;
        $("txtMessageVi").readOnly = false;
        $("txtMessageId").readOnly = false;
        $("txtMessageKr").readOnly = false;
        $("txtMessageJp").readOnly = false;
        $("btnAdd").style.display = "none";
        $("btnEdit").style.display = "none";
        $("btnUpdate").style.display = "";
        $("btnDeleteCur").style.display = "";
        $("hdMode").value = "edit";
    }

    function ShowAddNewMessage() {
        $("txtMessageEn").readOnly = false;
        $("txtMessageCn").readOnly = false;
        $("txtMessageTw").readOnly = false;
        $("txtMessageTh").readOnly = false;
        $("txtMessageEs").readOnly = false;
        $("txtMessageJp").readOnly = false;
        $("txtMessageKr").readOnly = false;
        $("txtMessageEn").value = "";
        $("txtMessageCn").value = "";
        $("txtMessageTw").value = "";
        $("txtMessageTh").value = "";
        $("txtMessageEs").value = "";
        $("txtMessageVi").value = "";
        $("txtMessageId").value = "";
        $("txtMessageJp").value = "";
        $("txtMessageKr").value = "";
        $("btnAdd").style.display = "";
        $("btnUpdate").style.display = "none";
        $("btnEdit").style.display = "none";
        $("btnDeleteCur").style.display = "none";

    }
    
    function UpdateMessage() {
        if (($("txtMessageEn").value == "") && ($("txtMessageCn").value == "") && ($("txtMessageTw").value == "") && ($("txtMessageTh").value == "") && ($("txtMessageEs").value == "") && ($("txtMessageVi").value == "") && ($("txtMessageId").value == "") && ($("txtMessageKr").value == "") && ($("txtMessageJp").value == "")) {
                alert($("AddEmptyMessError").value);
                return;
            }
        if (confirm($("ConfirmUpdateMess").value)) {
            try {
                var params;
                var important;
                var curMsgID = $("mid").value;
                if ($$("div .chk")[0].hasClassName("chk ic-check")) {
                    important = true;
                }
                else {
                    important = false;
                }
                params = "params=" + curMsgID + "$$$";
                params += encodeURIComponent(important) + "$$$";
                params += encodeURIComponent($("txtMessageEn").value) + "$$$";
                params += encodeURIComponent($("txtMessageCn").value) + "$$$";
                params += encodeURIComponent($("txtMessageTw").value) + "$$$";
                params += encodeURIComponent($("txtMessageTh").value) + "$$$";
                params += encodeURIComponent($("txtMessageEs").value) + "$$$";
                params += encodeURIComponent($("txtMessageVi").value) + "$$$";
                params += encodeURIComponent($("txtMessageId").value) + "$$$";
                params += encodeURIComponent($("txtMessageKr").value) + "$$$";
                params += encodeURIComponent($("txtMessageJp").value);

                var ajax = new Ajax.Request("../Handler/UpdateMessage.ashx", {
                    method: 'post',
                    parameters: params,
                    onComplete: function(data) {

                        if (data.responseText != "") {
                            alert(data.responseText);
                        }
                        ClosePopup();
                    }
                });

            }
            catch (e) {
                alert(e);
            }
        }
    }

    function AddNewMessage() {
        try {
            var params;
            if (($("txtMessageEn").value == "") && ($("txtMessageCn").value == "") && ($("txtMessageTw").value == "") && ($("txtMessageTh").value == "") && ($("txtMessageEs").value == "") && ($("txtMessageVi").value == "") && ($("txtMessageId").value == "") && ($("txtMessageKr").value == "") && ($("txtMessageJp").value == "")) {
                alert($("AddEmptyMessError").value);
                return;
            }
            params = "params=";
            var important;
            if ($$("div .chk")[0].hasClassName("chk ic-check"))
                important = true;
            else
                important = false;
            params += encodeURIComponent(important) + "$$$";
            params += encodeURIComponent($("txtMessageEn").value) + "$$$";
            params += encodeURIComponent($("txtMessageCn").value) + "$$$";
            params += encodeURIComponent($("txtMessageTw").value) + "$$$";
            params += encodeURIComponent($("txtMessageTh").value) + "$$$";
            params += encodeURIComponent($("txtMessageEs").value) + "$$$";
            params += encodeURIComponent($("txtMessageVi").value) + "$$$";
            params += encodeURIComponent($("txtMessageId").value) + "$$$";
            params += encodeURIComponent($("txtMessageKr").value) + "$$$";
            params += encodeURIComponent($("txtMessageJp").value);

            var ajax = new Ajax.Request("../Handler/AddMessage.ashx", {
                method: 'post',
                parameters: params,
                onComplete: function(data) {
                    if (data.responseText != "") {
                        alert(data.responseText);
                    }
                    ClosePopup();
                }
            });

        }
        catch (e) {
            alert(e);
        }
    }
</script>
<!--/js-->
