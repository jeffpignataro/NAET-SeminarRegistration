Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class objData
    Dim DBConnStr As String = ConfigurationManager.ConnectionStrings("DBConnStr").ConnectionString
    Dim DBConnStrCS As String = ConfigurationManager.ConnectionStrings("DBConnStr_CS").ConnectionString

    Function GetCaseStudyTitle(ByVal sGUID As String) As String
        Dim sTitle As String = ""
        Dim sqlConn As New SqlConnection
        sqlConn.ConnectionString = DBConnStrCS
        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = sqlConn
        sqlCmd.CommandType = CommandType.StoredProcedure
        Dim param1 As New SqlParameter
        param1.DbType = DbType.String
        param1.ParameterName = "@ipCaseStudyGUID"
        param1.Value = sGUID

        Dim param2 As New SqlParameter
        param2.DbType = DbType.Boolean
        param2.ParameterName = "@ipActive"
        param2.Value = "True"

        sqlCmd.CommandText = "sp_GetCaseStudy"
        sqlCmd.Parameters.Add(param1)
        sqlCmd.Parameters.Add(param2)

        Dim sdad As SqlDataAdapter = New SqlDataAdapter
        sdad.SelectCommand = sqlCmd
        Dim dt As DataTable = New DataTable
        Try
            sdad.Fill(dt)
        Catch ex As Exception

        End Try
        sqlCmd.Connection.Close()
        If dt.Rows.Count > 0 Then
            sTitle = dt.Rows(0).Item("CaseStudyTitle").ToString
        End If
        Return sTitle
    End Function

    Function GetDataTable(ByVal sStoredProc As String, Optional ByVal IsCS As Boolean = False) As DataTable
        Dim sqlConn As New SqlConnection
        If IsCS = True Then
            sqlConn.ConnectionString = DBConnStrCS
        Else
            sqlConn.ConnectionString = DBConnStr
        End If
        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = sqlConn
        sqlCmd.CommandType = CommandType.StoredProcedure
        sqlCmd.CommandText = sStoredProc
        Dim sdad As SqlDataAdapter = New SqlDataAdapter
        sdad.SelectCommand = sqlCmd
        Dim dt As DataTable = New DataTable
        Try
            sdad.Fill(dt)
        Catch ex As Exception

        End Try
        sqlCmd.Connection.Close()
        Return dt
    End Function

    Function GetDataTableSQL(ByVal sSQL As String, Optional ByVal IsCS As Boolean = False) As DataTable
        Dim sqlConn As New SqlConnection
        If IsCS = True Then
            sqlConn.ConnectionString = DBConnStrCS
        Else
            sqlConn.ConnectionString = DBConnStr
        End If
        Dim sqlCmd As New SqlCommand
        sqlCmd.Connection = sqlConn
        sqlCmd.CommandType = CommandType.Text
        sqlCmd.CommandText = sSQL
        Dim sdad As SqlDataAdapter = New SqlDataAdapter
        sdad.SelectCommand = sqlCmd
        Dim dt As DataTable = New DataTable
        Try
            sdad.Fill(dt)
        Catch ex As Exception

        End Try
        sqlCmd.Connection.Close()
        Return dt
    End Function

    Function GetDataTableSP(ByVal sqlCmd As SqlCommand, Optional ByVal IsCS As Boolean = False) As DataTable
        Dim sqlConn As New SqlConnection
        If IsCS = True Then
            sqlConn.ConnectionString = DBConnStrCS
        Else
            sqlConn.ConnectionString = DBConnStr
        End If
        sqlCmd.Connection = sqlConn
        Dim sdad As SqlDataAdapter = New SqlDataAdapter
        sdad.SelectCommand = sqlCmd
        Dim dt As DataTable = New DataTable
        Try
            sdad.Fill(dt)
        Catch ex As Exception

        End Try
        sqlCmd.Connection.Close()
        Return dt
    End Function

    Function InsertUpdateDataScalar(ByVal sqlCmd As SqlCommand, Optional ByVal IsCS As Boolean = False) As String
        Dim s As String = ""
        Dim sqlConn As New SqlConnection
        If IsCS = True Then
            sqlConn.ConnectionString = DBConnStrCS
        Else
            sqlConn.ConnectionString = DBConnStr
        End If
        sqlCmd.Connection = sqlConn
        sqlCmd.Connection.Open()
        If sqlCmd.CommandType = CommandType.Text Then
            Try
                s = sqlCmd.ExecuteScalar()
            Catch ex As Exception
                If ex.Message Is Nothing = False Then
                    s = ex.Message.ToString
                End If
            End Try
        ElseIf sqlCmd.CommandType = CommandType.StoredProcedure Then
            Try
                sqlCmd.ExecuteScalar()
                s = sqlCmd.Parameters("@MyIdentity").Value
            Catch ex As Exception
                If ex.Message Is Nothing = False Then
                    s = ex.Message.ToString
                End If
            End Try
        End If
        sqlCmd.Connection.Close()
        Return s
    End Function

    Function InsertUpdateDataNonQuery(ByVal sqlCmd As SqlCommand, Optional ByVal IsCS As Boolean = False) As String
        Dim s As String = "Success"
        Dim sqlConn As New SqlConnection
        If IsCS = True Then
            sqlConn.ConnectionString = DBConnStrCS
        Else
            sqlConn.ConnectionString = DBConnStr
        End If
        sqlCmd.Connection = sqlConn
        sqlCmd.Connection.Open()
        Try
            sqlCmd.ExecuteNonQuery()
        Catch ex As Exception
            If ex.Message Is Nothing = False Then
                s = ex.Message.ToString
            End If
        End Try
        sqlCmd.Connection.Close()
        Return s
    End Function
End Class
