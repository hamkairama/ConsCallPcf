Imports Oracle.ManagedDataAccess.Client
Public Class ConnectionDB
    Shared fs_ConnectionString As String
    Shared fo_ORAConnection As OracleConnection
    Shared fo_ORATransaction As OracleTransaction
    Shared fo_ORACommand As OracleCommand
    Shared fo_ORADataAdapter As OracleDataAdapter

    Public Shared Function ConnectionString() As String
        Try
            Dim fs_ConnectionStringConfig As String = System.Configuration.ConfigurationManager.ConnectionStrings("EntitiesEproc").ConnectionString
            For i As Integer = Split(fs_ConnectionStringConfig, ";").Count - 1 To 0 Step -1
                If InStr(Split(fs_ConnectionStringConfig, ";")(i).ToUpper, "DATA SOURCE") > 0 Then
                    fs_ConnectionStringConfig = Replace(fs_ConnectionStringConfig,
                                                        Split(fs_ConnectionStringConfig, ";")(i),
                                                        "DATA SOURCE=" & Cryptoghrap.DecryptString(Mid(Split(fs_ConnectionStringConfig, ";")(i), Len("DATA SOURCE=") + 1, Len(Split(fs_ConnectionStringConfig, ";")(i)) - Len("DATA SOURCE=") + 1)))
                ElseIf InStr(Split(fs_ConnectionStringConfig, ";")(i).ToUpper, "PASSWORD") > 0 Then
                    fs_ConnectionStringConfig = Replace(fs_ConnectionStringConfig,
                                                        Split(fs_ConnectionStringConfig, ";")(i),
                                                        "PASSWORD=" & Cryptoghrap.DecryptString(Mid(Split(fs_ConnectionStringConfig, ";")(i), Len("PASSWORD=") + 1, Len(Split(fs_ConnectionStringConfig, ";")(i)) - Len("PASSWORD=") + 1)))
                ElseIf InStr(Split(fs_ConnectionStringConfig, ";")(i).ToUpper, "USER ID") > 0 Then
                    fs_ConnectionStringConfig = Replace(fs_ConnectionStringConfig,
                                                        Split(fs_ConnectionStringConfig, ";")(i),
                                                        "USER ID=" & Cryptoghrap.DecryptString(Mid(Split(fs_ConnectionStringConfig, ";")(i), Len("USER ID=") + 1, Len(Split(fs_ConnectionStringConfig, ";")(i)) - Len("USER ID=") - 1)))
                Else
                    fs_ConnectionStringConfig = Replace(fs_ConnectionStringConfig,
                                                        Split(fs_ConnectionStringConfig, ";")(i) & ";",
                                                        "")
                End If
            Next
            Return fs_ConnectionStringConfig
        Catch ex As Exception
            Throw
        End Try
        Return ""
    End Function

    Public Shared Function CreateConnection() As OracleConnection
        Try
            fs_ConnectionString = ConnectionString()
            fo_ORAConnection = New OracleConnection(fs_ConnectionString)
            fo_ORAConnection.Open()
            Return fo_ORAConnection
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function GetDataTable(ByVal fs_Query As String) As DataTable
        Try
            Dim dtTable As New DataTable
            fs_ConnectionString = ConnectionString()
            fo_ORAConnection = New OracleConnection(fs_ConnectionString)
            fo_ORADataAdapter = New OracleDataAdapter(fs_Query, fo_ORAConnection)

            fo_ORADataAdapter.Fill(dtTable)
            Return dtTable
        Catch ex As Exception
            fo_ORAConnection.Close()
        End Try
        Return Nothing
    End Function

    Public Shared Sub ExecuteQuery(ByVal fs_Query As String,
                                   Optional ByRef _ORACommand As OracleCommand = Nothing)
        Try
            If IsNothing(_ORACommand) Then
                fs_ConnectionString = ConnectionString()
                fo_ORAConnection = New OracleConnection(fs_ConnectionString)
                fo_ORAConnection.Open()
                fo_ORACommand = New OracleCommand(fs_Query, fo_ORAConnection)
                fo_ORACommand.ExecuteNonQuery()
            Else
                _ORACommand.CommandText = fs_Query
                _ORACommand.ExecuteNonQuery()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
