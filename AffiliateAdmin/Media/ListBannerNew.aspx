<%@ Page Language="C#" AutoEventWireup="true" Inherits="ListMediaBanner" Codebehind="ListBannerNew.aspx.cs" %>
<!--js-->
<script src="../App_Js/prototype.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../App_Js/ListBanner.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/Common.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/RefreshTime.js" language="javascript" type="text/javascript"></script>
<script src="../App_Js/IBCWnd/IBCWnd.js" language="javascript" type="text/javascript"></script>
 <script type="text/javascript">
     var jQ = jQuery.noConflict();
     window.onload = Init;

     function Init() {
         parent.frames['fMenu'].ActiveMenu('menu6');
         
         InitDivChkBox();
         if (($("totalMedia").value == 0)) {
             $("btnDelete").style.display = "none";
         }

         jQ('#cateoption').change(function () {
             var category = jQ(this).val();
             var showCategory = (category == "Casino" || category == "Sportsbook");
             if (showCategory) {
                 jQ('#tdSubCategory').show();
                 jQ('#tdOptSubCategory').show();
                 getSubCategory(category);
             }
             else {
                 jQ('#tdSubCategory').hide();
                 jQ('#tdOptSubCategory').hide();
             }
         });
     }

     function getSubCategory(category) {
         try {
             var td = $("tdOptSubCategory");
             while (td.childNodes.length >= 1) {
                 td.removeChild(td.firstChild);
             }

             var loadingSpan = document.createElement("span");
             loadingSpan.innerHTML = '<img width="24px" height="24px" src="../App_Themes/DefaultV2/Images/loading.gif" alt="Loading ....." />';
             td.appendChild(loadingSpan);

             var ajax = new Ajax.Request("../Handler/GetSubCategory.ashx", {
                 method: 'post',
                 parameters: 'category=' + category + '&genAll=true',
                 onComplete: function (data) {
                     var tempDiv = document.createElement("DIV");
                     tempDiv.innerHTML = data.responseText;
                     var select = tempDiv.firstChild;

                     while (td.childNodes.length >= 1) {
                         td.removeChild(td.firstChild);
                     }

                     td.appendChild(select);
                 }
             });

         }
         catch (e) {
             alert(e);
         }
     }

     function LoadData(currPage) {
         try {
             $("page").value = currPage;
             SubmitPagingNew();
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