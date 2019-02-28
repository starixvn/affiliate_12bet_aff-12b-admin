<%@ Page Language="C#" AutoEventWireup="true" Inherits="Home" Codebehind="Home.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>   
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
    window.onload = Init;
    var time = 0;//set Timeout for clock refresh
    var timer = null;//set Timeout for choose refresh time
    var counrDown = null;
    function Init() {
        clearInterval(counrDown);
        counrDown = "";        
        $("slTime").value = $("timer").value;
        if ($("slTime").value != "0") {
            time = parseInt($("slTime").value);
            counrDown = setInterval("HomeCountDown();", 1000);
        }
    }
    function HomeCountDown() {
        if (time > 0) {
            time -= 1;
            var min = Math.floor(time / 60);
            var sec = time % 60;
            min = (min < 10 ? "0" + min : min);
            sec = (sec < 10 ? "0" + sec : sec);
            $("spTime").innerHTML = min + ":" + sec;
        }
        else {
            var params = "params="+$("page").value+","+$("pagesize").value;
            var ajax = new Ajax.Request("../Handler/GetOnlineList.ashx", {
                method: "post",
                parameters: params,
                onComplete: function(data) {
                    var result = data.responseText;
                    if (result != "") {
                        $("table_conts").innerHTML = data.responseText;
                        $("slPageSize").value = $("pagesize").value;
                        Init();
                    }
                    else
                        alert("Sorry! The online list could not be updated");
                }
            });
            time = parseInt($("slTime").value);
        }
    }
</script>
<!--/js-->