<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="MyAccount" CodeBehind="MyAccount.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript" src="../App_Js/IBCWnd/IBCWnd.js"></script>
    <style type="text/css">
        .payment {
            width: 170px;
        }
    </style>

    <script type="text/javascript">
        var radios = document.getElementsByName("radio");
        var genders = document.getElementsByName("rdGender");
        var formats = document.getElementsByName("rdFormat");
        var websites = document.getElementsByName("rdWebsite");
        window.onload = function () { GetMethod(); }
        function GetMethod() {
        <%if (hasGender)
        { %>
            <%if (isGender)
        {%>
        genders[0].checked = true;
            <%}
        else {%>
        genders[1].checked = true;
        <%}
        }%>

        <%if (!string.IsNullOrEmpty(strSiteUrl))
        { %>
        websites[0].checked = true;
        <%}
        else {%>
        websites[1].checked = true;
        <%} %>

        $('slPreferLang').value = "<%=strPreferLang %>";
        $('selCountry').value = "<%=strCountry %>";
        var moreurl = "<%=strMoreSiteUrl %>";

        clock();
        setInterval("clock();", 1000);
    }
    function submitForm() {
        arrItem.clear();
        arrError.clear();
        $('errMsg').hide();
        $("hdMoreurl").value = "";
        for (var i = 2; i < 100; i++) {
            if (!$("txtSiteUrl" + i)) {
                var length = $("hdMoreurl").value.length;
                if (length > 0)
                    $("hdMoreurl").value = $("hdMoreurl").value.substr(0, length - 1);
                break;
            }
            $("hdMoreurl").value = $("hdMoreurl").value + $("txtSiteUrl" + i).value + ";";
        }
        if (!CheckSuccess())
            return false;
        return true;
    }
    function CheckSuccess() {
        /*if (!genders[0].checked) {
            if(!genders[1].checked){
                AddError("rdGender", ErrMsg.ErrGender);
            }
        }
        if(isEmpty($('txtAddress').value))
            AddError("txtAddress", ErrMsg.Address);
            */
        if (isEmpty($('txtNumber').value))
            AddError("txtNumber", ErrMsg.ContactNumber);
        if (isEmpty($('txtMobile').value))
            AddError("txtMobile", ErrMsg.MobileNumber);
        var typeValue = $$('input:checked[type="radio"][name="rdWebsite"]').pluck('value');

        if (websites[0].checked && isEmpty($("txtSiteUrl").value))
            AddError("txtSiteUrl", ErrMsg.SiteUrl);
        else if (websites[1].checked && isEmpty($("txtNoWebsite").value))
            AddError("txtNoWebsite", "<%=Resources.Lang.DescWebsite%>");
        else if (websites[0].checked) {
            var listSites = $$(".sites");
            for (var i = 1; i < listSites.length; i++) {
                if (listSites[i]) {
                    if (isEmpty(listSites[i].value)) {
                        AddError(listSites[i].id, "<%=Resources.Lang.WebsiteURL %> " + (i == 0 ? "" : (i + 1).toString()) + " <%=Resources.Lang.IsRequired%>");
                        break;
                    }
                }
            }
        }
        else if (isEmpty($("txtSiteUrl").value) && isEmpty($("txtNoWebsite").value))
            AddError("txtSiteUrl", ErrMsg.SiteUrl);

        //if(isEmpty($('slPreferLang').value))
        //AddError("slPreferLang", ErrMsg.PreferLang);

    if (isEmpty($('txtBet12UserName').value))
        AddError("txtBet12UserName", ErrMsg.UserName12Bet);

    if (arrItem.length > 0) {
        DisplayMsg(false);
        return false;
    }
    return true;
}
function UpdateSuccess() {
    DisplayMsg(true);
    window.scroll(0, 0);
}
function AddOneMoreURL() {
    var listSites = $$(".sites");
    var siteIndex = listSites.length;
    if (listSites[siteIndex - 1])
        if (isEmpty(listSites[siteIndex - 1].value)) {
            alert("<%=Resources.Lang.WebsiteUrli %>".replace("{i}", siteIndex.toString()));
                listSites[siteIndex - 1].focus();
                return;
            }
        siteIndex = siteIndex + 1;
        var table = $("siteUrl");
        var tr = table.insertRow(table.rows.length);
        var td1 = IBCWnd.createElement("td", tr);
        var td2 = IBCWnd.createElement("td", tr);
        var td3 = IBCWnd.createElement("td", tr);
        var td4 = IBCWnd.createElement("td", tr);
        tr.setAttribute("id", "tr" + siteIndex);
        td2.className = "cont12oe";
        td2.innerHTML = "*";
        td1.innerHTML = "<%=Resources.Lang.WebsiteURL %> " + siteIndex.toString();
        td3.innerHTML = "<input type='text' size='30' class='sites' id='txtSiteUrl" + siteIndex + "' name='txtSiteUrl" + siteIndex + "' />";
        td4.innerHTML = "<input type=\"button\" onclick=\"DelUrl(" + siteIndex + ");\" class=\"button\" value=\"<%=Resources.Lang.Del %>\" />";
        if ($("txtUrl" + siteIndex))
            $("txtUrl" + siteIndex).focus();
    }
    function DelUrl(rowNum) {
        $("siteUrl").deleteRow(rowNum);
        reorderRows(rowNum);
    }
    function reorderRows(starting) {
        var tbl = $("siteUrl");
        if (tbl.tBodies[0].rows[starting]) {
            for (var i = starting; i < tbl.tBodies[0].rows.length; i++) {
                var tr = tbl.tBodies[0].rows[i];
                var tds = tr.getElementsByTagName("td");

                var index = i;
                tds[0].innerHTML = "<%=Resources.Lang.WebsiteURL %> " + index.toString();
                tds[2].innerHTML = "<input type='text' size='30' value='" + tds[2].childNodes[0].value + "' class='sites' id='txtSiteUrl" + index + "' name='txtSiteUrl" + index + "' />";
                tds[3].innerHTML = "<input type=\"button\" onclick=\"DelUrl(" + index + ");\" class=\"button\" value=\"<%=Resources.Lang.Del %>\" />";
            }
        }
    }
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="144" class="in_top">
                            <asp:Literal ID="ltrMyAcc" runat="server"></asp:Literal></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>
                </table>
            </div>
            <div class="in_conts">
                <div id="errMsg" style="display: none;" class="msg-wid">
                    <div id="divErr">
                        <table border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td style="width: 40px;" valign="top">
                                    <div class="warning">
                                        &nbsp;
                                    </div>
                                </td>
                                <td valign="top">
                                    <div id="error_item">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="succMsg" style="display: none;" class="msg-wid">
                    <div id="divSuccess">
                        <table border="0" cellspacing="0" cellpadding="0" width="100%">
                            <tr>
                                <td style="width: 40px;" valign="top">
                                    <div class="icsuccess">
                                        &nbsp;
                                    </div>
                                </td>
                                <td valign="top">
                                    <ul>
                                        <li><span class="success" id="succ_item">
                                            <%=Resources.Lang.UpdateAccSuccess%>
                                        </span></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="box_conts_header">
                    <asp:Literal runat="server" ID="ltrAccInfo"></asp:Literal>
                </div>
                <div class="box_conts_line"></div>
                <br />
                <form id="Update_form" name="Update_form" action="../Handler/UpdateAcc.ashx" method="post" onsubmit="return submitForm()" target="frRegister">
                    <table width="100%" cellspacing="0" cellpadding="2">
                        <tr>
                            <td style="width: 150px;">
                                <asp:Literal ID="ltrUser" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe" style="width: 14px;">&nbsp;
                            </td>
                            <td>
                                <asp:Literal runat="server" ID="ltrUsername"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrPass" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe">&nbsp;
                            </td>
                            <td>
                                <a href="ChangePass.aspx">
                                    <%=Resources.Lang.Change%></a>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="box_conts_header">
                        <asp:Literal runat="server" ID="ltrContactInfo"></asp:Literal>
                    </div>
                    <div class="box_conts_line"></div>
                    <br />
                    <table width="100%" cellspacing="0" cellpadding="2">
                        <tr>
                            <td style="width: 150px;">
                                <%=Resources.Lang.Gender %>
                            </td>
                            <td class="cont12oe"></td>
                            <td>
                                <input id="rdGender" class="noborder" type="radio" value="1" name="rdGender" onclick="return false;" <%= isGender ? "" : "disabled" %> />
                                <%=Resources.Lang.Male %>
                                <input id="rdGender" class="noborder" type="radio" value="0" name="rdGender" onclick="return false;" <%= isGender ? "disabled" : "" %> />
                                <%=Resources.Lang.Female %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrAddress" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe">*
                            </td>
                            <td>
                                <input name="txtAddress" class="inputbox" id="txtAddress" value="<%=strAddress %>"
                                    size="30" type="text" disabled />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrCity" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe"></td>
                            <td>
                                <input name="txtCity" disabled id="txtCity" value="<%=strCity %>"
                                    size="30" type="text" />
                            </td>
                        </tr>
                        <%if (AppSettings.Session.Language == "zh-CN" || AppSettings.Session.Language == "zh-TW")
                            {%>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="ltrState"></asp:Literal>
                            </td>
                            <td class="cont12oe"></td>
                            <td>
                                <input name="txtState" id="txtState" class="disable" readonly="readonly" value="<%=strState %>"
                                    size="30" type="text" />
                            </td>
                        </tr>
                        <%} %>
                        <tr>
                            <td>
                                <asp:Literal runat="server" ID="ltrPostcode"></asp:Literal>
                            </td>
                            <td class="cont12oe"></td>
                            <td>
                                <input name="txtPost" id="txtPost" class="disable" readonly="readonly" value="<%=strPost%>"
                                    size="30" type="text" disabled />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrCountry" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe"></td>
                            <td>
                                <select name="selCountry" id="selCountry" style="size: 30;" disabled="disabled">
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
                        <tr>
                            <td>
                                <asp:Literal ID="ltrEmail" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe"></td>
                            <td>
                                <input name="txtEmail" disabled id="txtEmail" value="<%=strEmail%>"
                                    size="30" type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrContactNumber" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe">*
                            </td>
                            <td>
                                <input name="txtNumber" id="txtNumber" class="disable" value="<%=strNumber %>" size="30"
                                    type="text" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMobile" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe">*
                            </td>
                            <td>
                                <input name="txtMobile" id="txtMobile" class="disable" value="<%=strMobile %>" size="30"
                                    type="text" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="box_conts_header">
                        <asp:Literal runat="server" ID="ltrSiteinfor"></asp:Literal>
                    </div>
                    <div class="box_conts_line"></div>
                    <br />
                    <table id="siteUrl" width="100%" cellspacing="0" cellpadding="2">
                        <tr>
                            <td colspan="3">
                                <input type="radio" style="border: 0;" name="rdWebsite" checked onclick="showRequire()" value="0" />
                                <%=Resources.Lang.OwnWebsite %></td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">
                                <asp:Literal runat="server" ID="ltrSiteurl"></asp:Literal>
                            </td>
                            <td style="width: 15px;" class="cont12oe">
                                <span id="spURL">*</span>
                            </td>
                            <td style="width: 190px;">
                                <input name="txtSiteUrl" id="txtSiteUrl" class="inputbox sites" size="30"
                                    type="text" value="<%=strSiteUrl %>" />
                            </td>
                            <td>
                                <input type="button" id="addURL" class="button" onclick="AddOneMoreURL();" value="<%=Resources.Lang.AddUrl%>" />
                            </td>
                        </tr>
                        <%=strWebsites %>
                    </table>
                    <table width="100%" cellspacing="0" cellpadding="2">

                        <tr>
                            <td colspan="3">
                                <input type="radio" style="border: 0;" onclick="showRequire()" name="rdWebsite" value="1" />
                                <asp:Literal runat="server" ID="ltrNoWebsite"></asp:Literal></td>
                        </tr>
                        <tr valign="top">
                            <td style="width: 150px;">
                                <%=Resources.Lang.DescWebsite%>
                            </td>
                            <td style="width: 15px;" class="cont12oe">
                                <span id="spNoURL">*</span>
                            </td>
                            <td>
                                <textarea id="txtNoWebsite" name="txtNoWebsite" cols="60" rows="3"><%=strNoSiteDesc%></textarea>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="box_conts_header">
                        <asp:Literal runat="server" ID="ltrPrefer"></asp:Literal>
                    </div>
                    <div class="box_conts_line"></div>
                    <br />
                    <table width="100%" cellspacing="0" cellpadding="2">
                        <tr>
                            <td style="width: 150px;">
                                <asp:Literal ID="ltrLang" runat="server"></asp:Literal>
                            </td>
                            <td class="cont12oe" style="width: 14px;">*</td>
                            <td>
                                <select id="slPreferLang" name="slPreferLang" disabled>
                                    <option value="">
                                        <%=Resources.Lang.SelectOne %></option>
                                    <option value="en-US">
                                        <%=Resources.Lang.English%></option>
                                    <option value="en-GB">
                                        <%=Resources.Lang.EnglishEU%></option>
                                    <option value="zh-TW">
                                        <%=Resources.Lang.ChineseTraditio%></option>
                                    <option value="zh-CN">
                                        <%=Resources.Lang.ChineseSimplifi%></option>
                                    <option value="th-TH">
                                        <%=Resources.Lang.Thai%></option>
                                    <option value="es-ES">
                                        <%=Resources.Lang.Spanish%></option>
                                    <option value="vi-VN">
                                        <%=Resources.Lang.Vietnamese%></option>
                                    <option value="id-ID">
                                        <%=Resources.Lang.Indonesian%></option>
                                    <option value="ko-KR">
                                        <%=Resources.Lang.Korean%></option>
                                    <option value="ja-JP">
                                        <%=Resources.Lang.Japanese%></option>
                                    <option value="hi-IN">
                                        <%=Resources.Lang.India%></option>
                                </select>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="box_conts_header">
                        <asp:Literal runat="server" ID="ltrPaymentInfor"></asp:Literal>
                    </div>
                    <div class="box_conts_line"></div>
                    <br />
                    <table width="100%" cellspacing="0" cellpadding="2">
                        <tr>
                            <td colspan="3">
                                <asp:Literal runat="server" ID="ltrPaymentMethod"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" id="payment">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top">
                                        <td valign="top" width="37%">
                                            <table class="ContentInput">
                                                <tbody>
                                                    <tr valign="top">
                                                        <td>
                                                            <!--12Bet-->
                                                            <input name="radio" class="noborder" value="0" type="radio" checked />
                                                            <asp:Literal runat="server" ID="ltrAccount12Bet"></asp:Literal>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </td>
                                        <td valign="top" width="63%">
                                            <div id="div12bet" style="padding-top: 3px;">
                                                <table class="ContentInput">
                                                    <tr valign="top">
                                                        <td class="payment">
                                                            <asp:Literal runat="server" ID="ltr12UserName"></asp:Literal>
                                                        </td>
                                                        <td class="cont12oe">*
                                                        </td>
                                                        <td>
                                                            <input name="txtBet12UserName" class="disable" value="<%=strBet12UserName %>" id="txtBet12UserName"
                                                                size="28" type="text" <%= (string.IsNullOrEmpty(strBet12UserName) ? string.Empty : "readonly") %> />
                                                            <br />
                                                            <%=Resources.Lang.NonDeposit%>
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
                    <div class="submit-bar">
                        <div align="center">
                            <input name="btnSubmit" id="btnSubmit" class="button" value="<%=Resources.Lang.Submit %>"
                                type="submit" />
                            <input name="btnCancel" id="btnCancel" value="<%=Resources.Lang.Cancel%>" class="button"
                                type="button" onclick="javascript: window.location.href = 'Account_Overview.aspx';" />
                        </div>
                    </div>
                    <input type="hidden" value="<%=strMethod %>" id="hdMethod" name="hdMethod" />
                    <input type="hidden" value="<%=strPayment %>" id="hdPayment" name="hdPayment" />
                    <input type="hidden" value="<%=str12Bet %>" id="hd12Bet" name="hd12Bet" />
                    <input type="hidden" value="" id="hdMoreurl" name="hdMoreurl" />
                </form>
                <iframe id="frRegister" name="frRegister" class="hidden" style="height: 0px; width: 0px; display: none;"></iframe>
            </div>
        </div>
    </div>
</asp:Content>