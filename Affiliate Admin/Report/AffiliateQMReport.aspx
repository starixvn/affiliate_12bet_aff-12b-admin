<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AffiliateQMReport.aspx.cs" Inherits="Report_AffiliateQMReport" %>
<!--js-->
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<script type="text/javascript" src="../App_Js/Common.js"></script>
<link rel='stylesheet' href='http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/themes/base/jquery-ui.css'>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script> 
<script src='https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/jquery-ui.min.js'></script>
<script src="../App_Js/MonthPicker/jquery.mtz.monthpicker.js" type="text/javascript"></script>
<link href="../App_Js/MonthPicker/month-picker.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    .month-picker {
        background: url('../App_Themes/DefaultV2/images/cal.gif') no-repeat right 2px center white; 
        cursor: pointer;
    }
</style>

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

        if ($("hdLanguage").value != "")
            $("slPreferLang").value = $("hdLanguage").value;
        if ($("hdSortBy").value != "") {
            $("slSortBy").value = $("hdSortBy").value;
        }

        SetMonthConstraint(monthObject);
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

    
    function Search() {
        $("page").value = "1";
        $("frmSearch").submit();
        DelayReloadPage1();
    }
    
    function Focus(){
        if($("affname").value == "[All]")
        {
            $("affname").value = "";
        }
        else
        {
            
        }
    }
    
    function Blur()
    {
        if($("affname").value == "")
        {
            $("affname").value = $("all").value;
        }
    }
</script>
<!--/js-->
