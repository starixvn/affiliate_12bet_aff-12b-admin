<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master.master" CodeFile="SiteMap.aspx.cs" Inherits="SiteMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <table width="100%" cellspacing="0" cellpadding="0" style="margin-bottom:5px">
        <tr style="height: 34px">
            <td class="box-topleft">
                &nbsp;
            </td>
            <td class="box-topcenter">
                <div class="ContentHeader">
                    <span>Site Map</span>
                    <span class="titleBG">&nbsp;</span>
                </div>
            </td>
            <td class="box-topright">
            </td>
        </tr>
        <tr>
            <td class="box-left">
            </td>
            <td valign="top" bgcolor="#fdfdfd">
                <div id="para_content">
                    <ul>
                        <li><a href="Root.aspx">Home</a></li>
                        <li><a href="ProgramDetails.aspx">Program Details</a></li>
                        <li><a href="FAQs.aspx">FAQs</a></li>
                        <li><a href="ContactUs.aspx">Contact Us</a></li>
                        <li><a href="AboutUs.aspx">About Us</a></li>
                        <li><a href="Condition.aspx">Terms and Conditions</a></li>
                        <li><a href="SignUp.aspx">Sign Up</a></li>
                        <li><a href="ForgetPass.aspx">Forget Password</a></li>
                    </ul>
                </div>
            </td>
            <td class="box-right">
            </td>
        </tr>
        <tr>
            <td class="box-bottomleft" />
            <td class="box-bottomcenter" />
            <td class="box-bottomright" />
        </tr>
    </table>
</asp:Content>