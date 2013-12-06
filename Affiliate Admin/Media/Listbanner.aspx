<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listbanner.aspx.cs" Inherits="Listbanner" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/ListBannerOld.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/RefreshTime.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/IBCWnd/IBCWnd.js" language="javascript" type="text/javascript"></script>
 <script type="text/javascript">
     window.onload = Init;

     function Init() {
         InitDivChkBox();
         if (($("totalMedia").value == 0)) {
             $("btnDelete").style.display = "none";
         }
     }

     function LoadData(currPage) {
         try {
             $("page").value = currPage;
             SubmitPaging();
         }
         catch (e) {
             alert('Invalid data!');
         }
     }

     function ShowBanner(me) {
         me.style.overflow = "";
     }
</script>
<!--/js-->