<%@ Page Language="C#" AutoEventWireup="true" Inherits="UserInfo_MemberActiveList" Codebehind="MemberActiveList.aspx.cs" %>

<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>  
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    window.onload = MyFocus;
    var time = 0;//set Timeout for clock refresh
    var timer = null;//set Timeout for choose refresh time
    var timePage = "";//set Timeout for go to page
    
    function NavigatePage(mypage){
        $("page").value = mypage
        $("frData").submit();
    }

    function MyFocus() {
        $('txtUsername').focus();
    }
    function CloseDiv() {    
        $('divChangePass').style.display ='none';            
    }
    function DoCal(url) {    
        ibcWnd.open(url, 'Change Password', 150, 50, 350, 200, 'class1', 'class2');
    }
    function Refresh(url)
    {
        location.href = url;
    }
    function PreSearch(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            LoadData(1);
        }
    }

    function CheckStatus() {
        if (Common.isEmpty($("txtUsername").value))
            alert("Please input Deposit User Name!")
        else
            LoadData(1);
    }

    function Active12BetUsername(usernames) {        
        var arrError = new Array();
        var arrItem = new Array();

        var ajax = new Ajax.Request('../Handler/ActiveMember.ashx', {
            method: 'post',
            parameters: { usernames: usernames },
            onComplete: function (strdata) {

                if (strdata.responseText == 'Ok') {
                    $("errMsg").className = "done";
                    arrError.push("Members has been activated successfully!");
                    arrItem.push("errMsg");
                    $("errMsg").show();
                    Common.showError(arrError, arrItem, "error_item");
                    location.href = location.href;
                }
                else {
                    var errors = strdata.responseText.split(',');
                    if (errors[0] == "errMsg")//exceptional errors
                    {
                        Common.alertError(errors[1]);
                    }
                    else
                    if (errors[0] == "errUserNames")//user errors
                    {
                        for (var i = 1; i < errors.length; i++) {
                            if (errors[i] != "") {
                                var error = errors[i].split('&');
                                $(error[0] + "-err").innerHTML = error[1];
                            }
                        }
                        setTimeout(function () { location.href = location.href; }, 3000);
                    }
                }
            }
        });
    }
   
</script>
<!--/js-->