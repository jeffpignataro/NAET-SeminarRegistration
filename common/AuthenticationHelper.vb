Imports System.Data.SqlClient
Imports PayPal.Payments.DataObjects

Public Class AuthenticationHelper
    Public Shared Function ValidateAdmin(ByVal username As String, ByVal password As String) As Boolean
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetAuthenticationParameters(username, password)
        Dim executeStoredProcedure As DataTable = SqlHelper.ExecuteStoredProcedure("sp_ValidateAdmin", parameters, SqlHelper.ConnectionString)
        Return (executeStoredProcedure.Rows.Count > 0) 'If true admin is valid
    End Function
    Public Shared Sub ValidateAdminWithRedirect(ByVal username As String, ByVal password As String)
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetAuthenticationParameters(username, password)
        Dim executeStoredProcedure As DataTable = SqlHelper.ExecuteStoredProcedure("sp_ValidateAdmin", parameters, SqlHelper.ConnectionString)
        If Not (executeStoredProcedure.Rows.Count > 0) Then 'If true admin is valid
            HttpContext.Current.Response.Redirect("Login.aspx")
        End If
    End Sub
End Class
