<%@ Page Language="C#" AutoEventWireup="true" Inherits="AffiliateList" Codebehind="AffiliateList.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script type="text/javascript">
    window.onload = MyFocus;
    var time = 0;//set Timeout for clock refresh
    var timer = null;//set Timeout for choose refresh time
    var timePage = "";//set Timeout for go to page
    function MyFocus() {
        parent.frames['fMenu'].ActiveMenu('menu3');
        
        //$("slPageSize").value = $("pagesize").value;
        $("slTime").value = $("timer").value;
        if($("slTime").value != "0")
        {
            time = parseInt($("slTime").value);
            setInterval("CountDown();", 1000);
        }        
        //use for add favorite affiliate
        var listFavor = $$("div .favor");
        for(var i = 0; i < listFavor.length; i++)
        {
            listFavor[i].observe('click', favorOnClick);
        }
        //disable bottom button if no mem found
        /*if ($("nummem").value == 0) {
            $("btnCGroupBottom").style.display = "none";
        }*/
        //Use for checkbox icon
        CheckBox_AddEvent();
        if ($('txtGroupName') != null && $('txtGroupName') != 'undefined') {
            $('txtGroupName').focus();
        }
        else {            
            $('txtUsername').focus();
        }      
    }
    function ViewGroup(groupid) {
        if (groupid > 1000)
            ibcWnd.open("../GroupSetting/EditGroup.aspx?groupid=" + groupid, 'Edit Commission Group for ' + $("u"+groupid).innerHTML, 150, 50, 755, 529, 'class1', 'class2');
        else
            ibcWnd.open("../GroupSetting/EditGroup.aspx?groupid=" + groupid, 'Edit Commission Group', 150, 50, 755, 529, 'class1', 'class2');
            
    }
    //Start favorite 
    function favorOnClick(event)
    {
        var element = Event.element(event);
        element.readOnly = true;
        if(element.hasClassName("favor-check"))
        {            
            element.className = "favor favor-uncheck";
            //Call ajax to remove this aff in favorite list
            var ajax = new Ajax.Request("../Handler/Favorite.ashx", {
                method: 'post',
                parameters: "affid=" + element.id + "&action=0",
                onComplete: function(data) {
                    var result = data.responseText.split(";");
                    if(result[0] != "0")
                    {
                        alert(result[1]);
                        element.className = "favor favor-check";
                    }
                }
            });
        }
        else
        {
            element.className = "favor favor-check";
            //Call ajax to add this aff in favorite list
            var ajax = new Ajax.Request("../Handler/Favorite.ashx", {
                method: 'post',
                parameters: "affid=" + element.id + "&action=1",
                onComplete: function(data) {
                    var result = data.responseText.split(";");
                    if (result[0] != "0")
                    {
                        alert(result[1]);
                        element.className = "favor favor-uncheck";
                    }
                }
            });
        }
        element.readOnly = false;
    }
    //End favorite     
    function ChangeGroup()
    {
        listChk = $$("div .chk");  
        var i = 0;
        var listAffID = "";
        var count = 0;
        var affid = 0;
        for(i = 0; i < listChk.length; i++)
        {
            if (listChk[i].hasClassName("ic-check")) {
                affid = listChk[i].id;
                count++;
            }
        }
        if (count == 1) {
            ibcWnd.open("../GroupSetting/AssignGroup.aspx?affid=" + affid, 'Change Commission Group', 150, 50, 585, 270, 'class1', 'class2');
        }
        else if (count > 1) {
            ibcWnd.open("../GroupSetting/AssignGroup.aspx", 'Change Commission Group', 150, 50, 585, 270, 'class1', 'class2');
        }
        else {
            alert('Please select Affiliate(s) to change group!');
        }
            
    }
    //End change group block
    function CloseDiv() {
        $('divChangePass').style.display ='none';
    }
    function DoCal(url) {
        ibcWnd.open(url, 'Change Password', 150, 50, 350, 175, 'class1', 'class2');
    }
    function Refresh(url)
    {
        location.href = url;
    }
    function setvaluefocus(e,isok,id) {
        if(isok ==1) {
            if(e.value ==$(id).value) 
                e.value=''; 
        }
        else 
            if(e.value=='')
                 e.value =$(id).value;
    }
    function PreSearch(e) {
        e = (e) ? e : window.event;
        var key = (e.keyCode) ? e.keyCode : e.which;
        if (key == 13) {
            checkData();
        }
    }   
    function checkData() {
        var page = $('frmSearch').getAttribute('data-page') || 1
        if($("txtID").value.toLowerCase() == "id" || $("txtID").value.trim() == '')
        {
            LoadData(page);  
            return false;
        }
        if(!$("txtID").value.isNumber())
        {
            alert("ID is not number. Please input number!");
            $("txtID").focus();
            return false;
        }
        LoadData(page);
    }

    function getAction() {
        var uriRoot = "../Handler/ExportExcel.ashx";

        var param = {

            slLanguage: $('slLanguage') === null ? '' : $('slLanguage').value,
            txtUsername: $('txtUsername').value,
            txtEmail: $('txtEmail').value,
            txtID: $('txtID').value,
            status: $('status').value,
            isclosed: $('isclosed').value,
            favor: $('favor').value
        };

        var queryString = "";
        
        for (var key in param) {
            queryString += key + "=" + param[key] + "&";
        }
        
        queryString = queryString.substring(0, queryString.length - 1);

        var url = encodeURI(uriRoot + "?" + queryString);

        var ele = document.getElementById('frameExport');
        ele.innerHTML = '';
        ele.innerHTML = "<iframe src='" + url + "' style='display:none;'></iframe>";
    }
</script>
<!--/js-->