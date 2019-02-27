<%@ Page Language="C#" AutoEventWireup="true" Inherits="Report_AffiliateReport" Codebehind="AffiliateReport.aspx.cs" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<link rel='stylesheet' href='../App_Js/libs/jquery-ui/jquery-ui.css'>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script src='../App_Js/vendors/jquery.min.js'></script> 
<script src='../App_Js/libs/jquery-ui/jquery-ui.min.js'></script>
<script src="../App_Js/MonthPicker/jquery.mtz.monthpicker.js" type="text/javascript"></script>
<link href="../App_Js/MonthPicker/month-picker.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">

    var $j = jQuery.noConflict();

    window.onload = function () {
        var currentYear = $('hdCurrentYear').value;

        var monthPickerOptions = {
            selectedYear: currentYear,
            startYear: 2007,
            finalYear: currentYear
        };

        $j('#fdate').monthpicker(monthPickerOptions);

        $j('#tdate').monthpicker(monthPickerOptions);

        SetDefaultMonthConstraint($j('#fdate'));
        SetDefaultMonthConstraint($j('#tdate'));

        if ($("hdLanguage").value != "")
            $("slPreferLang").value = $("hdLanguage").value;
        if ($("hdSport").value != "")
            $("slSport").value = $("hdSport").value;

        EnableMonthCalendar($("chkDate").className == "ic-uncheck");

        SetMonthConstraint($j('#fdate'));
        SetMonthConstraint($j('#tdate'));

        $j('#fdate-Trigger').click(function () {
            $j('#fdate').monthpicker('show');
        });

        $j('#tdate-Trigger').click(function () {
            $j('#tdate').monthpicker('show');
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
        var dateParts = value.split('/');
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

    function EnableMonthCalendar(isEnable) {
        if (isEnable) {
            $j('#fdate').removeAttr("disabled");
            $j('#fdate').removeAttr("disabled");
        } else {
            $j('#fdate').attr("disabled", "disabled");
            $j('#tdate').attr("disabled", "disabled");
        }
    }
    
    function chkOnclick() {
        if($("chkDate").className == "ic-uncheck")
        {
            $("chkDate").className = "ic-check";
            $("isGetAll").value = "ic-check";
            $j('#fdate').attr('disabled', 'disabled');
            $j('#tdate').attr('disabled', 'disabled');
        }
        else {
            $("chkDate").className = "ic-uncheck";
            $("isGetAll").value = "ic-uncheck";
            $j('#fdate').removeAttr('disabled');
            $j('#tdate').removeAttr('disabled');
        }
    }
    
    function Search() {

        if($("isGetAll").value === "ic-check")
        {
            $("frmSearch").submit();
            Common.delayReloadPage1();
        }
        else {
            $("frmSearch").submit();
            Common.delayReloadPage1();
        }
    }

    var ValidatePeriod = (function ($) {
        var convertDate = function (monthDate) {
            var parts = monthDate.split('/');
            if (parts.length == 2) {
                return new Date(parts[0] + '/01/' + parts[1]);
            }
            return new Date();
        };
        return {
            validate: function (fromDate, toDate) {
                fromDate = convertDate(fromDate);
                toDate = convertDate(toDate);

                var errors = {
                    0: "Valid",
                    1: "From Date is greater than To Date",
                    2: "Sorry! We just allow to view the report within 6 months.",
                    3: "Date Period is invalid."
                };

                if (fromDate.getTime() > toDate.getTime()) {
                    return errors[1];
                }

                var fMonth, fYear, tMonth, tYear;
                fMonth = fromDate.getMonth();
                fYear = fromDate.getYear();
                tMonth = toDate.getMonth();
                tYear = toDate.getYear();

                var tempMonth = tMonth;

                if (fYear < tYear) {
                    tempMonth = tempMonth + 12;
                }

                if (tempMonth - fMonth <= 6) {
                    return errors[0];
                } else {
                    return errors[2];
                }
            }
        };
    })($j);

</script>
<!--/js-->
