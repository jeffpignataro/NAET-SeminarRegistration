Imports System.Data.SqlClient
Imports System.Linq

Public Class Seminar

    'Constants for seminar pricing
    Public Const BasicFirst = 660
    Public Const BasicRefresh = 390
    Public Const Adv1First = 660
    Public Const Adv1Refresh = 390
    Public Const Adv2CaseManage = 225
    Public Const Adv2 = 225
    Public Const Nst = 210

    Property Id() As Integer
    Property SeminarDate() As DateTime
    Property Type() As EnumHelper.SeminarType

    Public Sub New()
    End Sub

    Public Sub New(ByVal id As Integer)
        Me.Id = id
    End Sub

    Public Sub New(ByVal seminarDate As Date, ByVal type As EnumHelper.SeminarType)
        Me.SeminarDate = seminarDate
        Me.Type = type
    End Sub

    Public Sub New(ByVal id As Integer, ByVal seminarDate As Date, ByVal type As EnumHelper.SeminarType)
        Me.Id = id
        Me.SeminarDate = seminarDate
        Me.Type = type
    End Sub

    Public Shared Sub AddSeminar(ByVal transaction As Transaction, ByVal seminarDates As List(Of Seminar))
        For Each seminar As Seminar In seminarDates
            Dim parameters As List(Of SqlParameter) = SqlHelper.AddSeminar(transaction.TransactionId, seminar)
            SqlHelper.ExecuteStoredProcedure("sp_AddSeminarRegistrant", parameters, SqlHelper.ConnectionString)
        Next
    End Sub

    Public Function GetSeminars(ByVal id As Integer) As List(Of Seminar)
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetSeminarParameters(id)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetSeminars", parameters, SqlHelper.ConnectionString)
        Return Seminar.MapSeminar(dataTable)
    End Function

    Public Shared Function GetSeminarById(ByVal id As Integer) As Seminar
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetSeminarParameters(id)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetSeminarById", parameters, SqlHelper.ConnectionString)
        Return Seminar.MapSeminar(dataTable).FirstOrDefault()
    End Function

    Public Shared Function GetSeminarDateById(ByVal id As Integer) As DateTime
        Return GetSeminarById(id).SeminarDate
    End Function

    Public Shared Function MapSeminar(ByVal dataTable As DataTable) As List(Of Seminar)
        Dim returnList As New List(Of Seminar)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim seminar As New Seminar
                With seminar
                    .Id = SqlHelper.CheckForNull(dataRow.Item("SeminarRegistrantId"), GetType(String))
                    .Type = CType(SqlHelper.CheckForNull(dataRow.Item("SeminarType"), GetType(Integer)), EnumHelper.SeminarType)
                    .SeminarDate = SqlHelper.CheckForNull(dataRow.Item("SeminarDate"), GetType(Date))
                End With
                returnList.Add(seminar)
            Next
        End If
        Return returnList
    End Function
End Class
