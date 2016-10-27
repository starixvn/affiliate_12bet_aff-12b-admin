<%@ Page Language="C#" AutoEventWireup="true" Inherits="Customer_Statistics" Codebehind="Statistics.aspx.cs" %>

<!--js-->
<link rel='stylesheet' href='../App_Js/libs/jquery-ui/jquery-ui.css'>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script src='../App_Js/vendors/jquery.min.js'></script> 
<script src='../App_Js/libs/jquery-ui/jquery-ui.min.js'></script>
<script src="../App_Js/MonthPicker/jquery.mtz.monthpicker.js" type="text/javascript"></script>
<link href="../App_Js/MonthPicker/month-picker.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../App_Js/Common.js"></script>

<script type="text/javascript">
    var $j = jQuery.noConflict();

    window.onload = Init;

    function  Init() {
        var currentYear = $('hdCurrentYear').value;

        var monthObject = $j('#fmonth');

        var monthPickerOptions = {
            pattern: 'mmm-yyyy',
            startYear: 2007,
            finalYear: parseInt(currentYear, 10)
        };

        monthObject.monthpicker(monthPickerOptions);

        SetDefaultMonthConstraint(monthObject);

        SetMonthConstraint(monthObject);

        monthObject.monthpicker().bind("monthpicker-hide", function () {
            Search();
        });

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

    function ChangeDate(spId, selId, isSelDate)
    {
        if(isSelDate)
        {
            $(spId).hide();
            $(selId).show();
            $(selId).focus();
        }
        else
        {
            $(spId).show();
            $(selId).hide();
        }
    }
    function Search()
    {
        DelayReloadPage1();
        $('frmSearch').submit();
    }
    function ShowPage(isShow)
    {
        if(!isShow)
        {
            $("tblContent").hide();
            $("divExpend").show();
            parent.document.getElementById("fRightContent").rows="35,*";
        }
        else
        {
            $("tblContent").show();
            $("divExpend").hide();
            parent.document.getElementById("fRightContent").rows="210,*";
        }
    }
</script>
<!--/js-->