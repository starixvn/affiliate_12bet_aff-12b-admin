<%@ Page Language="C#" MasterPageFile="~/Master2.master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs"
    Inherits="SignUp" %>
<%@ OutputCache Duration="600" VaryByParam="None" VaryByHeader="*" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" src="App_Js/IBCWnd/IBCWnd.js"></script>
    <script type="text/javascript" src="App_Js/wz_tooltip.js"></script>
    <style type="text/css">
        .payment
        {
            width: 180px;
        }
        .ex
        {
            color: #505050;
            display: none;
            font-size: 85%;
            margin-top: 0.2em;
        }
    </style>

    <script type="text/javascript"> 
    
    window.onload = function() {
        //MoreUrl();
        AgreeTerm();
        $("txtFName").blur();
        $("txtFName").focus(); 
        clock();
        setInterval("clock();", 1000);
        showRequire();
    }
    function ResetClick()
    {
        arrItem.clear();
        arrError.clear();
        $('errMsg').hide();
        document.getElementsByName("radio")[0].checked = true;
        $('CheckTerm').checked = false;
        AgreeTerm();
    }
    function showRequire()
    {
        var spans = $('spURL', 'spNoURL');
        var typeValue = $$('input:checked[type="radio"][name="rdWebsite"]').pluck('value');
        for(i =0; i < 2; i++)//hide all require icon
        {
            spans[i].hide();
        }
        spans[typeValue].show();
        var tbl = $("siteUrl");
        if(typeValue == 0)
        {//disable no website
            $("txtSiteUrl").enable();
            $("addURL").enable();
            $("txtNoWebsite").disable();

            for(var i = 2; i < tbl.tBodies[0].rows.length; i++)
            {          
                tbl.tBodies[0].rows[i].show();
            }
        }
        else
        {
            $("txtSiteUrl").disable();
            $("addURL").disable();
            $("txtNoWebsite").enable();

            for(var i = 2; i < tbl.tBodies[0].rows.length; i++)
            {          
                tbl.tBodies[0].rows[i].hide();
            }
        }
    }

    function AgreeTerm()
    {
        if($('CheckTerm').checked)
        {
            $('btnSubmit').disabled = false;   
            $('btnSubmit').className = "button";
        }
        else
        {
            $('btnSubmit').disabled = true;
            $('btnSubmit').className = "disabled";
        }
    }
    function submitForm()
    {
        arrItem.clear();
        arrError.clear();
        $('errMsg').hide();

        $("hdMoreurl").value = "";
        for(var i = 2; i < 100; i++)
        {
            if(!$("txtSiteUrl" + i))
            {
                var length = $("hdMoreurl").value.length;
                if(length > 0)
                    $("hdMoreurl").value = $("hdMoreurl").value.substr(0, length - 1);
                break;
            }
            $("hdMoreurl").value = $("hdMoreurl").value + $("txtSiteUrl" + i).value + ";";
        }

        if(!$('CheckTerm').checked)
        {
            AddError("CheckTerm", ErrMsg.Term);
            DisplayMsg(false);
            return false;
        }
        if(!CheckSuccess())
            return false;
        return true;
    }
    function CheckSuccess()
    {
        if(isEmpty($('txtFName').value))
            AddError("txtFName", ErrMsg.FName);
        if(isEmpty($('txtLName').value))
            AddError("txtLName", ErrMsg.LName);
        if (!document.getElementsByName("rdGender")[0].checked) {
            if(!document.getElementsByName("rdGender")[1].checked){
                AddError("rdGender", ErrMsg.ErrGender);
            }
        }
        if(isEmpty($('txtEmail').value))
            AddError("txtEmail", ErrMsg.Email);
        else
        {
            if(!checkMail($('txtEmail').value))
                AddError("txtEmail", ErrMsg.EmailValid);
            else
            {
                if ($('txtEmail').value.trim() != $('txtConfirmEmail').value.trim())
                    AddError("txtConfirmEmail", ErrMsg.EmailMatch);
            }
        }
        if (isEmpty($('txtNumber').value))
            AddError("txtNumber", ErrMsg.ContactNumber);
        if (isEmpty($('txtMobile').value))
            AddError("txtMobile", ErrMsg.MobileNumber);            
        if(isEmpty($('txtAddress').value))
            AddError("txtAddress", ErrMsg.Address);
        <%if (AppSettings.Session.Language == "zh-CN" || AppSettings.Session.Language == "zh-TW"){%>
        if(isEmpty($('txtState').value))
            AddError("txtState", ErrMsg.State);
        <%} %>
        if(isEmpty($('txtCity').value))
            AddError("txtCity", ErrMsg.City);
        if(isEmpty($('txtPost').value))
            AddError("txtPost", ErrMsg.PostCode);
        if(isEmpty($('selCountry').value))
            AddError("selCountry", ErrMsg.Country);
        
        var typeValue = $$('input:checked[type="radio"][name="rdWebsite"]').pluck('value');

        if(document.getElementsByName("rdWebsite")[0].checked && isEmpty($("txtSiteUrl").value))
            AddError("txtSiteUrl", ErrMsg.SiteUrl);
        else if(document.getElementsByName("rdWebsite")[1].checked && isEmpty($("txtNoWebsite").value))
            AddError("txtNoWebsite", "<%=Resources.Lang.DescWebsite%>");
        else if(document.getElementsByName("rdWebsite")[0].checked){            
            var listSites = $$(".sites");
            for(var i = 1; i < listSites.length; i++)
            {
                if(listSites[i])
                {
                    if(isEmpty(listSites[i].value))
                    {
                        AddError(listSites[i].id, "<%=Resources.Lang.WebsiteURL %> " + (i==0 ? "" : (i + 1).toString()) + " <%=Resources.Lang.IsRequired%>");
                        break;
                    }  
                }
            }   
        }        
        else if (isEmpty($("txtSiteUrl").value) && isEmpty($("txtNoWebsite").value))
                AddError("txtSiteUrl", ErrMsg.SiteUrl);

        if(isEmpty($('slPreferLang').value))
            AddError("slPreferLang", ErrMsg.PreferLang);
        if (isEmpty($('slSecurityQuestion').value))
            AddError("slSecurityQuestion", ErrMsg.SecurityQuestion);
        if (isEmpty($('txtAnswer').value))
            AddError("txtAnswer", ErrMsg.YourAnswer);
        if (isEmpty($('txtValidateRegister').value) || $('txtValidateRegister').value == gMsg.entryvalidation)
            AddError("txtValidateRegister", gMsg.Errmsgentryvalidcode);
        if (isEmpty($('txtBet12UserName').value))
            AddError("txtBet12UserName", ErrMsg.UserName12Bet);

        if(arrItem.length > 0)
        {
            $('errMsg').show();
            ShowError(arrError, arrItem, "error_item");
            return false;
        } 
        return true;
    }

    function AddOneMoreURL()
    {
        var listSites = $$(".sites");
        var siteIndex = listSites.length;        
        if(listSites[siteIndex - 1])
            if(isEmpty(listSites[siteIndex - 1].value))
            {
                alert("<%=Resources.Lang.WebsiteUrli %>".replace("{i}", siteIndex.toString()));
                listSites[siteIndex - 1].focus();
                return;
            }
        siteIndex = siteIndex +1;
        var table = $("siteUrl");
        var tr = table.insertRow(table.rows.length);
        var td1 = IBCWnd.createElement("td", tr);
        var td2 = IBCWnd.createElement("td", tr);
        var td3 = IBCWnd.createElement("td", tr);
        tr.setAttribute("id", "tr" + siteIndex);
        td2.className = "cont12oe";
        td2.innerHTML = "*";
        td1.innerHTML = "<%=Resources.Lang.WebsiteURL %> " + siteIndex.toString();
        td3.innerHTML = "<input type='text' size='30' class='sites' id='txtSiteUrl" + siteIndex + "' name='txtSiteUrl" + siteIndex + "' />&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"button\" onclick=\"DelUrl(" + siteIndex + ");\" class=\"button\" value=\"<%=Resources.Lang.Del %>\" />";
        if($("txtUrl" + siteIndex))
            $("txtUrl" + siteIndex).focus();
    }
    function DelUrl(rowNum)
    {        
        $("siteUrl").deleteRow(rowNum);
        reorderRows(rowNum);
    }
    function reorderRows(starting)
    {
        var tbl = $("siteUrl");
        if (tbl.tBodies[0].rows[starting]) {
            for(var i = starting; i < tbl.tBodies[0].rows.length; i++)
            {          
                var tr = tbl.tBodies[0].rows[i];
                var tds = tr.getElementsByTagName("td");
                
                var index = i;
                tds[0].innerHTML = "<%=Resources.Lang.WebsiteURL %> " + index.toString();
                tds[2].innerHTML = "<input type='text' size='30' value='" + tds[2].childNodes[0].value + "' class='sites' id='txtSiteUrl" + index + "' name='txtSiteUrl" + index + "' />&nbsp;&nbsp;&nbsp;&nbsp;<input type=\"button\" onclick=\"DelUrl(" + index + ");\" class=\"button\" value=\"<%=Resources.Lang.Del %>\" />";
            }
        }
    }
    </script>
    <form id="Register_form" name="Register_form" action="Handler/SignUp.ashx" method="post"
                    onsubmit="return submitForm();" target="frRegister">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="160" class="in_top"><asp:Literal ID="ltrRegisForm" runat="server"></asp:Literal></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="in_conts">
                <asp:Literal runat="server" ID="ltrCompleteForm"></asp:Literal>
                <br />
                <asp:Literal ID="ltrRequireInfo" runat="server"></asp:Literal>
                <br />
                <asp:Literal ID="ltrExtraTextForKorean" runat="server"></asp:Literal>
                <br />
                <br />
                <input id="CliectAct" name="ClientAct" type="hidden" />
                <div id="errMsg" style="display: none;" class="msg-wid">
                    <div id="divErr">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 40px;" valign="top">
                                    <div class="warning">
                                        &nbsp;</div>
                                </td>
                                <td valign="top">
                                    <div id="error_item">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrContactInfo"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrFName" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtFName" class="inputbox" id="txtFName" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrLName" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtLName" class="inputbox" id="txtLName" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrGender" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input id="rdGender" class="noborder" type="radio" value="1" name="rdGender" />
                            <%=Resources.Lang.Male %>
                            <input id="rdGender" class="noborder" type="radio" value="0" name="rdGender" />
                            <%=Resources.Lang.Female %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtEmail" class="inputbox" id="txtEmail" size="30" type="text" onfocus="Tip($('hdEmailAddress').value, BGCOLOR, '#eeeeee', FONTCOLOR, '#da3614', SHADOW, true, SHADOWCOLOR, '#530208', DELAY, 10,TAGHTML,this);" onblur="tt_Blur();" onmouseover="Tip($('hdEmailAddress').value, BGCOLOR, '#eeeeee', FONTCOLOR, '#da3614', SHADOW, true, SHADOWCOLOR, '#530208', DELAY, 10);" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px; white-space: normal;">
                            <asp:Literal ID="ltrReEmail" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtConfirmEmail" id="txtConfirmEmail" class="inputbox"
                                size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrContactNumber" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtNumber" id="txtNumber" class="inputbox" size="30" type="text" onfocus="Tip($('hdContactNumber').value, BGCOLOR, '#eeeeee', FONTCOLOR, '#da3614', SHADOW, true, SHADOWCOLOR, '#530208', DELAY, 10,TAGHTML,this);" onblur="tt_Blur();" onmouseover="Tip($('hdContactNumber').value, BGCOLOR, '#eeeeee', FONTCOLOR, '#da3614', SHADOW, true, SHADOWCOLOR, '#530208', DELAY, 10);" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtMobile" id="txtMobile" class="inputbox" size="30" type="text" />
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal ID="ltrMailingInfor" runat="server"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtAddress" class="inputbox" id="txtAddress" size="30"
                                type="text" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrCity" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtCity" class="inputbox" id="txtCity" size="30" type="text" />
                        </td>
                    </tr>
                    <%if (AppSettings.Session.Language == "zh-CN" || AppSettings.Session.Language == "zh-TW")
                      {%>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="ltrState"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtState" id="txtState" class="inputbox" size="30" type="text" />
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <td>
                            <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtPost" id="txtPost" class="inputbox" size="30" type="text" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Literal ID="ltrCountry" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <select name="selCountry" id="selCountry">
                                <option value="">
                                    <%=Resources.Lang.SelectOne %></option>
                                <option value="Albania">
                                    <%=Resources.Lang.Albania%>
                                </option>
                                <option value="Algeria">
                                    <%=Resources.Lang.Algeria%></option>
                                <option value="Angola">
                                    <%=Resources.Lang.Angola%>
                                </option>
                                <option value="Anguilla">
                                    <%=Resources.Lang.Anguilla%></option>
                                <option value="Argentina">
                                    <%=Resources.Lang.Argentina%></option>
                                <option value="Armenia">
                                    <%=Resources.Lang.Armenia%></option>
                                <option value="Aruba">
                                    <%=Resources.Lang.Aruba%></option>
                                <option value="Australia">
                                    <%=Resources.Lang.Australia%></option>
                                <option value="Austria">
                                    <%=Resources.Lang.Austria%></option>
                                <option value="Azerbaijan">
                                    <%=Resources.Lang.Azerbaijan%></option>
                                <option value="Bahamas">
                                    <%=Resources.Lang.Bahamas%></option>
                                <option value="Bahrain">
                                    <%=Resources.Lang.Bahrain%></option>
                                <option value="Bangladesh">
                                    <%=Resources.Lang.Bangladesh%></option>
                                <option value="Barbados">
                                    <%=Resources.Lang.Barbados%></option>
                                <option value="Belarus">
                                    <%=Resources.Lang.Belarus%></option>
                                <option value="Belgium">
                                    <%=Resources.Lang.Belgium%></option>
                                <option value="Belize">
                                    <%=Resources.Lang.Belize%></option>
                                <option value="Benin">
                                    <%=Resources.Lang.Benin%></option>
                                <option value="Bermuda">
                                    <%=Resources.Lang.Bermuda%></option>
                                <option value="Bhutan">
                                    <%=Resources.Lang.Bhutan%></option>
                                <option value="Bolivia">
                                    <%=Resources.Lang.Bolivia%></option>
                                <option value="Bosnia and Herzegovina">
                                    <%=Resources.Lang.BosniaandHerzegovina%></option>
                                <option value="Botswana">
                                    <%=Resources.Lang.Botswana%></option>
                                <option value="Brazil">
                                    <%=Resources.Lang.Brazil%></option>
                                <option value="Brunei">
                                    <%=Resources.Lang.BruneiDarussalam%></option>
                                <option value="Bulgaria">
                                    <%=Resources.Lang.Bulgaria%></option>
                                <option value="Burkina">
                                    <%=Resources.Lang.BurkinaFaso%></option>
                                <option value="Burundi">
                                    <%=Resources.Lang.Burundi%></option>
                                <option value="Cambodia">
                                    <%=Resources.Lang.Cambodia%></option>
                                <option value="Cameroon">
                                    <%=Resources.Lang.Cameroon%></option>
                                <option value="Canada">
                                    <%=Resources.Lang.Canada%></option>
                                <option value="Cape Verde">
                                    <%=Resources.Lang.CapeVerde%></option>
                                <option value="Cayman Islands">
                                    <%=Resources.Lang.CaymanIslands%></option>
                                <option value="Central African Rep">
                                    <%=Resources.Lang.CentralAfricanRep%></option>
                                <option value="Chad">
                                    <%=Resources.Lang.Chad%></option>
                                <option value="Chile">
                                    <%=Resources.Lang.Chile%></option>
                                <option value="China (People's Rep)">
                                    <%=Resources.Lang.ChinaPeoplesRep%></option>
                                <option value="Colombia">
                                    <%=Resources.Lang.Colombia%></option>
                                <option value="Congo(Rep)">
                                    <%=Resources.Lang.CongoRep%></option>
                                <option value="Cook Islands">
                                    <%=Resources.Lang.CookIslands%></option>
                                <option value="Costa Rica">
                                    <%=Resources.Lang.CostaRica%></option>
                                <option value="Cote d'Ivoire (Rep)">
                                    <%=Resources.Lang.CotedIvoireRep%></option>
                                <option value="Croatia">
                                    <%=Resources.Lang.Croatia%></option>
                                <option value="Cyprus">
                                    <%=Resources.Lang.Cyprus%></option>
                                <option value="Czech (Rep)">
                                    <%=Resources.Lang.CzechRep%></option>
                                <option value="Dem Rep of Congo">
                                    <%=Resources.Lang.DemRepofCongo%></option>
                                <option value="Denmark">
                                    <%=Resources.Lang.Denmark%></option>
                                <option value="Dominica (Commonwealth of)">
                                    <%=Resources.Lang.DominicaCommonwealthof%></option>
                                <option value="Dominican Rep.">
                                    <%=Resources.Lang.DominicanRep%></option>
                                <option value="Ecuador">
                                    <%=Resources.Lang.Ecuador%></option>
                                <option value="Egypt">
                                    <%=Resources.Lang.Egypt%></option>
                                <option value="El Salvador">
                                    <%=Resources.Lang.ElSalvador%></option>
                                <option value="Eritrea">
                                    <%=Resources.Lang.Eritrea%></option>
                                <option value="Estonia">
                                    <%=Resources.Lang.Estonia%></option>
                                <option value="Ethiopia">
                                    <%=Resources.Lang.Ethiopia%></option>
                                <option value="Fiji">
                                    <%=Resources.Lang.Fiji%></option>
                                <option value="Finland">
                                    <%=Resources.Lang.Finland%></option>
                                <option value="France">
                                    <%=Resources.Lang.France%></option>
                                <option value="French Polynesia">
                                    <%=Resources.Lang.FrenchPolynesia%></option>
                                <option value="Gabon">
                                    <%=Resources.Lang.Gabon%></option>
                                <option value="Georgia">
                                    <%=Resources.Lang.Georgia%></option>
                                <option value="Germany">
                                    <%=Resources.Lang.Germany%></option>
                                <option value="Ghana">
                                    <%=Resources.Lang.Ghana%></option>
                                <option value="Gibraltar">
                                    <%=Resources.Lang.Gibraltar%></option>
                                <option value="Greece">
                                    <%=Resources.Lang.Greece%></option>
                                <option value="Grenada">
                                    <%=Resources.Lang.Grenada%></option>
                                <option value="Guatemala">
                                    <%=Resources.Lang.Guatemala%></option>
                                <option value="Guinea">
                                    <%=Resources.Lang.Guinea%></option>
                                <option value="Guyana">
                                    <%=Resources.Lang.Guyana%></option>
                                <option value="Haiti">
                                    <%=Resources.Lang.Haiti%></option>
                                <option value="Honduras">
                                    <%=Resources.Lang.Honduras%></option>
                                <option value="Hong Kong">
                                    <%=Resources.Lang.HongKong%></option>
                                <option value="Hungary">
                                    <%=Resources.Lang.Hungary%></option>
                                <option value="Iceland">
                                    <%=Resources.Lang.Iceland%></option>
                                <option value="India">
                                    <%=Resources.Lang.India%></option>
                                <option value="Indonesia">
                                    <%=Resources.Lang.Indonesia%></option>
                                <option value="Iran">
                                    <%=Resources.Lang.Iran%></option>
                                <option value="Iraq">
                                    <%=Resources.Lang.Iraq%></option>
                                <option value="Ireland">
                                    <%=Resources.Lang.Ireland%></option>
                                <option value="Israel">
                                    <%=Resources.Lang.Israel%></option>
                                <option value="Italy">
                                    <%=Resources.Lang.Italy%></option>
                                <option value="Jamaica">
                                    <%=Resources.Lang.Jamaica%></option>
                                <option value="Japan">
                                    <%=Resources.Lang.Japan%></option>
                                <option value="Jordan">
                                    <%=Resources.Lang.Jordan%></option>
                                <option value="Kenya">
                                    <%=Resources.Lang.Kenya%></option>
                                <option value="Korea">
                                    <%=Resources.Lang.KoreaRep%></option>
                                <option value="Kuwait">
                                    <%=Resources.Lang.Kuwait%></option>
                                <option value="Lao People's Dem Rep">
                                    <%=Resources.Lang.LaoPeoplesDemRep%></option>
                                <option value="Latvia">
                                    <%=Resources.Lang.Latvia%></option>
                                <option value="Lesotho">
                                    <%=Resources.Lang.Lesotho%></option>
                                <option value="Liechtenstein">
                                    <%=Resources.Lang.Liechtenstein%></option>
                                <option value="Lithuania">
                                    <%=Resources.Lang.Lithuania%></option>
                                <option value="Luxembourg">
                                    <%=Resources.Lang.Luxembourg%></option>
                                <option value="Macao">
                                    <%=Resources.Lang.Macao%></option>
                                <option value="Macedonia">
                                    <%=Resources.Lang.Macedonia%></option>
                                <option value="Madagascar">
                                    <%=Resources.Lang.Madagascar%></option>
                                <option value="Malawi">
                                    <%=Resources.Lang.Malawi%></option>
                                <option value="Malaysia">
                                    <%=Resources.Lang.Malaysia%></option>
                                <option value="Maldives">
                                    <%=Resources.Lang.Maldives%></option>
                                <option value="Mali">
                                    <%=Resources.Lang.Mali%></option>
                                <option value="Malta">
                                    <%=Resources.Lang.Malta%></option>
                                <option value="Mauritania">
                                    <%=Resources.Lang.Mauritania%></option>
                                <option value="Mauritius">
                                    <%=Resources.Lang.Mauritius%></option>
                                <option value="Mexico">
                                    <%=Resources.Lang.Mexico%></option>
                                <option value="Moldova">
                                    <%=Resources.Lang.Moldova%></option>
                                <option value="Mongolia">
                                    <%=Resources.Lang.Mongolia%></option>
                                <option value="Morocco">
                                    <%=Resources.Lang.Morocco%></option>
                                <option value="Myanmar">
                                    <%=Resources.Lang.Myanmar%></option>
                                <option value="Namibia">
                                    <%=Resources.Lang.Namibia%></option>
                                <option value="Nauru">
                                    <%=Resources.Lang.Nauru%></option>
                                <option value="Nepal">
                                    <%=Resources.Lang.Nepal%></option>
                                <option value="Netherlands">
                                    <%=Resources.Lang.Netherlands%></option>
                                <option value="New Caledonia">
                                    <%=Resources.Lang.NewCaledonia%></option>
                                <option value="New Zealand">
                                    <%=Resources.Lang.NewZealand%></option>
                                <option value="Niger">
                                    <%=Resources.Lang.Niger%></option>
                                <option value="Nigeria">
                                    <%=Resources.Lang.Nigeria%></option>
                                <option value="Norway">
                                    <%=Resources.Lang.Norway%></option>
                                <option value="Oman">
                                    <%=Resources.Lang.Oman%></option>
                                <option value="Pakistan">
                                    <%=Resources.Lang.Pakistan%></option>
                                <option value="Panama">
                                    <%=Resources.Lang.Panama%></option>
                                <option value="Papua New Guinea">
                                    <%=Resources.Lang.PapuaNewGuinea%></option>
                                <option value="Paraguay">
                                    <%=Resources.Lang.Paraguay%></option>
                                <option value="Peru">
                                    <%=Resources.Lang.Peru%></option>
                                <option value="Philippines">
                                    <%=Resources.Lang.Philippines%></option>
                                <option value="Poland">
                                    <%=Resources.Lang.Poland%></option>
                                <option value="Portugal">
                                    <%=Resources.Lang.Portugal%></option>
                                <option value="Qatar">
                                    <%=Resources.Lang.Qatar%></option>
                                <option value="Romania">
                                    <%=Resources.Lang.Romania%></option>
                                <option value="Russian Federation">
                                    <%=Resources.Lang.RussianFederation%></option>
                                <option value="Rwanda">
                                    <%=Resources.Lang.Rwanda%></option>
                                <option value="Saint Christopher (St. Kitts) and Nevis">
                                    <%=Resources.Lang.SaintChristopherStKittsandNevis%></option>
                                <option value="Saint Lucia">
                                    <%=Resources.Lang.SaintLucia%></option>
                                <option value="Saint Vincent and the Grenadines">
                                    <%=Resources.Lang.SaintVincentandtheGrenadines%></option>
                                <option value="Sao Tome and Principe">
                                    <%=Resources.Lang.SaoTomeandPrincipe%></option>
                                <option value="Saudi Arabia">
                                    <%=Resources.Lang.SaudiArabia%></option>
                                <option value="Senegal">
                                    <%=Resources.Lang.Senegal%></option>
                                <option value="Seychelles">
                                    <%=Resources.Lang.Seychelles%></option>
                                <option value="Sierra Leone">
                                    <%=Resources.Lang.SierraLeone%></option>
                                <option value="Singapore">
                                    <%=Resources.Lang.Singapore%></option>
                                <option value="Slovakia">
                                    <%=Resources.Lang.Slovakia%></option>
                                <option value="Slovenia">
                                    <%=Resources.Lang.Slovenia%></option>
                                <option value="Solomon Islands">
                                    <%=Resources.Lang.SolomonIslands%></option>
                                <option value="Somalia">
                                    <%=Resources.Lang.Somalia%></option>
                                <option value="South Africa">
                                    <%=Resources.Lang.SouthAfrica%></option>
                                <option value="Spain">
                                    <%=Resources.Lang.Spain%></option>
                                <option value="Sri Lanka">
                                    <%=Resources.Lang.SriLanka%></option>
                                <option value="Sudan">
                                    <%=Resources.Lang.Sudan%></option>
                                <option value="Suriname">
                                    <%=Resources.Lang.Suriname%></option>
                                <option value="Swaziland">
                                    <%=Resources.Lang.Swaziland%></option>
                                <option value="Sweden">
                                    <%=Resources.Lang.Sweden%></option>
                                <option value="Switzerland">
                                    <%=Resources.Lang.Switzerland%></option>
                                <option value="Taiwan">
                                    <%=Resources.Lang.Taiwan%></option>
                                <option value="Tanzania">
                                    <%=Resources.Lang.Tanzania%></option>
                                <option value="Thailand">
                                    <%=Resources.Lang.Thailand%></option>
                                <option value="Togo">
                                    <%=Resources.Lang.Togo%></option>
                                <option value="Trinidad and Tobago">
                                    <%=Resources.Lang.TrinidadandTobago%></option>
                                <option value="Tunisia">
                                    <%=Resources.Lang.Tunisia%></option>
                                <option value="Turkey">
                                    <%=Resources.Lang.Turkey%></option>
                                <option value="Uganda">
                                    <%=Resources.Lang.Uganda%></option>
                                <option value="Ukraine">
                                    <%=Resources.Lang.Ukraine%></option>
                                <option value="United Arab Emirates">
                                    <%=Resources.Lang.UnitedArabEmirates%></option>
                                <option value="United Kingdom of Great Britain">
                                    <%=Resources.Lang.UnitedKingdomofGreatBritainandNorthernIreland%></option>
                                <option value="United States of America">
                                    <%=Resources.Lang.UnitedStatesofAmerica%></option>
                                <option value="Uruguay">
                                    <%=Resources.Lang.Uruguay%></option>
                                <option value="Venezuela">
                                    <%=Resources.Lang.Venezuela%></option>
                                <option value="Viet Nam">
                                    <%=Resources.Lang.VietNam%></option>
                                <option value="Western Samoa">
                                    <%=Resources.Lang.WesternSamoa%></option>
                                <option value="Yemen">
                                    <%=Resources.Lang.Yemen%></option>
                                <option value="Yugoslavia">
                                    <%=Resources.Lang.Yugoslavia%></option>
                                <option value="Zambia">
                                    <%=Resources.Lang.Zambia%></option>
                                <option value="Zimbabwe">
                                    <%=Resources.Lang.Zimbabwe%></option>
                            </select>
                        </td>
                    </tr>
                </table>
                <br />
                 <div class="box_conts_header">
                    <asp:Literal ID="ltrSiteinfor" runat="server"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table id="siteUrl" width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td colspan="3"><input type="radio" style="border:0;" name="rdWebsite" checked onclick="showRequire()" value="0" /> <%=Resources.Lang.OwnWebsite %></td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">
                             <asp:Literal runat="server" ID="ltrSiteurl"></asp:Literal>
                        </td>
                        <td style="width: 15px;" class="cont12oe">
                            <span id="spURL">*</span>
                        </td>
                        <td>
                            <input name="txtSiteUrl" id="txtSiteUrl" class="inputbox sites" size="30"
                                type="text" />&nbsp;&nbsp;&nbsp;<input type="button" id="addURL" class="button" onclick="AddOneMoreURL();" value="<%=Resources.Lang.AddUrl%>" />
                        </td>
                    </tr>
                </table>
                <table width="100%" cellspacing="0" cellpadding="2">
                    
                    <tr>
                        <td colspan="3"><input type="radio" style="border:0;" onclick="showRequire()" name="rdWebsite" value="1" /> <asp:Literal runat="server" ID="ltrNoWebsite"></asp:Literal></td>
                    </tr>
                    <tr valign="top">
                        <td style="width: 150px;">
                            <%=Resources.Lang.DescWebsite%>
                        </td>
                        <td style="width: 15px;" class="cont12oe">
                            <span id="spNoURL">*</span>
                        </td>
                        <td>
                            <textarea id="txtNoWebsite" name="txtNoWebsite" cols="60" rows="3"></textarea>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal ID="ltrPrefer" runat="server"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                     <tr>
                        <td style="width: 150px;">
                            <asp:Literal ID="ltrLang" runat="server"></asp:Literal>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <select id="slPreferLang" name="slPreferLang">
                                <option value=""><%=Resources.Lang.SelectOne %></option>
                                <option value="en-US"
                                    <%= AppSettings.DefaultPreferLang.Equals("en-US") ? "selected=selected" : string.Empty %>>
                                    <%=Resources.Lang.English%></option>
                                <option value="en-GB"
                                    <%= AppSettings.DefaultPreferLang.Equals("en-GB") ? "selected=selected" : string.Empty %>>
                                    <%=Resources.Lang.EnglishEU%></option>
                                <option value="zh-TW"
                                    <%= AppSettings.DefaultPreferLang.Equals("zh-TW") ? "selected=selected" : string.Empty %>>
                                    <%=Resources.Lang.ChineseTraditio%></option>
                                <option value="zh-CN"
                                    <%= AppSettings.DefaultPreferLang.Equals("zh-CN") ? "selected=selected" : string.Empty %>>>
                                    <%=Resources.Lang.ChineseSimplifi%></option>
                                <option value="th-TH"
                                    <%= AppSettings.DefaultPreferLang.Equals("th-TH") ? "selected=selected" : string.Empty %>>>
                                    <%=Resources.Lang.Thai%></option>
                                <option value="es-ES"
                                    <%= AppSettings.DefaultPreferLang.Equals("es-ES") ? "selected=selected" : string.Empty %>>>
                                    <%=Resources.Lang.Spanish%></option>
                                <option value="vi-VN"
                                    <%= AppSettings.DefaultPreferLang.Equals("vi-VN") ? "selected=selected" : string.Empty %>>>
                                    <%=Resources.Lang.Vietnamese%></option> 
                                <option value="id-ID"
                                    <%= AppSettings.DefaultPreferLang.Equals("id-ID") ? "selected=selected" : string.Empty %>>>
                                    <%=Resources.Lang.Indonesian%></option>
                                <option value="ko-KR"
                                    <%= AppSettings.DefaultPreferLang.Equals("ko-KR") ? "selected=selected" : string.Empty %>>>
                                    <%=Resources.Lang.Korean%></option>
                                <option value="ja-JP"
                                    <%= AppSettings.DefaultPreferLang.Equals("ja-JP") ? "selected=selected" : string.Empty %>>>
                                    <%=Resources.Lang.Japanese%></option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Resources.Lang.lblSecureQuestion%>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <select id="slSecurityQuestion" name="slSecurityQuestion">
                                <option selected="" value=""><%=Resources.Lang.SelectOne %></option>
                                <%=strSecureQuestion%>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Resources.Lang.lblYourAnswer%>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input type="text" autocomplete="off" maxlength="100" class="inputbox"
                                size="30" value="" id="txtAnswer" name="txtAnswer" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%=Resources.Lang.Validation%>
                        </td>
                        <td class="cont12oe">
                            *
                        </td>
                        <td>
                            <input name="txtValidateRegister" id="txtValidateRegister" type="text" class="inputbox"
                                value="<%=Resources.Lang.Validation %>" style="width: 80px;" align="absmiddle"
                                onfocus="setvaluefocus(this,1,'val');" onblur="setvaluefocus(this,0,'val');"
                                autocomplete="off" />
                            <img id="validCodeRegister" height="20px" width="40px" alt="Valiation Code" src="Authorization/LoginCaptcha.ashx"
                                align="absmiddle" onclick="ChangeCapchaCode(this);" class="hand" />
                        </td>
                    </tr>
                </table>
                <br />
                <div class="box_conts_header">
                    <asp:Literal ID="ltrPaymentInfor" runat="server"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <table width="100%" cellspacing="0" cellpadding="2">
                    <tr>
                        <td colspan="3" class="b">
                            <asp:Literal runat="server" ID="ltrPaymentMethod"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top">
                                    <td valign="top" width="37%">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td>
                                                    <!--12Bet-->
                                                    <input name="radio" class="noborder" value="0" type="radio" checked />
                                                    <asp:Literal runat="server" ID="ltrAccount12Bet"></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top" width="63%">
                                        <div id="div12bet">
                                            <table class="ContentInput">
                                                <tr valign="top">
                                                    <td class="payment">
                                                        <asp:Literal runat="server" ID="ltr12UserName"></asp:Literal>
                                                    </td>
                                                    <td class="cont12oe">
                                                        *
                                                    </td>
                                                    <td>
                                                        <input name="txtBet12UserName" class="inputbox" id="txtBet12UserName" size="28" type="text" />
                                                        <br /><%=Resources.Lang.NonDeposit%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="100%">
                    <tr>
                        <td colspan="3">
                            <div class="submit-bar">
                                <input name="CheckTerm" id="CheckTerm" onclick="AgreeTerm();" value="CheckTerm" class="noborder" type="checkbox" />
                                <a class="link_black" onclick="MM_openBrWindow('TermNCondition.aspx','','scrollbars=yes,resizable=yes,width=980,height=800')"
                                    href="#">
                                    <asp:Literal ID="ltrAgreeto" runat="server"></asp:Literal></a>
                                <br />
                                <br />
                                <div align="center">
                                    <input name="btnSubmit" id="btnSubmit" class="disabled" disabled value="<%=Resources.Lang.Submit %>"
                                        type="submit" />
                                    <input name="btnReset" id="btnReset" onclick="ResetClick();" value="<%=Resources.Lang.Reset %>"
                                        class="button" type="reset" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
                
            </div>
        </div>        
    </div>
    <input type="hidden" value="12" id="hdMethod" name="hdMethod" />
    <input type="hidden" value="" id="hdMoreurl" name="hdMoreurl" />
    <input type="hidden" value='<%=Resources.Lang.TooltipEmailAddess%>' id="hdEmailAddress" name="hdEmailAddress" />
    <input type="hidden" value="<%=Resources.Lang.TooltipContactNumber%>" id="hdContactNumber" name="hdContactNumber" />
    </form>
    <iframe id="frRegister" name="frRegister" class="hide" style="height: 0px; width: 0px;"></iframe>
</asp:Content>
