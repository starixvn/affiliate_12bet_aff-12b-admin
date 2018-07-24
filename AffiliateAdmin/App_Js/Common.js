function obj(id) {
    return document.getElementById(id);
}
Array.prototype.clear = function () {
    if (this.length > 0) this.splice(0, this.length);
}
String.prototype.isNumber = function () {
    var regs = new RegExp(/^-?\d+(\.\d+)?\s*$/);
    return (regs.test(this));
}

StringBuilder = function (initialText) {
    var _parts = new Array();
    if (initialText) _parts.append(initialText);
    this.append = function (text) {
        if (text) _parts.push(text);
    }
    this.appendLine = function (text) {
        this.append(text);
        this.append("\r\n");
    }
    this.clear = function () {
        _parts.clear();
    }
    this.isEmpty = function () {
        return (_parts.length == 0);
    }
    this.toString = function (delimiter) {
        delimiter = delimiter || '';
        return _parts.join(delimiter);
    }
}

Number.prototype.toDecimals = function (n) {
    n = (isNaN(n)) ? 2 : n;
    var num = Math.pow(10, n);
    function pad(s) {
        s = s || '.';
        return (s.length > 2) ? s : pad(s + '0');
    }
    return (isNaN(this)) ? this : (new String(Math.round(this * num) / num)).replace(/(\.\d*)?$/, pad);
}

String.prototype.startsWith = function (prefix) {
    return (this.substr(0, prefix.length) == prefix);
}

String.prototype.endsWith = function (suffix) {
    return (this.substr(this.length - suffix.length) == suffix);
}

String.prototype.contains = function (item) {
    return (this.indexOf(item) >= 0);
}

String.prototype.trimLeft = function () {
    return this.replace(/^\s*/, "");
}

String.prototype.trimRight = function () {
    return this.replace(/\s*$/, "");
}

String.prototype.trim = function () {
    return this.trimRight().trimLeft();
}

String.prototype.compare = function (args, isIgnoreCase) {
    if (isIgnoreCase) {
        return ((new String(this.toLowerCase()) == (new String(args)).toLowerCase()) ? 0 : 1);
    }
    return ((this == args) ? 0 : 1);
}

String.format = function (format) {
    for (var i = 1; i < arguments.length; i++) {
        format = format.replace("{" + (i - 1) + "}", arguments[i]);
    }
    return format;
}
var Common = (function () {
    function createMaskLoadingDiv() {
        window.divMaskLoading = IBC.createElement("div");
        var div1 = IBC.createElement("div");

        window.divMaskLoading.className = "divframeloading"

        window.divMaskLoading.style.filter = 'alpha(opacity=50)';
        window.divMaskLoading.style.opacity = '0.5';
        window.divMaskLoading.appendChild(div1);
        document.body.appendChild(window.divMaskLoading);
    }

    function reloadMainFrame() {
        var fMenu = top.document.getElementById('fMenu');

        var fInfo = top.document.getElementById("fInfo");
        var currentMenu = fMenu.contentWindow.document.getElementById("current");
        var link = currentMenu.childNodes[0];
        link.click();
    }

    return {
        blockUI: function () {
            Common.delayReloadPage1();
        },
        unBlockUI: function () {
            window.divMaskLoading.style.display = 'none';
        },
        checkMail: function (value) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return filter.test(value);
        },
        isEmpty: function (value) {
            return value == null || value.trim() == "";
        },
        toNumber: function (str) {
            var value = "";
            for (i = 0; i < str.length; i++) {
                var c = str.charAt(i);
                if (c != ',') value += c;
            }
            return value;
        },
        showError: function (errMsgs, tagIds, placeHolder) {
            window.scroll(0, 0);
            var errHtml = "";
            $(placeHolder).style.display = (errMsgs.length == 0) ? 'none' : '';
            for (var i = 0; i < errMsgs.length; ++i) {
                errHtml += "<li><a class='err-msg' href='' onclick=\"$('" + tagIds[i] + "').focus(); return false;\">" + errMsgs[i] + "</a></li>";
            }
            $(placeHolder).innerHTML = "<ul>" + errHtml + "</ul>";
            setTimeout(function () { $('errMsg').hide() }, 3000);
        },
        delayReloadPage: function (id, data) {
            if (!window.divMaskLoading)
                createMaskLoadingDiv();
            if (!window.viewport)
                IBC.calculateViewport();
            window.divMaskLoading.style.width = (window.viewport.width - 20) + 'px';
            window.divMaskLoading.style.height = window.viewport.height + 'px';
            var div1 = IBC.createElement("div");
            window.divMaskLoading.firstChild.style.left = Math.floor(window.viewport.width / 2) + 'px';
            window.divMaskLoading.firstChild.style.top = document.body.scrollTop + 'px';

            window.divMaskLoading.style.display = 'block';
            setTimeout(function () {
                window.divMaskLoading.style.display = 'none';
                if ($(id)) {
                    $(id).innerHTML = data;
                }
            }, 500);
        },
        delayReloadPage1: function () {
            if (!window.divMaskLoading)
                createMaskLoadingDiv();
            if (!window.viewport)
                IBC.calculateViewport();
            window.divMaskLoading.style.width = (window.viewport.width - 3) + 'px';
            window.divMaskLoading.style.height = window.viewport.height + 'px';
            var div1 = IBC.createElement("div");
            window.divMaskLoading.firstChild.style.left = Math.floor(window.viewport.width / 2) + 'px';
            window.divMaskLoading.firstChild.style.top = document.body.scrollTop + 'px';
            window.divMaskLoading.style.display = 'block';
            setTimeout(function () {
                window.divMaskLoading.style.display = 'none';
            }, 30000);
        },
        delayReloadPage2: function (time) {
            if (!window.divMaskLoading)
                createMaskLoadingDiv();
            if (!window.viewport)
                IBC.calculateViewport();
            window.divMaskLoading.style.width = (window.viewport.width - 20) + 'px';
            window.divMaskLoading.style.height = window.viewport.height + 'px';
            window.divMaskLoading.style.display = 'block';
            var div1 = IBC.createElement("div");
            window.divMaskLoading.firstChild.style.left = Math.floor(window.viewport.width / 2) + 'px';
            window.divMaskLoading.firstChild.style.top = document.body.scrollTop + 'px';
            setTimeout(function () { window.divMaskLoading.style.display = 'none'; location.href = location.href; }, time);
        },
        hideLoading: function () {
            if (window.divMaskLoading)
                setTimeout("window.divMaskLoading.style.display = 'none';", 400);
        },
        alertError: function (msg) {
            if ($('errMsg') && $('errMsg')) {
                $('errMsg').className = "";
                $('errMsg').show();
                $('error_msg').innerHTML = msg;
                window.scroll(0, 0);
                setTimeout("Common.hideError()", 10000);
            }
        },
        alertSuccess: function (msg) {
            if ($('errMsg') && $('errMsg')) {
                $("errMsg").className = "done";
                $('errMsg').show();
                $('error_msg').innerHTML = msg;
                window.scroll(0, 0);
                setTimeout("Common.hideError()", 10000);
            }
        },
        hideError: function () {
            $('errMsg').style.display = 'none';
        },
        initDivCheckBox: function () {
            var listChk = $$("div .chk");
            if (listChk.length > 0) {
                for (var i = 0; i < listChk.length; i++) {
                    listChk[i].observe('click', chkOnclick);
                }
            }
        },

        enableCalendar: function (isEnable, dateFormat) {
            if (isEnable) {
                $("fdate").style.backgroundColor = "";
                $("tdate").style.backgroundColor = "";
                $("fdateTrigger").disabled = false;
                $("tdateTrigger").disabled = false;

                if (dateFormat === undefined) {
                    dateFormat = "%m/%d/%Y";
                }

                Calendar.setup({
                    inputField: "fdate",
                    ifFormat: dateFormat,
                    button: "fdateTrigger",
                    singleClick: true,
                    showsTime: false,
                    showOthers: false,
                    step: 1
                });

                Calendar.showMonthsCombo();

                Calendar.setup({
                    inputField: "tdate",
                    ifFormat: dateFormat,
                    button: "tdateTrigger",
                    singleClick: true,
                    showsTime: false,
                    showOthers: false,
                    step: 1
                });
            }
            else {
                $("fdate").style.backgroundColor = "#ECE9D8";
                $("tdate").style.backgroundColor = "#ECE9D8";
                $("fdateTrigger").disabled = true;
                $("tdateTrigger").disabled = true;
                $("fdateTrigger").onclick = "";
                $("tdateTrigger").onclick = "";
            }
        },

        reloadMainFrame: function () {
            reloadMainFrame();
        }
    };
}());


//-------------------------------------------------------------------------
// JScript File
var IE = document.all ? true : false;
// utility functions --------------------------
IBC = [];
IBC.addEvent = function (el, evname, func) {
    if (el.attachEvent) { // IE
        el.attachEvent("on" + evname, func);
    } else if (el.addEventListener) { // Gecko / W3C
        el.addEventListener(evname, func, true);
    } else { // Opera (or old browsers)
        el["on" + evname] = func;
    }
}

IBC.removeEvent = function (el, evname, func) {
    if (el.detachEvent) { // IE
        el.detachEvent("on" + evname, func);
    } else if (el.removeEventListener) { // Gecko / W3C
        el.removeEventListener(evname, func, true);
    } else { // Opera (or old browsers)
        el["on" + evname] = null;
    }
}

IBC.calculateViewport = function () {
    window.viewport = [];
    if (!this.is_ie) {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined'
         && typeof document.documentElement.scrollWidth !=
         'undefined' && document.documentElement.scrollWidth != 0) {
            window.viewport.width = document.documentElement.scrollWidth;
            window.viewport.height = document.documentElement.scrollHeight;
        }

        window.viewport.width = Math.max(window.viewport.width, (Math.max(document.body.scrollWidth, document.body.clientWidth)));
        window.viewport.height = Math.max(window.viewport.height, (Math.max(document.body.scrollHeight, document.body.clientHeight)));
    }
    else {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined'
         && typeof document.documentElement.clientWidth !=
         'undefined' && document.documentElement.clientWidth != 0) {
            window.viewport.width = document.documentElement.clientWidth;
            window.viewport.height = document.documentElement.clientHeight;
        }
            // older versions of IE
        else {
            window.viewport.width = document.getElementsByTagName('body')[0].clientWidth;
            window.viewport.height = document.getElementsByTagName('body')[0].clientHeight;
        }

        window.viewport.width = Math.max(document.body.scrollWidth, window.viewport.width);
        window.viewport.height = Math.max(document.body.scrollHeight, window.viewport.height);
    }
}

IBC.createElement = function (type, parent) {
    var el = null;
    if (document.createElementNS) {
        // use the XHTML namespace; IE won't normally get here unless
        // _they_ "fix" the DOM2 implementation.
        el = document.createElementNS("http://www.w3.org/1999/xhtml", type);
    } else {
        el = document.createElement(type);
    }
    if (typeof parent != "undefined") {
        parent.appendChild(el);
    }
    return el;
}

function GotoPage(pageIndex, maxPage) {
    var currPage = parseInt($("page").value);
    if (currPage == 1 && pageIndex <= 1)
        return false;
    if (currPage == maxPage && pageIndex >= maxPage)
        return false;
    if (pageIndex > maxPage || pageIndex <= 0)
        return false;
    LoadData(pageIndex);
}


function chkOnclick(event) {
    var element = Event.element(event);
    divChkChangeState(element);
}

function divChkChangeState(chk) {
    if (chk.hasClassName("ic-check"))
        chk.className = "chk ic-uncheck";
    else
        chk.className = "chk ic-check";
}

function ClosePopup() {
    if (parent.ibcWnd) {
        var url = parent.location.href;
        parent.location.href = url.substr(0, url.indexOf("#") > 0 ? url.indexOf("#") : url.length);
        parent.ibcWnd.close();
    } else {
        window.close();
    }
}

function DoCal(url, title, width, heigh) {
    if (width == null || heigh == null) {
        ibcWnd.open(url, title, 150, 50, 350, 200, 'class1', 'class2');
    }
    else {
        ibcWnd.open(url, title, 150, 50, width, heigh, 'class1', 'class2');
    }
}

function DoCalFull(url, title, width, heigh, top, left) {
    if (width == null || heigh == null || top == null || left == null) {
        ibcWnd.open(url, title, 150, 50, 350, 200, 'class1', 'class2');
    }
    else {
        ibcWnd.open(url, title, top, left, width, heigh, 'class1', 'class2');
    }
}

function ReloadPage() {
    var url = location.href;
    location.href = url.substr(0, url.indexOf("#") > 0 ? url.indexOf("#") : url.length);
}