Imports System.ComponentModel
Imports System.Linq

Public Class EnumHelper
    Public Enum TransactionMode
        Dev
        Live
    End Enum

    Public Enum SeminarType
        <Description("Basic")>
        Basic
        <Description("Advanced 1")>
        Adv1
        <Description("Advanced 2")>
        Adv2
        <Description("Advanced 2 with Case Management")>
        Adv2WithCaseManage
        <Description("NST")>
        Nst
    End Enum

    Public Enum CourseType
        <Description("Basic")>
        Basic = 1
        <Description("NST")>
        Nst = 2
        <Description("Advanced I")>
        AdvancedI = 3
        <Description("Symposium")>
        Symposium = 5
        <Description("Advanced II")>
        AdvancedII = 6
        <Description("Basic Refresher")>
        BasicRefresher = 8
        <Description("Advanced II A")>
        AdvancedIIA = 9
        <Description("Advanced II B")>
        AdvancedIIB = 10
        <Description("Advanced II C Part I")>
        AdvancedIICPartI = 11
        <Description("Advanced II C Part II")>
        AdvancedIICPartII = 12
        <Description("Advanced II D")>
        AdvancedIID = 13
        <Description("Advanced II E")>
        AdvancedIIE = 14
        <Description("Advanced II F")>
        AdvancedIIF = 15
        <Description("Advanced II G")>
        AdvancedIIG = 16
        <Description("Advanced II H")>
        AdvancedIIH = 17
        <Description("Advanced II I")>
        AdvancedIII = 18
        <Description("Advanced II J")>
        AdvancedIIJ = 19
        <Description("Advanced II K")>
        AdvancedIIK = 20
        <Description("Advanced II L")>
        AdvancedIIL = 21
        <Description("Advanced II M")>
        AdvancedIIM = 22
        <Description("Advanced II N")>
        AdvancedIIN = 23
        <Description("Advanced II O")>
        AdvancedIIO = 24
        <Description("Advanced II P")>
        AdvancedIIP = 25
        <Description("Advanced II Q")>
        AdvancedIIQ = 26
        <Description("Advanced II R")>
        AdvancedIIR = 27
        <Description("Advanced II S")>
        AdvancedIIS = 28
        <Description("Advanced II T")>
        AdvancedIIT = 29
        <Description("Advanced II U")>
        AdvancedIIU = 30
        <Description("Advanced II V")>
        AdvancedIIV = 31
    End Enum

    Public Enum RegistrantStatus
        Unapproved = 1
        Approved = 2
        Attended = 3
        Cancelled = 4
    End Enum

    Public Shared Function GetDescriptionFromEnumValue(value As System.Enum) As String
        Dim attribute As DescriptionAttribute = TryCast(value.[GetType]().GetField(value.ToString()).GetCustomAttributes(GetType(DescriptionAttribute), False).SingleOrDefault(), DescriptionAttribute)
        Return If(attribute Is Nothing, value.ToString(), attribute.Description)
    End Function

    Public Shared Function EnumToDictionary(ByVal enumType As Type) As Dictionary(Of String, String)

        Dim dict As New Dictionary(Of String, String)
        Dim itemValues As Array = System.Enum.GetValues(enumType)
        Dim itemNames As Array = System.Enum.GetNames(enumType)

        For i As Integer = 0 To itemNames.Length - 1
            dict.Add(itemValues(i), EnumHelper.GetDescriptionFromEnumValue(itemValues(i)))
        Next
        Return dict
    End Function
End Class
