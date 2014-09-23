<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/style/Site1.Master" CodeBehind="SeminarPaymentList.aspx.vb" Inherits="SeminarRegistrationForm.SeminarPaymentList" %>

<%@ Register Src="~/usercontrols/AdminMenu.ascx" TagPrefix="uc1" TagName="AdminMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function() {
            $(".SeminarPaymentList").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:AdminMenu runat="server" id="AdminMenu" />
    <div id="wrapper">
        <div id="Content">
            <div>
                Transaction Status: 
            <asp:DropDownList runat="server" ID="ddlStatus" AutoPostBack="True">
                <Items>
                    <asp:ListItem Text="Successful" Value="1" Selected="True" />
                    <asp:ListItem Text="Failed" Value="0" />
                </Items>
            </asp:DropDownList>
            </div>
            <div>
                Date Range:
                <asp:Label runat="server" ID="lblStartDate" />
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/calicon.gif" />
                <asp:Calendar ID="Cal1" runat="server" Width="96px" Height="112px" Visible="False" BackColor="White" />
                to
                <asp:Label runat="server" ID="lblEndDate" />
                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="../images/calicon.gif" />
                <asp:Calendar ID="Cal2" runat="server" Width="96px" Height="112px" Visible="False" BackColor="White" />
                <asp:Button runat="server" ID="btnDateRange" Text="Update Date Range" />
            </div>
            <div>Select registrant for more detail.</div>
            <div class="row">
                <asp:Label runat="server" ID="lblErr" ForeColor="red" />
            </div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="Id" Width="100%">
                <AlternatingRowStyle BackColor="LightGrey" />
                <Columns>
                    <asp:BoundField DataField="RegistrationDate" HeaderText="Payment Date" DataFormatString="{0:g}" />
                    <asp:BoundField DataField="BillingAddress.FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="BillingAddress.LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="CardType" HeaderText="Payment Method" />
                    <asp:BoundField DataField="CardNumLast4" HeaderText="CC Last 4" />
                    <asp:BoundField DataField="CardExp" HeaderText="CC Exp Date" />
                    <asp:BoundField DataField="TotalAmount" HeaderText="Amount Paid" DataFormatString="{0:c}" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
