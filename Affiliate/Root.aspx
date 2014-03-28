<%@ Page Language="C#" MasterPageFile="~/Master2.master" AutoEventWireup="true" CodeFile="Root.aspx.cs" Inherits="Root" %>

<%@ Register Src="Controls/Home_Center.ascx" TagName="Home_Center" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" Runat="Server">
    <title>12BET Affiliate Program | Sports Betting and Online Casino</title>
    <meta name="google-site-verification" content="iFkj5nsimoERnX2jd-9krswREsacSaFCr3Q22CJtQe0" />
    <meta name="msvalidate.01" content="D40D4C0F9E67ABAAAFA183F5D313FEFD" />
    <meta name="description" content="Earn Extra Cash, Join 12BET.com Fast and Easy Online Sports Betting and Casino Affiliate Program. With high conversions, Earn 40% commission for life and more!" />
    <meta name="keywords" content="12BET, 12BET Affiliate Program, Sports Betting Affiliate, Casino Affiliate, betting affiliate programs, earn extra cash, top affiliate programs, asian casino affiliate programs, Asian betting affiliate programs, high payout affiliate programs" />
    <meta name="robots" content="index,follow,archive" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
        <uc3:Home_Center ID="Home_Center" runat="server" />
    </div>        
</asp:Content>


