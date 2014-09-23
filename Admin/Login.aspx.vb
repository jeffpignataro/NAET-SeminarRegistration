Public Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblErr.Text = ""
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtUsername.Text.Length = 0 Then
            lblErr.Text = "Username Required."
            Exit Sub
        End If
        If txtPassword.Text.Length = 0 Then
            lblErr.Text = "Password Required."
            Exit Sub
        End If

        If (AuthenticationHelper.ValidateAdmin(txtUsername.Text, txtPassword.Text)) Then
            Session("username") = txtUsername.Text
            Session("password") = txtPassword.Text
            Response.Redirect("SeminarPaymentList.aspx")
        Else
            lblErr.Text = "Login failed. Please try again."
        End If
    End Sub
End Class