<%@ Page Language="C#" AutoEventWireup="true" Inherits="Others_EmailHistory" Codebehind="EmailHistory.aspx.cs" %>

<!--js-->
   <link rel="stylesheet" type="text/css" href="../App_Js/Calendar2/Skin/calendar-blue.css" media="all" title="win2k-cold-1" />
    <script type="text/javascript" src="../App_Js/prototype.js"></script>
    <script type="text/javascript" src="../App_Js/common.js"></script>  
    <script type="text/javascript" src="../App_Js/RefreshTime.js"></script>  
    <script src="../App_Js/Calendar2/calendar-normal.js" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/date.js" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/Lang/calendar-e.js" type="text/javascript"></script>
    <script src="../App_Js/Calendar2/calendar-setup.js" type="text/javascript"></script>

    <link href="../App_Js/dhtmlwindow/dhtmlwindow.css" rel="stylesheet" type="text/css" />
    <script src="../App_Js/dhtmlwindow/dhtmlwindow.js" type="text/javascript"></script>
    <script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    window.onload = MyFocus;
    var time = 0; //set Timeout for clock refresh
    var timer = null; //set Timeout for choose refresh time
    var timePage = ""; //set Timeout for go to page
    function MyFocus() {
        try {
            //keep the state of controls if have
            if ($("hdTotalEmail").value == "0") {

                $("btnDelBottom").style.display = "none";
            }

            $("slTime").value = $("timer").value;
            if ($("hdSearchOption").value == -1 || $("hdSearchOption").value == -2) {
                $("txtSearchText").style.display = "none";
                if ($("hdSearchOption").value == -1) {
                    $("slSearchOption").options[0].selected = true;
                }
                else {
                    $("slSearchOption").options[4].selected = true;
                }
            }
            else {
                $("slSearchOption").value = $("hdSearchOption").value;
                $("txtSearchText").style.display = "";
            }

            $("txtSearchText").value = $("hdSearchText").value;

            checkSearchDate();

            $("fdate").value = $("hdfDate").value;
            $("tdate").value = $("hdtDate").value;



            if ($("slTime").value != "0") {
                time = parseInt($("slTime").value);
                setInterval("CountDown();", 1000);
            }

            
            //Use for checkbox icon
            var listChk = $$("div .chk");            
            for (var i = 0; i < listChk.length; i++) {
                listChk[i].observe('click', chkOnclick);
            }
        }
        catch (e) { }
    }

    function onCheckSearchDate() {
        var element = $("chkDate");
        if (element.hasClassName("date-check")){
            $("hdchkDate").value = ""
        }
        else {
            $("hdchkDate").value = "on"
        }

        checkSearchDate();
    }

    //Start favorite 
    function favorOnClick(event) {
        var element = Event.element(event);
        element.readOnly = true;
        if (element.hasClassName("favor-check")) {
            element.className = "favor favor-uncheck";
            //Call ajax to remove this aff in favorite list
            var ajax = new Ajax.Request("../Handler/Favorite.ashx", {
                method: 'post',
                parameters: "affid=" + element.id + "&action=0",
                onComplete: function(data) {
                    var result = data.responseText.split(";");
                    if (result[0] != "0")
                        alert(result[1]);
                }
            });
        }
        else {
            element.className = "favor favor-check";
            //Call ajax to add this aff in favorite list
            var ajax = new Ajax.Request("../Handler/Favorite.ashx", {
                method: 'post',
                parameters: "affid=" + element.id + "&action=1",
                onComplete: function(data) {
                    var result = data.responseText.split(";");
                    if (result[0] != "0")
                        alert(result[1]);
                }
            });
        }
        element.readOnly = false;
    }
    //End favorite 
    //Start change group block
    function chkOnclick(event) {
        var element = Event.element(event);
        if (element.hasClassName("ic-check"))
            element.className = "chk ic-uncheck";
        else
            element.className = "chk ic-check";
    }

    function checkAll(isCheckAll) {
        listChk = $$("div .chk");
        var i = 0;
        if (isCheckAll)
            for (i = 0; i < listChk.length; i++) {
                listChk[i].className = "chk ic-check";
            }
        else
            for (i = 0; i < listChk.length; i++) {
                listChk[i].className = "chk ic-uncheck";
            }
    }
    //End change group block
    function CloseDiv() {
        $('divChangePass').style.display = 'none';
    }
    function DoCal(url) {
        ibcWnd.open(url, 'Email Detail', 150, 50, 500, 320, 'class1', 'class2');
    }
    function Refresh(url) {
        location.href = url;
    }
    function setvaluefocus(e, isok, id) {
        if (isok == 1) {
            if (e.value == $(id).value)
                e.value = '';
        }
        else
            if (e.value == '')
            e.value = $(id).value;
    }
    function PreSearch(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            LoadData(1);
        }
    }
    
</script>
<!--/js-->
