<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/style/Site1.Master" CodeBehind="Login.aspx.vb" Inherits="SeminarRegistrationForm.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <div>
        <div>
            Username:
            <asp:TextBox ID="txtUsername" runat="server" autocomplete="off" />
        </div>
        <div>
            Password:
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" autocomplete="off" />
        </div>
        <div>
            <asp:Label runat="server" ID="lblErr" ForeColor="red"/>
        </div>
        <asp:Button runat="server" ID="btnSubmit" Text="Login"/>
    </div>
</asp:Content>
