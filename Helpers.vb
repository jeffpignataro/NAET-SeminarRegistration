Imports Microsoft.VisualBasic

Public Class Helpers
    Public Shared Function FixNull(ByVal s) As String       'If Null then return empty string
        If s Is DBNull.Value Then Return ""
        Return s
    End Function

    Public Shared Function CleanString(ByVal s As String) As String
        If s Is DBNull.Value Then
            s = ""
        Else
            s = s.Replace("'", "&#39;")
            s = s.Replace("<", "&lt;")
            s = s.Replace(">", "&gt;")
            s = s.Replace("=", "&#61;")
        End If
        Return s
    End Function

    Public Shared Function CleanEmailString(ByVal s As String) As String
        If s Is DBNull.Value Then
            s = ""
        Else
            s = s.Replace("'", "")
            s = s.Replace(";", "")
            s = s.Replace("&", "")
            s = s.Replace("<", "")
            s = s.Replace(">", "")
            s = s.Replace("\\", "")
            s = s.Replace("=", "")
        End If
        Return s
    End Function

    Public Shared Function CleanPasswordString(ByVal s As String) As String
        If s Is DBNull.Value Then
            s = ""
        Else
            s = s.Replace("'", "''")
            s = s.Replace("<", "")
            s = s.Replace(">", "")
            s = s.Replace("\\", "")
            s = s.Replace("=", "")
        End If
        Return s
    End Function

    Public Shared Function ReplaceCR(ByVal s As String) As String
        If s Is DBNull.Value Then
            s = ""
        Else
            s = s.Replace(ControlChars.Cr, "<br/>")
            s = s.Replace(ControlChars.CrLf, "<br/>")
        End If
        Return s
    End Function

    Public Shared Function TruncateEmail(ByVal s As String) As String
        Dim sString As String = ""
        Dim sb As New StringBuilder
        If s Is DBNull.Value Then
            sb.Append("")
        Else
            Dim iIndex As Integer = 0
            iIndex = s.IndexOf("@")
            If iIndex > 0 Then
                sb.Append(s.Substring(0, iIndex))
            Else
                sb.Append(s)
            End If
        End If
        sString = sb.ToString
        Return sString
    End Function

    Public Shared Function FixEmptyCell(ByVal s) As String  'So borders will still show up when data is not present in a <td>
        If s Is DBNull.Value Then Return "&nbsp;"
        If Trim(s) = "" Then Return "&nbsp;"
        Return s
    End Function

    Public Shared Function FixNumNull(ByVal n) As Double    'If Null then return 0
        If n Is DBNull.Value Then Return 0
        Return n
    End Function

    Public Shared Function FixDateNull(ByVal d) As Date
        If d Is DBNull.Value Then Return "1/1/1900"
        Return d
    End Function

    Public Shared Function FixBooleanNull(ByVal b) As Boolean
        If b Is DBNull.Value Then Return False
        Return b
    End Function

    Public Shared Function FixQuotes(ByVal s As String) As String   'For SQL inserts
        s = s.Replace("'", "''")
        Return s
    End Function

    Public Shared Function FormatDollars(ByVal price As String) As String
        Dim numPrice As Double
        FixNull(price)
        If IsNumeric(price) = False Then
            numPrice = 0
        Else
            numPrice = CDbl(price)
        End If
        Return String.Format("{0:c}", numPrice)
    End Function

    Public Shared Function FormatDateTime(ByVal s As String) As String
        s = FixDateNull(s)
        s = s.Substring(0, 10)
        Return s
    End Function

    Public Shared Function FormatDateString(ByVal s) As String
        Dim d As Date = Date.Now
        Dim sMonth As String = d.Month.ToString
        Dim sDay As String = d.Day.ToString
        Dim sYear As String = d.Year.ToString
        If s Is DBNull.Value Then Return sMonth & "/" & sDay & "/" & sYear
        Dim sDate As String
        Dim dDate As Date
        Try
            dDate = s
        Catch ex As Exception
            dDate = "1/1/2011"
        End Try
        sDate = dDate.ToString("d")
        Return sDate
    End Function

    Public Shared Function IsNumeric(ByVal anyString As String) As Boolean
        If anyString Is Nothing Then
            anyString = ""
        End If
        If anyString.Length > 0 Then
            Dim dummyOut As Double = New Double()
            Dim cultureInfo As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-US", True)
            Return Double.TryParse(anyString, System.Globalization.NumberStyles.Any, cultureInfo.NumberFormat, dummyOut)
        Else
            Return False
        End If
    End Function

    Public Shared Function StateList() As DropDownList
        Dim ddl As New DropDownList
        Dim li1 As New ListItem("Alabama", "AL")
        Dim li2 As New ListItem("Alaska", "AK")
        Dim li3 As New ListItem("Arizona", "AZ")
        Dim li4 As New ListItem("Arkansas", "AR")
        Dim li5 As New ListItem("California", "CA")
        Dim li6 As New ListItem("Colorado", "CO")
        Dim li7 As New ListItem("Connecticut", "CT")
        Dim li8 As New ListItem("District of Columbia", "DC")
        Dim li9 As New ListItem("Delaware", "DE")
        Dim li10 As New ListItem("Florida", "FL")
        Dim li11 As New ListItem("Georgia", "GA")
        Dim li12 As New ListItem("Hawaii", "HI")
        Dim li13 As New ListItem("Idaho", "ID")
        Dim li14 As New ListItem("Illinois", "IL")
        Dim li15 As New ListItem("Indiana", "IN")
        Dim li16 As New ListItem("Iowa", "IA")
        Dim li17 As New ListItem("Kansas", "KS")
        Dim li18 As New ListItem("Kentucky", "KY")
        Dim li19 As New ListItem("Louisiana", "LA")
        Dim li20 As New ListItem("Maine", "ME")
        Dim li21 As New ListItem("Maryland", "MD")
        Dim li22 As New ListItem("Massachusetts", "MA")
        Dim li23 As New ListItem("Michigan", "MI")
        Dim li24 As New ListItem("Minnesota", "MN")
        Dim li25 As New ListItem("Mississippi", "MS")
        Dim li26 As New ListItem("Missouri", "MO")
        Dim li27 As New ListItem("Montana", "MT")
        Dim li28 As New ListItem("Nebraska", "NE")
        Dim li29 As New ListItem("Nevada", "NV")
        Dim li30 As New ListItem("New Hampshire", "NH")
        Dim li31 As New ListItem("New Jersey", "NJ")
        Dim li32 As New ListItem("New Mexico", "NM")
        Dim li33 As New ListItem("New York", "NY")
        Dim li34 As New ListItem("North Carolina", "NC")
        Dim li35 As New ListItem("North Dakota", "ND")
        Dim li36 As New ListItem("Ohio", "OH")
        Dim li37 As New ListItem("Oklahoma", "OK")
        Dim li38 As New ListItem("Oregon", "OR")
        Dim li39 As New ListItem("Pennsylvania", "PA")
        Dim li40 As New ListItem("Rhode Island", "RI")
        Dim li41 As New ListItem("South Carolina", "SC")
        Dim li42 As New ListItem("South Dakota", "SD")
        Dim li43 As New ListItem("Tennessee", "TN")
        Dim li44 As New ListItem("Texas", "TX")
        Dim li45 As New ListItem("Utah", "UT")
        Dim li46 As New ListItem("Vermont", "VT")
        Dim li47 As New ListItem("Virginia", "VA")
        Dim li48 As New ListItem("Washington", "WA")
        Dim li49 As New ListItem("West Virginia", "WV")
        Dim li50 As New ListItem("Wisconsin", "WI")
        Dim li51 As New ListItem("Wyoming", "WY")
        ddl.Items.Add(li1)
        Return ddl
    End Function
End Class
