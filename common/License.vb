Imports System.Data.SqlClient
Imports System.Linq

Public Class License
    Property LicenseId() As Integer
    Property SeminarRegistrationId() As Integer
    Property FileContent() As Byte()
    Property FileName() As String
    Property FileType() As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal licenseId As Integer, ByVal seminarRegistrationId As Integer, ByVal fileContent As Byte(), ByVal fileName As String, ByVal fileType As String)
        Me.LicenseId = licenseId
        Me.SeminarRegistrationId = seminarRegistrationId
        Me.FileContent = fileContent
        Me.FileName = fileName
        Me.FileType = fileType
    End Sub

    Public Function AddLicense(ByVal license As License) As DataTable
        Dim parameters As List(Of SqlParameter) = SqlHelper.AddLicenseParameters(license)
        Return SqlHelper.ExecuteStoredProcedure("sp_AddLicense", parameters, SqlHelper.ConnectionString)
    End Function

    Public Function GetLicense(ByVal seminarRegistartionId As Integer) As License
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetLicenseParameters(seminarRegistartionId)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetLicense", parameters, SqlHelper.ConnectionString)
        Dim licenseList As List(Of License) = MapLicense(dataTable)
        Return licenseList.FirstOrDefault()
    End Function

    Private Shared Function MapLicense(ByVal dataTable As DataTable) As List(Of License)
        Dim returnList As New List(Of License)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim license As New License
                With license
                    .LicenseId = SqlHelper.CheckForNull(dataRow.Item("SeminarLicenseId"), GetType(Integer))
                    .FileName = SqlHelper.CheckForNull(dataRow.Item("FileName"), GetType(String))
                    .FileContent = SqlHelper.CheckForNull(dataRow.Item("FileContent"), GetType(Byte()))
                    .FileType = SqlHelper.CheckForNull(dataRow.Item("FileType"), GetType(String))
                End With
                returnList.Add(license)
            Next
        End If
        Return returnList
    End Function
End Class
