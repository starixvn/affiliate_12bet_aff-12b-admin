/* Time manipulation*/
var listChk = null;    


function showRefreshTime(isSetTime) {
    timer = "";
    clearTimeout(timer);
    if (isSetTime) {
        $("divTime").show();
        $("slTime").focus();
        $("icontime").hide();
    }
    else {
        $("icontime").show();
        $("divTime").hide();
    }
}

function hideRefreshTime() {
    timer = setTimeout("showRefreshTime(false);", 3000);
}

function UpdateTime() {
    var currPage = parseInt($("page").value);
    LoadData(currPage);
}

function CountDown() {
    if (time > 0) {
        time -= 1;
        var min = Math.floor(time / 60);
        var sec = time % 60;
        min = (min < 10 ? "0" + min : min);
        sec = (sec < 10 ? "0" + sec : sec);
        $("spTime").innerHTML = min + ":" + sec;
    }
    else
        LoadData(parseInt($("page").value));
}

/* when paging enable, used to load data of specific page to the grid*/
function LoadData(currPage) {
    DelayReloadPage1();
    try {
        if($("page"))
            $("page").value = currPage;
        if ($("pagesize") && $("slPageSize"))
            $("pagesize").value = $("slPageSize")[$("slPageSize").selectedIndex].value;        
        $("frmSearch").submit();
    }
    catch (e) {
        alert('Invalid data!');
    }
}

/*Manipulating the head and bottom paging. When mouse clicked on the page number, the text changed to textbox*/
var pageTimeOut = null;    
function PageNum(aID, txtID, isInputPage) {
    clearTimeout(pageTimeOut);
    pageTimeOut = null;
    if (isInputPage) {
        $(aID).hide();
        $(txtID).show();
        if ($(txtID))
            $(txtID).focus();
    }
    else 
        pageTimeOut = setTimeout("$(\"" + txtID + "\").hide();$(\"" + txtID + "\").value=\"" + $("page").value + "\";$(\"" + aID + "\").show();", 3000);
}

function ChangePage(e, txtID, maxPage, aID) {
    e = (e) ? e : window.event;
    var key = (e.keyCode) ? e.keyCode : e.which;
    aID = (aID == null) ? "aPageTop" : aID;
    if (key == 13) {
        var currPage = parseInt($(txtID).value);
        if (currPage > maxPage || isNaN(currPage) || currPage <= 0) {
            setTimeout("$(\"" + txtID + "\").hide();$(\"" + txtID + "\").value=\"" + $("page").value + "\";$(\"" + aID + "\").show();", 3000);
            return false;
        }
        LoadData(currPage);
        return true;
    }
    return false;
}    
function checkAll(isCheckAll)
{
    listChk = $$("div.chk");
    var i = 0;
    if(isCheckAll)
        for(i = 0; i < listChk.length; i++)
            listChk[i].className = "chk ic-check"
    else
        for(i = 0; i < listChk.length; i++)
            listChk[i].className = "chk ic-uncheck"
}
function CheckBox_AddEvent() {
    listChk = $$("div.chk");
    for (var i = 0; i < listChk.length; i++) {
        listChk[i].observe('click', chkOnclick);
    }
}
//Start change group block
function chkOnclick(event) {
    var element = Event.element(event);
    if(element.hasClassName("ic-check"))
        element.className = "chk ic-uncheck";
    else
        element.className = "chk ic-check";
}