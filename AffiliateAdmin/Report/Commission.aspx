<%@ Page Language="C#" AutoEventWireup="true" Inherits="Report_Commission" Codebehind="Commission.aspx.cs" %>
<!--js-->
<link rel='stylesheet' href='../App_Js/libs/jquery-ui/jquery-ui.css'>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script src='../App_Js/vendors/jquery.min.js'></script> 
<script src='../App_Js/libs/jquery-ui/jquery-ui.min.js'></script>
<script src="../App_Js/MonthPicker/jquery.mtz.monthpicker.js" type="text/javascript"></script>
<link href="../App_Js/MonthPicker/month-picker.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>

<script type="text/javascript" language="javascript">
    var $j = jQuery.noConflict();
    
    window.onload = Init;

    function Init() {
        var currentYear = $('hdCurrentYear').value;

        var monthPickerOptions = {
            selectedYear: currentYear,
            startYear: 2007,
            finalYear: currentYear
        };

        var monthObject = $j('#fmonth');
        monthObject.monthpicker(monthPickerOptions);
        SetDefaultMonthConstraint(monthObject);
        SetMonthConstraint(monthObject);

        if ($("hdLanguage").value != "") {
            $("slPreferLang").value = $("hdLanguage").value;
        }

        if ($("hdSortBy").value != "") {
            $("slSortBy").value = $("hdSortBy").value;
        }

        $("slSport").value = $("hdSport").value;

        $j('#fmonth-Trigger').click(function () {
            $j('#fmonth').monthpicker('show');
        });
    }

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

    function chkOnclick() {
        if ($("chkDesc").className == "ic-uncheck") {
            $("chkDesc").className = "ic-check";
            $("isDesc").value = "ic-check";
        }
        else {
            $("chkDesc").className = "ic-uncheck";
            $("isDesc").value = "ic-uncheck";
        }
    }

    function Search() {
        $("frmSearch").submit();
        Common.delayReloadPage1();
    }

    function Focus() {
        if ($("affname").value == "[All]") {
            $("affname").value = "";
        }
        else {

        }
    }

    function Blur() {
        if ($("affname").value == "") {
            $("affname").value = $("all").value;
        }
    }
</script>
<!--/js-->