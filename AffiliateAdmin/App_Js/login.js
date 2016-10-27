// JScript File

function setvaluefocus(e, isok, id) {
    if (isok == 1) {
        if (e.value == $(id).value) {
            e.value = '';
        }
    }
    else {
        if (e.value == '')
            e.value = $(id).value;
    }
}

function ExcLogin() {
    if ($('txtun').value == '') {
        ErrAlter('User Name is empty. Please check!');
        $('txtun').focus();
        return;
    }
    else if ($('txtpw').value == '') {
        ErrAlter('Password is empty. Please check!');
        $('txtpw').focus();
        return;
    }
    else {
        $('error_msg').innerHTML = '';
        var ajax = new Ajax.Request("Auth/ProcessLogin.ashx", {
            method: 'post',
            parameters: 'un=' + $('txtun').value + '&pw=' + $('txtpw').value,
            onComplete: AlertMsg
        });
    }
}
function setdisable() {
    if ($('txtun').value != '' || $('txtpw').value != '') {
        $('btnlogin').disabled = false;
    }
    else {
        $('btnlogin').disabled = true;
    }
}
function AlertMsg(res) {
    if (res.responseText == 'OK') {
        parent.location.href = 'Default.aspx';
    }
    else if (res.responseText == 'UM') {
        location.href = 'UnderMaintenance.aspx';
    }
    else {
        $('errMsg').show();
        $('error_msg').className = "comment";
        $('error_msg').innerHTML = res.responseText;
    }
}

function PreLogin(e) {
    e = (e) ? e : window.event;
    var key = (e.keyCode) ? e.keyCode : e.which;
    if (key == 13)
        ExcLogin();
}

if (top != window) {
    top.location.href = location.href;
}