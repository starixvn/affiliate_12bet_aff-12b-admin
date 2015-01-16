<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Account_Overview.aspx.cs"
    Inherits="Account_Overview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <!--js-->
    <link rel='stylesheet' href='http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/themes/base/jquery-ui.css'>
    <!--<script src="../App_Js/prototype.js" type="text/javascript"></script>-->
    <script src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script> 
    <script src='https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/jquery-ui.min.js'></script>
    <script src="../App_Js/MonthPicker/jquery.mtz.monthpicker.js" type="text/javascript"></script>
    <link href="../App_Js/MonthPicker/month-picker.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript" src="../App_Js/swfobject.js"></script>
    <script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
    <script src="../App_Js/IBCWnd/IBCWnd.js" language="javascript" type="text/javascript"></script>
    
    <style type="text/css">
        .month-picker {
            cursor: pointer;
            border: 1px solid #ccc;
            height: 14px;
            padding-left: 3px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        function HidePopup() {
            var p = document.getElementById("pop_up");
            p.style.display = "none";
            //set cookie according to checkbox status
        }

        function SetPopupShow() {
            if ($("dontshow").checked)
                setCookie("dontshow" + "$" + $("custid").value, "true", 30);
            else
                setCookie("dontshow" + "$" + $("custid").value, "false", 30);
        }
    
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">
                            &nbsp;
                        </td>
                        <td width="210" class="in_top">
                            <asp:Literal runat="server" ID="ltrAccOverview"></asp:Literal>
                        </td>
                        <td class="in_right">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <form action="" id="Register_form" name="Register_form" method="get">
            <div class="in_conts">
                <div class="box_conts_header">
                    <div class="flright">
                        <input type="text" name="fmonth" size="13" value="<%= selectedMonth.ToString("MMM-yyyy") %>" class="txtdate-no-time month-picker" id="fmonth" />&nbsp;
                        <img width="16" height="16" src="../App_Themes/Default/images/cal.gif" class="img-date-selector hand"
                        style="margin-bottom: -3px;" title="Date selector" id="fmonth-Trigger" />&nbsp;&nbsp;
                        <input value="<%=Resources.Lang.Submit %>" class="button" type="submit" />&nbsp;
                    </div>
                    <asp:Literal runat="server" ID="ltrStatic"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="5" style="margin-bottom: 5px">
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td style="width: 210px;">
                            <asp:Literal runat="server" ID="ltrNewSignup"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal runat="server" ID="ltrSignup"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td>
                            <asp:Literal runat="server" ID="ltrTotalActive"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTotalActiveVal"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td>
                            <%--<%=Resources.Lang.TotalQualifiedMembers %>:--%>
                            <%=Resources.Lang.Tqacsbc%>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTotalQualifiedMembers"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td>
                            <asp:Literal runat="server" ID="ltrTotalDeposit"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTotalDepositVal"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td>
                            <%=Resources.Lang.Turnover %>:
                        </td>
                        <td class="r">
                            <asp:Literal runat="server" ID="ltrTurnoverMonth"></asp:Literal>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrStaticUp"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="5" style="margin-bottom: 5px">
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td style="width: 190px;">
                            <asp:Literal runat="server" ID="ltrTotalPlay"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal ID="ltrTotalPlayer" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td>
                            <asp:Literal runat="server" ID="ltrTurnover"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal ID="ltrTurnoverVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgaccodd" onmouseover="this.className='bghover'" onmouseout="this.className='bgaccodd'">
                        <td>
                            <asp:Literal runat="server" ID="ltrDepositUp"></asp:Literal>:
                        </td>
                        <td class="r">
                            <asp:Literal ID="ltrDepositUpVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr class="bgacceven" onmouseover="this.className='bghover'" onmouseout="this.className='bgacceven'">
                        <td style="width: 190px;">
                            <asp:Literal runat="server" ID="BalanceBroughtText"></asp:Literal>:
                        </td>
                        <td class="r" style="width: 110px;">
                            <asp:Literal ID="BalanceBroughtVal" runat="server"></asp:Literal>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrComm"></asp:Literal>
                </div>
                <div class="box_conts_line">
                </div>
                <br />
                <div class="tbl-box">
                    <%=strTierSetting %>
                </div>
            </div>
            </form>
        </div>
    </div>
    <script type="text/javascript">
        $("slMonth").value = "<%=strMonths%>";
        $("slYear").value = "<%=strYears%>";
    </script>
    <input type="hidden" name="flashpath" id="flashpath" value="<%=flashPath %>" />
    <input type="hidden" name="showpopup" id="showpopup" value="<%=showPopup %>" />
    <input type="hidden" name="custid" id="custid" value="<%=AppSettings.Session.CustID %>" />
    <input type="hidden" name="hdCurrentYear" id="hdCurrentYear" value="<%= DateTime.Now.Year %>" />
    <input type="hidden" name="hdCurrentMonth" id="hdCurrentMonth" value="<%= DateTime.Now.Month %>" />

    <%--<script type="text/javascript">
        var so = new SWFObject(""+$("flashpath").value, "flCommission", "420", "270", "9", "#000000");
        so.addVariable("afflink", "../ProgramDetails.aspx");
        so.write("fl");
    </script>
    --%>
    <script type="text/javascript">
        var $j = jQuery.noConflict();

        var getPopupHeight = function () {
            var bodyEle = document.getElementsByTagName('body')[0];
            var className = bodyEle.className;
            var height = 210;

            switch (className) {
            case "id-ID":
                height = 220;
                break;
            case "zh-CN":
            case "zh-TW":
            case "ja-JP":
                height = 180;
                break;
            default:
                height = 210;
                break;
            }
            return height;
        };

        var Init = function () {
            var username = getCookie("Username");
            var dontShowMessage = getCookie(username + '_DontShowMessage');

            if (dontShowMessage !== 'true') {
                var height = document.body.clientHeight;
                var width = document.body.clientWidth;
                var widthDialog = 450;
                var heightDialog = getPopupHeight();
                var top = (height - heightDialog) / 2;
                var left = (width - widthDialog) / 2 - 180;
                DoCalFull('Announcement.aspx', '', widthDialog, heightDialog, top, left);

                setTimeout(function () {
                    var ibcTable = document.getElementsByClassName('IBCWndTable')[0];
                    var iconTitle = ibcTable.getElementsByClassName('iconpopup')[0];
                    iconTitle.style.display = 'none';
                }, 200);
            }

            // support month picker.

            var currentYear = $('hdCurrentYear').value;

            var monthPickerOptions = {
                pattern: 'mmm-yyyy',
                selectedYear: currentYear,
                startYear: 2007,
                finalYear: currentYear
            };

            var monthObject = $j('#fmonth');
            
            monthObject.monthpicker(monthPickerOptions);

            SetDefaultMonthConstraint(monthObject);

            SetMonthConstraint(monthObject);

            $j('#fmonth-Trigger').click(function () {
                $j('#fmonth').monthpicker('show');
            });	
        };

        function SetMonthConstraint(selector) {
            var currentYear = $('hdCurrentYear').value;

            selector.monthpicker().bind('monthpicker-change-year', function (e, year) {
                selector.monthpicker('disableMonths', []); // (re)enables all
                if (year === currentYear) {
                    selector.monthpicker('disableMonths', GetDisabledMonths());
                }
            });
        }

        function getSelectedYear(selector) {
            var value = selector.val();
            var dateParts = value.split('-');
            if (dateParts.length == 2) {
                return dateParts[1];
            } else {
                return $('hdCurrentYear').value;
            }
        };

        function SetDefaultMonthConstraint(selector) {
            var currentYear = $('hdCurrentYear').value;

            var selectedYear = getSelectedYear(selector);

            if (selectedYear === currentYear) {
                selector.monthpicker('disableMonths', GetDisabledMonths());
            }
        }

        function GetDisabledMonths() {
            var currentMonth = parseInt($('hdCurrentMonth').value, 10);

            var disabledMonths = [];

            for (var i = 0; i <= 12; i++) {
                if (i > currentMonth) {
                    disabledMonths.push(i);
                }
            }

            return disabledMonths;
        }

        window.onload = Init;
    </script>
</asp:Content>
