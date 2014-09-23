<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/style/Site1.Master" CodeBehind="SeminarPaymentDetail.aspx.vb" Inherits="SeminarRegistrationForm.SeminarPaymentDetail" %>
<%@ Import Namespace="SeminarRegistrationForm" %>
<%@ Register Src="~/usercontrols/AdminMenu.ascx" TagPrefix="uc1" TagName="AdminMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".SeminarPaymentList").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderContent" runat="server">
    <uc1:AdminMenu runat="server" id="AdminMenu" />
    <div id="wrapper">
        <div id="Content">
            <a href="SeminarPaymentList.aspx">Back to List</a>
            <asp:Label runat="server" ID="lblStudentAgreementFormError" ForeColor="red"/>
            <asp:FormView ID="FormView1" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <h4>Personal Information</h4>
                        <span class="col1">First Name:</span>
                        <span class="col1"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.FirstName%></span>
                        <span class="col2">Degree:</span>
                        <span class="col2"><%# DirectCast(Container.DataItem, Transaction).MemberInfo.Degree%></span>
                    </div>
                    <div class="row">
                        <span class="col1">Last Name:</span>
                        <span class="col1"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.LastName%></span>
                        <span class="col2">Year Graduated:</span>
                        <span class="col2"><%# DirectCast(Container.DataItem, Transaction).MemberInfo.YearGraduated%></span>
                    </div>
                    <div class="row">
                        <span class="col1">License:</span>
                        <span class="col1">
                            <asp:Button runat="server" ID="btnViewLicense" Text="View" OnClick="btnViewLicense_OnClick" /></span>
                        <span class="col2">Student Agreement Form:</span>
                        <span class="col2">
                            <asp:Button runat="server" ID="btnViewStudentAgreementForm" Text="View" OnClick="btnViewStudentAgreementForm_OnClick" />
                        </span>
                    </div>
                    <div class="row">
                        <h4>NAET Information</h4>
                        <span class="col1">Year Attended NAET Basic:</span>
                        <span class="col1"><%# DirectCast(Container.DataItem, Transaction).MemberInfo.YearAttendedNaetBasic%></span>
                        <span class="col2">Year Attended NAET Adv1:</span>
                        <span class="col2"><%# DirectCast(Container.DataItem, Transaction).MemberInfo.YearAttendedNaetAdv1%></span>
                    </div>
                    <div class="row">
                        <span class="col1">NAET Member:</span>
                        <span class="col1">
                            <asp:CheckBox runat="server" ID="chkMember" Checked="<%# DirectCast(Container.DataItem, Transaction).MemberInfo.Member%>" /></span>
                        <span class="col2">Referrer:</span>
                        <span class="col2"><%# DirectCast(Container.DataItem, Transaction).MemberInfo.Referrer%></span>
                    </div>
                    <div class="row">
                        <h4>Address Information</h4>
                        <span class="col1">Business Address:</span>
                        <span class="colThreeQuarter"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.Address%></span>
                    </div>
                    <div class="row">
                        <span class="col1">City:</span>
                        <span class="col1"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.City%></span>
                        <span class="col2">State:</span>
                        <span class="col2"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.State%></span>
                    </div>
                    <div class="row">
                        <span class="col1">Zip:</span>
                        <span class="col1"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.Zip%></span>
                        <span class="col2">Country:</span>
                        <span class="col2"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.Country%></span>
                    </div>
                    <div class="row">
                        <span class="col1">Phone:</span>
                        <span class="col1"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.Phone%></span>
                        <span class="col2">Mobile:</span>
                        <span class="col2"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.Mobile%></span>
                    </div>
                    <div class="row">
                        <span class="col1">Fax:</span>
                        <span class="col1"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.Fax%></span>
                        <span class="col2">Email:</span>
                        <span class="col2"><%# DirectCast(Container.DataItem, Transaction).BillingAddress.Email%></span>
                    </div>
                    <div class="row">
                        <span class="col1">Special Considerations:</span>
                        <span class="colThreeQuarter"><%# DirectCast(Container.DataItem, Transaction).MemberInfo.SpecialConsiderations%></span>
                    </div>
                </ItemTemplate>
            </asp:FormView>
            <asp:Repeater runat="server" ID="rptSeminars">
                <HeaderTemplate>
                    <div class="row">
                        <h4>Seminar List</h4>
                        <span class="col1">Seminar Type</span>
                        <span class="col2">Seminar Date</span>
                    </div>
                </HeaderTemplate>
                <ItemTemplate>
                    <div class="row">
                        <span class="col1"><%# EnumHelper.GetDescriptionFromEnumValue(DirectCast(Container.DataItem, Seminar).Type)%></span>
                        <span class="col2"><%# String.Format("{0:d}", DirectCast(Container.DataItem, Seminar).SeminarDate)%></span>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
