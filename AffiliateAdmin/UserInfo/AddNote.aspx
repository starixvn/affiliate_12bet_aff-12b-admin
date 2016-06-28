<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_AddNote" CodeBehind="AddNote.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript">
    function addNote() {
        $("errMsg").className = "";
        if (isEmpty($("txtNote").value)) {
            ErrAlter(top.frames[0].Msg.NoteEmpty);
            $("txtNote").value = "";
            $("txtNote").focus();
            return;
        }
        DelayReloadPage1();
        var ajax = new Ajax.Request('../Handler/Note.ashx', {
            method: 'post',
            parameters: { status: 1, custid: $("custid").value, note: $("txtNote").value.trim() },
            onComplete: function (strdata)//Get list note here
            {
                HideDelayReloadPage();
                var returnData = strdata.responseText.split("@");
                if (returnData[0] == 'ok') {
                    $("errMsg").className = "done";
                    setTimeout("parent.LoadData(1);", 1500);
                    SuccAlter(top.frames[0].Msg.NoteAdd);
                }
                else {
                    ErrAlter(returnData[1]);                   
                }
            }
        });
    }
    function updateNote() {
        $("errMsg").className = "";
        if (isEmpty($("txtNote").value)) {
            ErrAlter(top.frames[0].Msg.NoteEmpty);
            $("txtNote").value = "";
            $("txtNote").focus();
            return;
        }
        DelayReloadPage1();
        var ajax = new Ajax.Request('../Handler/Note.ashx', {
            method: 'post',
            parameters: { status: 2, id: $("noteid").value, note: $("txtNote").value.trim(), custid: $("custid").value },
            onComplete: function (strdata)//Get list note here
            {
                HideDelayReloadPage();
                var returnData = strdata.responseText.split("@");
                if (returnData[0] == 'ok') {                  
                    $("errMsg").className = "done";
                    SuccAlter(top.frames[0].Msg.UpdateSucc);
                    setTimeout("parent.LoadData(1);", 1500);
                }
                else {
                    ErrAlter(returnData[1]);
                }

            }
        });
    }
</script>
<!--/js-->