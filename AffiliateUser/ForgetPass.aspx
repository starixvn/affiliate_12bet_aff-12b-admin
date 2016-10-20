<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true" Inherits="ForgetPass" Codebehind="ForgetPass.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<script type="text/javascript">
function ExecRecover()
{     
    $('contentsuc').style.display='none';
    $('errMsg').style.display='none';
    $('err').innerHTML='';
    if($('txtemail').value=='')
    {
      $('errMsg').style.display='';
      $('err').innerHTML='<li><a class=\'err-msg\' onclick="$(\'txtemail\').focus();" href=\'#\'><%=emailrequire %></a></li>';      
      return;
    }
    else if(Common.validateEmail($('txtemail'))==false)
    {
      $('errMsg').style.display='';
      $('err').innerHTML='<li><a class=\'err-msg\' onclick="$(\'txtemail\').focus();" href=\'#\'><%=emailrequire %></a></li>';      
    }
    else
    {
        var ajax= new Ajax.Request("Handler/CheckForgetPassWord.ashx",{
        method:'post',  
        parameters:'eml=' + $('txtemail').value,
        onComplete:function(datastran)
            {
                if(datastran.responseText=='Ok')
                {
                    $('contentsuc').style.display='block';
                    //$('content').style.display='none';
                    $('errMsg').style.display='none';
                    $('tablContent').hide();
                    $('err').innerHTML='';
                }
                else
                {
                    $('errMsg').style.display='block';
                    $('err').innerHTML = '<li><a class=\'err-msg\' onclick="$(\'txtemail\').focus();" href=\'#\'>' + datastran.responseText + '</a></li>';
                }
            }
        });
    }
}
function Reset()
{
    $('txtemail').value='';
    $('errMsg').style.display='none';
}
function EnterEvent(e){        
    e =(e) ?e :window.event;
	var key =(e.keyCode) ?e.keyCode :e.which;
	if (key ==13) ExecRecover();
}
</script>
<div id="contents">
    <div id="box_insite">
        <div class="header_in">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td class="in_left">&nbsp;</td>
                    <td width="220" class="in_top"><asp:Literal ID="ltrtitelrecoveruspw" runat="server"></asp:Literal></td>
                    <td class="in_right">&nbsp;</td>
                </tr>                
            </table>
        </div>
        <div class="in_conts">
            <div style="width: 570px; margin-top: 0px; margin-bottom: 0pt; display:none;" id="errMsg">
                <div id="divErr">
                    <table cellspacing="0" cellpadding="0" border="0" style="">
                        <tr>
                            <td valign="top" rowspan="2" style="width: 60px;">
                                <div class="warning">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <div id="error_item"><ul id="err"></ul></div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="contentsuc" name="contentsuc" style="display:none;" class="ContentBox pad15">
            <asp:Literal ID="ltrmsgsucc" runat="server"></asp:Literal></div>
            <table border="0" cellpadding="3" cellspacing="0" style="width: 100%" id="tablContent">      
                <tr>
                    <td colspan="2" >                        
                        <span class="box_conts_detail"><asp:Literal ID="ltrtitleforget" runat="server"></asp:Literal></span>
                    </td>
                    <td rowspan="3"></td>
                </tr>
                <tr>
                    <td style="width:130px; height: 35px;" class="b">
                        <asp:Literal ID="ltryouremail" runat="server"></asp:Literal>                        
                    </td>
                    <td style="width:320px;">
                        <input class="w90pc inputbox" id="txtemail" name="txtemail" type="text" style="width: 246px" onkeyup="EnterEvent(event)" /></td>
                </tr>                         
                <tr>          
                    <td colspan="2" class="c"><input id="Button1" class="button" type="button" value="<%=submit %>" onclick="ExecRecover();" />&nbsp;<input id="Button2" class="button" type="button" value="<%=reset %>" onclick="Reset();" /></td>                           
                </tr>                          
            </table>
        </div>
    </div>
</div>
</asp:Content>

