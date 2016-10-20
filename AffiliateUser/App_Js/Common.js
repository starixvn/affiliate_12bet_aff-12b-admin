var IE = document.all ? true : false;
var arrItem = new Array();
var arrError = new Array();
var timeDelay = "";
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
    var pageTimeOut = null;
    return {
        checkMail: function (value) {
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (filter.test(value.trim()))
                return true;
            else
                return false;
        },

        isEmpty: function (value) {
            if (value == null || value.trim() == "")
                return true;
            else
                return false;
        },

        openWindow: function (theURL, winName, features) {
            window.open(theURL, winName, features);
        },
        validateEmail: function (field) {
            with (field) {
                apos = value.indexOf("@");
                dotpos = value.lastIndexOf(".");
                if (apos < 1 || dotpos - apos < 2) {
                    return false;
                }
                else {
                    return true;
                }
            }
        },
        addError: function (objID, errMsg) {
            arrItem.push(objID);
            arrError.push(errMsg)
        },
        showError: function (errMsgs, tagIds, placeHolder) {
            window.scroll(0, 0);
            parent.scroll(0, 0);
            var errHtml = "";
            $(placeHolder).style.display = (errMsgs.length == 0) ? 'none' : '';
            for (var i = 0; i < errMsgs.length; ++i) {
                errHtml += "<li><a class='err-msg' href='' onclick=\"$('" + tagIds[i] + "').focus(); return false;\">" + errMsgs[i] + "</a></li>";
            }
            $(placeHolder).innerHTML = "<ul>" + errHtml + "</ul>";
        },
        showErrorNotLink: function (errMsgs, tagIds, placeHolder) {
            window.scroll(0, 0);
            parent.scroll(0, 0);
            var errHtml = "";
            $(placeHolder).style.display = (errMsgs.length == 0) ? 'none' : '';
            for (var i = 0; i < errMsgs.length; ++i) {
                errHtml += "<li><span class='err-msg b' href=''>" + errMsgs[i] + "</span></li>";
            }
            $(placeHolder).innerHTML = "<ul>" + errHtml + "</ul>";
        },
        displayMessage: function (isErr) {
            if (!isErr) {
                Common.showError(arrError, arrItem, "error_item");
                $('errMsg').show();
                if ($('succMsg'))
                    $('succMsg').hide();
            }
            else {
                Common.showError(arrError, arrItem, "error_item");
                $('errMsg').hide();
                if ($('succMsg'))
                    $('succMsg').show();
            }
        },

        /*Manipulating the head and bottom paging. When mouse clicked on the page number, the text changed to textbox*/

        pageNum: function (aID, txtID, isInputPage) {
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
        },

        changePage: function (e, txtID, maxPage, aID) {
            e = (e) ? e : window.event;
            var key = (e.keyCode) ? e.keyCode : e.which;
            aID = (aID == null) ? "aPageTop" : aID;
            if (key == 13) {
                var currPage = parseInt($(txtID).value);
                if (currPage > maxPage || isNaN(currPage) || currPage <= 0) {
                    setTimeout("$(\"" + txtID + "\").hide();$(\"" + txtID + "\").value=\"" + $("page").value + "\";$(\"" + aID + "\").show();", 3000);
                    return false;
                }
                Common.loadData(currPage);
            }
        },

        gotoPage: function (pageIndex, maxPage) {
            var currPage = parseInt($("page").value);
            if (currPage == 1 && pageIndex <= 1)
                return false;
            if (currPage == maxPage && pageIndex >= maxPage)
                return false;
            if (pageIndex > maxPage || pageIndex <= 0)
                return false;
            Common.loadData(pageIndex);
        },

        /*End paging processing functions*/
        /* when paging enable, used to load data of specific page to the grid*/
        loadData: function (currPage) {

            // write its own ValidData() on each page
            if (window.ValidData && !window.ValidData()) {
                return;
            }

            try {
                if ($("page"))
                    $("page").value = currPage;
                $("frmData").submit();
            }
            catch (e) {
                alert('Invalid data!');
            }
        },
        getDataFromUrl: function (fieldName) {
            var url = location.href;
            var start = url.indexOf(fieldName, 0);
            var end = url.indexOf("&", start);
            if (end == -1)
                end = url.length;
            if (start < 0 || end < 0)
                return "";
            start = start + fieldName.length + 1;
            return url.substr(start, end - start);
        },

        // Cookie
        getCookie: function (name) {
            var nameEQ = name + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
            }
            return null;
        },

        setCookie: function (name, value, days) {
            if (days) {
                var date = new Date();
                date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
                var expires = "; expires=" + date.toGMTString();
            }
            else var expires = "";
            document.cookie = name + "=" + value + expires + "; path=/";
        },

        //-----------------------------------------------
        // display betlist popup
        openPopup: function (url, title, width, heigh, top, left) {
            if (width == null || heigh == null || top == null || left == null) {
                ibcWnd.open(url, title, 150, 50, 350, 200, 'class1', 'class2');
            }
            else {
                ibcWnd.open(url, title, top, left, width, heigh, 'class1', 'class2');
            }
        }
    };
}());

// utility functions --------------------------
var IBC = (function () {
    return {
        addEvent: function (el, evname, func) {
            if (el.attachEvent) { // IE
                el.attachEvent("on" + evname, func);
            } else if (el.addEventListener) { // Gecko / W3C
                el.addEventListener(evname, func, true);
            } else { // Opera (or old browsers)
                el["on" + evname] = func;
            }
        },

        removeEvent: function (el, evname, func) {
            if (el.detachEvent) { // IE
                el.detachEvent("on" + evname, func);
            } else if (el.removeEventListener) { // Gecko / W3C
                el.removeEventListener(evname, func, true);
            } else { // Opera (or old browsers)
                el["on" + evname] = null;
            }
        },

        calculateViewport: function () {
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
        },

        createElement: function (type, parent) {
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
        },

        createElement: function (type, parent) {
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
    };
}());