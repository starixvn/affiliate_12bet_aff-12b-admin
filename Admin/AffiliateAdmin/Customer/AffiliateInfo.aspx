<%@ Page Language="C#" AutoEventWireup="true" Inherits="Customer_AffiliateInfo" CodeBehind="AffiliateInfo.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/AutoComplete/autocomplete.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>

<link href='../App_Js/AutoComplete/styles.css' type='text/css' rel='stylesheet' />
<script type="text/javascript">
    var baseUrl = window.__baseRootUrl || "/";
    function hasClass(el, className) {
        if (el.classList)
            return el.classList.contains(className)
        else
            return !!el.className.match(new RegExp('(\\s|^)' + className + '(\\s|$)'))
    }

    function addClass(el, className) {
        if (el.classList)
            el.classList.add(className)
        else if (!hasClass(el, className)) el.className += " " + className
    }

    function removeClass(el, className) {
        if (el.classList)
            el.classList.remove(className)
        else if (hasClass(el, className)) {
            var reg = new RegExp('(\\s|^)' + className + '(\\s|$)')
            el.className = el.className.replace(reg, ' ')
        }
    }

    var minHeight = 510;
    window.onresize = function () { setHeight(); };
    window.onload = function () {
        var affName = $("spUN").innerHTML;
        var subQueryString = "affId=" + $("affID").innerHTML + "&affName=" + affName;
        window.subQueryString = subQueryString;

        if ($("hdNoInfo").value == "1")
            window.onresize = "";
        else {
            if ($("hdStatus").value.toLowerCase() != "approved")
                minHeight = 310;
            setHeight();
        }

        if ($("hdStatus").value.toLowerCase() != "approved") {
            addClass(parent.document.getElementById("right-panel-frame"), "not-approved");
            if ($("hdNoInfo").value != "1") {
                if ($("isReload").value != "2")
                    parent.fContent.location.href = "../UserInfo/AffiliateUpd.aspx?approved=0&"+subQueryString;
            }
            else
                parent.fContent.location.href = "blank.html";
        }
        else {

            if (hasClass(parent.document.getElementById("right-panel-frame"), "not-approved"))
                removeClass(parent.document.getElementById("right-panel-frame"), "not-approved");
            if ($("hdNoInfo").value == "0") {
                parent.fStatistics.location.href = baseUrl + "Portal/CustomerStatistics/Index?" + subQueryString;
                switch ($("hdPageid").value)//linked from affiliate list
                {
                    case "1":
                        parent.fContent.location.href = "../Portal/UserPanel/StatementHistory/Index?status=Pending&" + subQueryString;
                        break;
                    case "2":
                        parent.fContent.location.href = "../UserInfo/MemberListAll.aspx?all=0&" + subQueryString;
                        break;
                    default:
                        parent.fContent.location.href = "../Portal/UserPanel/StatementHistory/Index?" + subQueryString;
                        break;
                }
            } else if ($("hdNoInfo").value == "1") {
                parent.fStatistics.location.href = "blank.html";
                parent.fContent.location.href = "blank.html";
            }
            else {
                switch ($("isReload").value) {
                    case "1": //Change to approved status
                        parent.fStatistics.location.href = "Statistics.aspx";
                        parent.fContent.location.href = "../UserInfo/AffiliateUpd.aspx?"+subQueryString;
                        break;
                    case "2": //update aff info after aff had approved
                        parent.fContent.location.href = "../UserInfo/AffiliateUpd.aspx?"+subQueryString;
                        break;
                    case "3": //Update statement
                        parent.fContent.location.href = "../UserInfo/EarningsHistory.aspx?" + subQueryString;
                        break;
                    case "4": //Update statement
                        parent.fContent.location.href = "../UserInfo/Adjustment.aspx?" + subQueryString;
                        break;
                    default:
                        parent.fContent.location.href = "../UserInfo/EarningsHistory.aspx?" + subQueryString;
                        break;
                }
                $("isReload").value = "0";
            }
        }
    }
    function ViewGroup(groupID, groupName) {
        var url = document.getElementById("relativeRootUrl").value + 'Portal/CommissionGroup/AssignPrivateGroup?affId=' + $("affID").innerHTML;
        var left = parent.innerWidth / 2 - 400;
        parent.ibcWnd.open(url, 'Edit Commission Group for ' + $("spUN").innerHTML, left, 50, 800, 529, 'class1', 'class2');
    }
    function setHeight() {
        var height = 0;
        if (self.innerHeight) {
            height = self.innerHeight;
        }
        else if (document.documentElement && document.documentElement.clientHeight) {
            height = document.documentElement.clientHeight;
        }
        else if (document.body) {
            height = document.body.clientHeight;
        }
        height = height - 50;
        $("leftpaneC_close").style.height = height + "px";
        $("contens_left").style.height = (height - 67) > minHeight ? (height - 67) + "px" : minHeight + "px";
    }

    var status = null;
    var comm = null;
    function ChangePass(url) {
        var left = parent.innerWidth / 2 - 180;
        parent.ibcWnd.open(url, 'Change Password', left, 50, 350, 175, 'class1', 'class2');
    }
    //Language Update
    var lang = null;
    function LangTimeout(isShow) {
        clearTimeout(lang);
        lang = null;
        if (!isShow)
            lang = setTimeout("ChangeLang(false, 0);", 4000);
        else
            lang = setTimeout("ChangeLang(true, 0);", 4000);
    }
    function ChangeLang(isChange, setValue) {
        if (isChange) {
            $("slPreferLang").show();
            $("spPreferLang").hide();
            $("slPreferLang").focus();
            if (setValue == 1)
                $("slPreferLang").value = $("hdPreferLang").value;
        }
        else {
            $("slPreferLang").hide();
            $("spPreferLang").show();
        }
    }
    function SubmitLang() {
        if (confirm("Are you sure you want to change language for this affiliate?")) {
            var ajax = new Ajax.Request("../Handler/UpdAffInfo.ashx", {
                method: 'post',
                parameters: 'func=2&lang=' + $('slPreferLang').value + "&X-CSRF-Token=" + $('X-CSRF-Token').value,
                onComplete: function (data) {
                    var arr = data.responseText.split("@");
                    if (arr[0] == '0') {
                        $("hdPreferLang").value = $("slPreferLang").value;
                        $("spPreferLang").innerHTML = $("slPreferLang").options[$("slPreferLang").selectedIndex].text;
                        if (parent.fContent.location.href.indexOf("AffiliateUpd.aspx") >= 0)
                            parent.fContent.location.href = parent.fContent.location.href;
                    }
                    $('slPreferLang').blur();
                    alert(arr[1]);
                }
            });
        }
        ChangeLang(false, 0);
    }

    //Status select box event.
    function AffTypeTimeout(isShow) {
        clearTimeout(status);
        status = null;
        if (!isShow)
            status = setTimeout("ChangeAffType(false, 0);", 4000);
        else
            status = setTimeout("ChangeAffType(true, 0);", 4000);
    }
    function ChangeAffType(isChange, setValue) {
        if (isChange) {
            $("slAffType").show();
            $("spAffType").hide();
            $("slAffType").focus();
            if (setValue == 1) {
                var currentAffType = $("spAffType").innerHTML;
                var options = $$("select#slAffType option");
                var len = options.length;

                for (var i = 0; i < len; i++) {
                    if (options[i].text == currentAffType) {
                        $("slAffType").value = options[i].value;
                        break;
                    }
                }
            }

        }
        else {
            $("slAffType").hide();
            $("spAffType").show();
        }
    }

    function SubmitAffType() {
        var ajax = new Ajax.Request("../Handler/UpdAffType.ashx", {
            method: 'post',
            parameters: 'afftype=' + $("slAffType").value + "&X-CSRF-Token=" + $('X-CSRF-Token').value,
            onComplete: function (data) {
                var arr = data.responseText.split("@");
                if (arr[0] == '1') {
                    $('slAffType').blur();
                    alert(arr[1]);
                } else {
                    var options = $$("select#slAffType option");
                    var len = options.length;

                    for (var i = 0; i < len; i++) {
                        if (options[i].selected == true) {
                            $("spAffType").innerHTML = options[i].text;
                            break;
                        }
                    }
                    parent.fInfo.location.href = parent.fInfo.location.href;
                }
            }
        });
    }

    //Status select box event.
    function StatusTimeout(isShow) {
        clearTimeout(status);
        status = null;
        if (!isShow)
            status = setTimeout("ChangeStatus(false, 0);", 4000);
        else
            status = setTimeout("ChangeStatus(true, 0);", 4000);
    }
    function ChangeStatus(isChange, setValue) {
        if (isChange) {
            $("slStatus").show();
            $("spStaus").hide();
            $("slStatus").focus();
            if (setValue == 1)
                $("slStatus").value = $("spStaus").innerHTML;
        }
        else {
            $("slStatus").hide();
            $("spStaus").show();
        }
    }
    function SubmitStatus() {
        if (confirm("Are you sure you want to change status for this affiliate?")) {

            var status = $('slStatus').value;
            if (status == "Approved") {
                var url = document.getElementById("relativeRootUrl").value + 'Portal/CommissionGroup/AssignGroup?affId=' + $("affID").innerHTML;
                var left = parent.innerWidth / 2 - 250;
                parent.ibcWnd.open(url, 'Assign Commission Group For New Affiliate', left, 50, 585, 310, 'class1', 'class2');
            }
            else {
                var ajax = new Ajax.Request("../Handler/UpdAffInfo.ashx", {
                    method: 'post',
                    parameters: "X-CSRF-Token=" + $('X-CSRF-Token').value + '&func=0&status=' + $('slStatus').value + '&fname=' + $("hdFname").value + '&username=' + $("hdUsername").value + '&lang=' + $("hdPreferLang").value + '&email=' + $("hdEmail").value + '&mailoption=' + $("hdMailOption").value,
                    onComplete: function (data) {
                        var arr = data.responseText.split("@");
                        if (arr[0] == '0') {
                            $("spStaus").innerHTML = $("slStatus").value;
                            if (parent.fContent.location.href.indexOf("AffiliateUpd.aspx") >= 0)
                                parent.fContent.location.href = parent.fContent.location.href;
                        }
                        $('slStatus').blur();
                        alert(arr[1]);
                    }
                });
            }
        }
        ChangeStatus(false, 0);
    }
    //Open, close affiliate
    function ClosedOpenAff() {
        var isClose = false;
        if ($("spClosed").innerHTML == "Yes")
            isClose = false;
        else
            isClose = true;
        if (confirm("Are you sure you want to " + (isClose ? "close" : "open") + " this affiliate?")) {
            var ajax = new Ajax.Request("../Handler/UpdAffInfo.ashx", {
                method: 'post',
                parameters: 'func=1&closed=' + isClose + "&X-CSRF-Token=" + $('X-CSRF-Token').value,
                onComplete: function (data) {
                    var arr = data.responseText.split("@");
                    if (arr[0] == '0') {

                        $("spClosed").innerHTML = (isClose == 0 ? "No" : "Yes");
                        if (isClose) {
                            $("spUN").show();
                            $("lnkUN").hide();
                            $("spCommission").innerHTML = 'NoCommission';
                            $("tdCommission").title = 'NoCommission';

                        } else {

                            $("spUN").hide();
                            $("lnkUN").show();
                        }
                        if (parent.fContent.location.href.indexOf("AffiliateUpd.aspx") >= 0)
                            parent.fContent.location.href = parent.fContent.location.href;
                        parent.fInfo.location.href = parent.fInfo.location.href;
                    }
                    alert(arr[1]);
                }
            });
        }
    }
    function ShowLeft(isShow) {
        if (isShow) {
            parent.document.getElementById("fInfo").scrolling = "auto";
            removeClass(parent.document.getElementById("customer-page-container"), "collapse");
            $("left_pane").show();
            $("divExpLeft").hide();
            //parent.IBCWnd.calculateViewport();
        }
        else {
            addClass(parent.document.getElementById("customer-page-container"), "collapse");
            parent.document.getElementById("fInfo").scrolling = "no";
            window.scroll(0, 0);
            $("left_pane").hide();
            $("divExpLeft").show();
            //parent.IBCWnd.calculateViewport();
        }
    }
    function ChangeGroup(affid, username) {
        var url = document.getElementById("relativeRootUrl").value + 'Portal/CommissionGroup/AssignGroup?affId=' + affid;
        var left = parent.innerWidth / 2 - 250;
        top.mainFrame.ibcWnd.open(url, 'Change Commission Group For \"' + username + '\"', left, 50, 585, 310, 'class1', 'class2');
    }
</script>
<!--/js-->