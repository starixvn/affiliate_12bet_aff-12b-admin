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
        Common.alertError('User Name is empty. Please check!');
        $('txtun').focus();
    }

    if ($('txtpw').value == '') {
        Common.alertError('Password is empty. Please check!');
        $('txtpw').focus();
    }

    $('error_msg').innerHTML = '';
    var ajax = new Ajax.Request("Auth/ProcessLogin.ashx", {
        method: 'post',
        parameters: 'un=' + $('txtun').value + '&pw=' + $('txtpw').value + "&X-CSRF-Token=" + $('X-CSRF-Token').value,
        onComplete: function (res) {
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
    });
}
function setdisable() {
    if ($('txtun').value != '' || $('txtpw').value != '') {
        $('btnlogin').disabled = false;
    }
    else {
        $('btnlogin').disabled = true;
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