Imports Microsoft.VisualBasic
Imports System.IO
Imports System.IO.File
Imports System.Net.Mail

Public Class objEmailer
    Public mFr As String = "automsg@naet.com"
    Public mTo As String
    Public sToTest As String = "steveh@mboxdesign.com"
    Public mSub As String = ""
    Public mBody As String
    Public isTest As Boolean = False
    Dim smtp As New SmtpClient

    Public Sub SendEmail()
        'change "true" to "false" below when not in test mode.
        mTo = TestEmail(mTo, isTest)
        Dim mail As New MailMessage
        Dim ma As New MailAddress(mFr)
        Dim mt As New MailAddress(mTo)
        mail.From = ma
        mail.To.Add(mt)
        mail.Subject = mSub
        mail.Body = mBody
        smtp.Send(mail)
    End Sub

    Function SendHTMLEmailMsg() As String
        Dim sMsg As String = "Email Sent"
        'change "true" to "false" below when not in test mode.
        mTo = TestEmail(mTo, isTest)
        Dim mail As New MailMessage
        Dim ma As New MailAddress(mFr)
        Dim mt As New MailAddress(mTo)
        mail.IsBodyHtml = True
        mail.From = ma
        mail.To.Add(mt)
        mail.Subject = mSub
        mail.Body = mBody
        Try
            smtp.Send(mail)
        Catch ex As Exception
            If ex.Message Is Nothing = False Then
                sMsg = ex.Message
            End If
        End Try
        Return sMsg
    End Function

    Public Sub SendSignupEmail()
        mSub = ""
        Dim mail As New MailMessage
        Dim ma As New MailAddress(mFr)
        Dim mt As New MailAddress(mTo)
        mail.IsBodyHtml = True
        mail.From = ma
        mail.To.Add(mt)
        mail.Subject = mSub
        mail.Body = mBody
        smtp.PickupDirectoryLocation = SmtpDeliveryMethod.PickupDirectoryFromIis
        smtp.DeliveryMethod = SmtpDeliveryMethod.Network
        smtp.Send(mail)
    End Sub

    Function ShowEmailString(ByVal s As String) As String
        Dim sEmailString As String = ""
        Dim sb As New StringBuilder
        If s Is DBNull.Value Then
            sb.Append("")
        Else
            Dim iIndexStart As Integer = 0
            Dim iIndexEnd As Integer = 0
            iIndexStart = s.IndexOf("<!--EMAILSTART-->")
            iIndexEnd = s.IndexOf("<!--EMAILEND-->")
            If iIndexStart > 0 Then
                sb.Append(s.Substring(iIndexStart, (iIndexEnd - iIndexStart) + 15))
            Else
                sb.Append(s)
            End If
            sb.Replace("<!--EMAILSTART-->", "<html><body bgcolor=""#d5deed"">")
            sb.Replace("<!--EMAILEND-->", "</body></html>")
        End If
        sEmailString = sb.ToString
        Return sEmailString
    End Function

    Function ShowEmailPreview(ByVal s As String) As String
        Dim sEmailString As String = ""
        Dim sb As New StringBuilder
        If s Is DBNull.Value Then
            sb.Append("")
        Else
            Dim iIndexStart As Integer = 0
            Dim iIndexEnd As Integer = 0
            iIndexStart = s.IndexOf("<!--EMAILSTART-->")
            iIndexEnd = s.IndexOf("<!--EMAILEND-->")
            If iIndexStart > 0 Then
                sb.Append(s.Substring(iIndexStart, (iIndexEnd - iIndexStart) + 15))
            Else
                sb.Append(s)
            End If
        End If
        sEmailString = sb.ToString
        Return sEmailString
    End Function

    Function TestEmail(ByVal sEmail As String, ByVal blnTest As Boolean) As String
        Dim s As String = ""
        If blnTest = True Then
            s = sToTest
        Else
            s = sEmail
        End If
        Return s
    End Function
End Class
