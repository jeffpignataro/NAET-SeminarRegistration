<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/style/Site1.Master" CodeBehind="SeminarRegistrantEdit.aspx.vb" Inherits="SeminarRegistrationForm.SeminarRegistrantEdit" EnableEventValidation="false" %>

<%@ Register Src="~/usercontrols/AdminMenu.ascx" TagPrefix="uc1" TagName="AdminMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".SeminarRegistrantList").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <uc1:AdminMenu runat="server" ID="AdminMenu" />
    <div id="wrapper">
        <div id="Content">
            <h4>Update Registration Record</h4>
            <asp:FormView runat="server" ID="fvSeminarRegistration" DataKeyNames="SeminarRegistrantId, SeminarRegistrationId, SeminarId, RegistrantStatusId">
                <ItemTemplate>
                    <div class="row">
                        <span class="col1">First Name <span class="req">*</span></span>
                        <asp:TextBox runat="server" ID="tbfName" Text='<%#Bind("FirstName")%>' CssClass="col1 fName" />
                        <asp:CustomValidator ID="CustomValidator1" runat="server"
                            ControlToValidate="tbfName" ClientValidationFunction="ValidateTextBox"
                            ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="First Name"
                            ErrorMessage="First name is required." Display="None" />
                        <span class="col2">Last Name <span class="req">*</span></span>
                        <asp:TextBox runat="server" ID="tblName" Text='<%#Bind("LastName")%>' CssClass="col2 lName" />
                        <asp:CustomValidator ID="CustomValidator8" runat="server"
                            ControlToValidate="tblName" ClientValidationFunction="ValidateTextBox"
                            ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Last Name"
                            ErrorMessage="Last name is required." Display="None" />
                    </div>
                    <div class="row">
                        <span class="colFull">Business Address <span class="req">*</span></span>
                        <asp:TextBox runat="server" ID="tbAddress" Text='<%# Bind("Address")%>' CssClass="colFull Address" />
                        <asp:CustomValidator ID="CustomValidator5" runat="server"
                            ControlToValidate="tbAddress" ClientValidationFunction="ValidateTextBox"
                            ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Business Address"
                            ErrorMessage="Address is required." Display="None" />
                    </div>
                    <div class="row">
                        <span class="col1">City <span class="req">*</span></span>
                        <asp:TextBox runat="server" ID="tbCity" Text='<%# Bind("City")%>' CssClass="col1 City" />
                        <asp:CustomValidator ID="CustomValidator6" runat="server"
                            ControlToValidate="tbCity" ClientValidationFunction="ValidateTextBox"
                            ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="City"
                            ErrorMessage="City is required." Display="None" />
                        <span class="col2">State <span class="req">*</span></span>
                        <asp:TextBox runat="server" ID="tbState" Text='<%# Bind("State")%>' CssClass="col2 State" />
                        <asp:CustomValidator ID="CustomValidator7" runat="server"
                            ControlToValidate="tbState" ClientValidationFunction="ValidateTextBox"
                            ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="State"
                            ErrorMessage="State is required." Display="None" />
                    </div>
                    <div class="row">
                        <span class="col1">Zip <span class="req">*</span></span>
                        <asp:TextBox runat="server" ID="tbZip" Text='<%# Bind("Zip")%>' CssClass="col1 Zip" />
                        <asp:CustomValidator ID="CustomValidator4" runat="server"
                            ControlToValidate="tbZip" ClientValidationFunction="ValidateTextBox"
                            ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Zip"
                            ErrorMessage="Zip is required." Display="None" />
                        <span class="col2">Country <span class="req">*</span></span>
                        <asp:DropDownList runat="server" ID="ddlCountry" CssClass="col2 Country" />

                    </div>
                    <div class="row">
                        <span class="col1">Phone <span class="req">*</span></span>
                        <asp:TextBox runat="server" ID="tbPhone" Text='<%# Bind("Phone")%>' CssClass="col1 Phone" />
                        <asp:CustomValidator ID="CustomValidator10" runat="server"
                            ControlToValidate="tbPhone" ClientValidationFunction="ValidateTextBox"
                            ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Phone"
                            ErrorMessage="Phone is required." Display="None" />
                        <span class="col2">Mobile</span>
                        <asp:TextBox runat="server" ID="tbMobile" Text='<%# Bind("Mobile")%>' CssClass="col2" />
                    </div>
                    <div class="row">
                        <span class="col1">Fax</span>
                        <asp:TextBox runat="server" ID="tbFax" Text='<%# Bind("Fax")%>' CssClass="col1 Fax" />
                        <span class="col2">Email <span class="req">*</span>
                            <asp:RegularExpressionValidator runat="server" ID="RegExValidator1" ControlToValidate="tbEmail"
                                ValidationExpression="[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?" ErrorMessage="Invalid Email" ValidationGroup="SeminarRegistration" /></span>
                        <asp:TextBox runat="server" ID="tbEmail" Text='<%# Bind("Email")%>' CssClass="col2 Email" />
                        <asp:CustomValidator ID="CustomValidator11" runat="server"
                            ControlToValidate="tbEmail" ClientValidationFunction="ValidateTextBox"
                            ValidateEmptyText="True" ValidationGroup="SeminarRegistration" ControlName="Email"
                            ErrorMessage="Email is required." Display="None" />
                    </div>
                </ItemTemplate>
            </asp:FormView>
            <div class="row" style="text-align: center;">
                <asp:Button runat="server" ID="btnSubmit" Text="Submit" ValidationGroup="SeminarRegistration" CssClass="SubmitBtn" />
                <asp:Button runat="server" ID="btnCancel" Text="Cancel" CssClass="SubmitBtn" />
            </div>
        </div>
    </div>
</asp:Content>
