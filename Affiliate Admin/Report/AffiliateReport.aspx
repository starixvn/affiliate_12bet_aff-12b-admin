<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AffiliateReport.aspx.cs" Inherits="Report_AffiliateReport" %>
<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<script type="text/javascript" src="../App_Js/RefreshTime.js"></script>
<link rel='stylesheet' href='http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/themes/base/jquery-ui.css'>
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script src='https://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js'></script> 
<script src='https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.18/jquery-ui.min.js'></script>
<script src="../App_Js/MonthPicker/jquery.mtz.monthpicker.js" type="text/javascript"></script>
<link href="../App_Js/MonthPicker/month-picker.css" rel="stylesheet" type="text/css" />
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

        if ($("hdLanguage").value != "")
            $("slPreferLang").value = $("hdLanguage").value;
        if ($("hdSport").value != "")
            $("slSport").value = $("hdSport").value;
    };
    
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
            DelayReloadPage1();
        }
        else {
            
            var validateResult = ValidatePeriod.validate($j("#fdate").val(), $j("#tdate").val());
        
            if (validateResult === "Valid") {
                $("frmSearch").submit();
                DelayReloadPage1();
            } else {
                alert(validateResult);
            }
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
                    2: "Date Period is greater than 6 months",
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