Public Class HttpHelper
    Public Shared Sub RedirectSsl()
        If Not HttpContext.Current.Request.IsSecureConnection Then
            If Not ConfigurationManager.AppSettings("devmode") = "yes" Then
                HttpContext.Current.Response.Redirect(Replace(HttpContext.Current.Request.Url.AbsoluteUri, "http://", "https://"))
            End If
        End If
    End Sub
End Class
