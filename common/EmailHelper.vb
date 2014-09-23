Public Class EmailHelper
    Public Shared ReadOnly Property SmtpServer() As String
        Get
            Return ConfigurationManager.AppSettings("smtpserver")
        End Get
    End Property
    Public Shared ReadOnly Property SmtpUserId() As String
        Get
            Return ConfigurationManager.AppSettings("smtpserveruid")
        End Get
    End Property
    Public Shared ReadOnly Property SmtpPassword() As String
        Get
            Return ConfigurationManager.AppSettings("smtpserverpwd")
        End Get
    End Property

    Public Shared Sub SendEmail(ByVal body As String, ByVal subject As String, ByVal recip As String, ByVal sender As String)
        Try

            'Const cdoSendUsingPickup = 1
            Const cdoSendUsingPort = 2 'Must use this to use Delivery Notification
            'Const cdoAnonymous = 0
            Const cdoBasic = 1 ' clear text
            'Const cdoNTLM = 2 'NTLM
            'Delivery Status Notifications
            'Const cdoDSNDefault = 0 'None
            'Const cdoDSNNever = 1 'None
            'Const cdoDSNFailure = 2 'Failure
            'Const cdoDSNSuccess = 4 'Success
            'Const cdoDSNDelay = 8 'Delay
            'Const cdoDSNSuccessFailOrDelay = 14 'Success, failure or delay
            Dim ObjMsg, ObjConf, ObjFlds
            'Dim StrBody As String
            ObjMsg = CreateObject("CDO.Message")
            ObjConf = CreateObject("CDO.Configuration")

            ObjFlds = ObjConf.Fields
            With ObjFlds
                .Item("http://schemas.microsoft.com/cdo/configuration/sendusing") = cdoSendUsingPort
                .Item("http://schemas.microsoft.com/cdo/configuration/smtpserver") = SmtpServer
                .Item("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate") = cdoBasic
                .Item("http://schemas.microsoft.com/cdo/configuration/sendusername") = SmtpUserId
                .Item("http://schemas.microsoft.com/cdo/configuration/sendpassword") = SmtpPassword
                .Update()
            End With

            'strBody = "This is a sample message." & vbCrLf
            'strBody = strBody & "It was sent using CDO." & vbCrLf
            If (ConfigurationManager.AppSettings("DevMode") = "yes") Then
                recip = ConfigurationManager.AppSettings("EmailNotify3")
            End If
            With ObjMsg
                .Configuration = ObjConf
                .To = recip
                .From = sender
                .Subject = subject
                .TextBody = body
                .Fields.update()
                .Send()
            End With
        Catch ex As Exception

        End Try


    End Sub
End Class
