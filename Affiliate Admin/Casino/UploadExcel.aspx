<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadExcel.aspx.cs" Inherits="Casino_UploadExcel" %>

<!--js-->
<script type="text/javascript" src="../App_Js/Common.js"></script>
<link rel="stylesheet" type="text/css" href="../App_Js/Calendar2/Skin/calendar-blue.css" media="all" title="win2k-cold-1" />
<script type="text/javascript" src="../App_Js/prototype.js"></script>
<script src="../App_Js/Calendar2/calendar-normal.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/date.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/Lang/calendar-e.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/calendar-setup.js" type="text/javascript"></script>

<script type="text/javascript">
    var arrItem = new Array();
    var arrError = new Array();
    function IsValidFileExcel(objTag) {
        var filename = objTag.value;
        var isOK = true;
        if (filename != "") {
            var ext = filename.substring(filename.lastIndexOf('.') + 1, filename.length).toLowerCase();
            isOK = (ext == "xlsx");
            if (!isOK) {
                alert($('hdErrFileExtension').value);
            }
        }
        if ($('btnuploadbanner')) {
            $('btnuploadbanner').disabled = !isOK;
        }
        return isOK;
    }
    function ExcelUpload() {
        arrItem.clear();
        arrError.clear();
        //if ($("hdHasData").value == "1") {//confirm replace data or not
        if (!confirm("New data will be replaced existing/old data"))
            return;
        //}
        if ($("txtfile").value == "") {
            alert("Please select an excel file to upload.");
            return;
        }
        else {
            $("divData").innerHTML = "";
            $("UploadExcel").submit();
        }
    }
    function UpdateErr(msg) {
        ErrAlter(msg);
    }
    function UpdateSucc(tbl) {
        $("hdHasData").value = "1";//Change status to has data
        SuccAlter("Import data successfully!")
        $("divData").innerHTML = tbl;
    }
    function searchCasino() {
        location.href = "UploadExcel.aspx?date=" + $("fdate").value;
    }
    function confirmUpdate() {
        $("hdHasData").value = "1";
        ExcelUpload();
    }
</script>
<!--/js-->