<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" CodeFile="Welcome.aspx.cs"
    Inherits="Welcome" %>
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
        
        .width160 {
            width: 160px;
        }
        
        .width150 {
            width: 150px;
        }
        
        .width30 {
            width: 16px;
        }
        
        .width11 {
            width: 11px;
        }
        
    </style>

    <script type="text/javascript"> 
    
    window.onload = function() {
        //MoreUrl();
        AgreeTerm();
        $("txtOldPassword").blur();
        $("txtOldPassword").focus(); 
        clock();
        setInterval("clock();", 1000);
    }
    function ResetClick()
    {
        arrItem.clear();
        arrError.clear();
        $('errMsg').hide();
        $('CheckTerm').checked = false;        
        AgreeTerm();
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
        if(isEmpty($('txtOldPassword').value))
            AddError("txtOldPassword", ErrMsg.OldPass);
        if(isEmpty($('txtNewPassword').value))
            AddError("txtNewPassword", ErrMsg.NewPass);
        else if ($('txtNewPassword').value != $('txtReNewPassword').value)
            AddError("txtReNewPassword", ErrMsg.RePass);
        if (isEmpty($('slSecurityQuestion').value))
            AddError("slSecurityQuestion", ErrMsg.SecurityQuestion);
        if (isEmpty($('txtYourAnswer').value))
            AddError("txtYourAnswer", ErrMsg.YourAnswer);

        if (arrItem.length > 0) {
            ShowError(arrError, arrItem, "error_item");
            $("errMsg").show();
            return false;
        }

        if (!checkpass($("txtNewPassword"), "td_pass") || !checkpass($("txtReNewPassword"), "td_confirm")) {
            AddError("txtNewPass", $("hdPassCheck").value);
            ShowError(arrError, arrItem, "error_item");
            $('errMsg').show();
            return false;
        }
        
        return true;
    }

    function check(str) {
        numbers = new String("0123456789");
        var i = 0;
        var nCount = 0;
        
        while (i < str.length) {
            (numbers.indexOf(str.substring(i, i + 1)) >= 0) ? nCount++ : false;
            i++;
        }
        
        return ((nCount > (str.length - 2)) || nCount < 2) ? false : true;
    }
    
    function createCheck(id) {
        t = document.createElement("img");
        t.setAttribute("id", id);
        t.width = 15; t.height = 11; t.border = 0;
        t.src = "../<%= AppSettings.ThemeDir %>/Images/1.gif";
        return t;
    }

    function checkpass(obj, tdid) {
        if (!tdid) tdid = 'td_pass';
        var bol = false;
        var pass = obj.value;
        var t = $("pass_" + tdid);
        var td = $(tdid);
        if (pass.length < 6) {
            if (!t) {
                t = createCheck("pass_" + tdid);
                td.appendChild(t);
            }
            else {
                t.src = "../<%= AppSettings.ThemeDir %>/Images/1.gif";
                t.title = $("hdPassCheck").value;
            }
        }
        else {
            bol = check(pass);
            if (bol) {
                if (!t) {
                    td = $(tdid);
                    t = createCheck("pass_" + tdid);
                    td.appendChild(t);
                }
                t.src = "../<%= AppSettings.ThemeDir %>/Images/2.gif";
                t.title = "";
            }
        }
        return bol;
    }
    
    </script>
    <form id="ForceChangePass_form" name="ForceChangePass_form" action="Handler/ForceResetPassword.ashx" method="post"
                    onsubmit="return submitForm();" target="frForceChangePass">
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="160" class="in_top"><asp:Literal ID="ltrForceResetPasswordForm" runat="server"></asp:Literal></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">
                <asp:Literal runat="server" ID="ltrDearAff"></asp:Literal>
                <br /><br />
                <%=Resources.Lang.WelcomeResettingNewPassword%>
                <br/><br />
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
                <table cellspacing="0" cellpadding="2">
                    <tr>
                        <td class="width150">
                            <%=Resources.Lang.OldPass%>
                        </td>
                        <td class="cont12oe width11">
                            *
                        </td>
                        <td class="width160">
                            <input name="txtOldPassword" class="inputbox width150" id="txtOldPassword" value="" size="30" type="password" />
                        </td>
                        <td class="width30">
                        </td>
                    </tr>
                    <tr>
                        <td class="width150">
                            <%=Resources.Lang.NewPass%>
                        </td>
                        <td class="cont12oe width11">
                            *
                        </td>
                        <td class="width160">
                            <input name="txtNewPassword" class="inputbox width150" id="txtNewPassword" value=""
                                            size="30" onkeyup="return checkpass(this,'td_pass');" type="password" onmouseover="Tip($('hdPassCheck').value.replace(',', ',<br />'), BGCOLOR, '#eeeeee', FONTCOLOR, '#da3614', SHADOW, true, SHADOWCOLOR, '#530208', DELAY, 10);" />
                        </td>
                        <td id="td_pass" class="width30" style="vertical-align: middle;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="width150">
                            <%=Resources.Lang.RePass%>
                        </td>
                        <td class="cont12oe width11">
                            *
                        </td>
                        <td class="width160">
                            <input name="txtReNewPassword" class="inputbox width150" id="txtReNewPassword" value="" size="30" onkeyup="return checkpass(this,'td_confirm');" type="password" />
                        </td>
                        <td id="td_confirm" class="width30" style="vertical-align: middle;">
                            &nbsp;
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="width150">
                            <%=Resources.Lang.lblSecureQuestion%>
                        </td>
                        <td class="cont12oe width11">
                            *
                        </td>
                        <td class="width160">
                            <select id="slSecurityQuestion" name="slSecurityQuestion">
                                <option selected="" value=""><%=Resources.Lang.SelectOne %></option>
                                <%=strSecureQuestion%>
                            </select>
                        </td>
                        <td class="witdh30">
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="width150">
                            <%=Resources.Lang.lblYourAnswer%>
                        </td>
                        <td class="cont12oe width11">
                            *
                        </td>
                        <td class="width160">
                            <input name="txtYourAnswer" class="inputbox width150" id="txtYourAnswer"
                                value="" size="30" onkeyup="return checkpass(this,'td_confirm');" type="text" />
                        </td>
                        <td class="width30">
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="3">
                            <div class="submit-bar">
                                <input name="CheckTerm" id="CheckTerm" onclick="AgreeTerm();" value="CheckTerm" class="noborder" type="checkbox" />
                                <a class="link_black" onclick="MM_openBrWindow('TermNCondition.aspx','','scrollbars=yes,resizable=yes,width=980,height=800')"
                                    href="#">
                                    <%=Resources.Lang.AgreeTermNCondition%></a>
                                <br />
                                <br />
                                <div align="center">
                                    <input name="btnSubmit" id="btnSubmit" value="<%=Resources.Lang.Submit %>" class="button" type="submit" />
                                    <input name="btnReset" id="btnReset" onclick="ResetClick();" value="<%=Resources.Lang.Reset %>"class="button" type="reset" />
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>        
    </div>
    <input type="hidden" value="<%=Resources.Lang.PassCheck %>" id="hdPassCheck" name="hdPassCheck" />
    </form>
    <iframe id="frForceChangePass" name="frForceChangePass" class="hide" style="height: 0px; width: 0px;"></iframe>
</asp:Content>
