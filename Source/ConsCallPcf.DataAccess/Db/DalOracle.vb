' Decompiled with JetBrains decompiler
' Type: dal.dal
' Assembly: dal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
' MVID: 63E2E02F-82CD-4EA6-9379-AA94CC96FCC3
' Assembly location: D:\Users\hamkair\Desktop\Dal.dll

Imports Microsoft.VisualBasic.CompilerServices
Imports Microsoft.Win32
Imports System.Collections
Imports System.Data
Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.Runtime.CompilerServices
Imports System
Imports Oracle.ManagedDataAccess.Client
Imports System.Configuration
Imports System.Data.Entity.Core.EntityClient

Public Class DalOracle
    Private mvar_conn As String
    Private mvar_return_val As String
    Private oConn As OracleConnection
    Private nameOfConstring As String

    Public Property RETURN_VALU() As String
        Get
            Return Me.mvar_return_val
        End Get
        Set
            Me.mvar_return_val = Value
        End Set
    End Property

    Public Property p_constring() As String
        Get
            Return Me.mvar_conn
        End Get
        Set
            Me.mvar_conn = Value
        End Set
    End Property

    Private ReadOnly Property p_GetConStringRegistry() As Object
        Get
            Return DirectCast(Me.f_getConString(), Object)
        End Get
    End Property

    Public Sub New(conString As String)
        Me.oConn = New OracleConnection()
        nameOfConstring = conString
    End Sub

    Protected Function WriteToEventLog(Entry As String, Optional AppName As String = "VB.NET Application", Optional EventType As EventLogEntryType = EventLogEntryType.Information, Optional LogName As String = "Application") As Boolean
        Dim eventLog__1 As New EventLog()
        Dim flag As Boolean
        Try
            If Not EventLog.SourceExists(AppName) Then
                EventLog.CreateEventSource(AppName, LogName)
            End If
            eventLog__1.Source = AppName
            eventLog__1.WriteEntry(Entry, EventType)
            flag = True
        Catch ex As Exception
            ProjectData.SetProjectError(ex)
            flag = False
            ProjectData.ClearProjectError()
        End Try
        Return flag
    End Function

    Protected Sub sbCloseConnection()
        If Me.oConn.State <> ConnectionState.Open Then
            Return
        End If
        Me.oConn.Close()
    End Sub

    Protected Sub sbPrepareParameters(ByRef cmd As OracleCommand, ArrL As ArrayList, ArrO As ArrayList)
        Dim num1 As Integer = 0
        Dim num2 As Integer = ArrL.Count - 1
        Dim index1 As Integer = num1
        While index1 <= num2
            Dim cArrayList As cArrayList = DirectCast(ArrL(index1), cArrayList)
            cmd.Parameters.Add(cArrayList.p_name, RuntimeHelpers.GetObjectValue(cArrayList.p_value))
            index1 += 1

        End While
        Dim num3 As Integer = 0
        Dim num4 As Integer = ArrO.Count - 1
        Dim index2 As Integer = num3
        While index2 <= num4
            Dim cArrayList As cArrayList = DirectCast(ArrO(index2), cArrayList)
            cmd.Parameters.Add(New OracleParameter() With {
                    .Direction = ParameterDirection.Output,
                    .ParameterName = cArrayList.p_name,
                    .DbType = cArrayList.p_dbtype,
                    .Size = cArrayList.p_size
            })
            index2 += 1

        End While
    End Sub

    Protected Sub sbPrepareParameters(ByRef cmd As OracleCommand, ArrL As ArrayList)
        Dim OracleParameter As New OracleParameter()
        Dim num1 As Integer = 0
        Dim num2 As Integer = ArrL.Count - 1
        Dim index As Integer = num1
        While index <= num2
            Dim cArrayList As cArrayList = DirectCast(ArrL(index), cArrayList)
            cmd.Parameters.Add(cArrayList.p_name, RuntimeHelpers.GetObjectValue(cArrayList.p_value))
            index += 1

        End While
        cmd.Parameters.Add("RETURN_VALUE", OracleDbType.RefCursor).Direction = ParameterDirection.Output
    End Sub

    Protected Sub sbPrepareParameterWithoutReturn(ByRef cmd As OracleCommand, ArrL As ArrayList)
        Dim OracleParameter As New OracleParameter()
        Dim num1 As Integer = 0
        Dim num2 As Integer = ArrL.Count - 1
        Dim index As Integer = num1
        While index <= num2
            Dim cArrayList As cArrayList = DirectCast(ArrL(index), cArrayList)
            cmd.Parameters.Add(cArrayList.p_name, RuntimeHelpers.GetObjectValue(cArrayList.p_value))
            index += 1

        End While
    End Sub

    Public Function fn_GetOracleDataReader(sSPName As String, ArrayL As ArrayList, ByRef dr As OracleDataReader) As Long
        Dim num1 As Long = 0
        Dim cmd As New OracleCommand()
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        Else
            Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
            Try
                Me.oConn.Open()
            Catch ex As Exception
                ProjectData.SetProjectError(ex)
                Me.WriteToEventLog(ex.Message, "VB.NET Application", EventLogEntryType.Information, "Application")
                ProjectData.ClearProjectError()
                GoTo label_6
            End Try
        End If
        cmd.Connection = Me.oConn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = sSPName
        Me.sbPrepareParameters(cmd, ArrayL)
        dr = cmd.ExecuteReader()
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        Dim num2 As Long = num1
label_6:
        Return num2
    End Function

    Public Function fn_GetOracleDataReader(sSPName As String, queryString As String, ArrayL As ArrayList, ByRef dr As OracleDataReader) As Long
        Dim num1 As Long = 0
        Dim cmd As New OracleCommand()
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        Else
            Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
            'Try
            Me.oConn.Open()
            'Catch ex As Exception
            '    ProjectData.SetProjectError(ex)
            '    Me.WriteToEventLog(ex.Message, "VB.NET Application", EventLogEntryType.Information, "Application")
            '    ProjectData.ClearProjectError()
            '    GoTo label_6
            'End Try
        End If
        cmd.Connection = Me.oConn
        If sSPName IsNot Nothing Then
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = sSPName
        Else
            cmd.CommandText = queryString
        End If
        Me.sbPrepareParameters(cmd, ArrayL)
        dr = cmd.ExecuteReader()
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

        Dim num2 As Long = num1
label_6:
        Return num2
    End Function

    Public Function fn_AddRecordSP(sSPName As String, ByRef ArrayL As ArrayList, ByRef ArrayLOut As ArrayList) As Long
        Dim num1 As Long = 0
        Dim cmd As New OracleCommand()
        Dim oracleCommand As OracleCommand = cmd
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        Else
            Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
            Try
                Me.oConn.Open()
            Catch ex As Exception
                ProjectData.SetProjectError(ex)
                Me.WriteToEventLog(ex.Message, "VB.NET Application", EventLogEntryType.Information, "Application")
                ProjectData.ClearProjectError()
                GoTo label_15
            End Try
        End If
        oracleCommand.Connection = Me.oConn
        oracleCommand.CommandText = sSPName
        oracleCommand.CommandType = CommandType.StoredProcedure
        Me.sbPrepareParameters(cmd, ArrayL, ArrayLOut)
        oracleCommand.ExecuteNonQuery()
        ArrayLOut.Clear()
        Try
            For Each parameter As OracleParameter In cmd.Parameters
                If parameter.Direction = ParameterDirection.Output Then
                    ArrayLOut.Add(DirectCast(New cArrayList(parameter.ParameterName, RuntimeHelpers.GetObjectValue(parameter.Value), parameter.DbType, parameter.Size), Object))
                End If
            Next
        Finally
            Dim enumerator As IEnumerator
            If TypeOf enumerator Is IDisposable Then
                TryCast(enumerator, IDisposable).Dispose()
            End If
        End Try
        Me.sbCloseConnection()
        Dim num2 As Long = num1
label_15:
        Return num2
    End Function

    Public Function fn_AddRecordSP(sSPName As String, queryString As String, ByRef ArrayL As ArrayList, Optional ByRef nReturnValue As Integer = 0) As Long
        Dim num1 As Long = 0
        Dim cmd As New OracleCommand()
        Dim oracleCommand As OracleCommand = cmd
        Dim oracleTransaction As OracleTransaction = Nothing
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        Else
            Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
            Me.oConn.Open()
            oracleTransaction = Me.oConn.BeginTransaction(Data.IsolationLevel.ReadCommitted)
        End If
        cmd.Connection = Me.oConn
        cmd.Transaction = oracleTransaction
        If sSPName IsNot Nothing Then
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandText = sSPName
        Else
            cmd.CommandText = queryString
        End If

        Me.sbPrepareParameters(cmd, ArrayL)

        cmd.ExecuteNonQuery()
        nReturnValue = Conversions.ToInteger(cmd.Parameters("RETURN_VALUE").Value)

        oracleTransaction.Commit()
        Me.sbCloseConnection()
        Dim num2 As Long = num1
label_6:
        Return num2
    End Function

    Public Function fn_AddRecordSP(queryString As String) As String
        Dim cmd As New OracleCommand()
        Dim oracleCommand As OracleCommand = cmd
        Dim oracleTransaction As OracleTransaction = Nothing
        Dim result As String = ""
        Try
            If Me.oConn.State = ConnectionState.Open Then
                Me.oConn.Close()
            Else
                Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
                Me.oConn.Open()
                oracleTransaction = Me.oConn.BeginTransaction(Data.IsolationLevel.ReadCommitted)
            End If
            cmd.Connection = Me.oConn
            cmd.Transaction = oracleTransaction

            Dim split = queryString.Split(";")
            For j = 0 To split.Count - 1
                If split(j) <> "" And split(j) <> " " Then
                    cmd.CommandText = split(j)
                    cmd.ExecuteNonQuery()
                End If
            Next

            oracleTransaction.Commit()
            Me.sbCloseConnection()
            result = "Data has been prosessed successfully"
        Catch ex As Exception
            oracleTransaction.Rollback()
            Dim msgEx = String.Format(" exception : [message : {0}, inner exception : {1}]", ex.Message.ToString, ex.StackTrace.ToString)
            result = msgEx
        End Try

        Return result
    End Function



    Public Function fn_SendEmail(sSPName As String, ByRef ArrayL As ArrayList, Optional ByRef nReturnValue As Integer = 0) As Long
        Dim num1 As Long = 0
        Dim cmd As New OracleCommand()
        Dim oracleCommand As OracleCommand = cmd
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        Else
            Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
            Try
                Me.oConn.Open()
            Catch ex As Exception
                ProjectData.SetProjectError(ex)
                Me.WriteToEventLog(ex.Message, "VB.NET Application", EventLogEntryType.Information, "Application")
                ProjectData.ClearProjectError()
                GoTo label_6
            End Try
        End If
        oracleCommand.Connection = Me.oConn
        oracleCommand.CommandText = sSPName
        oracleCommand.CommandType = CommandType.StoredProcedure
        Me.sbPrepareParameterWithoutReturn(cmd, ArrayL)
        oracleCommand.ExecuteNonQuery()
        'nReturnValue = Conversions.ToInteger(cmd.Parameters("RETURN_VALUE").Value)
        Me.sbCloseConnection()
        Dim num2 As Long = num1
label_6:
        Return num2
    End Function

    Public Function fn_GetOracleDataReader(sSPName As String, ByRef dr As OracleDataReader) As Long
        Dim num1 As Long = 0
        Dim oracleCommand As New OracleCommand()
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        Else
            Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
            Try
                Me.oConn.Open()
            Catch ex As Exception
                ProjectData.SetProjectError(ex)
                Me.WriteToEventLog(ex.Message, "VB.NET Application", EventLogEntryType.Information, "Application")
                ProjectData.ClearProjectError()
                GoTo label_6
            End Try
        End If
        oracleCommand.Connection = Me.oConn
        oracleCommand.CommandType = CommandType.StoredProcedure
        oracleCommand.CommandText = sSPName
        dr = oracleCommand.ExecuteReader(CommandBehavior.CloseConnection)
        Dim num2 As Long = num1
label_6:
        Return num2
    End Function

    Public Function fn_GetOracleDataReader(sSPName As String) As DataSet
        Dim dataSet1 As New DataSet()
        Dim table As New DataTable()
        Dim dataSet2 As DataSet
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        Else
            Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
            Try
                Me.oConn.Open()
            Catch ex As Exception
                ProjectData.SetProjectError(ex)
                Me.WriteToEventLog(ex.Message, "VB.NET Application", EventLogEntryType.Information, "Application")
                dataSet2 = dataSet1
                ProjectData.ClearProjectError()
                GoTo label_6
            End Try
        End If
        Dim sqlDataReader As OracleDataReader = New OracleCommand(sSPName, Me.oConn).ExecuteReader(CommandBehavior.CloseConnection)
        table.Load(DirectCast(sqlDataReader, IDataReader))
        dataSet1.Tables.Add(table)
        dataSet2 = dataSet1
label_6:
        Return dataSet2
    End Function

    Public Function fn_GetOracleDataReader(sSPName As String, ArrayL As ArrayList) As DataSet
        Dim table As New DataTable()
        Dim dataSet1 As New DataSet()
        Dim cmd As New OracleCommand()
        Dim dataSet2 As DataSet
        If Me.oConn.State = ConnectionState.Open Then
            Me.oConn.Close()
        Else
            Me.oConn = New OracleConnection(Conversions.ToString(Me.p_GetConStringRegistry))
            Try
                Me.oConn.Open()
            Catch ex As Exception
                ProjectData.SetProjectError(ex)
                Me.WriteToEventLog(ex.Message, "VB.NET Application", EventLogEntryType.Information, "Application")
                dataSet2 = dataSet1
                ProjectData.ClearProjectError()
                GoTo label_6
            End Try
        End If
        cmd.Connection = Me.oConn
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = sSPName
        Me.sbPrepareParameters(cmd, ArrayL)
        Dim sqlDataReader As OracleDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        table.Load(DirectCast(sqlDataReader, IDataReader))
        dataSet1.Tables.Add(table)
        dataSet2 = dataSet1
label_6:
        Return dataSet2
    End Function

    Private Function f_getConString() As String
        'Dim cs As String = ConfigurationManager.ConnectionStrings("EntitiesEproc").ToString()
        'Dim connStr As String = String.Format("Data Source=10.129.35.239:1521/CAS4GUJ1;User ID=EPROC;Password=eproc1234")

        'Dim eprocSettings As ConnectionStringSettings = ConfigurationManager.ConnectionStrings("EntitiesEproc")
        'If (eprocSettings Is Nothing Or String.IsNullOrEmpty(eprocSettings.ConnectionString)) Then
        '    Throw New Exception("Fatal error: Missing connection string 'EntitiesEproc' in web.config file")
        'End If

        Dim eprocConnectionString As String = BaseConnstringOrc.ConnString(nameOfConstring)
        Dim entityConnectionStringBuilder As EntityConnectionStringBuilder = New EntityConnectionStringBuilder(eprocConnectionString)

        Dim oracleConnectionStringBuilder As OracleConnectionStringBuilder = New OracleConnectionStringBuilder(entityConnectionStringBuilder.ProviderConnectionString)
        Dim eprocDataSource As String = oracleConnectionStringBuilder.DataSource
        Dim eprocUserId As String = oracleConnectionStringBuilder.UserID
        Dim eprocPassword As String = oracleConnectionStringBuilder.Password

        Dim connStr As String = String.Format("Data Source={0};User ID={1};Password={2}", eprocDataSource, eprocUserId, eprocPassword)
        Return connStr

    End Function

End Class

