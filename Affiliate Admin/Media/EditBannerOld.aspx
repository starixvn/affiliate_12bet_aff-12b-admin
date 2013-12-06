<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditBannerOld.aspx.cs" Inherits="AddBanner" %>
<!--js-->
<script src="../App_Js/prototype.js" type="text/javascript"></script>
<script src="../App_Js/jquery/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../App_Js/ListBanner.js" type="text/javascript"></script>
<script src="../App_Js/Common.js" type="text/javascript"></script>

<script type="text/javascript">
    var jQ = jQuery.noConflict();

    function getSubCategory(category, selectedSubCategory) {
        try {
            var td = $("trSubCategoryMedia").childNodes[1];
            while (td.childNodes.length >= 1) {
                td.removeChild(td.firstChild);
            }
            var loadingSpan = document.createElement("span");
            loadingSpan.innerHTML = '<img width="24px" height="24px" src="../App_Themes/DefaultV2/Images/loading.gif" alt="Loading ....." />';
            td.appendChild(loadingSpan);

            var ajax = new Ajax.Request("../Handler/GetSubCategory.ashx", {
                method: 'post',
                parameters: 'category=' + category + '&selectedSubCategory=' + selectedSubCategory +'&genAll=false',
                onComplete: function (data) {
                    var tempDiv = document.createElement("DIV");
                    tempDiv.innerHTML = data.responseText;
                    var select = tempDiv.firstChild;

                    //var td = $("trSubCategoryMedia").childNodes[1];
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

    jQ(function () {


        jQ('#txtfile').change(function () {
            var fullFileName = jQ(this).val();
            var temp = fullFileName.split("\\");
            var fileName = temp[temp.length - 1];

            jQ("#inpFileName").val(fileName);
        });

        if (jQ('#hdOldFileName').val().indexOf('CategoryMedia') < 0) {
            jQ('#trFileName').hide();
            //jQ('#trSubCategoryMedia').hide();
        }

        var category = jQ('#slCategory').val();
        var showCategory = (category == "Casino" || category == "Sportsbook");
        if (showCategory) {
            var selectedSubCategory = jQ('#hidSubCategory').val();
            if (selectedSubCategory != 'None')
                jQ('#hidSubCategory').val('None');

            jQ('#trSubCategoryMedia').show();
            getSubCategory(category, selectedSubCategory);
        }
        else
            jQ('#trSubCategoryMedia').hide();

        jQ('#slCategory').change(function () {
            var category = jQ(this).val();
            var showCategory = (category == "Casino" || category == "Sportsbook");
            if (showCategory) {
                jQ('#trSubCategoryMedia').show();
                getSubCategory(category, 'None');
            }
            else {
                jQ('#trSubCategoryMedia').hide();
            }
        });

    });

</script>
<!--/js-->