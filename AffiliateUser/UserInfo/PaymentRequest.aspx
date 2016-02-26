<%@ Page Language="C#" MasterPageFile="~/Master.master" AutoEventWireup="true"
    Inherits="PaymentRequest" Title="12Bet Affiliate Program" Codebehind="PaymentRequest.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
<%
    bool canRequestPayout = AppSettings.LocalTest || DateTime.Now.Day >= 8 && DateTime.Now.Day <= 15;
 %>
    <script type="text/javascript">
        RefreshPage(300);
    </script>
    <div id="contents">
        <div id="box_insite">
            <div class="header_in">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td class="in_left">&nbsp;</td>
                        <td width="155" class="in_top"><%=Resources.Lang.PaymentRequest %></td>
                        <td class="in_right">&nbsp;</td>
                    </tr>                
                </table>
            </div>
            <div class="in_conts">                                
                <form id="frUpdate" name="frUpdate" method="post" onsubmit="return RequestPayout();" target="frUpdateData" action="../Handler/RequestPayout.ashx">
                    <div class="box_conts_header">
                        <%=Resources.Lang.PaymentInfor %>
                    </div>
                    <div class="box_conts_line"></div>
                    <br />
                    <%if (canRequestPayout)
                      {%>  
                    <div id="errMsg" style="display: none;" class="msg-wid">
                        <div id="divErr">
                            <table  width="100%" cellspacing="0" cellpadding="0">
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
                    <%}
                      else
                      { %>
                        <div id="errMsg" class="msg-wid">
                            <div id="divErr">
                                <table  width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <div id="error_item">
                                                <ul>
                                                    <li><span class="err-msg b"><%=Resources.Lang.PayoutTime %></span></li>
                                                </ul>                                                
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    <%} %>
                    <div id="succMsg" style="display: none;" class="msg-wid">
                        <div id="divSuccess">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td style="width: 40px;" valign="top">
                                        <div class="icsuccess">
                                            &nbsp;</div>
                                    </td>
                                    <td valign="top">
                                        <ul>
                                            <li><span class="success" id="succ_item">
                                                <%=Resources.Lang.RequestPayoutSucc %></span></li>
                                        </ul>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <table width="100%" cellspacing="0" cellpadding="3">
                       
                        <tr>
                            <td style="width: 185px;">
                                <%=Resources.Lang.BalForward %>
                            </td>
                            <td id="spBalance" class="greater0 r" style="width: 130px;">
                                <%=strBalance%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%=Resources.Lang.MinWithdraw%>
                            </td>
                            <td id="spMinAmt" class="greater0 r">
                                <%=strMinAmt%>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td><%=Resources.Lang.RequestAmt %></td>
                            <td class="r"><input type="text" id="txtAmt" name="txtAmt" <%= !canRequestPayout ? "readonly" : string.Empty %> class="inputbox" style="width: 90px; text-align: right;" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="2" class="c">
                                <%if (canRequestPayout)
                                  {%>                                
                                    <input type="submit" class="button" id="btnRequest" name="btnRequest" value="<%=Resources.Lang.RequestPayout %>" />
                                    <%}
                                  else
                                  { %>
                                    <button type="button" disabled="disabled"><%=Resources.Lang.RequestPayout %></button>
                                    <%} %>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <input type="hidden" id="hdBalance" name="hdBalance" value="<%=strBalance%>" />
                    <input type="hidden" id="hdMinAmt" name="hdMinAmt" value="<%=strMinAmt%>" />
                    <input type="hidden" id="hdMethod" name="hdMethod" value="<%=strPaymentMethod%>" />
                </form>
                <br />
                <div id="divListRequest">
                    <asp:Literal ID="ltrListRequest" runat="server"></asp:Literal>
                </div>
                <iframe id="frUpdateData" name="frUpdateData" class="hidden" style="height: 0px;
                    width: 0px; display: none;"></iframe>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        window.onload = function() {
            if ($("hdMethod").value != "Player") {
                $("frUpdate").disable();

                if ($("btnRequest")) {
                    $("btnRequest").className = "disabled";
                }
                
                AddError("error_item", $("hdMethod").value);
                $('errMsg').show();
                ShowErrorNotLink(arrError, arrItem, "error_item");
                return;
            }
            var Balance = parseFloat($("hdBalance").value.replace(/,/g, "").replace("$", ""));
            var MinAmt = parseFloat($("hdMinAmt").value.replace(/,/g, "").replace("$", ""));
            if (Balance <= 0 || Balance < MinAmt) {
                $("frUpdate").disable();
                
                if ($("btnRequest")) {
                    $("btnRequest").className = "disabled";
                }
                
                AddError("error_item", "<%=Resources.Lang.Balance0 %>");
                DisplayMsg(false);
            }
            else
                $("frUpdate").enable();
                
            clock();
            setInterval("clock();", 1000);
        };
        function ShowError1(msg) {
            $('errMsg').hide();
            $('succMsg').hide();
            arrItem.clear();
            arrError.clear();
            AddError("error_item", msg);
            $('errMsg').show();
            ShowErrorNotLink(arrError, arrItem, "error_item");
        }
        function RequestPayout() {
            <%if (!canRequestPayout)
              {%>  
                return false;
              <%} %>
            $('errMsg').hide();
            $('succMsg').hide();
            arrItem.clear();
            arrError.clear();
            if ($("isPending").value == "1") {
                AddError("error_item", "<%=Resources.Lang.Err3001 %>");
                DisplayMsg(false);
                return false;
            }
            if (!$("txtAmt").value.isNumber()) {
                AddError("txtAmt", "<%=Resources.Lang.AmtErr %> " + $("hdMinAmt").value + " <%=Resources.Lang.And %> " + $("hdBalance").value);
                DisplayMsg(false);
                return false;
            }
            var amtRequest = parseFloat($("txtAmt").value.replace(/,/g, "").replace("$", ""));
            var amtBalance = parseFloat($("hdBalance").value.replace(/,/g, "").replace("$", ""));
            var MinAmt = parseFloat($("hdMinAmt").value.replace(/,/g, "").replace("$", ""));
            if (amtRequest > amtBalance) {
                AddError("txtAmt", "<%=Resources.Lang.ErrBalanceForward%>");
            }
            if (MinAmt > amtRequest) {
                if (arrItem.length == 0)
                    AddError("txtAmt", "<%=Resources.Lang.ErrMinAmt %>");
                else
                    AddError("error_item", "<%=Resources.Lang.ErrMinAmt %>");
            }
            if (arrItem.length > 0 || arrError.length > 0) {
                DisplayMsg(false);
                return false;
            }
            return true;
        }
        function cancelRequest() {
            $('errMsg').hide();
            $('succMsg').hide();
            arrItem.clear();
            arrError.clear();
        }
        //var tblink = null;
        function UpdateSuccess(Msg, balance, minAmt, listPayment) {
            AddError("error_item", Msg);
            $("succ_item").innerHTML = Msg;
            DisplayMsg(true);
            $("spBalance").innerHTML = balance;
            $("hdBalance").value = balance;
            $("spMinAmt").innerHTML = minAmt;
            $("hdMinAmt").value = minAmt;
            $("txtAmt").value = "";
            $("divListRequest").innerHTML = listPayment;
            //tblink = setTimeout("ClearBlink();", 5000);
        }
        function UpdateErr(Msg) {
            $('errMsg').hide();
            $('succMsg').hide();
            arrItem.clear();
            arrError.clear();
            AddError("error_item", Msg);
            parent.DisplayMsg(false);
        }
    </script>

</asp:Content>
