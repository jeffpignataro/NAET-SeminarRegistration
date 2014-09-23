Imports System.Data.SqlClient
Imports System.Linq

Public Class Course
    Property Id() As Integer
    Property Name() As String
    Property Type() As EnumHelper.CourseType
    Property DateTime() As DateTime
    ReadOnly Property FormattedDateTime() As String
        Get
            Return DateTime.ToString("MMM dd, yyyy")
        End Get
    End Property
    ReadOnly Property FormattedDateTimeWithSecondDay() As String
        Get
            Dim origDateTime as DateTime = DateTime
            Return origDateTime.ToString("MMM dd") & " - " & origDateTime.AddDays(1).ToString("MMM dd, yyyy")
        End Get
    End Property
    ReadOnly Property FormattedDatetimeWithSpecialization() As String
        Get
            Dim sType As String = Type.ToString()
            Return FormattedDateTime & String.Format(" ({0})", sType.Substring(sType.Length - 1))
        End Get
    End Property

    Public Function GetCourses() As List(Of Course)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetCourses", New List(Of SqlParameter)(), SqlHelper.ConnectionString)
        Return MapCourse(dataTable)
    End Function

    Public Function GetCoursesByType(ByVal courseType As EnumHelper.CourseType) As List(Of Course)
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetCoursebyCourseTypeParameters(courseType)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetCoursesByCourseType", parameters, SqlHelper.ConnectionString)
        Return MapCourse(dataTable)
    End Function

    Public Function GetCoursesBySpecialization() As List(Of Course)
        Dim dataTable As DataTable = SqlHelper.ExecuteStoredProcedure("sp_GetCoursesBySpecialization", New List(Of SqlParameter)(), SqlHelper.ConnectionString)
        Return MapCourse(dataTable)
    End Function

    Public Function GetCourse(ByVal id As String) As Course
        Dim parameters As List(Of SqlParameter) = SqlHelper.GetCourseParameters(id)
        Dim dataTable = SqlHelper.ExecuteStoredProcedure("sp_GetCourse", parameters, SqlHelper.ConnectionString)
        Dim mapCourse As List(Of Course) = Course.MapCourse(dataTable)
        Return mapCourse.FirstOrDefault()
    End Function

    Private Shared Function MapCourse(ByVal dataTable As DataTable) As List(Of Course)
        Dim returnList As New List(Of Course)
        If Not (IsNothing(dataTable)) Then
            For Each dataRow As DataRow In dataTable.Rows
                Dim course As New Course
                With course
                    .Id = SqlHelper.CheckForNull(dataRow.Item("ClassId"), GetType(Integer))
                    .Name = SqlHelper.CheckForNull(dataRow.Item("ClassName"), GetType(Date))
                    .Type = SqlHelper.CheckForNull(dataRow.Item("ClassType"), GetType(Integer))
                    .DateTime = SqlHelper.CheckForNull(dataRow.Item("ClassDate"), GetType(DateTime))
                End With
                returnList.Add(course)
            Next
        End If
        Return returnList
    End Function
End Class
