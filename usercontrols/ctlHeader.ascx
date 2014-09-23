<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ctlHeader.ascx.vb" Inherits="SeminarRegistrationForm.ctlHeader" %>
<table cellpadding="0" cellspacing="0"  width="879px" border="0">
    <tr>
        <td align="left">
           <table cellpadding="0" cellspacing="0" width="898px" border="0">
                <tr>
                    <td style="width: 174px" align="right" valign="top"><img alt="" border="0" src="<%=Request.ApplicationPath %>/images/NARFLogo.gif" /></td>
                    <td style="width: 705px" align="left" valign="top"><img alt="" border="0" src="<%=Request.ApplicationPath %>/images/topright2.gif" width="705px" height="87px"/></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center">
            <table cellpadding="0" cellspacing="0" width="898" border="0">
                <tr>
                    <td width="17px">
                        <img alt="" src="<%=Request.ApplicationPath %>/images/spacer.gif" width="17px" height="1" />
                    </td>
                    <td width="864px" valign="top">
                        <asp:Label ID="Label1" runat="server" Text="" CssClass="PageHeader"></asp:Label>
                    </td>
                    <td width="17px">
                        <img alt="" src="<%=Request.ApplicationPath %>/images/spacer.gif" width="17px" height="1" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
