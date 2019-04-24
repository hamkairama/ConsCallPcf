Imports System.Configuration

Public Class BaseConnstringOrc
    Public Shared ReadOnly Property ConnString(conString As String) As String
        Get
            Return DataSource(conString)
        End Get
    End Property

    Private Shared Function DataSource(conString As String) As String
        Dim curCon As String = ConfigurationManager.ConnectionStrings(conString).ConnectionString
        Dim splitCon As String() = curCon.Split(";"c)
        Dim dtCurrent As String = Nothing
        Dim dtIdx As Integer = -1
        For i As Integer = 0 To splitCon.Length - 2
            If splitCon(i).TrimStart().ToUpper().StartsWith("DATA SOURCE") Then
                dtIdx = i
                dtCurrent = splitCon(i)
                Exit For
            End If
        Next
        If dtCurrent IsNot Nothing Then
            Try
                Dim idx As Integer = dtCurrent.IndexOf("="c) + 1
                Dim newdt As String = Cryptoghrap.DecryptString(dtCurrent.Substring(idx).Trim())
                splitCon(dtIdx) = dtCurrent.Substring(0, idx) & newdt
            Catch generatedExceptionName As Exception
                Throw New ArgumentException("Invalid Connection String")
            End Try
        End If
        Dim _newConn = ProbableDataSource(String.Join(";", splitCon))
        Return Password(_newConn)
    End Function

    Private Shared Function Password(BaseConnStr As String) As String
        Dim curCon As String = BaseConnStr
        Dim splitCon As String() = curCon.Split(";"c)
        Dim pwdCurrent As String = Nothing
        Dim pwdIdx As Integer = -1
        For i As Integer = 0 To splitCon.Length - 2
            If splitCon(i).TrimStart().ToUpper().StartsWith("PASSWORD") Then
                pwdIdx = i
                pwdCurrent = splitCon(i)
                Exit For
            End If
        Next
        If pwdCurrent IsNot Nothing Then
            Try
                Dim idx As Integer = pwdCurrent.IndexOf("="c) + 1
                Dim newpass As String = Cryptoghrap.DecryptString(pwdCurrent.Substring(idx).Trim())
                splitCon(pwdIdx) = pwdCurrent.Substring(0, idx) & newpass
            Catch generatedExceptionName As Exception
                Throw New ArgumentException("Invalid Connection String")
            End Try
        End If
        Return UserID(String.Join(";", splitCon))
    End Function

    Private Shared Function UserID(BaseConnStr As String) As String
        Dim curCon As String = BaseConnStr
        Dim splitCon As String() = curCon.Split(";"c)
        Dim uIdCurrent As String = Nothing
        Dim uIdx As Integer = -1
        For i As Integer = 0 To splitCon.Length - 1
            If splitCon(i).TrimStart().ToUpper().StartsWith("USER ID") Then
                uIdx = i
                Dim x = splitCon(i)
                uIdCurrent = splitCon(i).Remove(splitCon(i).Length - 1)
                Exit For
            End If
        Next
        If uIdCurrent IsNot Nothing Then
            Try
                Dim idx As Integer = uIdCurrent.IndexOf("="c) + 1
                Dim newUid As String = Cryptoghrap.DecryptString(uIdCurrent.Substring(idx).Trim()) + """"
                splitCon(uIdx) = uIdCurrent.Substring(0, idx) & newUid
            Catch generatedExceptionName As Exception
                Throw New ArgumentException("Invalid Connection String")
            End Try
        End If
        Return String.Join(";", splitCon)
    End Function


    Private Shared Function ProbableDataSource(Param As String) As String
        Dim _dt = ""

        Dim before1 = "; " + "DATA SOURCE"
        Dim before2 = ";" + "DATA SOURCE"
        Dim after = " DATA SOURCE"

        If Param.ToUpper().Contains(before1) Then
            _dt = Param.Replace(before1, after)
        End If

        If Param.ToUpper().Contains(before2) Then
            _dt = Param.Replace(before2, after)
        End If

        Return _dt
    End Function

End Class
