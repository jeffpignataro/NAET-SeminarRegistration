Public Class TypeHelper
    Public Shared Function ConvertStringtoInt(ByVal text As String) As Integer
        Dim returnVal As Integer
        If Integer.TryParse(text, returnVal) Then
            Return returnVal
        Else
            Return 0
        End If
    End Function
End Class
