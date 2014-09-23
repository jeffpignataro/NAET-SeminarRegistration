Imports System.Data.SqlClient
Imports System.Linq
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Registrant

    Property Id() As Integer
    Property RegistrationId() As Integer
    Property CourseType() As EnumHelper.CourseType
    Property CourseDate() As DateTime
    Property SeminarId() As Integer
    Property Status() As EnumHelper.RegistrantStatus

    Public Sub New()
    End Sub

    Public Sub New(ByVal id As Integer, ByVal registrationId As Integer, ByVal courseType As EnumHelper.CourseType, ByVal courseDate As Date, ByVal seminarId As Integer, ByVal status As EnumHelper.RegistrantStatus)
        Me.Id = id
        Me.RegistrationId = registrationId
        Me.CourseType = courseType
        Me.CourseDate = courseDate
        Me.SeminarId = seminarId
        Me.Status = status
    End Sub

    Public Function GetRegistrants() As List(Of Registrant)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetSeminarRegistrants", New List(Of SqlParameter)(), SqlHelper.ConnectionString)
        Return MapRegistrant(dataTable)
    End Function

    Public Function GetRegistrant(ByVal id As String) As Registrant
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetRegistrantParameters(id)
        Dim dataTable = SqlHelper.ExecuteStoredProcedure("sp_GetSeminarRegistrantById", parameters, SqlHelper.ConnectionString)
        Dim mapCourse As List(Of Registrant) = MapRegistrant(dataTable)
        Return mapCourse.FirstOrDefault()
    End Function

    Public Function GetRegistrantsBySeminar(ByVal seminarId As Integer) As List(Of Registrant)
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetRegistrantBySeminarIdParameters(seminarId)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetSeminarRegistrantsBySeminarId", parameters, SqlHelper.ConnectionString)
        Return MapRegistrant(dataTable)
    End Function

    Public Function UpdateRegistrant(ByVal id As Integer, ByVal registrantStatusId As Integer) As Boolean
        Dim parameters As List(Of SqlParameter) = SqlHelper.UpdateRegistrantByIdParameters(id, registrantStatusId)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_UpdateRegistrantStatusById", parameters, SqlHelper.ConnectionString)
        Return Not IsNothing(dataTable)
    End Function

    Private Function MapRegistrant(ByVal dataTable As DataTable) As List(Of Registrant)
        Dim returnList As New List(Of Registrant)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim course As New Registrant
                With course
                    .Id = SqlHelper.CheckForNull(dataRow.Item("SeminarRegistrantId"), GetType(Integer))
                    .RegistrationId = SqlHelper.CheckForNull(dataRow.Item("SeminarRegistrationId"), GetType(Integer))
                    .CourseType = SqlHelper.CheckForNull(dataRow.Item("SeminarType"), GetType(Integer))
                    .CourseDate = SqlHelper.CheckForNull(dataRow.Item("SeminarDate"), GetType(DateTime))
                    .SeminarId = SqlHelper.CheckForNull(dataRow.Item("SeminarId"), GetType(Integer))
                    .Status = SqlHelper.CheckForNull(dataRow.Item("RegistrantStatusId"), GetType(Integer))
                End With
                returnList.Add(course)
            Next
        End If
        Return returnList
    End Function

    Public Sub AddRegistrantStatusAudit(ByVal seminarRegistrantId As Integer, ByVal registrantStatusId As String, ByVal username As String, ByVal dateTimeStamp As DateTime)
        Dim parameters As List(Of SqlParameter) = SqlHelper.AddRegistrantStatusAuditParameters(seminarRegistrantId, registrantStatusId, username, dateTimeStamp)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_AddRegistrantStatusAudit", parameters, SqlHelper.ConnectionString)
    End Sub
End Class

