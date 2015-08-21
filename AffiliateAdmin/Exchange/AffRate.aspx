<%@ Page Language="C#" AutoEventWireup="true" Inherits="AffRate" Codebehind="AffRate.aspx.cs" %>

<!--js-->
<link rel="stylesheet" type="text/css" href="../App_Js/Calendar2/Skin/calendar-blue.css" media="all" title="win2k-cold-1" />
<script src="../App_Js/prototype.js" type="text/javascript"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>
<script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
<script src="../App_Js/Calendar2/calendar-normal.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/date.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/Lang/calendar-e.js" type="text/javascript"></script>
<script src="../App_Js/Calendar2/calendar-setup.js" type="text/javascript"></script>

<script type="text/javascript">
    window.onload = function() {
        /*Assign handler for link*/
        var now = new Date();
        var listDiv = $$("div.popup");
        
        if ($("hdIsUpdate").value == "1") {
            for (var i = 0; i < listDiv.length; i++) {
                listDiv[i].className = "link popup";
                listDiv[i].observe("click", UpdateRate);                
            }
        }
    }
    function UpdateRate(event) {
        var link = Event.element(event);
        
        var popH = 200, popW = 290;
        var arr = link.id.split("_");
                
        var id = arr[0];
        var curr = arr[1];
        var rate = arr[2];
        var col = link.getAttribute("data-col");// "buyingrate";

        var url = 'AffEditRate.aspx?exid=' + id + '&curr=' + curr + '&rate=' + rate + (col ? '&column='+col : "");
        
	    ibcWnd.open(url, (col ? 'Buying Rate' : 'Ex.Rate'), 150, 50, popW, popH, 'class1', 'class2');
	}

</script>
<!--/js-->
