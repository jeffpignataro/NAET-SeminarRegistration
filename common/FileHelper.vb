Imports System.IO

Public Class FileHelper
    Public Shared Function BinaryReader(stream As Stream) As Byte()
        Dim contentLength As Integer = stream.Length
        Dim byteArray(contentLength) As Byte
        stream.Read(byteArray, 0, contentLength)
        Return byteArray
    End Function
End Class
