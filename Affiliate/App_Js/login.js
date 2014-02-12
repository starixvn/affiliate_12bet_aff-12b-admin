// JScript File
var isLogin = true;

function setvaluefocus(e,isok,id) {          
    if(isok ==1) {       
        if(e.value ==$(id).value) 
        {
            e.focus()
            e.value=''; 
            
        }
    }
    else {   
        if(e.value=='')
        {
             e.value =$(id).value;
        }
    }
}

function ExcLogin() {        
    if($('txtusername').value ==gMsg.entryusername) {  
         
        alert(gMsg.Errmsgentryusername);
        $('txtusername').focus();
        isLogin = false;
         return false;
    }
    else if($('txtpassword').value ==gMsg.entrypassword) {
         alert(gMsg.Errmsgentrypassword);
         $('txtpassword').focus();
         isLogin = false;
        return false;
    }
     if($('txtvalidate').value ==gMsg.entryvalidation) {
        alert(gMsg.Errmsgentryvalidcode);
        $('txtvalidate').focus();
        isLogin = false;
         return false;
    }
    var ajax= new Ajax.Request("Authorization/ProcessLogin.ashx",{
        method:'post',  
        parameters:'un=' + $('txtusername').value + '&pw=' + $('txtpassword').value +'&vcode=' +$('txtvalidate').value,
        onComplete:Login
    });
    return false;
}
function Login(rs) { 
    var err=rs.responseText.split(':');

    switch (err[0]) {
        case '100':
            parent.location.href = 'UserInfo/Account_Overview.aspx';                   
            break;
        case '101':
            parent.location.href = 'Welcome.aspx';
            break;
        default:
            isLogin = false;
            $('validCode').src = 'Authorization/LoginCaptcha.ashx?t=' + new Date().toJSON();
            if (err[0] == 101) {
                alert(err[1]);
                $('txtvalidate').value = '';
                $('txtvalidate').focus();
            }
            else {
                alert(err[1]);
                $('txtvalidate').value = '';
                $('txtpassword').value = '';
                $('txtpassword').focus();
            }
            break;
    }
}



function PreLogin(e){        
    e =(e) ?e :window.event;
	var key =(e.keyCode) ?e.keyCode :e.which;
	if (key ==13) 
	{
	    if(navigator.appName.indexOf("Netscape") >= 0)
	        if(!isLogin)
	        {
	            isLogin = true;
	            return;
	        }
	    ExcLogin();
	}
}
function ExcLogout() {    
    window.location.href = "Authorization/Logout.ashx";        
}

