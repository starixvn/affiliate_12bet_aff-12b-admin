<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Performance.aspx.cs" Inherits="Report_Performance" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<link rel='stylesheet' href='http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/themes/base/jquery-ui.css'>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script> 
<script src='https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/jquery-ui.min.js'></script>
<script src="../App_Js/MonthPicker/jquery.mtz.monthpicker.js" type="text/javascript"></script>
<link href="../App_Js/MonthPicker/month-picker.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    #tdate, #fdate {
        background: url('../App_Themes/DefaultV2/images/cal.gif') no-repeat right 2px center white; 
        cursor: pointer;
    }
</style>

<script type="text/javascript">
    var $j = jQuery.noConflict();

    window.onload = function () {

        var currentYear = $('hdCurrentYear').value;

        var monthPickerOptions = {
            selectedYear: currentYear,
            startYear: 2009,
            finalYear: currentYear
        };

        $j('#fdate').monthpicker(monthPickerOptions);

        $j('#tdate').monthpicker(monthPickerOptions);


        var fromMonthObject = $j('#fdate'), toMonthObject = $j('#tdate');
        fromMonthObject.monthpicker(monthPickerOptions);
        SetDefaultMonthConstraint(fromMonthObject);
        SetMonthConstraint(fromMonthObject);

        SetDefaultMonthConstraint(toMonthObject);
        SetMonthConstraint(toMonthObject);


        $("slPreferLang").value = $("hdLanguage").value;

        //set selected
        Select_Value_Set('slPreferLang', getParam('slPreferLang'));
        Select_Value_Set('slSport', getParam('slSport'));
        Select_Value_Set('sortby', getParam('sortby'));
    };
    
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
        DelayReloadPage1();
    }
    function getParam(name) {
        name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
        var regexS = "[\\?&]" + name + "=([^&#]*)";
        var regex = new RegExp(regexS);
        var results = regex.exec(window.location.href);
        if (results == null)
            return "";
        else
            return results[1];
    }

    function Select_Value_Set(SelectName, Value) {
        var obj = document.getElementById(SelectName);
        for (index = 0; index < obj.options.length; index++) {
            if (obj.options[index].value == Value)
                obj.options[index].selected = true;
        }
    }

    function Focus() {
        if ($("affname").value == "[All]") {
            $("affname").value = "";
        }
    }

    function Blur() {
        if ($("affname").value == "") {
            $("affname").value = $("all").value;
        }
    }
    

    // constraint month here.
    
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

</script>
<!--/js-->